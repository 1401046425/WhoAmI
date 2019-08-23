using System.Collections;
using System.Collections.Generic;
using UIFrameWork;
using UIFrameWork.BasePanel;
using UnityEngine;

public class EndPanel : BaseUIPanel
{
    internal override void OnShow()
    {
        UIManager.FadeInFX(this.gameObject,0.01f,0.02f);
    }
}
