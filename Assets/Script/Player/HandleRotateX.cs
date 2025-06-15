using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーまたはオブジェクトをX軸方向（回転）に応じて傾けるスクリプト
/// Get_Informationスクリプトから受信したroll角に基づいて回転させる
/// </summary>
public class HandleRotateX : MonoBehaviour
{
    float roll = 0;                  // 受信したroll角（左右の傾き）
    [SerializeField] private float rotate_con;      // roll角の規格化のための定数
    Transform myTransform;          // このオブジェクトのTransform
    public bool canRotate = true;   // 回転を許可するかどうかのフラグ

    /// <summary>
    /// 初期化処理：自身のTransformの参照取得
    /// </summary>
    void Start()
    {
        myTransform = this.transform;               // 自身のTransformを保存
    }

    /// <summary>
    /// 毎フレーム呼び出され、roll角に応じてZ軸回転を更新する
    /// </summary>
    void Update()
    {
        // 回転が禁止されている場合は何もしない
        if (!canRotate) return;

        // Get_Informationが未初期化なら中断
        if (Get_Information.Instance == null) return;

        // 最新のセンサーデータを取得し、roll（左右の傾き）を取得
        float[] data = Get_Information.Instance.GetReceivedData();
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
        worldAngle.z = rotate_con * roll + 90;              // roll角を係数で拡大し、90度オフセット
        myTransform.localEulerAngles = worldAngle;          // 反映させる
    }
}
