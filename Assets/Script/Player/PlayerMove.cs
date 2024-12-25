using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Header("持ち手の移動スピード")] private float moveSpeed = 0.1f;
    [SerializeField] private GameObject circleHandle;
    private Vector3 playerFirstRight;
    void Start()
    {
        playerFirstRight= transform.right;
    }

    void Update()
    {
        if (GameSystem.Instance.GetCanMove())
        {
            MoveHandle();
        }

        // ConversionCircleHandleCoordinates();
        // ConversionPlayerPolarCoordinates();
    }

    /// <summary>
    /// 持ち手を移動させる
    /// </summary>
    private void MoveHandle()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(circleHandle.transform.up * moveSpeed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(-circleHandle.transform.forward * moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate((circleHandle.transform.forward) * moveSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate((-circleHandle.transform.up) * moveSpeed);
        }
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

    private void ConversionPlayerPolarCoordinates()
    {
        // プレイヤーの位置を取得
        float playerPositionX = transform.position.x;
        float playerPositionY = transform.position.y;
        float playerPositionZ = transform.position.z;

        // プレイヤーの位置を極座標に変換
        // float r = Mathf.Sqrt(playerPositionX * playerPositionX + playerPositionZ * playerPositionZ);
        // float theta = Mathf.Atan2(playerPositionZ, playerPositionX);
        // float phi = Mathf.Atan2(r, playerPositionY);

        // // プレイヤーの位置を極座標に変換したものを表示
        // Debug.Log("Player | r: " + r + " theta: " + theta + " phi: " + phi);
        Debug.Log("Player | x: " + playerPositionX + " y: " + playerPositionY + " z: " + playerPositionZ);
    }

    private void ConversionCircleHandleCoordinates()
    {
        // プレイヤーの位置を取得
        float handlePositionX = circleHandle.transform.position.x;
        float handlePositionY = circleHandle.transform.position.y;
        float handlePositionZ = circleHandle.transform.position.z;

        // プレイヤーの位置を極座標に変換
        // float r = Mathf.Sqrt(handlePositionX * handlePositionX + handlePositionZ * handlePositionZ);
        // float theta = Mathf.Atan2(handlePositionZ, handlePositionX);
        // float phi = Mathf.Atan2(r, handlePositionY);

        // // プレイヤーの位置を極座標に変換したものを表示
        // Debug.Log("Handle | r: " + r + " theta: " + theta + " phi: " + phi);

        Debug.Log("Handle | x: " + handlePositionX + " y: " + handlePositionY + " z: " + handlePositionZ);
    }
}