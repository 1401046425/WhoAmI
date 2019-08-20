using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{

   [SerializeField] private UnityEvent TriggerEvent;
   [SerializeField] private UnityEvent TriggerExitEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEvent?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerEvent?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerExitEvent?.Invoke();
    }
}
