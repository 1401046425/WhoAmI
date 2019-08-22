using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoryBlock : MonoBehaviour//故事区块，管理每个故事片段的功能
{
    [SerializeField] private string BlockName;

    public string _BlockName//区块名称
    {
        get { return BlockName; }
    }

    [SerializeField] public UnityEvent OnBlockInit;
    [SerializeField] public UnityEvent OnBlockEnter;
    [SerializeField] public UnityEvent OnBlockExit;

    public string SetBlockName
    {
        set { BlockName = value; }
    }

    public virtual void OnInit()//区块初始化
    {
        OnBlockInit?.Invoke();
    }
    public virtual void OnEnter()//区块进入
    {
        OnBlockEnter?.Invoke();
    }
    public virtual void OnExit()//区块退出
    {
        OnBlockExit?.Invoke();
    }
}
