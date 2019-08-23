using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

[RequireComponent (typeof (SpriteRenderer))]
[RequireComponent (typeof (CircleCollider2D))]
public class InfinityObj : MonoBehaviour {
    Vector3 selfScenePosition;
    private Transform myTransform;
    public bool isTouch;
    void Awake () {
        myTransform = transform;
        selfScenePosition = Camera.main.WorldToScreenPoint (myTransform.position);
    }
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable () {
        isTouch = true;
    }
    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable () {
        isTouch = false;
    }
    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        isTouch = false;
    }
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void FollowMouse () {

    }
    private void FixedUpdate () {
        if (isTouch) {

            Vector3 currentScenePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, selfScenePosition.z);
            //将屏幕坐标转换为世界坐标                                                                                                 
            Vector3 crrrentWorldPosition = Camera.main.ScreenToWorldPoint (currentScenePosition);
            myTransform.position = crrrentWorldPosition;
        }

    }
}