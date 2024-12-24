using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectButton : MonoBehaviour
{
    public void OnClick()
    {
        StageSelect();
    }

    private void StageSelect()
    {
        // シーンを再読み込み
        UnityEngine.SceneManagement.SceneManager.LoadScene("StageSelect");
    }
}