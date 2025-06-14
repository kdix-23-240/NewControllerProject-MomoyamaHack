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
    private void Awake()
    {
        info = FindObjectOfType<Get_Information>();
        delayManager = new WarningDelayManager(this, info.SetOutgoingByte);

        // ★追加: '5'が送られたら即 Reset する
        delayManager.OnForcedSend5 += () =>
        {
            delayManager.Reset();
            Debug.Log("[HandleCollision] Reset triggered by ForceSend5");
        };
    }

    // 衝突検出時に呼び出される処理
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stick") || collision.gameObject.CompareTag("Goal"))
        {
            GameSystem.Instance.SetCanRotate(false);
            GameSystem.Instance.SetCanMove(false);

            var rotateX = GetComponent<HandleRotateX>();
            if (rotateX != null)
            {
                rotateX.canRotate = false;
                Debug.Log("[HandleCollision] X回転停止");
            }

            var playerObj = transform.root;
            var rotateYZ = playerObj.GetComponent<HandleRotateYZ>();
            if (rotateYZ != null)
            {
                rotateYZ.canRotate = false;
                Debug.Log("[HandleCollision] YZ回転停止（Player経由）");
            }

            GameObject dialogPrefab = collision.gameObject.CompareTag("Stick") ? biribiriModal : goalModal;
            var dialog = Instantiate(dialogPrefab);
            dialog.transform.SetParent(parent.transform, false);

            if (collision.gameObject.CompareTag("Stick") && delayManager != null)
            {
                Debug.Log("Calling Send4Then5()");
                delayManager.Send4Then5(); // 警告レベル4→5の送信
                // Reset() は強制5送信時に自動で呼ばれるため不要
                Debug.Log("Game Over");
            }
        }
    }
}
