using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour
{
   [SerializeField] private Sprite FadeInSprite;
   [SerializeField] private float Speed;
   private SpriteRenderer Renderer;

   void Awake()
   {
      Renderer = GetComponent<SpriteRenderer>();
   }

   public void ToFadeIn()
   {
      SpriteFX._S.FadeSprite(FadeInSprite,Renderer,Speed);
   }
}