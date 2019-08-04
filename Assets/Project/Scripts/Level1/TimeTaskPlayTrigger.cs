using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTaskPlayTrigger : MonoBehaviour
{
    private bool isPlayed;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isPlayed)
            return;
        if (!other.CompareTag("Player"))
            return;
        TimeTaskManager.INS.PlayNextTask();
        isPlayed = true;
    }
}
