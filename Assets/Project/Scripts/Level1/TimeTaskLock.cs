using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeTaskLock : MonoBehaviour
{
    [SerializeField] private TimeTask TargetTask;
    [SerializeField] private UnityEvent FinishLockOpen;
    [SerializeField] private UnityEvent LockOpen;
    private void Awake()
    {
        TimeTaskManager.INS.OnTaskFinish += CheckLock;
        TimeTaskManager.INS.OnTaskPointSet += CheckPoint;
    }

    private void CheckPoint(TimeTask obj)
    {
        if (obj != TargetTask)
            return;
        LockOpen?.Invoke();
    }

    private void CheckLock(TimeTask obj)
    {
        if (obj != TargetTask)
            return;
        FinishLockOpen?.Invoke();
    }
}
