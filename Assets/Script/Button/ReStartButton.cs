using UnityEngine;

public class ReStartButton : MonoBehaviour
{
    [SerializeField] private GameObject player; // CircleHandle をアサインする予定だが、念のため再取得する

    void Start()
    {
        // 確実に CircleHandle を取得して使う（Inspectorに設定されていても上書き）
        GameObject circleHandle = GameObject.Find("CircleHandle");
        if (circleHandle != null)
        {
            player = circleHandle;
            Debug.Log("[診断] player を CircleHandle に上書き");
        }
        else
        {
            Debug.LogError("[ReStartButton] CircleHandle がシーン上に見つかりません！");
        }
    }

    public void OnClick()
    {
        ReStart();
    }

    private void ReStart()
    {
        GameSystem.Instance.SetCanRotate(true);
        GameSystem.Instance.SetCanMove(true);
        GameSystem.isReset = true;
        GameSystem.clearTime = 0;

        // ダイアログを閉じる
        Destroy(transform.parent.parent.gameObject);

        if (player == null)
        {
            Debug.LogError("[ReStartButton] player が null のままです");
            return;
        }

        // Rigidbody の拘束解除（回転フリー、位置固定）
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX |
                             RigidbodyConstraints.FreezePositionY |
                             RigidbodyConstraints.FreezePositionZ;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            Debug.Log("Rigidbody 拘束解除");
        }

        // X回転スクリプトの復帰（CircleHandle にある）
        var rotateX = player.GetComponent<HandleRotateX>();
        if (rotateX != null)
        {
            rotateX.canRotate = true;
        }
        else
        {
            Debug.LogWarning("HandleRotateX が " + player.name + " に見つかりません");
        }

        // YZ回転スクリプトの復帰（親 Player にある）
        var rotateYZ = player.transform.root.GetComponent<HandleRotateYZ>();
        if (rotateYZ != null)
        {
            rotateYZ.canRotate = true;
        }
        else
        {
            Debug.LogWarning("HandleRotateYZ が Player に見つかりません");
        }
    }
}
