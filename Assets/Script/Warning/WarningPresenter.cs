using UnityEngine;
using UniRx;
using System.Runtime.InteropServices;

public class WarningPresenter : MonoBehaviour
{
    private WarningModel warningModel;
    public WarningModel WarningModel { get { return warningModel; } }
    private WarningSender controllerPresenter;
    private void Awake()
    {
        warningModel = new WarningModel();
        controllerPresenter = GetComponent<WarningSender>();
    }
    private void Start()
    {
        Bind();
    }
    private void Bind()
    {
        warningModel.WarningLevel.Subscribe(level =>
        {
            Debug.Log("WarningPresenter:WarningLevelÇ™ïœçXÇ≥ÇÍÇ‹ÇµÇΩ");
            Debug.Log($"WarningPresenter:WarningLevel = {level}");
            controllerPresenter.OnChangeWarningLevel(level);
        }).AddTo(this);
    }
}