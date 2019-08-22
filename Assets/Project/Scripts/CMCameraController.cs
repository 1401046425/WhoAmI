using System;
using UnityEngine;
using System.Collections;
using Cinemachine;


    public class CMCameraController : MonoBehaviour
    {
        private CinemachineVirtualCamera CMCamera;
        private int OrinPriority;
        
        [SerializeField] private float EnterWaitTime;
        [SerializeField] private float ExitWaitTime;
        

        private void Awake()
        {
            CMCamera = GetComponent<CinemachineVirtualCamera>();
            OrinPriority = CMCamera.m_Priority;
        }

        public void ShowCamera()
        {
            StartCoroutine(DelayedCall(CameraShow, EnterWaitTime));
        }

        private void CameraShow()
        {
            CMCamera.m_Priority = 10;
        }
        
        public void CloseCamera()
        {
            StartCoroutine(DelayedCall(CameraClose, ExitWaitTime));
        }
        private void CameraClose()
        {
            CMCamera.m_Priority = OrinPriority;
        }
        IEnumerator DelayedCall(Action CallBack, float time)
        {
            yield return new  WaitForSecondsRealtime(time);
            CallBack?.Invoke();
        }
    }