using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;


public class Meteorite : MonoBehaviour
{
    public Transform target;
    public Vector2 Pos;
    private MeteoriteGroup Master;
    private MeteoriteSpawn Spawn;

    private Transform myTrans;
    //public Transform Fx;
    // Start is called before the first frame update
    private void Awake()
    {
        myTrans = transform;
    }

    void Start()
    {

        var Redius = Random.Range(-0.1f, 0.1f);
        transform.localScale=new Vector3(transform.localScale.x+Redius,transform.localScale.y+Redius,transform.localScale.z+Redius);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Master)
        {
            Move2TarGet();
        }
        else
        {
            if(Spawn)
                Move2Pos();
        }

    }

    public void InitMeteor(MeteoriteGroup _master,float _Speed,Transform _target )
    {

        myTrans = transform;
        Vector3 dir=_target.position-myTrans.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
        myTrans.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        target = _target;
        this.Speed = _Speed;
        Master = _master;
        if(!Master.Meteorites.Contains(this))
            Master.Meteorites.Add(this);
    }
    public void InitMeteor(MeteoriteSpawn _spawn,float _Speed,Vector2 pos )
    {
        myTrans = transform;
        Vector2 dir=pos-(Vector2)myTrans.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
        myTrans.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Pos = pos;
        this.Speed = _Speed;
        Spawn = _spawn;
        

    }
    public void Move2TarGet()
    {
        if(target==null)
            return;
        var dir = target.position - myTrans.position;
        myTrans.position = myTrans.position + dir.normalized * Time.fixedDeltaTime * Speed;
        if(myTrans.localScale.z>0.05f)
            myTrans.localScale-=Vector3.one*Time.fixedDeltaTime*0.05f;
       // transform.Translate(dir.normalized*Time.fixedDeltaTime*Speed);
        if (dir.magnitude < 3)
        {
            if(Master)
                 Master.CheckIsHit(myTrans.position);
            DestoryMeteorite();

        }
    }

    private void DestoryThis()
    {
        if (Master)
        {
            if(Master.Meteorites.Contains(this))
                Master.Meteorites.Remove(this);
        }

        if (Spawn)
        {
            if(Spawn.Meteorites.Contains(this))
                Spawn.Meteorites.Remove(this);
        }

        Destroy(gameObject);
    }

    private bool IsDestorying;
    public void DestoryMeteorite()
    {
        if(IsDestorying)
            return;
        IsDestorying = true;
        try
        {
            var fader= GetComponent<Fader>();
            fader.OnFadeOutFinishAction += DestoryThis;
            fader.FadeOutAction();
        }
        catch (Exception e)
        {
            DestoryThis();
        }
    }

    public void Move2Pos()
    {
        var dir = Pos - (Vector2)myTrans.position;
        myTrans.position = (Vector2)myTrans.position + dir.normalized * Time.fixedDeltaTime * Speed;

        if (dir.magnitude < 3)
        {
            if (Spawn)
            {
                Spawn.CheckIsHit(myTrans.position);
                Spawn.Meteorites.Remove(this);
            }
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        IsDestorying = true;
    }

    public float Speed;
}
