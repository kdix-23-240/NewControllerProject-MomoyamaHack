using UnityEngine;

public class CameraLay : MonoBehaviour
{
    void Update()
    {
        // カメラの視線に赤い線を描画する
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);
    }
}