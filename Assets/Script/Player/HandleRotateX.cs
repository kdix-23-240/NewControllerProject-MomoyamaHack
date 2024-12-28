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
    private string[] data = new string[5];
    float roll = 0;
    Transform myTransform;
    void Start()
    {
        this.info = new Get_Information();
        myTransform = this.transform;
    }

    void Update()
    {
        if (CountText(",", info.Getinfo()) == 4)
        {
            data = info.Getinfo().Split(',');
            roll = float.Parse(data[1]);
        }
        //Debug.Log("roll = " + roll);
        LimitRotate();
    }

    private int CountText(string search, string target)
    {
        int cnt = 0;
        bool check = true;

        while (check)
        {
            if (target.IndexOf(search, System.StringComparison.CurrentCulture) == -1)
            {
                check = false;
            }
            else
            {
                target = target.Remove(0, target.IndexOf(search, System.StringComparison.CurrentCulture) + 1);
                cnt++;
            }
        }

        return cnt;
    }

    /// <summary>
    /// 持ち手をz軸回転させる(持ち手自体がすでに回転させてあるので軸が狂ってる)
    /// </summary>
    private void LimitRotate()
    {
        //this.transform.Rotate(pitch, yaw, roll);//pitch, yaw, roll
        // ワールド座標を基準に、回転を取得
        Vector3 worldAngle = myTransform.localEulerAngles;
        worldAngle.z = 1.5f * roll + 90; // ワールド座標を基準に、x軸を軸にした回転を10度に変更
        myTransform.localEulerAngles = worldAngle; // 回転角度を設定

        //Debug.Log(pitch.ToString() + "," + roll.ToString() + "," + yaw.ToString());
    }
}