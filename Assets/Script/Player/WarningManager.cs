using System.Collections;
using UnityEngine;

/// <summary>
/// プレイヤーの位置に基づいて警告レベルを判断し、必要に応じてシリアル送信を行うマネージャ
/// 衝突判定（外側・中間・内側）に応じて警告レベルを1〜5に設定
/// </summary>
public class WarningManager : MonoBehaviour
{
    private int warningLevel = 5; // 現在の警告レベル（初期は最も安全な5）

    [SerializeField] private GameObject outSideWarning;   // 外側の警告エリア
    [SerializeField] private GameObject middleWarning;    // 中間の警告エリア
    [SerializeField] private GameObject inSideWarning;    // 内側の警告エリア

    /// <summary>
    /// 毎フレーム警告レベルを監視し、必要があればシリアル送信を行う
    /// </summary>
    void Update()
    {
        ObserveWarningLevel();
    }

    /// <summary>
    /// 各警告エリアの当たり判定に基づいて、現在の警告レベルを更新し、
    /// 前回の値と異なる場合は新しい値を送信
    /// </summary>
    private void ObserveWarningLevel()
    {
        int newLevel = 5; // 初期状態（安全）

        // 各警告エリアにアタッチされた当たり判定用スクリプトを取得
        var outSideWarningComponent = outSideWarning.GetComponent<HandleOutSideWarning>();
        var middleWarningComponent = middleWarning.GetComponent<HandleMiddleWarning>();
        var inSideWarningComponent = inSideWarning.GetComponent<HandleInSideWarning>();

        // プレイヤーが移動中のときのみ警告レベルを評価
        if (GameSystem.Instance.GetCanMove())
        {
            // 外側に触れた場合：警告レベル3
            if (outSideWarningComponent != null && outSideWarningComponent.GetIsHit())
                newLevel = Mathf.Min(newLevel, 3);

            // 中間に触れた場合：警告レベル2
            if (middleWarningComponent != null && middleWarningComponent.GetIsHit())
                newLevel = Mathf.Min(newLevel, 2);

            // 内側に触れた場合：警告レベル1（最も危険）
            if (inSideWarningComponent != null && inSideWarningComponent.GetIsHit())
                newLevel = Mathf.Min(newLevel, 1);

            // どこにも触れていない場合：警告レベル5（安全）
            else newLevel = Mathf.Min(newLevel, 5);
        }

        // 前回と異なる場合にのみ、警告レベルを送信
        if (newLevel != warningLevel)
        {
            warningLevel = newLevel;
            byte msg = (byte)(warningLevel + '0'); // 数値→ASCII（'1'〜'5'）

            // Get_Informationが存在する場合のみ送信
            if (Get_Information.Instance != null)
            {
                Get_Information.Instance.SetOutgoingByte(msg);
            }
        }

        // Debug.Logでの詳細出力（デバッグ時のみ有効に）
        // Debug.Log($"[WarningDebug] Outer: {outSideWarningComponent.GetIsHit()}, Middle: {middleWarningComponent.GetIsHit()}, Inner: {inSideWarningComponent.GetIsHit()}");
    }
}
