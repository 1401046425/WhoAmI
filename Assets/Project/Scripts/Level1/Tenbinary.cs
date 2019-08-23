using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Tenbinary : MonoBehaviour
{
    private const int TenbinaryValue=12;
    private int Value;

    [SerializeField] private UnityEvent OnValueFinish;

    [SerializeField] private TextMeshPro GeWei;
    [SerializeField] private TextMeshPro ShiWei;

    private bool isfinish;
        // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddValue()
    {
        if(isfinish)
            return;
        if (Value < TenbinaryValue)
        {
            Value++; 
            GeWei.text = Value.ToString();
            if (Value >= TenbinaryValue)
            {
                ShiWei.text = "1";
                GeWei.text = "0";
                OnValueFinish?.Invoke();
                isfinish = true;
            }
        }
                    
    }

    public void LessValue()
    {
        if(isfinish)
            return;
        if (Value > 0)
        {
            Value--;
            GeWei.text = Value.ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
