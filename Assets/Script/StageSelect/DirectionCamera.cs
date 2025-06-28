using UnityEngine;

public class DirectionCamera : MonoBehaviour
{
    private Camera camera;

    void Awake()
    {
        camera = this.gameObject.GetComponent<Camera>();
    }

    void Start()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            // レイに当たったオブジェクトで何かをする
            Debug.Log("Hit object: " + objectHit.name);
        }
    }
}