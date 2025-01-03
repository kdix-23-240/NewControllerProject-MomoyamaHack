using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollision : MonoBehaviour
{
    // ダイアログを追加する親のCanvas
    [SerializeField] private Canvas parent = default;
    [SerializeField] private GameObject biribiriModal = default;
    [SerializeField] private GameObject goalModal = default;
    private Get_Information info;

    private void Start()
    {
        this.info = new Get_Information();
    }

    /// <summary>
    /// 衝突判定
    /// 衝突したオブジェクトが棒ならば、ゲームオーバー
    /// 衝突したオブジェクトがゴールならば、ゴール
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトの名前をログに表示
        if (collision.gameObject.tag == "Stick")
        {
            // Debug.Log("Stick");
            GameSystem.Instance.SetCanRotate(false);
            GameSystem.Instance.SetCanMove(false);
            //BiribiriModalプレハブをCanvasの子要素として生成
            var _dialog = Instantiate(biribiriModal) as GameObject;
            _dialog.transform.SetParent(parent.transform, false);
            info.GetOutGoingMsg("4");
            Debug.Log("Game Over");

        }
        else if (collision.gameObject.tag == "Goal")
        {
            // Debug.Log("Goal");
            GameSystem.Instance.SetCanRotate(false);
            GameSystem.Instance.SetCanMove(false);
            //BiribiriModalプレハブをCanvasの子要素として生成
            var _dialog = Instantiate(goalModal) as GameObject;
            _dialog.transform.SetParent(parent.transform, false);
        }
    }
}