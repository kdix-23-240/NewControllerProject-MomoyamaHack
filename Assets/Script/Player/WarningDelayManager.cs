using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WarningDelayManager
{
    private Action<byte> sendFunc;
    private Coroutine currentCoroutine;
    private bool hasSent5 = false;

    public WarningDelayManager(MonoBehaviour coroutineHost, Action<byte> sendFunc)
    {
        this.sendFunc = sendFunc;

        var watcherGO = new GameObject("WarningUIWatcher");
        watcherGO.hideFlags = HideFlags.HideAndDontSave;
        UnityEngine.Object.DontDestroyOnLoad(watcherGO);

        var watcher = watcherGO.AddComponent<WarningUIWatcher>();
        watcher.onAnyButtonPressed = () =>
        {
            if (!hasSent5) ForceSend5();
        };

        coroutineHost.StartCoroutine(SetupCoroutineHost(coroutineHost));
    }

    private MonoBehaviour coroutineHostRef;
    private IEnumerator SetupCoroutineHost(MonoBehaviour host)
    {
        yield return null;
        coroutineHostRef = host;
    }

    public void Send4Then5()
    {
        Debug.Log($"[WarningDelay] Send4Then5() called — hasSent5={hasSent5}");

        if (hasSent5)
        {
            Debug.LogWarning("[WarningDelay] Send4Then5 skipped: already sent '5'");
            return;
        }

        sendFunc((byte)'4');
        Debug.Log("[WarningDelay] Sent '4'");

        if (currentCoroutine != null && coroutineHostRef != null)
            coroutineHostRef.StopCoroutine(currentCoroutine);

        if (coroutineHostRef != null)
            currentCoroutine = coroutineHostRef.StartCoroutine(DelayedSend5());
    }


    public void ForceSend5()
    {
        if (hasSent5) return;

        sendFunc((byte)'5');
        Debug.Log("[WarningDelay] Forced '5' sent");
        hasSent5 = true;
    }

    private IEnumerator DelayedSend5()
    {
        yield return new WaitForSeconds(3f);
        if (!hasSent5)
        {
            ForceSend5();
        }
    }

    public void Reset()
    {
        hasSent5 = false;
    }
}

public class WarningUIWatcher : MonoBehaviour
{
    public Action onAnyButtonPressed;
    private GameObject lastSelected = null;

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
