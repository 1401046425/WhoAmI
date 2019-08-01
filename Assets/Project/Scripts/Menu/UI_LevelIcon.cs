using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
public class UI_LevelIcon : MonoBehaviour,IPointerClickHandler
{
    public string Leve_name;
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.INS.StartLevel(Leve_name);
    }

}
