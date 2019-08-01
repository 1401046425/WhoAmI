using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class View_Panel_tuibei : MonoBehaviour, IPointerClickHandler{
    // Start is called before the first frame update
    public TextMeshProUGUI Text_tuibeitu;
    bool Isshow_tuibeitu;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (Isshow_tuibeitu)
        {
            Closetuibeitu();
        }
    }
    void ShowedTuibeitu()
    {
        Isshow_tuibeitu = true;
    }
    void ClosedTuibeitu()
    {
        GameObject.Find("BigBang").GetComponent<BigBang>().StartBigBang();
        gameObject.SetActive(false);
    }
     void Showtuibeitu()
    {
        UIFrameWork.UIManager.FadeInFX(Text_tuibeitu.gameObject, 0.01f, 0.02f, ShowedTuibeitu);
    }
     void Closetuibeitu()
    {
        Isshow_tuibeitu = false;
        UIFrameWork.UIManager.FadeOutFX(Text_tuibeitu.gameObject, 0.01f, 0.02f, ClosedTuibeitu);
    }


    void Start () {
        Showtuibeitu();
    }

    // Update is called once per frame
    void Update () {

    }
}