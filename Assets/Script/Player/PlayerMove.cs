using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Header("持ち手の移動スピード")] private float moveSpeed = 0.1f;
    [SerializeField] private GameObject circleHandle;
    void Start()
    {

    }

    void Update()
    {
        if (GameSystem.Instance.GetCanMove())
        {
            MoveHandle();
        }
    }

    /// <summary>
    /// 持ち手から見て右方向に持ち手を移動させる
    /// </summary>
    private void MoveHandle()
    {
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection -= circleHandle.transform.up;
        }
        transform.Translate(moveDirection.normalized * moveSpeed, Space.World);
    }

    public float playerPositionX
    {
        get
        {
            return transform.position.x;
        }
    }

    public float playerPositionY
    {
        get
        {
            return transform.position.y;
        }
    }

    public float playerPositionZ
    {
        get
        {
            return transform.position.z;
        }
    }
}