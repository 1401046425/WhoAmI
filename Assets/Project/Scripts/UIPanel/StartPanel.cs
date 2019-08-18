using System;
using System.Collections;
using System.Collections.Generic;
using UIFrameWork;
using UIFrameWork.BasePanel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StartPanel : BaseUIPanel,IPointerClickHandler
{
    [SerializeField] private GameObject Text;



    private bool isClick;
    private void OnEnable()
    {
        UIManager.FadeInFX(Text,0.01f,0.005f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
     if(isClick)
         return;
     UIManager.FadeOutFX(gameObject,0.01f,0.005f,Close);
StoryBlockManager.INS.PlayNextBlock();
     isClick = true;
    }
}
