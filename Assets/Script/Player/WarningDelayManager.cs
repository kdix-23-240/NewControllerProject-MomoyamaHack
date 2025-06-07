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

    /// <summary>
    /// コンストラクタ：コルーチンを使うためにMonoBehaviourを受け取り、UI監視用の非表示オブジェクトも作成
    /// </summary>
    public WarningDelayManager(MonoBehaviour coroutineHost, Action<byte> sendFunc)
    {
        this.sendFunc = sendFunc;

        // UI操作を検知するための非表示オブジェクトを作成し、監視スクリプトを追加
        var watcherGO = new GameObject("WarningUIWatcher");
        watcherGO.hideFlags = HideFlags.HideAndDontSave;
        UnityEngine.Object.DontDestroyOnLoad(watcherGO);

        var watcher = watcherGO.AddComponent<WarningUIWatcher>();
        watcher.onAnyButtonPressed = () =>
        {
            if (!hasSent5) ForceSend5(); // ボタンが押されたら即 '5' を送信
        };

        // StartCoroutine を呼ぶために、ホストMonoBehaviourをセット
        coroutineHost.StartCoroutine(SetupCoroutineHost(coroutineHost));
    }

    private MonoBehaviour coroutineHostRef;  // コルーチン開始に使うMonoBehaviourインスタンス

    /// <summary>
    /// 1フレーム遅延後にホストをセットする（Unityの制約による）
    /// </summary>
    private IEnumerator SetupCoroutineHost(MonoBehaviour host)
    {
        yield return null;
        coroutineHostRef = host;
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
            return; // すでに '5' を送っていれば再送しない
        }

        sendFunc((byte)'4'); // 軽度警告（レベル4）送信
        Debug.Log("[WarningDelay] Sent '4'");

        // 既存の遅延コルーチンがあれば停止して上書き
        if (currentCoroutine != null && coroutineHostRef != null)
            coroutineHostRef.StopCoroutine(currentCoroutine);

        // 3秒後に '5' を送る遅延コルーチンを開始
        if (coroutineHostRef != null)
            currentCoroutine = coroutineHostRef.StartCoroutine(DelayedSend5());
    }

    /// <summary>
    /// 即座に '5' を送信する（UI操作時など）
    /// </summary>
    public void ForceSend5()
    {
        if (hasSent5) return;

        sendFunc((byte)'5'); // 重大警告（レベル5）送信
        Debug.Log("[WarningDelay] Forced '5' sent");
        hasSent5 = true;
    }

    /// <summary>
    /// 3秒待ってから '5' を送信（途中でキャンセルされる可能性あり）
    /// </summary>
    private IEnumerator DelayedSend5()
    {
        yield return new WaitForSeconds(3f); // 3秒待機
        if (!hasSent5)
        {
            ForceSend5(); // UI操作がなければ自動的に '5' を送信
        }
    }

    /// <summary>
    /// マネージャの状態を初期化（再利用可能にする）
    /// </summary>
    public void Reset()
    {
        hasSent5 = false;
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
        // 現在選択されているボタンを取得（UI操作により変化）
        var selected = EventSystem.current?.currentSelectedGameObject;
        if (selected != null && selected != lastSelected)
        {
            // 新しく選ばれたオブジェクトがボタンだった場合、イベントを発火
            if (selected.GetComponent<Button>() != null)
            {
                onAnyButtonPressed?.Invoke();
            }
            lastSelected = selected;
        }
    }
}
