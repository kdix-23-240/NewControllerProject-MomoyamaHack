using UnityEngine;
using UniRx;

public class HitPresenter : MonoBehaviour
{
    [SerializeField] private GameObject warningParent;
    private HandleOutSideWarning outSideWarning;
    private HandleMiddleWarning middleWarning;
    private HandleInSideWarning inSideWarning;
    private PlayerCollision playerCollision;
    private WarningManager warningManager;

    private void Awake()
    {
        outSideWarning = warningParent.transform.Find("LowLevelWarning").gameObject.GetComponent<HandleOutSideWarning>();
        middleWarning = warningParent.transform.Find("MiddleLevelWarning").gameObject.GetComponent<HandleMiddleWarning>();
        inSideWarning = warningParent.transform.Find("HighLevelWarning").gameObject.GetComponent<HandleInSideWarning>();
        playerCollision = warningParent.transform.Find("PlayerCollision").gameObject.GetComponent<PlayerCollision>();
        warningManager = GetComponent<WarningManager>();
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
        if (warningManager == null)
        {
            Debug.LogError("WarningManagerが見つかりません。HitPresenterにアタッチしてください。");
        }

        Bind();
    }

    private void Bind()
    {
        Debug.Log("Bind");
        outSideWarning.IsHit
            .Subscribe(isHit => 
            {
                Debug.Log($"外側の警告: {isHit}");
                warningManager.ObserveWarningLevel1(isHit);
            })
            .AddTo(this);

        middleWarning.IsHit
            .Subscribe(isHit => 
            {
                Debug.Log($"中間の警告: {isHit}");
                warningManager.ObserveWarningLevel2(isHit);
            })
            .AddTo(this);

        inSideWarning.IsHit
            .Subscribe(isHit => 
            {
                Debug.Log($"内側の警告: {isHit}");
                warningManager.ObserveWarningLevel3(isHit);
            })
            .AddTo(this);

        playerCollision.IsHit
            .Subscribe(isHit => 
            {
                Debug.Log($"致命的な衝突: {isHit}");
                warningManager.ObserveWarningLevel4(isHit);
            })
            .AddTo(this);
    }
}