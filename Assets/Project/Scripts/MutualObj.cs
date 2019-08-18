using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MutualObj : MonoBehaviour
{
    [SerializeField] private UnityEvent OnMutualed;
    [HideInInspector] public bool HasMut;
    public void Mutualed()
    { 
if(HasMut)
return;
OnMutualed?.Invoke();
HasMut = true;

    }
}
