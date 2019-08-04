using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTask : MonoBehaviour
{
    public string TaskName;
    public int MaxPoint;
    private int Point;

    public int PointCount
    {
        get { return Point; }
        set
        {
            Point = value;
            TimeTaskManager.INS.OnTaskPointSet?.Invoke(this);
            if (Point >= MaxPoint)
            {
                TimeTaskManager.INS.OnTaskFinish?.Invoke(this);
            }
        }
    }

    internal void StartTask()
    {
        TimeTaskManager.INS.OnTaskStart?.Invoke(this);
    }

}
