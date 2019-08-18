using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fader : MonoBehaviour
{
 private SpriteRenderer SpRender;

 [SerializeField]
 private UnityEvent OnFadeFinish;

 [SerializeField] private bool AutoHide;

 public void FadeIn()
 {
  SpriteFX._S.FadeinSprite(SpRender.sprite,SpRender,1f,OnFinish);
 }

 public void FadeOut()
 {
  SpriteFX._S.FadeoutSprite(SpRender.sprite,SpRender,1f,OnFinish);
 }

 void Awake()
 {
  SpRender = GetComponent<SpriteRenderer>();
  if (AutoHide)
  {
   SpRender.color= new Color(SpRender.color.r,SpRender.color.g,SpRender.color.b,0);
  }
 }

 private void OnFinish()
 {
  OnFadeFinish?.Invoke();
 }
}
