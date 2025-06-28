using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// プレイヤーまたはオブジェクトをY軸・Z軸（ピッチ・ヨー）方向に傾けるスクリプト
/// シリアル通信から得た角度データに基づいて、姿勢を更新する
/// </summary>
public class HandleRotate : MonoBehaviour
{
    float pitch = 0;               // ピッチ角（上下の傾き）
    float roll = 0;                // ロール角（左右の傾き）
    float yaw = 0;                 // ヨー角（左右の方向）
    Transform myTransform;        // このオブジェクトのTransform情報
    public bool canRotate = true; // 回転の可否を制御するフラグ
    [SerializeField] private float offsetRotateX = 0f; // ピッチのオフセット（視覚補正用）
    [SerializeField] private float offsetRotateY = 0f; // ヨーのオフセット（視覚補正用）
    [SerializeField] private float offsetRotateZ = 0f; // ロールのオフセット（視覚補正用）

    /// <summary>
    /// 初期化処理：Transform取得
    /// </summary>
    void Start()
    {
        myTransform = this.transform; // 自身のTransform取得
    }

    /// <summary>
    /// 毎フレーム呼ばれる処理：pitchとrollとyawを取得して姿勢を更新
    /// </summary>
    void Update()
    {
        // 回転が許可されていない場合は処理をスキップ
        if (!GameSystem.Instance.GetCanMove()) return;

        // Get_Informationが未初期化の場合はスキップ
        if (Get_Information.Instance == null) return;

        // シリアルから角度データを取得
        float[] data = Get_Information.Instance.GetReceivedData();
        pitch = data[0]; // ピッチ（上下の傾き）
        roll = data[1];  // ロール（左右の傾き）
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
        worldAngle.x = -pitch - offsetRotateX;                             // ピッチは符号反転（視覚補正）
        worldAngle.z = roll + offsetRotateZ;                               // ロールはそのまま反映（左右の傾き）
        worldAngle.y = yaw + offsetRotateY;                                // ヨーはそのまま反映
        myTransform.localEulerAngles = worldAngle;         // 回転を適用
    }
}
