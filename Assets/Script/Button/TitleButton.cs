using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    public void OnClick()
    {
        Title();
    }

    private void Title()
    {
        // シーンを再読み込み
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}