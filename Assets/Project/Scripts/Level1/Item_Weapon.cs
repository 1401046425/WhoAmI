using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Weapon : Item
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void OnUse()
    {
      var Player = (PlayerController2D)PlayerManager.INS.GetNowActivePlayer();
        Player.m_Animator.SetTrigger("Attack");
        Invoke("CheckTouchSceneObject",1f);
    }

    
    void CheckTouchSceneObject()
    {
      var obj= Physics2D.OverlapCircleAll(transform.position, 5f);
      foreach (var VARIABLE in obj)
      {
          try
          {
            VARIABLE.GetComponent<SceneObject>().OnMutual();
          }
          catch (Exception e)
          {
          }
      }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
