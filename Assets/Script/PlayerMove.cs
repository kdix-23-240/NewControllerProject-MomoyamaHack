using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Header ("持ち手の移動スピード")] private float moveSpeed = 0.1f;
    void Start()
    {
        Debug.Log("PlayerMove Start");
    }

    void Update()
    {
        MoveHandle();
    }

    private void MoveHandle()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, moveSpeed, 0);
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -moveSpeed, 0);
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-moveSpeed, 0, 0);
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(moveSpeed, 0, 0);
        }
    }
}