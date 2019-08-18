using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class InfinityObjMain : MonoBehaviour {
    private InfinityObj m_Obj;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
    /// <summary>
    /// OnMouseUp is called when the user has released the mouse button.
    /// </summary>
    void OnMouseUp () {
        if (!m_Obj)
            return;
        m_Obj.isTouch = false;
       var colliders= Physics2D.OverlapCircleAll (m_Obj.transform.position, 2f);
foreach (var _item in colliders)
{
 try
 {
                _item.GetComponent<MutualObj>().Mutualed();
            }
 catch (Exception e)
 {

 }   
}
        GameObject.Destroy (m_Obj.gameObject);

    }
    void OnMouseDown () {
        if (m_Obj) //如果已经抓取了对象
            return; //则返回
        m_Obj = new GameObject ().AddComponent<InfinityObj> (); //创建一个对象
        m_Obj.transform.name = "InfinityObj";
        m_Obj.transform.localScale = transform.localScale;
        m_Obj.isTouch = true;
        try {
            var m_SpriteRenderer = m_Obj.GetComponent<SpriteRenderer> ();
            m_SpriteRenderer.sprite = GetComponent<SpriteRenderer> ().sprite;
        } catch (Exception e) { }
    }
}