using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningManager : MonoBehaviour
{
    private int warningLevel = 5;
    [SerializeField] private GameObject outSideWarning;
    [SerializeField] private GameObject middleWarning;
    [SerializeField] private GameObject inSideWarning;
    private Get_Information info;

    void Update()
    {
        ObserveWarningLevel();
    }

    void Start()
    {
        this.info = new Get_Information();
    }

    /// <summary>
    /// 警告レベルを観察する
    /// 各警告オブジェクトの侵入判定を監視し、どれかに侵入した場合、警告レベルを設定する
    /// 侵入していない場合は、警告レベルを5に設定する
    /// </summary>

    private void ObserveWarningLevel()
    {
        var outSideWarningComponent = outSideWarning.GetComponent<HandleOutSideWarning>();
        var middleWarningComponent = middleWarning.GetComponent<HandleMiddleWarning>();
        var inSideWarningComponent = inSideWarning.GetComponent<HandleInSideWarning>();
        if (GameSystem.Instance.GetCanMove())
        {
            if (outSideWarningComponent != null && outSideWarningComponent.GetIsHit())
            {
                info.GetOutGoingMsg("3");
                this.warningLevel = 3;
            }
            else if (middleWarningComponent != null && middleWarningComponent.GetIsHit())
            {
                info.GetOutGoingMsg("3");
                this.warningLevel = 2;
            }
            else if (inSideWarningComponent != null && inSideWarningComponent.GetIsHit())
            {
                info.GetOutGoingMsg("1");
                this.warningLevel = 1;
            }
            else
            {
                info.GetOutGoingMsg("5");
                this.warningLevel = 5;
            }
        }
        else
        {
            info.GetOutGoingMsg("5");
            this.warningLevel = 5;
        }
        //Debug.Log("warning:" + warningLevel);
    }
}