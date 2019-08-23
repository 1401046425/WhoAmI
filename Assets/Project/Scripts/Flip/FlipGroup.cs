using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class FlipGroup : MonoBehaviour
{
    List<Flip> Flips =new List<Flip>();
    private string NowFlipName;
    private Flip LastFlip;
    [SerializeField] private UnityEvent OnFlipsFinish;
    private void Awake()
    {
        InitFlip();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddCheckFlipName(Flip flip)
    {
        if (string.IsNullOrWhiteSpace(flip.FlipName))
        {
            Debug.LogError("翻牌名称为空，请检查翻牌设置");
            return;
        }
        
        if (LastFlip == null)
        {
            LastFlip = flip;
        }
        else
        {
            if(LastFlip.Equals(flip))
                return;
        }


        if (string.IsNullOrWhiteSpace(NowFlipName))
        {
            NowFlipName = flip.FlipName;
            return;
        }
        if (NowFlipName == flip.FlipName)
        {
            FinishFlip(flip.FlipName);
        }
        else
        {
            NowFlipName = string.Empty;
            LastFlip = null;
        }
    }

    private Flip GetFlip(string Name)
    {
        foreach (var VARIABLE in Flips)
        {
            if(VARIABLE.FlipName==Name)
                return VARIABLE;
        }

        return null;
    }

    void FinishFlip(string Name)
    {
        List<Flip> MatchFlip=new List<Flip>();
        foreach (var VARIABLE in Flips)
        {
         if(VARIABLE.FlipName==Name)
             MatchFlip.Add(VARIABLE);
        }

        foreach (var VARIABLE in MatchFlip)
        {
            VARIABLE.OnFinish();
            Flips.Remove(VARIABLE);
        }

        LastFlip = null;
        NowFlipName = string.Empty;
        if (Flips.Count <= 0)
            OnFlipsFinish?.Invoke();
    }

    void InitFlip()
    {
        foreach (var VARIABLE in transform.GetComponentsInChildren<Flip>())
        {
            Flips.Add(VARIABLE);
            VARIABLE.Master = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
