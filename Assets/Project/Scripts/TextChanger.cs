using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TextChanger : MonoBehaviour
{
	[SerializeField] private TextAsset info;
	private string[] Infos;
	private bool isFinish;
	[SerializeField] private UnityEvent OnTextFinish;
	[SerializeField] private TextMeshPro _textMeshPro;
	private int index;
	private Coroutine Update;
	private bool Canclick = true;

	void Awake()
	{
		InitInfo();

	}

	

private void InitInfo()
    {
     Infos=info.text.Split('\n');
    }

    private void OnMouseDown()
    {


        if(!Canclick)
            return;
        if (index < Infos.Length)
        {
            if (_textMeshPro.color.a >= 1)
            {
                Canclick = false;
                if(Update!=null)
                    StopCoroutine(Update);
                Update=  StartCoroutine(UpdateTextFX(_textMeshPro, Infos[index],AddIndex));
            }
        }

    }

    public void AutoPlayText(float WaitTime)
    {

        if(!Canclick)
            return;
        StartCoroutine(AutoPlayTextInfo(Infos,WaitTime));
    }

    IEnumerator AutoPlayTextInfo(string[] info,float WaitTime)
    {
        yield return new WaitForSecondsRealtime(WaitTime);
        foreach (var VARIABLE in info)
        { 
            Update=  StartCoroutine(UpdateTextFX(_textMeshPro, VARIABLE,AddIndex));
            while (_textMeshPro.color.a<1)
            {
                yield return null;
            }
            yield return new WaitForSecondsRealtime(VARIABLE.Length*0.1f); 
        }
    }

    void AddIndex()
    {
        if(isFinish)
            return;
        Canclick = true;
        index++;

        if (index >= Infos.Length)
        {
            OnTextFinish?.Invoke();
            isFinish = true;
        }
    }

    IEnumerator UpdateTextFX(TextMeshPro textmesh,string text,Action CallBack)
    {
        while (textmesh.color.a>0)
        {
            textmesh.color=new Color(textmesh.color.r,textmesh.color.g,textmesh.color.b,textmesh.color.a-0.025f);
            yield return  new WaitForFixedUpdate();
        }

        textmesh.color=new Color(textmesh.color.r,textmesh.color.g,textmesh.color.b,0);
        textmesh.text = text;
        while (textmesh.color.a<1)
        {
            textmesh.color=new Color(textmesh.color.r,textmesh.color.g,textmesh.color.b,textmesh.color.a+0.025f);
            yield return  new WaitForFixedUpdate();
        }
        CallBack?.Invoke();
    }
}
