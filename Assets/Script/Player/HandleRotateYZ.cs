using UnityEngine;
using Unity.VisualScripting;
using System;
using UnityEngine.UIElements;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class PlayerRotateYZ : MonoBehaviour
{
    private Get_Information info;
    float pitch = 0;
    float yaw = 0;
    Transform myTransform;

    void Start()
    {
        info = FindObjectOfType<Get_Information>();
        myTransform = this.transform;
    }

    void Update()
    {
        float[] data = info.GetReceivedData();
        pitch = data[0]; // pitch
        yaw = data[2];   // yaw

        LimitRotate();
    }

    private void LimitRotate()
    {
        Vector3 worldAngle = myTransform.localEulerAngles;
        worldAngle.x = -pitch;
        worldAngle.y = yaw;
        myTransform.localEulerAngles = worldAngle;
    }
}
