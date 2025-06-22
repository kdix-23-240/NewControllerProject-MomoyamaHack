using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// プレイヤーの位置に基づいて警告レベルを判断し、
/// また、衝突時の段階的警告（4→5）とUI操作時の即時警告（5）を送信する統合マネージャ
/// </summary>
public class WarningManager : MonoBehaviour
{
    private char warningLevel = '5';
    [SerializeField] private GameObject warningParent;
    private GameObject outSideWarning;
    private GameObject middleWarning;
    private GameObject inSideWarning;

    private Coroutine warningCoroutine;
    private bool isWarningSequenceRunning = false;

    private void Awake()
    {
        outSideWarning = warningParent.transform.Find("OutSideWarning").gameObject;
        middleWarning = warningParent.transform.Find("MiddleWarning").gameObject;
        inSideWarning = warningParent.transform.Find("InSideWarning").gameObject;
    }
    void Update()
    {
        ObserveWarningLevel();
    }

    /// <summary>
    /// ゲーム中の警告領域に応じて警告レベルを監視（自動送信はしない）
    /// </summary>
    private void ObserveWarningLevel()
    {
        int newLevel = 5;

        var outSideWarningComponent = outSideWarning.GetComponent<HandleOutSideWarning>();
        var middleWarningComponent = middleWarning.GetComponent<HandleMiddleWarning>();
        var inSideWarningComponent = inSideWarning.GetComponent<HandleInSideWarning>();

        if (GameSystem.Instance.GetCanMove())
        {
            if (outSideWarningComponent != null && outSideWarningComponent.GetIsHit())
                newLevel = Mathf.Min(newLevel, 3);

            if (middleWarningComponent != null && middleWarningComponent.GetIsHit())
                newLevel = Mathf.Min(newLevel, 2);

            if (inSideWarningComponent != null && inSideWarningComponent.GetIsHit())
                newLevel = Mathf.Min(newLevel, 1);
        }
        warningLevel = (char)newLevel;
   }

    /// <summary>
    /// 衝突時などに呼び出し、警告4→5を段階的に送信する
    /// </summary>
    public void StartWarningSequence()
    {
        if (!isWarningSequenceRunning)
        {
            if (warningCoroutine != null)
                StopCoroutine(warningCoroutine);

            warningCoroutine = StartCoroutine(WarningSequenceCoroutine());
        }
    }

    private IEnumerator WarningSequenceCoroutine()
    {
        isWarningSequenceRunning = true;

        SendWarning('4');
        Debug.Log("[WarningManager] Sent '4'");

        yield return new WaitForSeconds(3f);

        SendWarning('5');
        Debug.Log("[WarningManager] Sent '5'");

        isWarningSequenceRunning = false;
    }

    private void SendWarning(char levelChar)
    {
        if (Get_Information.Instance != null)
        {
            Get_Information.Instance.SetOutgoingByte((byte)levelChar);
            //Debug.Log($"[WarningManager] Sent warning level command: '{levelChar}'");
        }
    }
}

