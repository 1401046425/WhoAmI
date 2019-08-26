using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField]private UnityEvent OnTrigger;

    [SerializeField] private bool JustOne;

    private bool IsInoved;
    [SerializeField] private LayerMask Layer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(IsInoved)
            return;
        if((Layer.value & (int)Mathf.Pow(2,gameObject.layer)) == (int)Mathf.Pow(2,gameObject.layer))
            OnTrigger?.Invoke();
        if (JustOne)
            IsInoved = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsInoved)
            return;
        if (other.gameObject.layer == Layer)
            OnTrigger?.Invoke();
        if (JustOne)
            IsInoved = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
