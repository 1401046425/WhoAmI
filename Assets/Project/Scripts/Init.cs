using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Init : MonoBehaviour
{
    [SerializeField] private UnityEvent OnInit;

    private void Awake()
    {
        OnInit?.Invoke();
    }
}
