using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Mugun : Item
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void OnUse()
    {
      var Player = (PlayerController2D)PlayerManager.INS.GetNowActivePlayer();
        Player.m_Animator.SetTrigger("Attack");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
