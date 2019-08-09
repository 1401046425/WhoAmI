using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class FadeSpriteObj : MonoBehaviour
{
  [SerializeField] private List<Sprite> m_Sprites;
  [SerializeField] private float FadeSpeed;
  [SerializeField] private SpriteRenderer Renderer1;
  [SerializeField] private SpriteRenderer Renderer2;
    private int index = 0;
    [SerializeField] private UnityEvent OnFadeFinish;
    private void OnMouseDown()
  {
    if (index < m_Sprites.Count)
    {
      if(Renderer1.color.a<1)
        return;
        Renderer2.sprite = Renderer1.sprite;
      Renderer1.sprite = null;
      SpriteFX._S.FadeinSprite(m_Sprites[index],Renderer1,FadeSpeed);
     SpriteFX._S.FadeoutSprite(null,Renderer2,FadeSpeed);
      index++;
      if (index >= m_Sprites.Count)
      {
        OnFadeFinish?.Invoke();
        
      }
    }

  }
}
