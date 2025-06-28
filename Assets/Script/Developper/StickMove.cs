using UnityEngine;  

public class StickMove : MonoBehaviour
{
    void Update()
    {
        // スティックの入力を取得
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // 入力に応じてオブジェクトを移動
        Vector3 movement = new Vector3(horizontal, 0, vertical) * Time.deltaTime;
        transform.Translate(movement);
    }
}