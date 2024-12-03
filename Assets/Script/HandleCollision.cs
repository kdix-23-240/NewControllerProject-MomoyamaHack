using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトの名前をログに表示
        // if(collision.gameObject.tag == "Stick")
            // Debug.Log($"{gameObject.name} collided with {collision.gameObject.name}");
            Debug.LogError($"{gameObject.name} collided with {collision.gameObject.name}");
    }
}