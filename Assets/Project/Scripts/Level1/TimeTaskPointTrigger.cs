using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTaskPointTrigger : MonoBehaviour
{
    private bool isPlayed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isPlayed)
            return;
        if (!other.CompareTag("Player"))
            return;
        TimeTaskManager.INS.AddPoint();
        isPlayed = true;
    }
}
