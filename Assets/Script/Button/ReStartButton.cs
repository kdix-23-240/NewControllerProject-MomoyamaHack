using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ReStartButton : MonoBehaviour
{
    [SerializeField] private GameObject player; // Playerオブジェクトを参照するためのフィールド
    private Get_Information info;
    private string[] data = new string[5];
    int baibz = 0;

    void Start()
    {
        this.info = new Get_Information();
    }

    void Update()
    {
        if (CountText(",", info.Getinfo()) == 4)
        {
            data = info.Getinfo().Split(',');
            baibz = int.Parse(data[4]);
        }
        if (baibz == 1) ReStart();
    }
    public void OnClick()
    {
        ReStart();
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