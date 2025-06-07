// Unity用シリアル通信スクリプト（バイナリ通信 + COMポート選択 + 配列化対応）

using UnityEngine;
using System;
using System.IO.Ports;
using System.Threading;

public class Get_Information : MonoBehaviour
{
    [Header("Serial Port Settings")]
    public string portName = "COM9";   // インスペクターでCOMポート選択
    public int baudRate = 9600;

    private SerialPort serial;
    private Thread readThread;
    private volatile bool isRunning = false;

    public float[] receivedData = new float[4]; // pitch, roll, yaw, bend

    private const int messageSize = 14; // float*3 + int16 = 4*3 + 2 = 14 bytes
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

    // バイナリ形式でデータ受信
    private void ReadSerialData()
    {
        while (isRunning)
        {
            try
            {
                if (serial.BytesToRead >= messageSize)
                {
                    serial.Read(buffer, 0, messageSize);

                    for (int i = 0; i < 3; i++)
                    {
                        receivedData[i] = BitConverter.ToSingle(buffer, i * 4);
                    }

                    receivedData[3] = BitConverter.ToInt16(buffer, 12); // bend（int16として）
                }
            }
            catch (TimeoutException) { }
            catch (Exception e)
            {
                Debug.LogWarning("Serial Read Error: " + e.Message);
            }
        }
    }

    // 外部スクリプトから送信メッセージを設定する
    public void SetOutgoingByte(byte msg)
    {
        if (serial != null && serial.IsOpen)
        {
            serial.Write(new byte[] { msg }, 0, 1);
        }
    }

    // 外部スクリプトが取得する用（リアルタイムなデータ配列）
    public float[] GetReceivedData()
    {
        return receivedData;
    }
}
