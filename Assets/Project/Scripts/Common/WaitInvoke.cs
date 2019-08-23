using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class WaitInvoke : MonoBehaviour
{
    [SerializeField] private float WaitTime;
    [SerializeField] private UnityEvent Invoke;
    public void StartWait()
    {
        StartCoroutine(InvokeAction());
    }

    IEnumerator InvokeAction()
    {
        yield return new  WaitForSecondsRealtime(WaitTime);
        Invoke?.Invoke();
        
    }
}
