using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private float offsetX = 0.0f; // �v���C���[�����X���I�t�Z�b�g
    [SerializeField] private float offsetY = 0.0f; // �v���C���[�����Y���I�t�Z�b�g
    [SerializeField] private float offsetZ = 0.0f; // �v���C���[�����Z���I�t�Z�b�g
    private Vector3 playerPos = Vector3.zero;
    private Vector3 pos= Vector3.zero;

    void Awake()
    {
        if(player == null)
        {
            Debug.LogError("PlayerCamera:�v���C���[���A�^�b�`����Ă��܂���");
            return;
        }
        playerPos = player.transform.position;
        pos = new Vector3(playerPos.x + offsetX, playerPos.y + offsetY, playerPos.z + offsetZ);
    }
    private void Update()
    {
        ChasePlayer();
    }
    private void ChasePlayer()
    {
        if (player == null) return;
        playerPos = player.transform.position;
        pos = new Vector3(playerPos.x + offsetX, playerPos.y + offsetY, playerPos.z + offsetZ);
        transform.position = pos;
    }
}