// Unity用ヘッダー同期付き・整数角度対応シリアル受信スクリプト
using UnityEngine;
using System;
using System.IO.Ports;
using System.Threading;

public class Get_Information : MonoBehaviour
{
    [Header("Serial Port Settings")]
    public string portName = "COM9";
    public int baudRate = 9600;

    private SerialPort serial;
    private Thread readThread;
    private volatile bool isRunning = false;

    public float[] receivedData = new float[4]; // pitch, roll, yaw, bend（角度はfloatとして返す）

    private const int messageSize = 8; // int16 * 4
    private byte[] buffer = new byte[messageSize];

    void Start()
    {
        serial = new SerialPort(portName, baudRate);
        serial.ReadTimeout = 1000;
        serial.WriteTimeout = 1000;
        serial.DtrEnable = true;

        try
        {
            serial.Open();
            isRunning = true;
            readThread = new Thread(ReadSerialData);
            readThread.Start();
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to open serial port: " + e.Message);
        }
    }

    void OnDestroy()
    {
        isRunning = false;
        if (readThread != null && readThread.IsAlive)
            readThread.Join();

        if (serial != null && serial.IsOpen)
            serial.Close();
    }

    // ヘッダー同期付き整数角度受信
    private void ReadSerialData()
    {
        while (isRunning)
        {
            try
            {
                while (serial.ReadByte() != 'S')
                {
                    if (!isRunning) return;
                }

                int bytesRead = 0;
                while (bytesRead < messageSize)
                {
                    bytesRead += serial.Read(buffer, bytesRead, messageSize - bytesRead);
                }

                // int16_t → float変換（0.1度単位）
                for (int i = 0; i < 3; i++)
                {
                    short raw = BitConverter.ToInt16(buffer, i * 2);
                    receivedData[i] = raw / 10.0f;
                }

                receivedData[3] = BitConverter.ToInt16(buffer, 6); // bendはそのまま
            }
            catch (Exception e)
            {
                Debug.LogWarning("Serial Read Error: " + e.Message);
            }
        }
    }

    public void SetOutgoingByte(byte msg)
    {
        if (serial != null && serial.IsOpen)
        {
            serial.Write(new byte[] { msg }, 0, 1);
            Debug.Log($"[WarningSystem] Sent warning level command: '{(char)msg}'");
        }
    }

    public float[] GetReceivedData()
    {
        return receivedData;
    }
}
