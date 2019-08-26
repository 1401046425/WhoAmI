using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class MeteoriteGroup : MonoBehaviour
{

    private Camera m_Camera;

    private Vector3 selfScenePosition;

   [SerializeField] private Transform Target;

    private const int MaxNumber=8;
    [SerializeField] private GameObject Meteorite;
    [SerializeField] private float Speed;
    private int HasNumber;
    public bool CanHit {get; set;}
    public bool CanTouch { get; set; }
    [SerializeField] private UnityEvent OnMeteoriteHit;
  [HideInInspector]  public List<Meteorite> Meteorites = new List<Meteorite>();
    void Start()
    {
        m_Camera=Camera.main;
        selfScenePosition = m_Camera.WorldToScreenPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(!CanTouch)
            return;
        if(HasNumber>=MaxNumber)
            return;
        Vector3 currentScenePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, selfScenePosition.z);
    var InputPos= m_Camera.ScreenToWorldPoint(currentScenePosition);

    var meteorite= Instantiate(Meteorite,InputPos,quaternion.identity).GetComponent<Meteorite>();
        meteorite.InitMeteor(this,Speed,Target);
        HasNumber++;
        meteorite.transform.SetParent(transform);
        meteorite.transform.name = "meteorite" + HasNumber;
    }

    public void CheckIsHit(Vector3 Pos)
    {
        HasNumber--;
        if(!CanHit)
            return;
        OnMeteoriteHit?.Invoke();
        
    }

    private void OnDisable()
    {

    }

    public void DestoryAllMeteorite()
    {
        foreach (var VARIABLE in Meteorites)
        {
            VARIABLE.DestoryMeteorite();
        }

        CanTouch = false;
    }
}
