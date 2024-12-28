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
    static string[] data = new string[5];
    float bend = 0;
    void Start()
    {
        this.info = new Get_Information();
    }

    void Update()
    {
        if (CountText(",", info.Getinfo()) == 4)
        {
            data = info.Getinfo().Split(',');
            bend = int.Parse(data[3]);
        }

        this.moveSpeed = 0.00005f * bend * bend;
        //Debug.Log(bend);

        if (GameSystem.Instance.GetCanMove())
        {
            MoveHandle();
        }

        if (GameSystem.isReset)
        {
            ResetPlayerPosition();
        }


    }

    private int CountText(string search, string target)
    {
        int cnt = 0;
        bool check = true;

        while (check)
        {
            if (target.IndexOf(search, System.StringComparison.CurrentCulture) == -1)
            {
                check = false;
            }
            else
            {
                target = target.Remove(0, target.IndexOf(search, System.StringComparison.CurrentCulture) + 1);
                cnt++;
            }
        }

        return cnt;
    }

    /// <summary>
    /// 持ち手から見て右方向に持ち手を移動させる
    /// </summary>
    private void MoveHandle()
    {
        Vector3 moveDirection = Vector3.zero;
        //if (Input.GetKey(KeyCode.D))
        //{
        moveDirection -= circleHandle.transform.up;
        //}
        transform.Translate(moveDirection.normalized * moveSpeed, Space.World);
    }

    public float playerPositionX
    {
        get
        {
            return transform.position.x;
        }
    }

    public float playerPositionY
    {
        get
        {
            return transform.position.y;
        }
    }

    public float playerPositionZ
    {
        get
        {
            return transform.position.z;
        }
    }
    public void ResetPlayerPosition()
    {
        transform.position = new Vector3(firstPlayerPositionX, firstPlayerPositionY, firstPlayerPositionZ);
        transform.rotation = Quaternion.Euler(0, 0, 0);

        GameSystem.isReset = false;
    }
}