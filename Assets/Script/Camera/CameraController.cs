using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float cameraPositionX = 0;
    private float cameraPositionY = 0;
    private float cameraPositionZ = 0;
    private int clickCount = 0;

    void Start()
    {
        cameraPositionX = transform.position.x;
        cameraPositionY = transform.position.y;
        cameraPositionZ = transform.position.z;
    }

    void Update()
    {
        
    }

    public void ZoomIn()
    {
        RegisterCameraPosition();
        transform.position = new Vector3(cameraPositionX, cameraPositionY, cameraPositionZ + (++clickCount * 0.005f));
    }

    public void ZoomOut()
    {
        RegisterCameraPosition();
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
    }
}