using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReStartButton : MonoBehaviour
{
    [SerializeField] private GameObject player; // Playerオブジェクトを参照するためのフィールド

    public void OnClick()
    {
        ReStart();
    }

    private void ReStart()
    {
        // シーンを再読み込み
        GameSystem.Instance.SetCanRotate(true);
        GameSystem.Instance.SetCanMove(true);

        //親の親コンポーネントの破壊
        Destroy(transform.parent.parent.gameObject);

        GameSystem.isReset = true;
        GameSystem.clearTime = 0;
    }
}