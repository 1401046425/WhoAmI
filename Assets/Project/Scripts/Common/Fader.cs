using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fader : MonoBehaviour
{
 private SpriteRenderer SpRender;

 [SerializeField]
 private UnityEvent OnFadeFinish;

 public Action OnFadeFinishAction;
 
 public Action OnFadeInFinishAction;
 public Action OnFadeOutFinishAction;
 [SerializeField] public bool AutoHide;

 [HideInInspector] public bool IsShow;
 [HideInInspector] public bool IsInvoking;
 public void FadeIn()
 {
  if(IsInvoking)
   return;
  SpriteFX._S.FadeinSprite(SpRender.sprite,SpRender,1f,OnFinish);
  IsInvoking = true;
 }

 public void FadeOut()
 {
  if(IsInvoking)
   return;
  SpriteFX._S.FadeoutSprite(SpRender.sprite,SpRender,1f,OnFinish);
  IsInvoking = true;
 }
 public void FadeInAction()
 {
  if(IsInvoking)
   return;
  SpriteFX._S.FadeinSprite(SpRender.sprite,SpRender,1f,OnFadeInFinishAction);
  IsInvoking = true;
 }

 public void FadeOutAction()
 {
  if(IsInvoking)
   return;
  SpriteFX._S.FadeoutSprite(SpRender.sprite,SpRender,1f,OnFadeOutFinishAction);
  IsInvoking = true;
 }
 void Awake()
 {
  SpRender = GetComponent<SpriteRenderer>();
 }

 private void Start()
 {
  if (AutoHide)
  {
   SpRender.color= new Color(SpRender.color.r,SpRender.color.g,SpRender.color.b,0);
  }

  OnFadeFinishAction += OnFadeInFinish;
  OnFadeOutFinishAction += OnFadeOutFinish;
 }

 private void OnFadeOutFinish()
 {
    IsInvoking = false;
    IsShow = false;
 }

 private void OnFadeInFinish()
 {
  IsInvoking = false;
  IsShow = true;
 }

 private void OnFinish()
 {
  OnFadeFinish?.Invoke();
  OnFadeFinishAction?.Invoke();
  IsInvoking = false;
  if (SpRender.color.a >= 1)
   IsShow = true;
  else
  {
   IsShow = false;
  }
 }
}
