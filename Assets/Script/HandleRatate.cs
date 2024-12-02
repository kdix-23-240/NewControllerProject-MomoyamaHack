using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRatate : MonoBehaviour
{
    [SerializeField, Header ("持ち手の回転スピード")] private float rotateSpeed = 0.1f;
    void Start()
    {
        
    }

    void Update()
    {
        RotateHandle();
    }

    private void RotateHandle()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(rotateSpeed, 0, 0));
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(-rotateSpeed, 0, 0));
        }
    }
}