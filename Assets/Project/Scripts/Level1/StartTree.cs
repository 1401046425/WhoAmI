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
            if (treespritenumber == Tree_Sprites.Length)
            {
                Level1Manager.INS.PlayTimeLine(Level1Manager.INS.PLABDirectiors[0]);
            }
            if (UpdateTree!=null)
            {
                StopCoroutine(UpdateTree);
            }

           // UpdateTree = StartCoroutine(UpdateSprite(Tree_Sprites[TreeNumber], Tree_spriteRenderer));
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
