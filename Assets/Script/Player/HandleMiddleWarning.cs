using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMiddleWarning : MonoBehaviour
{
    private bool isHit = false;
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("[OutSide] Enter" );
        // 衝突したオブジェクトの名前をログに表示
        if (collision.gameObject.tag == "Stick")
        {
            isHit = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        Debug.Log("[OutSide] Exit" );
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