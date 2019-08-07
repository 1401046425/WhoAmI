using System;
using System.Collections;
using System.Collections.Generic;
using UIFrameWork;
using UIFrameWork.BasePanel;
using UnityEngine;
using UnityEngine.Events;

public class HolyBible : BaseUIPanel
{
    [SerializeField]private BookPro Book;
    [SerializeField]private int LastPageindex;

    private bool IsClose { get; set; }
    private void Awake()
    {
        Book.OnFlip.AddListener(CheckPageEnd);
    }
    internal override void OnShow()
    {
        UIManager.FadeInFX(this.gameObject,0.01f,0.02f);
    }
    private void CheckPageEnd()
    {
        if (Book.currentPaper == LastPageindex)
        {
            if(IsClose)
                return;
BaseLevelManager.INS.WaitCall(Fadeout,1f);
StoryBlockManager.INS.StopNowBlock(2f);
            IsClose = true;
        }
    }

    void Fadeout()
    {
        UIManager.FadeOutFX(this.gameObject, 0.01f, 0.02f, base.Close);
    }
}
