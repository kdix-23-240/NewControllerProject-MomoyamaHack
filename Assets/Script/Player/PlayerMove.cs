using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    private float moveSpeed;
    [SerializeField] private GameObject circleHandle;
    [SerializeField] private float firstPlayerPositionX;
    [SerializeField] private float firstPlayerPositionY;
    [SerializeField] private float firstPlayerPositionZ;
    private Get_Information info;
    float bend = 0;

    void Start()
    {
        info = FindObjectOfType<Get_Information>();
    }

    void Update()
    {
        float[] data = info.GetReceivedData();
        bend = data[3]; // bendはインデックス3

        moveSpeed = 0.00002f * bend * bend;

        if (GameSystem.Instance.GetCanMove())
            MoveHandle();

        if (GameSystem.isReset)
            ResetPlayerPosition();
    }

    private void MoveHandle()
    {
        Vector3 moveDirection = -circleHandle.transform.up;
        transform.Translate(moveDirection.normalized * moveSpeed, Space.World);
    }

    public float playerPositionX => transform.position.x;
    public float playerPositionY => transform.position.y;
    public float playerPositionZ => transform.position.z;

    public void ResetPlayerPosition()
    {
        transform.position = new Vector3(firstPlayerPositionX, firstPlayerPositionY, firstPlayerPositionZ);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        GameSystem.isReset = false;
    }
}
