using UnityEngine;  

public class StickMove : MonoBehaviour
{
    void Update()
    {
        // �X�e�B�b�N�̓��͂��擾
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // ���͂ɉ����ăI�u�W�F�N�g���ړ�
        Vector3 movement = new Vector3(horizontal, 0, vertical) * Time.deltaTime;
        transform.Translate(movement);
    }
}