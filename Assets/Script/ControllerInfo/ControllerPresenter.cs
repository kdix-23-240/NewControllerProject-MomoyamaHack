using UnityEngine;
using UniRx;

public class ControllerPresenter : MonoBehaviour
{

    void Awake()
    {

    }

    void Start()
    {
        var handleRotate = GetComponent<HandleRotate>();
        if (handleRotate == null)
        {
            Debug.LogError("PlayerオブジェクトにHandleRotateコンポーネントがアタッチされていません。");
            return;
        }

        Bind(handleRotate);
    }

    void Update()
    {
        // 必要に応じて拡張
    }

    private void Bind(HandleRotate handleRotate)
    {
        ControllerModel.GetInstance.RotateX
            .Subscribe(rotateX => handleRotate.RotateX(rotateX))
            .AddTo(this);
        ControllerModel.GetInstance.RotateY
            .Subscribe(rotateY => handleRotate.RotateY(rotateY))
            .AddTo(this);
        ControllerModel.GetInstance.RotateZ
            .Subscribe(rotateZ => handleRotate.RotateZ(rotateZ))
            .AddTo(this);
    }
}