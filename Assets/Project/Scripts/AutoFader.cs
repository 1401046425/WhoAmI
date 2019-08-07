using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFader : MonoBehaviour
{
    [SerializeField] private List<Sprite> m_Sprites;
    [SerializeField] private float FadeSpeed;
    [SerializeField] private float WaitTime;
    [SerializeField] private SpriteRenderer Renderer1;
    [SerializeField] private SpriteRenderer Renderer2;
    private int index;
    public void Fade()
    {
        StartCoroutine(Fade(WaitTime));
    }

    public void CloseFade()
    {
        StopAllCoroutines();
    }

    IEnumerator Fade(float time)
    {

        Renderer2.sprite = Renderer1.sprite;
        Renderer1.sprite = null;
        SpriteFX._S.FadeinSprite(m_Sprites[index],Renderer1,FadeSpeed);
        SpriteFX._S.FadeoutSprite(null,Renderer2,FadeSpeed);
        index++;
        if (index >= m_Sprites.Count)
        {
            index = 0;
        }
        yield return new WaitForSecondsRealtime(time);
        StartCoroutine(Fade(time));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
