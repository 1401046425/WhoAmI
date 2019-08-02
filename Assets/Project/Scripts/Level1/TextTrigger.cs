using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTrigger : MonoBehaviour
{
    [SerializeField]private bool AutoFadeOut;
    [SerializeField] private bool Repeat;
    TextMeshPro Textmesh;
    bool ISTriggered;
    private void Awake()
    {
        Textmesh = GetComponent<TextMeshPro>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Textmesh.color = new Color(Textmesh.color.r,
        Textmesh.color.g,
        Textmesh.color.b,
        0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Repeat)
        {
            if (ISTriggered)
                return;
        }
        StopAllCoroutines();
        StartCoroutine(FadeText(1, Textmesh));
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (AutoFadeOut)
        {
            StopAllCoroutines();
            StartCoroutine(FadeText(0, Textmesh));
        }
    }
    IEnumerator FadeText(int updatecode,TextMeshPro Renderer)
    {
        if (!Renderer)
            yield return null;

        switch (updatecode)
        {
            case 0:
                while (Renderer.color.a > 0)
                {
                    Renderer.color = new Color(
                        Renderer.color.r,
                        Renderer.color.g,
                        Renderer.color.b,
                        Renderer.color.a - Time.fixedDeltaTime);
                    yield return new WaitForFixedUpdate();
                }
                break;
            case 1:
                while (Renderer.color.a < 1)
                {
                    Renderer.color = new Color(
                        Renderer.color.r,
                        Renderer.color.g,
                        Renderer.color.b,
                        Renderer.color.a + Time.fixedDeltaTime);
                    yield return new WaitForFixedUpdate();
                }
                break;
        }


    }
}
