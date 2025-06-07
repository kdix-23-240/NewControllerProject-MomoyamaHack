using System.Collections;
using UnityEngine;

public class WarningManager : MonoBehaviour
{
    private int warningLevel = 5;
    [SerializeField] private GameObject outSideWarning;
    [SerializeField] private GameObject middleWarning;
    [SerializeField] private GameObject inSideWarning;

    private Get_Information info;

    void Start()
    {
        info = FindObjectOfType<Get_Information>();
    }

    void Update()
    {
        ObserveWarningLevel();
    }

    private void ObserveWarningLevel()
    {
        int newLevel = 5; // 初期は安全レベル

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

            else newLevel = Mathf.Min(newLevel, 5);
        }

        // レベルが変わった場合のみ送信
        if (newLevel != warningLevel)
        {
            warningLevel = newLevel;
            byte msg = (byte)(warningLevel + '0'); // '1'〜'5'
            info.SetOutgoingByte(msg);
        }

        // Debug.Log($"[WarningDebug] Outer: {outSideWarningComponent.GetIsHit()}, Middle: {middleWarningComponent.GetIsHit()}, Inner: {inSideWarningComponent.GetIsHit()}");
    }
}
