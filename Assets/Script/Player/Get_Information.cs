using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using Unity.VisualScripting;
using System;
using UnityEngine.UIElements;

public class Get_Information : MonoBehaviour
{
    public SerialPort serial = new SerialPort("COM8");
    static string incomingMsg = " ";
    public static string outgoingMsg = "5";
    private string[] data = new string[4];

    void Start()
    {
        serial.Open();
        serial.DtrEnable = true; //Configuramos control de datos por DTR.
                                 // We configure data control by DTR.
        serial.ReadTimeout = 10000;
        serial.WriteTimeout = 10000;

    }
    void Update()
    {
        //print(serial.ReadLine());
        //Debug.Log(incomingMsg);
        incomingMsg = serial.ReadLine();
        if (incomingMsg != " ")
        {
            if (outgoingMsg != " ")
            {
                serial.WriteLine(outgoingMsg);
                outgoingMsg = " ";
            }
            //Debug.Log("Out = " + outgoingMsg);
        }
    }

    public void GetOutGoingMsg(String Msg)
    {
        outgoingMsg = Msg;
    }

    public string Getinfo()
    {
        return (incomingMsg);
    }
}