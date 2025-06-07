using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollision : MonoBehaviour
{
    [SerializeField] private Canvas parent = default;
    [SerializeField] private GameObject biribiriModal = default;
    [SerializeField] private GameObject goalModal = default;

    private Get_Information info;
    private WarningDelayManager delayManager;

    private void Start()
    {
        info = FindObjectOfType<Get_Information>();
        delayManager = new WarningDelayManager(this, info.SetOutgoingByte);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Stick")
        {
            Debug.Log("Collision with Stick detected!");

            GameSystem.Instance.SetCanRotate(false);
            GameSystem.Instance.SetCanMove(false);

            var _dialog = Instantiate(biribiriModal);
            _dialog.transform.SetParent(parent.transform, false);

            if (delayManager != null)
            {
                Debug.Log("Calling Send4Then5()");
                delayManager.Send4Then5();

                // 👇ここで自動リセットを予約
                StartCoroutine(ResetDelayAfter(5f)); // 5秒後に自動でhasSent5をリセット
            }
            else
            {
                Debug.LogError("delayManager is null!");
            }

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

    private IEnumerator ResetDelayAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        delayManager?.Reset();
        Debug.Log("[HandleCollision] WarningDelayManager Reset after delay");
    }
}
