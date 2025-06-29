using UnityEngine;

public class CameraLay : MonoBehaviour
{
    void Update()
    {
        // ƒJƒƒ‰‚Ì‹ü‚ÉÔ‚¢ü‚ğ•`‰æ‚·‚é
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);
    }
}