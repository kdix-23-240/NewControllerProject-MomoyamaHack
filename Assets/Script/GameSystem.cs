using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム全体のシステムを管理するクラス
/// シングルトン
/// </summary>
public class GameSystem
{
    private static GameSystem instance = null;
    private bool canMove = true;
    private bool canRotate = true;

    public static float clearTime = 0;


    private GameSystem()
    {
    }

    public static GameSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameSystem();
            }
            return instance;
        }
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public bool GetCanRotate()
    {
        return canRotate;
    }

    public void SetCanRotate(bool canRotate)
    {
        this.canRotate = canRotate;
    }
}