using UnityEngine;

public class CameraLay : MonoBehaviour
{
    void Update()
    {
        // �J�����̎����ɐԂ�����`�悷��
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);
    }
}