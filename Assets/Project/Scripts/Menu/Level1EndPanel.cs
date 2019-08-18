using System;
using System.Collections;
using System.Collections.Generic;
using UIFrameWork;
using UIFrameWork.BasePanel;
using UnityEngine;

public class Level1EndPanel : BaseUIPanel
{
    private bool IsClose;
   [SerializeField] private BookPro book;
    internal override void OnShow()
    {
        UIManager.FadeInFX(this.gameObject,0.01f,0.02f);
    }

    internal override void OnClose()
    {

    }

    public override void Close()
    {
book.gameObject.SetActive(false);
    }


}
