using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;
using UnityEngine.UIElements;
using System.Drawing.Drawing2D;
using System.Threading;

public class HandleRotateX : MonoBehaviour
{
    private Get_Information info;
    float roll = 0;
    Transform myTransform;
    public bool canRotate = true; // ← フラグ追加

    void Start()
    {
        info = FindObjectOfType<Get_Information>();
        myTransform = this.transform;
    }

    void Update()
    {
        if (!canRotate) return; // ← フラグ判定を追加

        float[] data = info.GetReceivedData();
        roll = data[1]; // roll はインデックス1

        LimitRotate();
    }

    private void LimitRotate()
    {
        Vector3 worldAngle = myTransform.localEulerAngles;
        worldAngle.z = 1.5f * roll + 90;
        myTransform.localEulerAngles = worldAngle;
    }
}

