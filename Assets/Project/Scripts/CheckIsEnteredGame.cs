using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckIsEnteredGame : MonoBehaviour
{
    [SerializeField] private UnityEvent IsEnter;

    public bool IsEnterGame
    {
        set { GameManager.INS.IsEnterGame = value; }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        if(GameManager.INS.IsEnterGame)
            IsEnter?.Invoke();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
