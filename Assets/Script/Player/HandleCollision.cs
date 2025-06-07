using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollision : MonoBehaviour
{
    [SerializeField] private Canvas parent = default;
    [SerializeField] private GameObject biribiriModal = default;
    [SerializeField] private GameObject goalModal = default;
    private Get_Information info;

    private void Start()
    {
        info = FindObjectOfType<Get_Information>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Stick")
        {
            GameSystem.Instance.SetCanRotate(false);
            GameSystem.Instance.SetCanMove(false);
            var _dialog = Instantiate(biribiriModal);
            _dialog.transform.SetParent(parent.transform, false);
            info.SetOutgoingByte((byte)'4'); // ← '4' を送信
            Debug.Log("Game Over");
        }
        else if (collision.gameObject.tag == "Goal")
        {
            GameSystem.Instance.SetCanRotate(false);
            GameSystem.Instance.SetCanMove(false);
            var _dialog = Instantiate(goalModal);
            _dialog.transform.SetParent(parent.transform, false);
        }
    }
}
