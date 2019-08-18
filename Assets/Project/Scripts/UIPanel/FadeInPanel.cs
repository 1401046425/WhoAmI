using System.Collections;
using System.Collections.Generic;
using UIFrameWork;
using UIFrameWork.BasePanel;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeInPanel : BaseUIPanel
{
    internal override void OnShow()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        UIManager.FadeInFX(this.gameObject,0.01f,0.02f);
    }
}
