using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollision : MonoBehaviour
{
    [SerializeField] private Canvas parent = default;
    [SerializeField] private GameObject biribiriModal = default;
    [SerializeField] private GameObject goalModal = default;

    private Get_Information info;
    private WarningDelayManager delayManager;

    private void Start()
    {
        info = FindObjectOfType<Get_Information>();
        delayManager = new WarningDelayManager(this, info.SetOutgoingByte);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stick") || collision.gameObject.CompareTag("Goal"))
        {
            GameSystem.Instance.SetCanRotate(false);
            GameSystem.Instance.SetCanMove(false);

            // 回転スクリプトの停止
            var rotateX = GetComponent<HandleRotateX>();
            if (rotateX != null)
            {
                rotateX.canRotate = false;
                Debug.Log("[HandleCollision] X回転停止");
            }

            // Player側の YZ回転を止める
            var playerObj = transform.root; // または transform.parent
            var rotateYZ = playerObj.GetComponent<HandleRotateYZ>();
            if (rotateYZ != null)
            {
                rotateYZ.canRotate = false;
                Debug.Log("[HandleCollision] YZ回転停止（Player経由）");
            }


            // モーダルの表示
            GameObject dialogPrefab = collision.gameObject.CompareTag("Stick") ? biribiriModal : goalModal;
            var dialog = Instantiate(dialogPrefab);
            dialog.transform.SetParent(parent.transform, false);

            // 衝突時の追加処理（Goalには不要）
            if (collision.gameObject.CompareTag("Stick") && delayManager != null)
            {
                Debug.Log("Calling Send4Then5()");
                delayManager.Send4Then5();
                StartCoroutine(ResetDelayAfter(5f));
                Debug.Log("Game Over");
            }
        }
    }

    private IEnumerator ResetDelayAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        delayManager?.Reset();
        Debug.Log("[HandleCollision] WarningDelayManager Reset after delay");
    }
}
