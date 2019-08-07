using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class CMCameraLooker : MonoBehaviour
{
    private CinemachineVirtualCamera CMCamera;
    private int OrinPriority;
    [SerializeField] private float WaitTime;

   [SerializeField] private UnityEvent OnLookOver;
    // Start is called before the first frame update
    private void Awake()
    {
        CMCamera = GetComponent<CinemachineVirtualCamera>();
        OrinPriority = CMCamera.m_Priority;
    }

    void Start()
    {
        
    }

    public void ShowCamera()
    {
        StartCoroutine(Look());
    }

    IEnumerator Look()
    {
        CMCamera.m_Priority = 11;
yield return new  WaitForSecondsRealtime(WaitTime);
CMCamera.m_Priority = OrinPriority;
OnLookOver?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
