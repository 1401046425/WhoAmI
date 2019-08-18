using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class RotatePuzzle : MonoBehaviour
{
    private Vector3 selfScenePosition;

    private Transform mytransform;

    [SerializeField]private float TargetAngle;
    [SerializeField]private float range;
    [SerializeField] private bool DontMouseUp;
    private float angle;

    [SerializeField] private UnityEvent OnPuzzleFinish;

    private bool isFinish;

    private const float SemiValue = 180;
    // Start is called before the first frame update
    void Start()
    {
        mytransform = transform;
        selfScenePosition = Camera.main.WorldToScreenPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        if(isFinish)
            return;
        Vector3 currentScenePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, selfScenePosition.z);
        //将屏幕坐标转换为世界坐标
         Vector3 crrrentWorldPosition = Camera.main.ScreenToWorldPoint(currentScenePosition);
         var dir = crrrentWorldPosition - mytransform.position;
         angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
         mytransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
         if(DontMouseUp)
             CheckIsFinish();
    }

    private void CheckIsFinish()
    {
        if(isFinish)
            return;

            if (angle > TargetAngle - range && angle< TargetAngle + range)
            {
                try
                {
                    GetComponent<Collider2D>().enabled = false;
                }
                catch (Exception e)
                {

                }
                mytransform.rotation=Quaternion.AngleAxis(TargetAngle, Vector3.forward);
                OnPuzzleFinish?.Invoke();
                isFinish = true;
            }
            
    }

    private void OnMouseUp()
    {
        CheckIsFinish();
    }

}
