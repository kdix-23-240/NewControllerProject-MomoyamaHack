using UnityEngine;

public class CameraLay : MonoBehaviour
{
    void Update()
    {
        // 6�����Ɍ����ĐԂ�����`��
        Debug.DrawRay(transform.position, transform.forward * 50f, Color.red);
        Debug.DrawRay(transform.position, -transform.forward * 50f, Color.red);
        Debug.DrawRay(transform.position, transform.right * 50f, Color.red);
        Debug.DrawRay(transform.position, -transform.right * 50f, Color.red);
        Debug.DrawRay(transform.position, transform.up * 50f, Color.red);
        Debug.DrawRay(transform.position, -transform.up * 50f, Color.red);
    }
}