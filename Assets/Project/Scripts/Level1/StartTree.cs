using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTree:MonoBehaviour
{
   public Sprite[] Tree_Sprites;
    SpriteRenderer Tree_spriteRenderer;
    int treespritenumber;
    Coroutine UpdateTree;
    private void Awake()
    {
        try
        {
        Tree_spriteRenderer = GetComponent<SpriteRenderer>();

        }
        catch
        {
            Tree_spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        GrowUpTree(treespritenumber);
    }
    private void GrowUpTree(int TreeNumber)
    {
        if (treespritenumber >= Tree_Sprites.Length)
            return;
        if (Tree_spriteRenderer.color.a >= 1)
        {
            treespritenumber++;
            if (UpdateTree!=null)
            {
                StopCoroutine(UpdateTree);
            }

            UpdateTree = StartCoroutine(UpdateSprite(Tree_Sprites[TreeNumber], Tree_spriteRenderer));
        }

    }
    IEnumerator UpdateSprite(Sprite Sprite,SpriteRenderer Renderer)
    {
        if (!Renderer)
            yield return null;
        if (Sprite)
            yield return null;
        Tree_spriteRenderer.color = new Color(Tree_spriteRenderer.color.r,
                Tree_spriteRenderer.color.g,
                Tree_spriteRenderer.color.b,
                1);
        while (Tree_spriteRenderer.color.a > 0)
        {
            Tree_spriteRenderer.color = new Color(
                Tree_spriteRenderer.color.r,
                Tree_spriteRenderer.color.g,
                Tree_spriteRenderer.color.b,
                Tree_spriteRenderer.color.a - Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        Renderer.sprite = Sprite;
        while (Tree_spriteRenderer.color.a < 1)
        {
            Tree_spriteRenderer.color = new Color(
                Tree_spriteRenderer.color.r,
                Tree_spriteRenderer.color.g,
                Tree_spriteRenderer.color.b,
                Tree_spriteRenderer.color.a + Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    public void GrowUP()
    {
        
        GrowUpTree(treespritenumber);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
