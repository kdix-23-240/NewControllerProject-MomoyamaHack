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
            Debug.LogError("�O���̌x���������Ă�����Ă��܂���");
        }
        if (middleWarning == null)
        {
            Debug.LogError("���Ԃ̌x���������Ă�����Ă��܂���");
        }
        if (inSideWarning == null)
        {
            Debug.LogError("�����̌x���������Ă�����Ă��܂���");
        }
        if (playerCollision == null)
        {
            Debug.LogError("�v���I�ȏՓ˂̌x���������Ă�����Ă��܂���");
        }
        if (warningManager == null)
        {
            Debug.LogError("WarningManager��������܂���BHitPresenter�ɃA�^�b�`���Ă��������B");
        }

        Bind();
    }

    private void Bind()
    {
        Debug.Log("Bind");
        outSideWarning.IsHit
            .Subscribe(isHit => 
            {
                Debug.Log($"�O���̌x��: {isHit}");
                warningManager.ObserveWarningLevel1(isHit);
            })
            .AddTo(this);

        middleWarning.IsHit
            .Subscribe(isHit => 
            {
                Debug.Log($"���Ԃ̌x��: {isHit}");
                warningManager.ObserveWarningLevel2(isHit);
            })
            .AddTo(this);

        inSideWarning.IsHit
            .Subscribe(isHit => 
            {
                Debug.Log($"�����̌x��: {isHit}");
                warningManager.ObserveWarningLevel3(isHit);
            })
            .AddTo(this);

        playerCollision.IsHit
            .Subscribe(isHit => 
            {
                Debug.Log($"�v���I�ȏՓ�: {isHit}");
                warningManager.ObserveWarningLevel4(isHit);
            })
            .AddTo(this);
    }
}