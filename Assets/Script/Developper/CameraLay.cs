using UnityEngine;

public class CameraLay : MonoBehaviour
{
    void Update()
    {
        // 6•ûŒü‚ÉŒü‚¯‚ÄÔ‚¢ü‚ğ•`‰æ
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);
        Debug.DrawRay(transform.position, -transform.forward * 10f, Color.red);
        Debug.DrawRay(transform.position, transform.right * 10f, Color.red);
        Debug.DrawRay(transform.position, -transform.right * 10f, Color.red);
        Debug.DrawRay(transform.position, transform.up * 10f, Color.red);
        Debug.DrawRay(transform.position, -transform.up * 10f, Color.red);
    }
}