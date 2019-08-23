using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class LianliankanGroup : MonoBehaviour
{
    List<Lianliankan> Lianliankans=new List<Lianliankan>();
    
    List<Lianliankan> CheckList =new List<Lianliankan>();
    private int NowLianLianKanCount;
    private string NowLianLianKanName;

    [SerializeField] private UnityEvent OnLianLianKanFinish;
    private bool IsFinish;
    private void Awake()
    {
        InitLianlianKan();
    }

    void InitLianlianKan()
    {
        foreach (var VARIABLE in transform.GetComponentsInChildren<Lianliankan>())
        {
            Lianliankans.Add(VARIABLE);
            VARIABLE.Master = this;
        }
    }
/// <summary>
/// 添加检查连连看
/// </summary>
/// <param name="lianliankan"></param>
    public void AddCheckLianliankan(Lianliankan lianliankan)
    {
        if (IsFinish)
            return; 
        if (string.IsNullOrWhiteSpace(lianliankan.LianliankanName))
        {
            Debug.LogError("连连看模块名称为空");
            return;
        }
        
        if (lianliankan.LianliankanName != NowLianLianKanName&&!String.IsNullOrWhiteSpace(NowLianLianKanName))//连连看名称和当前选中的名称不匹配，并且不为空
        {
            lianliankan.OnReset();
            foreach (var VARIABLE in CheckList)
            {
                VARIABLE.OnReset();
            }
            ClearAllAddLianLianKan();
            return;
        }
        if (String.IsNullOrWhiteSpace(NowLianLianKanName))//当前没有选中任何连连看拼图
        {
            CheckList.Clear();
            NowLianLianKanCount = 0;
            ////统计该名称连连看的数量
            if (NowLianLianKanCount == 0)
            {
                foreach (var VARIABLE in Lianliankans)
                {
                    if (VARIABLE.LianliankanName == lianliankan.LianliankanName)
                        NowLianLianKanCount++;
                } 
            }
            ////
            NowLianLianKanName = lianliankan.LianliankanName;
        }
        if (!CheckList.Contains(lianliankan))//如果连连看检测数组中不存在
            {
                CheckList.Add(lianliankan);//添加已经检测完毕的连连看
               lianliankan.OnAdd();
            }
            if (CheckList.Count >= NowLianLianKanCount)
            {
                FinishLianLianKan(CheckList);
            }

    }
/// <summary>
/// 判断所有连连看是否完成
/// </summary>
/// <param name="lianliankan"></param>
    void FinishLianLianKan(List<Lianliankan> lianliankan)
    {

        foreach (var VARIABLE in lianliankan)
        {
            VARIABLE.OnFinish();
            if (Lianliankans.Contains(VARIABLE))
                Lianliankans.Remove(VARIABLE);
        }

        ClearAllAddLianLianKan();
        if (Lianliankans.Count <= 0)
        {
            OnLianLianKanFinish?.Invoke();
            IsFinish = true;
        }

            
    }
/// <summary>
/// 清空当前已配对的连连看数据
/// </summary>
void ClearAllAddLianLianKan()
{
    CheckList.Clear();
    NowLianLianKanCount = 0;
    NowLianLianKanName=String.Empty;
}

}
