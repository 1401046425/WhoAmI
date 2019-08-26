using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Lean.Touch;
using UnityEngine;
using UnityEngine.Events;

public class AlienPlane : MonoBehaviour
{
    [SerializeField] private UnityEvent OnGetHit;
   [SerializeField] private LeanTranslate _leanTranslate;
    private bool Control;
   [SerializeField] private Vector2 Radius;
  [SerializeField] private Vector2 Radiuscenter;
  private Transform mytransfrom;
    public bool CanControl {
        get { return Control; }
        set
        {
            Control = value;_leanTranslate.enabled=CanControl;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        mytransfrom = transform;
    }



 private void OnCollisionEnter2D(Collision2D other)
 {
     OnGetHit?.Invoke();
     try
     {
      var Met=other.transform.GetComponent<Meteorite>();
      if(!Met)
          return;
      Met.DestoryMeteorite();
     }
     catch (Exception e)
     {

     }
 }
 

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(CanControl)
            CheckIsOutSide();
    }

    void CheckIsOutSide()
    {
        if (mytransfrom.position.x > Radiuscenter.x + Radius.x)
        {
            mytransfrom.position=new Vector2(mytransfrom.position.x-1,mytransfrom.position.y);
        }

        if (mytransfrom.position.x < Radiuscenter.x - Radius.x)
        {
            mytransfrom.position=new Vector2(mytransfrom.position.x+1,mytransfrom.position.y);
        }
        if (mytransfrom.position.y < Radiuscenter.y - Radius.y)
        {
            mytransfrom.position=new Vector2(mytransfrom.position.x,mytransfrom.position.y+1);
        }
        if (mytransfrom.position.y > Radiuscenter.y + Radius.y)
        {
            mytransfrom.position=new Vector2(mytransfrom.position.x,mytransfrom.position.y-1);
        }
    }
    private void OnDrawGizmos () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube (Radiuscenter, Radius);
    }

    public void Move2Target(Transform Trans)
    {
        StartCoroutine(Move2(Trans));
    }

    IEnumerator Move2(Transform Trans)
    {
        yield return  new  WaitForSecondsRealtime(2f);
        var IsPathing = true;
        while (IsPathing)
        {
            var dir = Trans.position -mytransfrom.position;
            mytransfrom.Translate(dir.normalized*Time.fixedDeltaTime*Speed);
            if (dir.magnitude < 0.25f)
                IsPathing = false;
            yield return new  WaitForFixedUpdate();
        }
    }

    public float Speed;
}
