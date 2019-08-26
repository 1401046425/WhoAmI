using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TimerText : MonoBehaviour
{
    [SerializeField] private TextMeshPro Text;

    [SerializeField] private uint TargetTime;
    private uint NowTimeValue;
    public uint m_Time
    {
        get { return NowTimeValue; }
        set
        {
            NowTimeValue = value;
            Text.text = NowTimeValue.ToString();
            if (NowTimeValue <= 0)
            {
                OnTimeup?.Invoke();
                StopTime();
            }
            
        }
    }

    [SerializeField] private UnityEvent OnTimeup;

    private Coroutine TimeUpdate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartTime()
    {
        ResetTime();
        TimeUpdate = StartCoroutine(UpdateTime());
    }

    public void StopTime()
    {
        if(TimeUpdate!=null)
            StopCoroutine(TimeUpdate);
    }

    public void ResetTime()
    {
        m_Time = TargetTime;
    }

    IEnumerator UpdateTime()
    {
        while (true)
        {
            yield return new  WaitForSecondsRealtime(1f);
            m_Time--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
