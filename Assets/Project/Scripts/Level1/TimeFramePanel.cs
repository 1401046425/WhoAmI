using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeFramePanel : MonoBehaviour
{
    public Slider TimeTaskSlider;
    private void Awake()
    {
        TimeTaskManager.INS.OnTaskStart += OnTimeTaskStart;
        TimeTaskManager.INS.OnTaskPointSet += OnTimeTaskPointSet;
        TimeTaskManager.INS.OnTaskFinish += OnTimeTaskFinish;
    }

    private void OnTimeTaskPointSet(TimeTask obj)
    {
        TimeTaskSlider.value = obj.PointCount;
    }
    

    private void OnTimeTaskFinish(TimeTask obj)
    {
        ;
        if (TimeTaskSlider != null) TimeTaskSlider.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InitTimeTaskSlider(TimeTaskSlider,TimeTaskManager.INS.GetNowTask());
    }
    
    private void OnTimeTaskStart(TimeTask obj)
    {
        if (TimeTaskSlider != null)
        {
            TimeTaskSlider.gameObject.SetActive(true);
            InitTimeTaskSlider(TimeTaskSlider, obj);
        }
    }

    private void InitTimeTaskSlider(Slider TimeSlider,TimeTask task)
    {
        TimeSlider.maxValue = task.MaxPoint;
        TimeSlider.value = task.PointCount;
    }
}
