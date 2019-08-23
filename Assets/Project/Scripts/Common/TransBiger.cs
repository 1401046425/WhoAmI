using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TransBiger : MonoBehaviour
{
    private Transform MyTrans;
    [SerializeField]private float Speed;
    [SerializeField] private float limitSize;
    [SerializeField] private UnityEvent OnTransFinish;
    void Awake()
    {
        MyTrans = GetComponent<Transform>();
    }

    public void ToBiger()
    {
        StartCoroutine(ToBIG(Speed));
    }

    IEnumerator ToBIG(float _Speed)
    {
        while (MyTrans.localScale.x < limitSize && MyTrans.localScale.y < limitSize)
        {            MyTrans.localScale +=Vector3.one*Time.fixedDeltaTime*_Speed;
            yield return new WaitForFixedUpdate();
        }
        OnTransFinish?.Invoke();

    }

}
