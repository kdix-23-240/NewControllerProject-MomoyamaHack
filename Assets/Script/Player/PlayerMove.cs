using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Header("持ち手の移動スピード")] private float moveSpeed = 0.1f;
    [SerializeField] private GameObject circleHandle;
    [SerializeField] private float firstPlayerPositionX;
    [SerializeField] private float firstPlayerPositionY;
    [SerializeField] private float firstPlayerPositionZ;
    void Start()
    {

    }

    void Update()
    {
        if (GameSystem.Instance.GetCanMove())
        {
            MoveHandle();
        }
        if(GameSystem.isReset)
        {
            ResetPlayerPosition();
        }
    }

    /// <summary>
    /// 持ち手から見て右方向に持ち手を移動させる
    /// </summary>
    private void MoveHandle()
    {
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection -= circleHandle.transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += circleHandle.transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += circleHandle.transform.up;
        }
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

    /// <summary>
    /// プレイヤーの位置を初期位置(0,0,0)に戻す
    /// プレイヤーの傾きを初期位置(0,0,0)に戻す
    /// ゲームオーバー時に使用
    /// </summary>
    public void ResetPlayerPosition()
    {
        transform.position = new Vector3(firstPlayerPositionX, firstPlayerPositionY, firstPlayerPositionZ);
        transform.rotation = Quaternion.Euler(0, 0, 0);

        GameSystem.isReset = false;
    }
}