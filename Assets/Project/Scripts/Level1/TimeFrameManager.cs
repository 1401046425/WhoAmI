using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFrameManager : Singleton<TimeFrameManager>
{
  private  Queue<TimeTask> TimeFrameTasks = new Queue<TimeTask>();
   TimeTask TaskINS;
   private TimeTask NowPlayTask {
        get {
            return TaskINS;
        }
        set
        {
            TaskINS = value;
        }
    }

    public void PlayNextTask()
    {
        NowPlayTask = TimeFrameTasks.Dequeue();
    }
    public void AddTimeTask(TimeTask task)
    {
        TimeFrameTasks.Enqueue(task);
    }
}

