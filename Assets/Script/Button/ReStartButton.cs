using UnityEngine;

/// <summary>
/// 「再スタート」ボタンの処理を担当するスクリプト
/// ゲームの状態を初期化し、プレイヤーを再稼働させる
/// </summary>
public class ReStartButton : MonoBehaviour
{
    [SerializeField] private GameObject player; // CircleHandle をアサインする予定（再取得で上書きされる）

    /// <summary>
    /// ゲーム開始時に CircleHandle を自動取得し、player として登録
    /// </summary>
    void Start()
    {
        // シーン上の "CircleHandle" オブジェクトを検索して player に設定
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

    /// <summary>
    /// ボタンクリック時のコールバック（Unityイベントから呼ばれる）
    /// </summary>
    public void OnClick()
    {
        ReStart();
    }

    /// <summary>
    /// ゲーム状態をリセットしてプレイヤーの動作を再開させる
    /// </summary>
    private void ReStart()
    {
        // 移動・回転フラグを有効化、タイマーとリセットフラグも初期化
        GameSystem.Instance.SetCanRotate(true);
        GameSystem.Instance.SetCanMove(true);
        GameSystem.isReset = true;
        GameSystem.clearTime = 0;

        // 親Canvas上のダイアログUIを閉じる（ボタンの親の親）
        Destroy(transform.parent.parent.gameObject);

        if (player == null)
        {
            Debug.LogError("[ReStartButton] player が null のままです");
            return;
        }

        // X回転（HandleRotateX）を再び有効にする
        var rotateX = player.GetComponent<HandleRotateX>();
        if (rotateX != null)
        {
            rotateX.canRotate = true;
        }
        else
        {
            Debug.LogWarning("HandleRotateX が " + player.name + " に見つかりません");
        }

        // YZ回転（HandleRotateYZ）を再び有効にする（親オブジェクト側に存在）
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
