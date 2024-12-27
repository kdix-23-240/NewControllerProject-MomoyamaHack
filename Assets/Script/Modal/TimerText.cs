using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
    void Start()
    {
        Debug.Log("GameSystem.clearTime: " + GameSystem.clearTime);
        gameObject.GetComponent<Text>().text = GameSystem.clearTime.ToString("F2") + "s";
    }
}
