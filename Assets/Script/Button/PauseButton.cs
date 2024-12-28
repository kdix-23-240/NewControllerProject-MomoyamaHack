using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{

    // ダイアログを追加する親のCanvas
    [SerializeField] private Canvas parent = default;
    [SerializeField] private GameObject biribiriModal = default;

    private Get_Information info;
    static string[] data = new string[5];
    float baib = 0;
    void Start()
    {
        this.info = new Get_Information();
    }

    void Update()
    {
        if (CountText(",", info.Getinfo()) == 4)
        {
            data = info.Getinfo().Split(',');
            baib = int.Parse(data[4]);
        }

        if (baib == 1) OnClick();

    }

    private int CountText(string search, string target)
    {
        int cnt = 0;
        bool check = true;

        while (check)
        {
            if (target.IndexOf(search, System.StringComparison.CurrentCulture) == -1)
            {
                check = false;
            }
            else
            {
                target = target.Remove(0, target.IndexOf(search, System.StringComparison.CurrentCulture) + 1);
                cnt++;
            }
        }

        return cnt;
    }

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