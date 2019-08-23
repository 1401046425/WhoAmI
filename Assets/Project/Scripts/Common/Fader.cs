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

 public void FadeIn()
 {
  SpriteFX._S.FadeinSprite(SpRender.sprite,SpRender,1f,OnFinish);
 }

 public void FadeOut()
 {
  SpriteFX._S.FadeoutSprite(SpRender.sprite,SpRender,1f,OnFinish);
 }
 public void FadeInAction()
 {
  SpriteFX._S.FadeinSprite(SpRender.sprite,SpRender,1f,OnFadeInFinishAction);
 }

 public void FadeOutAction()
 {
  SpriteFX._S.FadeoutSprite(SpRender.sprite,SpRender,1f,OnFadeOutFinishAction);
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
 }

 private void OnFinish()
 {
  OnFadeFinish?.Invoke();
  OnFadeFinishAction?.Invoke();
 }
}
