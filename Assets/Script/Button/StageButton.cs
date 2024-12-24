using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    [SerializeField] private string stageName;
    public void OnClick()
    {
        Stage();
    }

    private void Stage()
    {
        // シーンを再読み込み
        UnityEngine.SceneManagement.SceneManager.LoadScene(stageName);
    }
}