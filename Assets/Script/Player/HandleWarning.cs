using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleWarning : MonoBehaviour
{
    [SerializeField] private int warningLevel;
    void OnTriggerEnter(Collider collision)
    {
        // 衝突したオブジェクトの名前をログに表示
        if(collision.gameObject.tag == "Stick")
        {
            Debug.Log("warning:" + warningLevel);
        }
    }
}