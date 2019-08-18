using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Lean.Touch;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.PlayerLoop;

public class CheckTransForm : MonoBehaviour
{[SerializeField]
    private Vector3 Target_Position;
    [SerializeField]private float PosRange;
    [SerializeField]
    private Vector3 Target_Rotation;
    [SerializeField]private float RotRange;
    [SerializeField]
    private Vector3 Target_Scale;
    [SerializeField]private float ScaRange;


    private Transform MyTrans;
[SerializeField]
    private UnityEvent OnPositionFinish;
    [SerializeField]
    private UnityEvent OnRotationFinish;
    [SerializeField]
    private UnityEvent OnScaleFinish;
    private bool PosFinish;
    private bool RotateFinish;
    private bool ScaleFinish;
    // Start is called before the first frame update
    void Awake()
    {
        MyTrans = transform;
    }

    private void CheckPos()
    {
        if (!PosFinish)
        {
            if (MyTrans.position.x < Target_Position.x + PosRange && MyTrans.position.x > Target_Position.x - PosRange)
            {
                if (MyTrans.position.y < Target_Position.y + PosRange && MyTrans.position.y > Target_Position.y - PosRange)
                {
                    if (MyTrans.position.z < Target_Position.z + PosRange && MyTrans.position.z > Target_Position.z - PosRange)
                    {
                        OnPositionFinish?.Invoke();
                        PosFinish = true;
                    }
  
                }
            }   
        }
    }
    private void CheckRot()
    {
        if (!RotateFinish)
        {
            if (MyTrans.rotation.x < Target_Rotation.x + RotRange && MyTrans.rotation.x > Target_Rotation.x - RotRange)
            {
                if (MyTrans.rotation.y < Target_Rotation.y + RotRange && MyTrans.rotation.y > Target_Rotation.y - RotRange)
                {
                    if (MyTrans.rotation.z < Target_Rotation.z + RotRange && MyTrans.rotation.z > Target_Rotation.z - RotRange)
                    {
                        OnRotationFinish?.Invoke();
                        RotateFinish = true;
                    }
  
                }
            }   
        }
    }
    private void CheckSca()
    {
        Debug.Log(MyTrans.localScale);
        
        if (MyTrans.localScale.x < Target_Scale.x + ScaRange && MyTrans.localScale.x > Target_Scale.x - ScaRange)
            if (MyTrans.localScale.y < Target_Scale.y + ScaRange && MyTrans.localScale.y > Target_Scale.y - ScaRange)
                if (MyTrans.localScale.z < Target_Scale.z + ScaRange && MyTrans.localScale.z > Target_Scale.z - ScaRange)
                    Debug.Log(1111);
    }
    void FixedUpdate()
    {
        CheckPos();
        CheckRot();
        CheckSca();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
