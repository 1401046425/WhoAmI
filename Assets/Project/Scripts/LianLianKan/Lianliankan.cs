using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Lianliankan : MonoBehaviour
{ 
    [SerializeField] public string LianliankanName;
    [SerializeField] private Color OnAddColor;
    Color OriginColor;
    [HideInInspector] public LianliankanGroup Master;
  [HideInInspector] public bool IsFinish;

  private SpriteRenderer Render;

  private void Awake()
  {
      Render = GetComponent<SpriteRenderer>();
      OriginColor = Render.color;
  }

  // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFinish()
    {
        IsFinish = true;
        StartCoroutine(WaitDestory());

    }

    IEnumerator WaitDestory()
    {
        yield return new WaitForSecondsRealtime(0.15f);
       var fade= gameObject.AddComponent<Fader>();
       fade.OnFadeFinishAction += DestoryLianLianKan;
       fade.FadeOut();

    }

    public void DestoryLianLianKan()
    {
        gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if(IsFinish)
            return;
        Master.AddCheckLianliankan(this);
    }

    public void OnReset()
    {
        Render.color=OriginColor;
    }

    public void OnAdd()
    {

        Render.color=OnAddColor;

    }
}
