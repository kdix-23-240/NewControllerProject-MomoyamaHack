using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// プレイヤーまたはオブジェクトをY軸・Z軸（ピッチ・ヨー）方向に傾けるスクリプト
/// シリアル通信から得た角度データに基づいて、姿勢を更新する
/// </summary>
public class HandleRotateYZ : MonoBehaviour
{
    float pitch = 0;               // ピッチ角（上下の傾き）
    float yaw = 0;                 // ヨー角（左右の方向）
    Transform myTransform;        // このオブジェクトのTransform情報
    public bool canRotate = true; // 回転の可否を制御するフラグ

    /// <summary>
    /// 初期化処理：Transform取得
    /// </summary>
    void Start()
    {
        myTransform = this.transform; // 自身のTransform取得
    }

    /// <summary>
    /// 毎フレーム呼ばれる処理：pitchとyawを取得して姿勢を更新
    /// </summary>
    void Update()
    {
        // 回転が許可されていない場合は処理をスキップ
        if (!canRotate) return;

        // Get_Informationが未初期化の場合はスキップ
        if (Get_Information.Instance == null) return;

        // シリアルから角度データを取得
        float[] data = Get_Information.Instance.GetReceivedData();
        pitch = data[0]; // ピッチ（上下の傾き）
        yaw = data[2];   // ヨー（水平回転）

        // 実際にオブジェクトの姿勢を更新
        LimitRotate();
    }

    /// <summary>
    /// ピッチとヨーに基づいてオブジェクトの回転を更新
    /// </summary>
    private void LimitRotate()
    {
        Vector3 worldAngle = myTransform.localEulerAngles; // 現在の回転角度を取得
        worldAngle.x = -pitch;                             // ピッチは符号反転（視覚補正）
        worldAngle.y = yaw;                                // ヨーはそのまま反映
        myTransform.localEulerAngles = worldAngle;         // 回転を適用
    }
}
