using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetCameraPosButton : MonoBehaviour
{
    private CameraController cameraController;

    public void Start()
    {
        // Playerオブジェクトを探して、その中のCameraControllerを取得
        GameObject player = GameObject.FindWithTag("PlayerCamera");
        if (player != null)
        {
            cameraController = player.GetComponentInChildren<CameraController>();
            if (cameraController == null)
            {
                Debug.LogError("CameraController component not found on Player object.");
            }
        }
        else
        {
            Debug.LogError("Player object not found.");
        }
    }

    public void OnClick()
    {
        if (cameraController != null)
        {
            cameraController.Reset();
        }
    }
}