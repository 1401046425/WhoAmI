using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTaskWall : LockWall
{
   [SerializeField] private int targetpoint;
   
    public override void UnLockAllWall()
    {
        if(TimeTaskManager.INS.GetNowTask().PointCount<targetpoint)
            return;
        base.UnLockAllWall();
    }
}
