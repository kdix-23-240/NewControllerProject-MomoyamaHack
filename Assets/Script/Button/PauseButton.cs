using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{

    // ダイアログを追加する親のCanvas
    [SerializeField] private Canvas parent = default;
    [SerializeField] private GameObject biribiriModal = default;
    public void OnClick()
    {
        Pause();
    }

    private void Pause()
    {
        // シーンを再読み込み
        GameSystem.Instance.SetCanRotate(false);
        GameSystem.Instance.SetCanMove(false);
        //BiribiriModalプレハブをCanvasの子要素として生成
        var _dialog = Instantiate(biribiriModal) as GameObject;
        _dialog.transform.SetParent(parent.transform, false);
    }
}