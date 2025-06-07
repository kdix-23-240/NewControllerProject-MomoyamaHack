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
        var outSideWarningComponent = outSideWarning.GetComponent<HandleOutSideWarning>();
        var middleWarningComponent = middleWarning.GetComponent<HandleMiddleWarning>();
        var inSideWarningComponent = inSideWarning.GetComponent<HandleInSideWarning>();

        if (GameSystem.Instance.GetCanMove())
        {
            if (outSideWarningComponent != null && outSideWarningComponent.GetIsHit())
            {
                info.SetOutgoingByte((byte)'3');
                warningLevel = 3;
            }
            else if (middleWarningComponent != null && middleWarningComponent.GetIsHit())
            {
                info.SetOutgoingByte((byte)'3');
                warningLevel = 2;
            }
            else if (inSideWarningComponent != null && inSideWarningComponent.GetIsHit())
            {
                info.SetOutgoingByte((byte)'1');
                warningLevel = 1;
            }
            else
            {
                info.SetOutgoingByte((byte)'5');
                warningLevel = 5;
            }
        }
        else
        {
            info.SetOutgoingByte((byte)'5');
            warningLevel = 5;
        }

        // Debug.Log("warning:" + warningLevel);
    }
}
