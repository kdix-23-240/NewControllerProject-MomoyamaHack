using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRotateX : MonoBehaviour
{
    [SerializeField, Header("持ち手の回転スピード")] private float rotateSpeed = 0.1f;
    void Start()
    {

    }

    void Update()
    {
        if(GameSystem.Instance.GetCanRotate())
        {
            RotateHandle();
        }
    }

    /// <summary>
    /// 持ち手をz軸回転させる(持ち手自体がすでに回転させてあるので軸が狂ってる)
    /// </summary>
    private void RotateHandle()
    {
        
        if (Input.GetKey(KeyCode.Return))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                transform.Rotate(new Vector3(-rotateSpeed, 0, 0));
                return;
            }
            transform.Rotate(new Vector3(rotateSpeed, 0, 0));
        }
    }
}