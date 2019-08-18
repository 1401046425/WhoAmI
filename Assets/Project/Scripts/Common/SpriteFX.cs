
    using System;
    using System.Collections;
    using UnityEngine;

    public class SpriteFX:MonoBehaviour
    {
        private static SpriteFX INS;
        internal static SpriteFX _S
        {
            get
            {
                if (INS == null)
                {
                    INS = new GameObject().AddComponent<SpriteFX>();
                    INS.name = "SpriteFX";
                    DontDestroyOnLoad(INS.gameObject);
                }
                return INS;
            }

        }
        public void FadeSprite(Sprite Sprite,SpriteRenderer Renderer,float Speed)
        {
            StartCoroutine(FadeInOutSprite(Sprite,Renderer,Speed));
        }
        public void FadeinSprite(Sprite Sprite,SpriteRenderer Renderer,float Speed)
        {
            StartCoroutine(FadeInSprite(Sprite,Renderer,Speed));
        }
        public void FadeoutSprite(Sprite Sprite,SpriteRenderer Renderer,float Speed)
        {
            StartCoroutine(FadeOutSprite(Sprite,Renderer,Speed));
        }
        public void FadeinSprite(Sprite Sprite,SpriteRenderer Renderer,float Speed,Action callback)
        {
            StartCoroutine(FadeInSprite(Sprite,Renderer,Speed,callback));
        }
        public void FadeoutSprite(Sprite Sprite,SpriteRenderer Renderer,float Speed,Action callback)
        {
            StartCoroutine(FadeOutSprite(Sprite,Renderer,Speed,callback));
        }
        IEnumerator FadeInOutSprite(Sprite Sprite,SpriteRenderer Renderer,float Speed)
        {
            if (!Renderer)
                yield return null;
            if (Sprite)
                yield return null;
            Renderer.color = new Color(Renderer.color.r,
                Renderer.color.g,
                Renderer.color.b,
                1);
            while (Renderer.color.a > 0)
            {
                Renderer.color = new Color(
                    Renderer.color.r,
                    Renderer.color.g,
                    Renderer.color.b,
                    Renderer.color.a - Time.fixedDeltaTime*Speed);
                yield return new WaitForFixedUpdate();
            }
            Renderer.sprite = Sprite;
            while (Renderer.color.a < 1)
            {
                Renderer.color = new Color(
                    Renderer.color.r,
                    Renderer.color.g,
                    Renderer.color.b,
                    Renderer.color.a + Time.fixedDeltaTime*Speed);
                yield return new WaitForFixedUpdate();
            }
        }
        IEnumerator FadeInSprite(Sprite Sprite,SpriteRenderer Renderer,float Speed)
        {
            if (!Renderer)
                yield return null;
            if (Sprite)
                yield return null;
            Renderer.color = new Color(Renderer.color.r,
                Renderer.color.g,
                Renderer.color.b,
                0);
            Renderer.sprite = Sprite;
            while (Renderer.color.a < 1)
            {
                Renderer.color = new Color(
                    Renderer.color.r,
                    Renderer.color.g,
                    Renderer.color.b,
                    Renderer.color.a + Time.fixedDeltaTime*Speed);
                yield return new WaitForFixedUpdate();
            }
        }
        IEnumerator FadeOutSprite(Sprite Sprite,SpriteRenderer Renderer,float Speed)
        {
            if (!Renderer)
                yield return null;
            if (Sprite)
                yield return null;
            Renderer.color = new Color(Renderer.color.r,
                Renderer.color.g,
                Renderer.color.b,
                1);
            while (Renderer.color.a > 0)
            {
                Renderer.color = new Color(
                    Renderer.color.r,
                    Renderer.color.g,
                    Renderer.color.b,
                    Renderer.color.a - Time.fixedDeltaTime*Speed);
                yield return new WaitForFixedUpdate();
            }
            Renderer.sprite = Sprite;
        }
        IEnumerator FadeInSprite(Sprite Sprite,SpriteRenderer Renderer,float Speed,Action CallBack)
        {
            if (!Renderer)
                yield return null;
            if (Sprite)
                yield return null;
            Renderer.color = new Color(Renderer.color.r,
                Renderer.color.g,
                Renderer.color.b,
                0);
            Renderer.sprite = Sprite;
            while (Renderer.color.a < 1)
            {
                Renderer.color = new Color(
                    Renderer.color.r,
                    Renderer.color.g,
                    Renderer.color.b,
                    Renderer.color.a + Time.fixedDeltaTime*Speed);
                yield return new WaitForFixedUpdate();
            }
            CallBack?.Invoke();
        }
        IEnumerator FadeOutSprite(Sprite Sprite,SpriteRenderer Renderer,float Speed,Action CallBack)
        {
            if (!Renderer)
                yield return null;
            if (Sprite)
                yield return null;
            Renderer.color = new Color(Renderer.color.r,
                Renderer.color.g,
                Renderer.color.b,
                1);
            while (Renderer.color.a > 0)
            {
                Renderer.color = new Color(
                    Renderer.color.r,
                    Renderer.color.g,
                    Renderer.color.b,
                    Renderer.color.a - Time.fixedDeltaTime*Speed);
                yield return new WaitForFixedUpdate();
            }
            Renderer.sprite = Sprite;
            CallBack?.Invoke();
        }
        
    }
    
