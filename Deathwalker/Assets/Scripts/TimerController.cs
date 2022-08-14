using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{

    [SerializeField]
    public FloatSO timerSO;

    void Update()
    {
        timerSO.Value = timerSO.Value + Time.deltaTime;
        GameManager.instance.timerTicker();
    }
}
