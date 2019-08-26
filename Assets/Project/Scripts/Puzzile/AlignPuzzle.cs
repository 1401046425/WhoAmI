using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Lean.Touch;
using UnityEngine;
using UnityEngine.Events;

public class AlignPuzzle : MonoBehaviour {
    [SerializeField] private bool UseY;

    [SerializeField] private Vector2 center;

    [SerializeField] private float radius;
    [SerializeField] private Vector2 Radiuscenter;
    [SerializeField] private Vector2 PuzzleRadius;

    [SerializeField]
    private bool AtPoint;

    [SerializeField]
    private bool UseAndroid;

    [SerializeField] private bool NoMouseUP;
    private bool IsFinish;
    private int OriginLayer;
    [SerializeField] private UnityEvent OnPuzzleFinish;
    // Start is called before the first frame update
    Transform myTransform;
    Vector3 selfScenePosition;
    private Vector3 TouchcurrentScenePosition;
    private Vector3 LastPosition;
    private void Start () {
        myTransform = transform;
        //将自身坐标转换为屏幕坐标
        selfScenePosition = Camera.main.WorldToScreenPoint (myTransform.position);
        // print("scenePosition   :  " + selfScenePosition);
        LastPosition = myTransform.position;
        try {
            OriginLayer = GetComponent<SpriteRenderer> ().sortingOrder;
        } catch (Exception e) {

        }
    }

    [ContextMenu ("重制拼图")]
    void ResetPuzzle () {
        transform.position = center;
    }

    [ContextMenu ("设置当前坐标为拼图点")]
    void SetPuzzle () {
        center = transform.position;
    }
    private void OnMouseDrag () {
        if(GameManager.INS.Status==GameStatus.Pause)
            return;
        if (IsFinish)
            return;
        // print(Input.mousePosition.x + "     y  " + Input.mousePosition.y + "     z  " + Input.mousePosition.z);
        //新的屏幕点坐标
        Vector3 currentScenePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, selfScenePosition.z);

        //将屏幕坐标转换为世界坐标
        // Vector3 crrrentWorldPosition = Camera.main.ScreenToWorldPoint(currentScenePosition-TouchcurrentScenePosition); 
        //设置对象位置为鼠标的世界位置
        var pos = currentScenePosition - TouchcurrentScenePosition;
        if (myTransform.position.y > (Radiuscenter + PuzzleRadius).y) {
            return;
        }
        if (myTransform.position.y < (Radiuscenter - PuzzleRadius).y) {
            return;
        }

        if (myTransform.position.x > (Radiuscenter + PuzzleRadius).x) {
            return;
        }
        if (myTransform.position.x < (Radiuscenter - PuzzleRadius).x) {
            return;
        }

        if (pos.sqrMagnitude > 3) {
            if (AtPoint) {
                var raypos= Camera.main.ScreenToWorldPoint (currentScenePosition);
                myTransform.position = new Vector3(raypos.x,raypos.y,myTransform.position.z);
                return;
            }

            if (Application.platform == RuntimePlatform.Android || UseAndroid) {
                if (UseY) {
                    myTransform.position = LastPosition + new Vector3 (pos.x, pos.y, 0) * Time.fixedDeltaTime;
                } else
                    myTransform.position = LastPosition + new Vector3 (pos.x, 0, 0) * Time.fixedDeltaTime;
            } else {
                if (UseY) {
                    myTransform.position = LastPosition + new Vector3 (pos.x + pos.normalized.x * pos.magnitude, pos.y + pos.normalized.y * pos.magnitude, 0) * Time.fixedDeltaTime;
                } else
                    myTransform.position = LastPosition + new Vector3 (pos.x + pos.normalized.x * pos.magnitude, 0, 0) * Time.fixedDeltaTime;
            }
        }

        if (NoMouseUP) {
            CheckIsAlign ();
        }
    }

    private void OnMouseDown () {
        try {
            GetComponent<SpriteRenderer> ().sortingOrder = 100;
        } catch (Exception e) {

        }
        TouchcurrentScenePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, selfScenePosition.z);
    }

    private void CheckIsAlign () {
        if (IsFinish)
            return;
        if (UseY) {
            if (myTransform.position.x > center.x - radius && myTransform.position.x < center.x + radius) {
                if (myTransform.position.y > center.y - radius && myTransform.position.y < center.y + radius) {
                    GetComponent<Collider2D> ().enabled = false;
                    myTransform.position = center;
                    OnPuzzleFinish?.Invoke ();
                    IsFinish = true;
                }
            }
        } else {
            if (myTransform.position.x > center.x - radius && myTransform.position.x < center.x + radius) {
                GetComponent<Collider2D> ().enabled = false;
                myTransform.position = center;
                OnPuzzleFinish?.Invoke ();
                IsFinish = true;
            }
        }
        try {
            GetComponent<SpriteRenderer> ().sortingOrder = OriginLayer;
        } catch (Exception e) {

        }
    }

    private void OnMouseUp () {
        CheckIsAlign ();
        if (myTransform.position.y > (Radiuscenter + PuzzleRadius).y) {
            myTransform.position = new Vector2 (myTransform.position.x, (Radiuscenter + PuzzleRadius).y - 1);
        }
        if (myTransform.position.y < (Radiuscenter - PuzzleRadius).y) {
            myTransform.position = new Vector2 (myTransform.position.x, (Radiuscenter - PuzzleRadius).y + 1);
        }
        if (myTransform.position.x > (Radiuscenter + PuzzleRadius).x) {
            myTransform.position = new Vector2 ((Radiuscenter + PuzzleRadius).x - 1, myTransform.position.y);
        }
        if (myTransform.position.x < (Radiuscenter - PuzzleRadius).x) {
            myTransform.position = new Vector2 ((Radiuscenter - PuzzleRadius).x + 1, myTransform.position.y);
        }

        LastPosition = myTransform.position;
        try {
            GetComponent<SpriteRenderer> ().sortingOrder = OriginLayer;
        } catch (Exception e) {

        }
    }

    private void Update () {

    }

    private void OnDrawGizmos () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (center, radius);
        Gizmos.DrawWireCube (Radiuscenter, PuzzleRadius);
    }
}