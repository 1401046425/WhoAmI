using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UIFrameWork;
using UIFrameWork.BasePanel;
using UnityEngine;

public class SuMeiWangBiao : BaseUIPanel
{
    private bool IsClose;
    internal override void OnShow()
    {
      UIManager.FadeInFX(this.gameObject,0.01f,0.02f);
    }

    internal override void OnClose()
    {

    }

    public override void Close()
    {
        if(IsClose)
            return;
        UIManager.FadeOutFX(this.gameObject, 0.01f, 0.02f, base.Close);
        IsClose = true;
        StoryBlockManager.INS.StopNowBlock(1f);
    }
}
