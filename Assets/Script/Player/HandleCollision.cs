using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトの名前をログに表示
        if(collision.gameObject.tag == "Stick")
        {
            
        }
    }
}