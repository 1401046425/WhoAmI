using System;
using System.Collections;
using System.Collections.Generic;
using UIFrameWork;
using UIFrameWork.BasePanel;
using UnityEngine;

public class PausePanel : BaseUIPanel
{
    private bool HasClose;
    
    internal override void OnShow()
    {
        HasClose = false;
        UIManager.FadeInFX(this.gameObject,0.01f,0.1f,GameManager.INS.PauseGame);
    }
    
    public override void Close()
    {
        if(HasClose)
            return;
        UIManager.FadeOutFX(this.gameObject, 0.01f, 0.1f, ClosePanel);
        HasClose = true;
    }

    private void ClosePanel()
    {
        GameManager.INS.UnPauseGame();
         UIManager.ClosePanel(this);
    }
    public void QuitLevel()
    {
        if(HasClose)
            return;
        UIManager.ShowPanel("FadeInPanel");
        GameManager.INS.UnPauseGame();
        BaseLevelManager.INS.StopBGM();
        StartCoroutine(WaitToBack());
        HasClose = true;
    }

    IEnumerator WaitToBack()
    {
        yield return new  WaitForSecondsRealtime(2f);
        GameManager.INS.QuitLevel();
    }
}
