using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{

   [SerializeField] private UnityEvent TriggerEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEvent?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerEvent?.Invoke();
    }
}
