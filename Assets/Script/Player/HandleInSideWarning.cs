using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleInSideWarning : MonoBehaviour
{
    private bool isHit = false;

    /// <summary>
    /// 侵入判定
    /// 衝突したオブジェクトが棒ならば、isHitをtrueにして警告を表示 
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("[Side] Enter" );
        // 衝突したオブジェクトの名前をログに表示
        if (collision.gameObject.tag == "Stick")
        {
            isHit = true;
        }
    }

    /// <summary>
    /// 侵入判定
    /// 侵入が終わったオブジェクトが棒ならば、isHitをfalseにして警告を非表示
    /// </summary>
    /// <param name="collision"></param>

    void OnTriggerExit(Collider collision)
    {
        Debug.Log("[Side] Exit" );
        // 衝突したオブジェクトの名前をログに表示
        if (collision.gameObject.tag == "Stick")
        {
            isHit = false;
        }
    }

    public bool GetIsHit()
    {
        return isHit;
    }
}