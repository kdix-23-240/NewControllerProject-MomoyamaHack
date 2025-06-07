using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReStartButton : MonoBehaviour
{
    [SerializeField] private GameObject player;

    void Start()
    {
        // 初期化が必要な場合はここに追加
    }

    void Update()
    {
        // baibzのチェックを完全に削除 → 自動リスタートなし
    }

    /// <summary>
    /// UIボタンから呼び出される再スタート処理
    /// </summary>
    public void OnClick()
    {
        ReStart();
    }

    /// <summary>
    /// プレイヤーを初期状態に戻す
    /// </summary>
    private void ReStart()
    {
        GameSystem.Instance.SetCanRotate(true);
        GameSystem.Instance.SetCanMove(true);

        Destroy(transform.parent.parent.gameObject); // ダイアログを閉じる

        GameSystem.isReset = true;
        GameSystem.clearTime = 0;
    }
}
