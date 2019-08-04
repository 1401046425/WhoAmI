using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TimeTaskManager : Singleton<TimeTaskManager>
{
   [SerializeField] private  List<TimeTask> TimeFrameTasks = new List<TimeTask>();
   TimeTask TaskINS;
   private Queue<TimeTask> TaskQueue=new Queue<TimeTask>();
   private TimeTask NowPlayTask {
        get {
            return TaskINS;
        }
        set
        {
            TaskINS = value;
            TaskINS.StartTask();
        }
    }
   public Action<TimeTask> OnTaskStart;
   public Action<TimeTask> OnTaskPointSet;
   public Action<TimeTask> OnTaskFinish;
   private void Awake()
   {
       PushAllTask();
   }

   private void PushAllTask()
   {
       foreach (var VARIABLE in TimeFrameTasks)
       {
           PushTimeTask(VARIABLE);
       }
   }

   public void PlayNextTask()
   {
       NowPlayTask = TaskQueue.Dequeue();
   }
    public void PushTimeTask(TimeTask task)
    {
        TaskQueue.Enqueue(task);
    }

    public TimeTask GetNowTask()
    {
        return NowPlayTask;
    }

    public void AddPoint()
    {
        NowPlayTask.PointCount++;
    }
    
}

