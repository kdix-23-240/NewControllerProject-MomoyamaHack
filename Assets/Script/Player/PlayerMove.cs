using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UIElements;

/// <summary>
/// bend（曲げセンサー）値に基づいてプレイヤーを移動させるスクリプト
/// ハンドルの向きに沿って進み、リセット時には元の位置に戻す
/// </summary>
public class PlayerMove : MonoBehaviour
{
    private float moveSpeed; // bend値に基づいて計算される移動速度
    [SerializeField] private GameObject circleHandle;         // 進行方向の基準となるハンドルオブジェクト
    [SerializeField] private float firstPlayerPositionX;      // 初期位置X
    [SerializeField] private float firstPlayerPositionY;      // 初期位置Y
    [SerializeField] private float firstPlayerPositionZ;      // 初期位置Z
    [SerializeField] private float speed;                     // 速度調節のための定数
    [SerializeField] private int bend_wall;                   // 曲げセンサーの閾値

    float bend = 0;               // bend値（曲げセンサーの出力）

    /// <summary>
    /// 初期化処理：特に不要（SingletonでGet_Informationにアクセスするため）
    /// </summary>
    void Start()
    {
        // Singletonアクセスなので何も不要
    }

    /// <summary>
    /// 毎フレーム実行：bend値に応じて移動・初期化処理を判断して実行
    /// </summary>
    void Update()
    {
        // 安全確認：Get_Information未初期化なら中断
        if (Get_Information.Instance == null) return;

        // bend値の取得（index 3 に格納されている）
        float[] data = Get_Information.Instance.GetReceivedData();
        bend = data[3];

        if (bend < bend_wall) bend = 0;

        // 移動速度は bend² に比例する（bendが大きいほど速く進む）
        moveSpeed = speed * bend * bend;

        // 移動許可フラグが有効な場合、プレイヤーを進行方向に移動
        if (GameSystem.Instance.GetCanMove())
            MoveHandle();

        // リセット命令が来ていたらプレイヤー位置を初期化
        if (GameSystem.isReset)
            ResetPlayerPosition();
    }

    /// <summary>
    /// ハンドルの向き（下方向）に対してプレイヤーを移動させる
    /// </summary>
    private void MoveHandle()
    {
        Vector3 moveDirection = -circleHandle.transform.up; // ハンドルの下向きを移動方向に設定
        transform.Translate(moveDirection.normalized * moveSpeed, Space.World);
    }

    // 以下はプレイヤーの現在位置を取得するためのプロパティ
    public float playerPositionX => transform.position.x;
    public float playerPositionY => transform.position.y;
    public float playerPositionZ => transform.position.z;

    /// <summary>
    /// プレイヤーの位置と角度を初期状態にリセットする
    /// </summary>
    public void ResetPlayerPosition()
    {
        transform.position = new Vector3(firstPlayerPositionX, firstPlayerPositionY, firstPlayerPositionZ); // 初期位置に戻す
        transform.rotation = Quaternion.Euler(0, 0, 0); // 回転も初期化
        GameSystem.isReset = false; // リセットフラグを解除
    }
}
