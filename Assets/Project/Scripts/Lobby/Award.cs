using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Award : MonoBehaviour
{
    [SerializeField] public string LevelName;

   [HideInInspector] public Vector3 OrinPos;

    private void Awake()
    {
        OrinPos = transform.position;
        if (String.IsNullOrWhiteSpace(LevelName))
        {
            Debug.LogError("勋章的关卡名为空，请赋予名称");
            return;
        }
    }
}
