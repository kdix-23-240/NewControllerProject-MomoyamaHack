using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 障害物やゴールとの衝突時に回転や移動を停止し、
/// モーダル表示や警告信号の送信などを行うスクリプト
/// </summary>
public class HandleCollision : MonoBehaviour
{
    [SerializeField] private Canvas parent = default;             // モーダルを表示する親Canvas
    [SerializeField] private GameObject biribiriModal = default;  // Stick（障害物）用モーダル
    [SerializeField] private GameObject goalModal = default;      // Goal用モーダル

    private Get_Information info;                 // シリアル通信を管理するスクリプト
    private WarningDelayManager delayManager;    // 警告信号の送信遅延を制御するマネージャ

    // 初期化処理：外部スクリプトの取得とマネージャ生成
    private void Start()
    {
        info = FindObjectOfType<Get_Information>();
        delayManager = new WarningDelayManager(this, info.SetOutgoingByte);
    }

    // 衝突検出時に呼び出される処理
    void OnCollisionEnter(Collision collision)
    {
        // 衝突相手が「Stick」または「Goal」の場合のみ処理
        if (collision.gameObject.CompareTag("Stick") || collision.gameObject.CompareTag("Goal"))
        {
            // プレイヤーの回転と移動を禁止
            GameSystem.Instance.SetCanRotate(false);
            GameSystem.Instance.SetCanMove(false);

            // X軸回転スクリプトを無効化
            var rotateX = GetComponent<HandleRotateX>();
            if (rotateX != null)
            {
                rotateX.canRotate = false;
                Debug.Log("[HandleCollision] X回転停止");
            }

            // 親オブジェクトから YZ軸回転スクリプトも無効化
            var playerObj = transform.root; // 通常はプレイヤーのルートオブジェクト
            var rotateYZ = playerObj.GetComponent<HandleRotateYZ>();
            if (rotateYZ != null)
            {
                rotateYZ.canRotate = false;
                Debug.Log("[HandleCollision] YZ回転停止（Player経由）");
            }

            // 該当のモーダル（biribiriModal または goalModal）をCanvas下に表示
            GameObject dialogPrefab = collision.gameObject.CompareTag("Stick") ? biribiriModal : goalModal;
            var dialog = Instantiate(dialogPrefab);
            dialog.transform.SetParent(parent.transform, false);

            // Stick（障害物）に衝突した場合の追加処理
            if (collision.gameObject.CompareTag("Stick") && delayManager != null)
            {
                Debug.Log("Calling Send4Then5()");
                delayManager.Send4Then5(); // 警告レベル4→5の送信
                StartCoroutine(ResetDelayAfter(1f)); // 1秒後に状態をリセット
                Debug.Log("Game Over");
            }
        }
    }

    /// <summary>
    /// 指定秒数経過後にWarningDelayManagerの状態をリセットする
    /// </summary>
    private IEnumerator ResetDelayAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        delayManager?.Reset();
        Debug.Log("[HandleCollision] WarningDelayManager Reset after delay");
    }
}
