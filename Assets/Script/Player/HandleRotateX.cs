using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;
using UnityEngine.UIElements;
using System.Drawing.Drawing2D;
using System.Threading;

/// <summary>
/// プレイヤーまたはオブジェクトをX軸方向（回転）に応じて傾けるスクリプト
/// Get_Informationスクリプトから受信したroll角に基づいて回転させる
/// </summary>
public class HandleRotateX : MonoBehaviour
{
    private Get_Information info;    // シリアル通信からデータを取得するスクリプト
    float roll = 0;                  // 受信したroll角（左右の傾き）
    [SerializeField] private float rotate_con;      // roll角の規格化のための定数
    Transform myTransform;          // このオブジェクトのTransform
    public bool canRotate = true;   // 回転を許可するかどうかのフラグ

    /// <summary>
    /// 初期化処理：Get_Informationの取得と自身のTransformの参照取得
    /// </summary>
    void Start()
    {
        info = FindObjectOfType<Get_Information>(); // データ受信スクリプトの参照を取得
        myTransform = this.transform;               // 自身のTransformを保存
    }

    /// <summary>
    /// 毎フレーム呼び出され、roll角に応じてZ軸回転を更新する
    /// </summary>
    void Update()
    {
        // 回転が禁止されている場合は何もしない
        if (!canRotate) return;

        // 最新のセンサーデータを取得し、roll（左右の傾き）を取得
        float[] data = info.GetReceivedData();
        roll = data[1]; // rollは配列のインデックス1に格納されている

        // Z軸回転を制限付きで反映
        LimitRotate();
    }

    /// <summary>
    /// roll角をZ軸回転に反映し、視覚的な傾きを表現する
    /// </summary>
    private void LimitRotate()
    {
        Vector3 worldAngle = myTransform.localEulerAngles;  // 現在の角度を取得
        worldAngle.z = rotate_con * roll + 90;                    // roll角を1.5倍し90度オフセットを追加
        myTransform.localEulerAngles = worldAngle;          // 反映させる
    }
}
