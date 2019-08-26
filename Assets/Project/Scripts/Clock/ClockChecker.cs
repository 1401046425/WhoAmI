using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClockChecker : MonoBehaviour
{
    [SerializeField] private Clock m_clock;

    [SerializeField] private int TargetLenth;
    [SerializeField] private UnityEvent MoreThanLenth;
    [SerializeField] private UnityEvent LessThanLenth;
    
    // Start is called before the first frame update
    void Start()
    {
     if(m_clock)
         m_clock.OnLenthChange+=CheckLenth;
    }

    private void CheckLenth(int obj)
    {
        if (obj >= TargetLenth)
        {
            MoreThanLenth?.Invoke();
        }
        else
        {
            LessThanLenth?.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
