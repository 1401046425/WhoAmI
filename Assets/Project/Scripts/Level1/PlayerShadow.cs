using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public GameObject Target;
    public float OnGroundaddpos;
    public float OnCrouchaddpos;
    public float Max;
    public float Min;
    private Vector2 Pos;
    Vector3 OrinScale;
    Transform Trans;
    PlayerController2D controller2D;
    SpriteRenderer Shadow_SpriteRenderer;
    bool isCrouch;
    Vector2 vector;
    Vector2  POS;
    Vector2 LandPos;
    Vector2 CrouchPos;
    private void Awake()
    {
        controller2D = Target.GetComponent<PlayerController2D>();
        controller2D.OnCrouchEvent.AddListener(OnPlayerCrouch);
        Trans = transform;
        OrinScale = transform.localScale;
        Shadow_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (!Target.activeInHierarchy)
        {
            Shadow_SpriteRenderer.enabled = false;
        }
    }


    private void OnPlayerCrouch(bool arg0)
    {
        isCrouch = arg0;
    }

    private void FixedUpdate()
    {
        if (!Target.activeInHierarchy)
            Shadow_SpriteRenderer.enabled = false;
        if (Target.activeInHierarchy)
        {
            if (!Shadow_SpriteRenderer.enabled)
                Shadow_SpriteRenderer.enabled = true;

            var hit = Physics2D.Raycast(controller2D.transform.position, Vector2.down, 1000, controller2D.m_WhatIsGround);
            Pos = hit.point;
            //Debug.DrawLine(controller2D.transform.position, Pos);

            if (controller2D.m_Grounded)
            {
                Pos.Set(Pos.x, Pos.y + OnGroundaddpos);
                if (isCrouch)
                {
                    CrouchPos = new Vector2(Pos.x, Pos.y + OnCrouchaddpos);
                    Pos = CrouchPos;
                }

            }
            if (Trans.localScale.x >= Min && Trans.localScale.x <= Max)
            {
                var Value = Trans.localScale - Vector3.one * Time.deltaTime * controller2D.m_Velocity.y / 3;
                if (Value.x >= Min && Value.x <= Max)
                {
                    Trans.localScale = Value;
                }

            }
            Trans.position = Pos;
        }
        
    }

}

