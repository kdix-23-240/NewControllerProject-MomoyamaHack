using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    void Start()
    {
        GameSystem.clearTime = 0;
    }
    void Update()
    {
        GameSystem.clearTime += Time.deltaTime;
    }
}