using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float cameraPositionX = 0;
    private float cameraPositionY = 0;
    private float cameraPositionZ = 0;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeedX;
    [SerializeField] private float positionLimitX;
    [SerializeField] private float rotationLimitX;
    private float newXPosition;
    private float newYRotation;
    private int clickCount = 0;
    private PlayerMove playerMove;

    void Start()
    {
        cameraPositionX = transform.position.x;
        cameraPositionY = transform.position.y;
        cameraPositionZ = transform.position.z;
    }

    void Update()
    {
        // カメラが常に親オブジェクトを向くようにする
        if (transform.parent != null)
        {
            transform.LookAt(transform.parent.position);
        }
        DelayMove();
    }

    private void DelayMove()
    {
        RegisterCameraPosition();
        if (Input.GetKey(KeyCode.A))
        {
            DelayMovePlus();
        }

        if (Input.GetKey(KeyCode.D))
        {
            DelayMoveMinus();
        }
    }

    private void DelayMovePlus()
    {
        newXPosition = transform.position.x + moveSpeed;
        if(-positionLimitX < (transform.parent.position.x - transform.position.x))
        {
            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    private void DelayMoveMinus()
    {
        newXPosition = transform.position.x - moveSpeed;
        if ((transform.parent.position.x - transform.position.x) < positionLimitX)
        {
            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    public void ZoomIn()
    {
        RegisterCameraPosition();
        if (clickCount < 20) // ZoomIn限界(0は初期位置)
            transform.position = new Vector3(cameraPositionX, cameraPositionY, cameraPositionZ + (++clickCount * 0.005f));
    }

    public void ZoomOut()
    {
        RegisterCameraPosition();
        if (clickCount > -60) // ZoomOut限界
            transform.position = new Vector3(cameraPositionX, cameraPositionY, cameraPositionZ + (--clickCount * 0.005f));
    }

    public void Reset()
    {
        RegisterCameraPosition();
        transform.position = new Vector3(cameraPositionX, cameraPositionY, cameraPositionZ);
        clickCount = 0;
    }

    private void RegisterCameraPosition()
    {
        cameraPositionX = transform.position.x;
        cameraPositionY = transform.position.y;
        cameraPositionZ = transform.position.z;
    }
}