using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotateYZ : MonoBehaviour
{
    [SerializeField, Header("yz軸の回転スピード")] private float rotateSpeed = 0.1f;
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

    private void RotateHandle()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(new Vector3(rotateSpeed, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(new Vector3(-rotateSpeed, 0, 0));
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, rotateSpeed, 0));
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, -rotateSpeed, 0));
        }
    }
}