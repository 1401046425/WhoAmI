using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class WordText : MonoBehaviour
{
    private TextMeshPro TextMesh;
    [SerializeField] private PassWord Master;
    public uint Password;
    public int Bit;
    [SerializeField] private Color ShowColor;
    private Color OriginColor;
    private Coroutine UpdatePassword;
    private bool Finish;
    public bool IsFinish
    {
        get { return Finish; }
        set
        {
            Finish = value;
            if(Finish)
             StopUpdate();
        }
    }

    private void Awake()
    {

        try
        {
            Master.transform.parent.GetComponent<PassWord>();
        }
        catch (Exception e)
        {
 
        }
        TextMesh=GetComponent<TextMeshPro>();
        if (Master != null)
        {
            Master.startAction += StartUpdate;
            Master.ShowPasswordAction += ShowTruthPassword;
            Master.OnPasswordTruth += PasswordFinish;
        }


        OriginColor = TextMesh.color;
    }

    private void PasswordFinish()
    {
        if(IsFinish)
            TextMesh.color = ShowColor;
    }

    private void ShowTruthPassword()
    {
        if (int.Parse(Master.TargetPassword[Bit].ToString()) == (int) Password)
            TextMesh.color = ShowColor;
    }

    private void OnMouseDown()
    {
CheckPassWord();
    }

    public void StartUpdate()
    {
        UpdatePassword = StartCoroutine(UpdatePassWordText());
    }

    public void StopUpdate()
    {
        if(UpdatePassword!=null)
            StopCoroutine(UpdatePassword);
    }

    IEnumerator UpdatePassWordText()
    {
        while (true)
        {
            yield return new  WaitForSecondsRealtime(Random.Range(0.25f,1f));
            Password++;
            if (Password > 9)
                Password = 0;
            TextMesh.text = Password.ToString();
            if(TextMesh.color!=OriginColor)
                  TextMesh.color = OriginColor;
        }
    }

    void CheckPassWord()
    {
        if(!Master)
            return;
        if(IsFinish)
            return;
        Master.CheckPassWord(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        Password =(uint)Random.Range(0, 9);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
