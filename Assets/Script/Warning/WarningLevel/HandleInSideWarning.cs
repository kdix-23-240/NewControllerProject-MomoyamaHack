using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleInSideWarning : MonoBehaviour
{
    private bool isHit = false;

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("[OutSide] Enter");
        // 衝突したオブジェクトの名前をログに表示
        if (other.gameObject.tag == "Stick")
        {
            isHit = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        Debug.Log("[OutSide] Exit");
        // 衝突したオブジェクトの名前をログに表示
        if (other.gameObject.tag == "Stick")
        {
            isHit = false;
        }
    }

    public bool GetIsHit()
    {
        return isHit;
    }
}