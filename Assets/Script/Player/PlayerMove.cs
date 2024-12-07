using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Header ("持ち手の移動スピード")] private float moveSpeed = 0.1f;
    void Start()
    {
        // Debug.Log("PlayerMove Start");
    }

    void Update()
    {
        if(GameSystem.Instance.GetCanMove())
        {
            MoveHandle();
        }
    }

    private void MoveHandle()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * moveSpeed);
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * moveSpeed);
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed);
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed);
        }
    }
}