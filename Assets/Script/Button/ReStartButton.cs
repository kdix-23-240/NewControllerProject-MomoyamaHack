using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReStartButton : MonoBehaviour
{
    public void OnClick()
    {
        ReStart();
    }

    private void ReStart()
    {
        // シーンを再読み込み
        GameSystem.Instance.SetCanRotate(true);
        GameSystem.Instance.SetCanMove(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        GameSystem.clearTime = 0;
    }
}