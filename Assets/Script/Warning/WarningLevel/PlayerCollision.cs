using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private bool isHit = false;

    /// <summary>
    /// 侵入判定
    /// 衝突したオブジェクトが棒ならば、isHitをtrueにして警告を表示 
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Hit");
        // 衝突相手が「Stick」または「Goal」の場合のみ処理
        if (other.gameObject.CompareTag("Stick") || other.gameObject.CompareTag("Goal"))
        {
            // プレイヤーの回転と移動を禁止
            isHit = true;
            //GameSystem.Instance.SetCanRotate(false);
            //GameSystem.Instance.SetCanMove(false);

            //// 回転スクリプトを無効化
            //var playerObj = transform.root;
            //var rotate = playerObj.GetComponent<HandleRotate>();
            //if (rotate != null)
            //{
            //    rotate.canRotate = false;
            //    Debug.Log("[HandleCollision] 回転停止（Player経由）");
            //}

            //// 該当のモーダル（biribiriModal または goalModal）をCanvas下に表示
            //GameObject dialogPrefab = collision.gameObject.CompareTag("Stick") ? biribiriModal : goalModal;
            //var dialog = Instantiate(dialogPrefab);
            //dialog.transform.SetParent(parent.transform, false);

            //// Stick（障害物）に衝突した場合の追加処理
            //if (collision.gameObject.CompareTag("Stick"))
            //{
            //    Debug.Log("Calling StartWarningSequence()");
            //    var warningManager = FindObjectOfType<WarningManager>();
            //    //warningManager?.StartWarningSequence(); // ← 送信管理はWarningManagerに任せる
            //    Debug.Log("Game Over");
            //}
        }
    }

    public bool GetIsHit()
    {
        return isHit;
    }
}