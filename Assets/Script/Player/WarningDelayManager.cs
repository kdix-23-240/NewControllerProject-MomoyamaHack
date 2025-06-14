using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 警告信号の送信を「段階的に（4→5）」行うためのマネージャクラス
/// 一定時間後に自動で '5' を送信するほか、UIの操作で即時送信も可能
/// </summary>
public class WarningDelayManager
{
    private Action<byte> sendFunc;           // バイトデータを送信する関数（外部から渡される）
    private Coroutine currentCoroutine;      // 実行中の遅延コルーチン
    private bool hasSent5 = false;           // '5'（重大警告）を送信済みかどうか
    private MonoBehaviour coroutineHostRef;  // コルーチン開始に使うMonoBehaviourインスタンス

    public bool HasSent5 => hasSent5;

    // 追加: '5' が送信されたときに呼ばれる外部通知イベント
    public event Action OnForcedSend5;

    /// <summary>
    /// コンストラクタ：コルーチンを使うためにMonoBehaviourを受け取り、UI監視用の非表示オブジェクトも作成
    /// </summary>
    public WarningDelayManager(MonoBehaviour coroutineHost, Action<byte> sendFunc)
    {
        this.sendFunc = sendFunc;
        this.coroutineHostRef = coroutineHost;

        var watcherGO = new GameObject("WarningUIWatcher");
        watcherGO.hideFlags = HideFlags.HideAndDontSave;
        UnityEngine.Object.DontDestroyOnLoad(watcherGO);

        var watcher = watcherGO.AddComponent<WarningUIWatcher>();
        watcher.onAnyButtonPressed = () =>
        {
            if (!hasSent5) ForceSend5(); // ボタンが押されたら即 '5' を送信
        };
    }

    /// <summary>
    /// '4'を即時送信し、3秒後に自動で'5'を送信（またはUI操作で即時'5'）
    /// </summary>
    public void Send4Then5()
    {
        Debug.Log($"[WarningDelay] Send4Then5() called — hasSent5={hasSent5}");

        if (hasSent5)
        {
            Debug.LogWarning("[WarningDelay] Send4Then5 skipped: already sent '5'");
            return;
        }

        sendFunc((byte)'4'); // 軽度警告（レベル4）送信
        Debug.Log("[WarningDelay] Sent '4'");

        if (currentCoroutine != null && coroutineHostRef != null)
            coroutineHostRef.StopCoroutine(currentCoroutine);

        if (coroutineHostRef != null)
            currentCoroutine = coroutineHostRef.StartCoroutine(DelayedSend5());
        else
            Debug.LogWarning("[WarningDelay] coroutineHostRef is null, delay coroutine not started");
    }

    /// <summary>
    /// 即座に '5' を送信する（UI操作時または遅延後）
    /// </summary>
    public void ForceSend5()
    {
        if (hasSent5) return;

        sendFunc((byte)'5'); // 重大警告（レベル5）送信
        Debug.Log("[WarningDelay] Forced '5' sent");
        hasSent5 = true;

        // ★追加: 強制送信後に外部へ通知
        OnForcedSend5?.Invoke();
    }

    /// <summary>
    /// 3秒待ってから '5' を送信（途中でキャンセルされる可能性あり）
    /// </summary>
    private IEnumerator DelayedSend5()
    {
        Debug.Log("[WarningDelay] DelayedSend5() start");
        yield return new WaitForSeconds(3f);
        if (!hasSent5)
        {
            Debug.Log("[WarningDelay] DelayedSend5() triggering ForceSend5");
            ForceSend5();
        }
        else
        {
            Debug.Log("[WarningDelay] DelayedSend5() skipped: already sent");
        }
    }

    /// <summary>
    /// マネージャの状態を初期化（再利用可能にする）
    /// </summary>
    public void Reset()
    {
        hasSent5 = false;

        if (currentCoroutine != null && coroutineHostRef != null)
            coroutineHostRef.StopCoroutine(currentCoroutine);
        currentCoroutine = null;
    }
}

/// <summary>
/// UIボタンの選択変更を検知して、コールバックを呼び出す監視用スクリプト
/// 非表示オブジェクトとして生成され、警告レベル即時送信に使われる
/// </summary>
public class WarningUIWatcher : MonoBehaviour
{
    public Action onAnyButtonPressed; // ボタンが選択されたときに呼ばれるイベント
    private GameObject lastSelected = null; // 前回選択されていたオブジェクトを保持

    void Update()
    {
        var selected = EventSystem.current?.currentSelectedGameObject;
        if (selected != null && selected != lastSelected)
        {
            if (selected.GetComponent<Button>() != null)
            {
                onAnyButtonPressed?.Invoke();
            }
            lastSelected = selected;
        }
    }
}
