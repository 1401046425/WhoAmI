using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hider : MonoBehaviour
{
    [SerializeField] private GameObject Obj;
    [SerializeField] private float WaitHideTime;
    private Coroutine hide_coroutine;
    private IEnumerator AutoHide()
    {
        yield return new  WaitForSecondsRealtime(WaitHideTime);
        Obj.SetActive(false);
    }

    public void ToHideObj()
    {
        Obj.SetActive(true);
        if(hide_coroutine!=null)
            StopCoroutine(hide_coroutine);
       hide_coroutine= StartCoroutine(AutoHide());
    }

    // Start is called before the first frame update
    void Start()
    {
        Obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
