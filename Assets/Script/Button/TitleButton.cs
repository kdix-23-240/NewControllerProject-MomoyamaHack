using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    public void OnClick()
    {
        // UI選択状態にする（WarningUIWatcher用）
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        // 直接シリアル送信で「5」を送る
        if (Get_Information.Instance != null)
        {
            Get_Information.Instance.SetOutgoingByte((byte)'5');
        }
        else
        {
            Debug.LogError("[ReStartButton] Get_Information.Instance が null です");
        }
        Title();
    }

    private void Title()
    {
        // シーンを再読み込み
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}
