using System;
using System.Collections;
using System.Collections.Generic;
using UIFrameWork;
using UIFrameWork.BasePanel;
using UnityEngine;

public class PausePanel : BaseUIPanel
{
    internal override void OnShow()
    {
        BaseLevelManager.INS.PauseBGM();
        UIManager.FadeInFX(this.gameObject,0.01f,0.05f,GameManager.INS.PauseGame);
    }
    
    public override void Close()
    {
        GameManager.INS.UnPauseGame();
        BaseLevelManager.INS.UnPauseBGM();
        UIManager.FadeOutFX(this.gameObject, 0.01f, 0.02f, base.Close);
    }
}
