using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Flip : MonoBehaviour
{
    public string FlipName;

    [SerializeField] private float ShowTime;

    [SerializeField] private SpriteRenderer ShowSprite;
    //private Color OriginColor;

    private Fader SpriteFX;
    private Fader ThisSpriteFX;

    private bool IsShow;

    private bool IsFinish;
    // Start is called before the first frame update
    private void Awake()
    {
        if (ShowSprite != null)
        {
          //OriginColor=  ShowSprite.color;
            SpriteFX= ShowSprite.GetComponent<Fader>();
            if(!SpriteFX)
                SpriteFX= ShowSprite.gameObject.AddComponent<Fader>();
            ThisSpriteFX= GetComponent<Fader>();
            if(!ThisSpriteFX)
                ThisSpriteFX= gameObject.AddComponent<Fader>();

            SpriteFX.OnFadeInFinishAction += OnShow;
            SpriteFX.OnFadeOutFinishAction += OnClose;
            SpriteFX.AutoHide = true;
            ThisSpriteFX.OnFadeOutFinishAction += OnThisClose; 
            ShowSprite.color=new Color(ShowSprite.color.r,ShowSprite.color.g,ShowSprite.color.b,0);
        }


    }

    private void OnThisClose()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFinish()
    {
        IsFinish = true;
        if(WaitCloseSprite!=null)
            StopCoroutine(WaitCloseSprite);
        SpriteFX.FadeOutAction();
        ThisSpriteFX.FadeOutAction();
    }



    void OnShow()
    {
        Master.AddCheckFlipName(this);
        WaitCloseSprite=StartCoroutine(ShowFlip());
    }

    private Coroutine WaitCloseSprite;
    void OnClose()
    {
        IsShow = false;
    }

    IEnumerator ShowFlip()
    {
        yield return new WaitForSecondsRealtime(ShowTime);
        SpriteFX.FadeOutAction();
    }


    private void OnMouseDown()
    {
        if(IsFinish)
            return;
        if(IsShow)
            return;
        SpriteFX.FadeInAction();
        IsShow = true;
    }

    [HideInInspector]public FlipGroup Master { get; set; }
}
