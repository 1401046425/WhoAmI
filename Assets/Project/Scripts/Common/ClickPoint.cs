using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickPoint : MonoBehaviour
{
    [SerializeField] private int PointCount;
    private int point;
   [SerializeField] private UnityEvent OnClickPointFinish;
    public void AddPoint()
    {
        if (point < PointCount)
        {
            point++;
            if(point>=PointCount)
                OnClickPointFinish?.Invoke();
        }
    }
}
