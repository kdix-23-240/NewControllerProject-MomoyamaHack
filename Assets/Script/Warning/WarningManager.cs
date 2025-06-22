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
    [SerializeField] private GameObject warningParent;
    private GameObject outSideWarning;
    private GameObject middleWarning;
    private GameObject inSideWarning;
    private GameObject playerCollision;
    private WarningPresenter warningPresenter;

    private void Awake()
    {
        outSideWarning = warningParent.transform.Find("LowLevelWarning").gameObject;
        middleWarning = warningParent.transform.Find("MiddleLevelWarning").gameObject;
        inSideWarning = warningParent.transform.Find("HighLevelWarning").gameObject;
        playerCollision = warningParent.transform.Find("PlayerCollision").gameObject;
        warningPresenter = GetComponent<WarningPresenter>();
    }
    private void Start()
    {
        if (outSideWarning == null)
        {
            Debug.LogError("外側の警告がせっていされていません");
        }
        if (middleWarning == null)
        {
            Debug.LogError("中間の警告がせっていされていません");
        }
        if (inSideWarning == null)
        {
            Debug.LogError("内側の警告がせっていされていません");
        }
        if (playerCollision == null)
        {
            Debug.LogError("致命的な衝突の警告がせっていされていません");
        }
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
        var outSideWarningComponent = outSideWarning.GetComponent<HandleOutSideWarning>();
        var middleWarningComponent = middleWarning.GetComponent<HandleMiddleWarning>();
        var inSideWarningComponent = inSideWarning.GetComponent<HandleInSideWarning>();
        var fatalCollisionComponent = playerCollision.GetComponent<PlayerCollision>();

        if (GameSystem.Instance.GetCanMove())
        {
            if (outSideWarningComponent != null && outSideWarningComponent.GetIsHit())
            {
                warningPresenter.WarningModel.WarningLevel.Value = 1; // 外側の警告
            }

            if (middleWarningComponent != null && middleWarningComponent.GetIsHit())
            {
                warningPresenter.WarningModel.WarningLevel.Value = 2; // 中間の警告
            }

            if (inSideWarningComponent != null && inSideWarningComponent.GetIsHit())
            {
                warningPresenter.WarningModel.WarningLevel.Value = 3; // 内側の警告
            }
            if (fatalCollisionComponent != null && fatalCollisionComponent.GetIsHit())
            {
                warningPresenter.WarningModel.WarningLevel.Value = 4; // 致命的な衝突
            }
        }
    }
}