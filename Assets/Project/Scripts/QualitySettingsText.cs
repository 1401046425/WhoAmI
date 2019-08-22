using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QualitySettingsText : MonoBehaviour
{
    private int index=2;

    private TextMeshPro TextMesh;

    private void Awake()
    {
        TextMesh = GetComponent<TextMeshPro>();
        if (ES3.KeyExists("QualityIndex"))
             index= ES3.Load<int>("QualityIndex"); 
        SetQuality();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnMouseDown()
    {
    SetQuality();
    index++;
    if (index >= 6)
        index = 0;
    ES3.Save<int>("QualityIndex",index); 
    }

    private void SetQuality()
    {
        QualitySettings.SetQualityLevel(index);

        switch (index)
        {
            case 0:
                TextMesh.text = "画质:非常低";
                break;
            case 1:
                TextMesh.text = "画质:低";
                break;
            case 2:
                TextMesh.text = "画质:中";
                break;
            case 3:
                TextMesh.text = "画质:高";
                break;
            case 4:
                TextMesh.text = "画质:非常高";
                break;
            case 5:
                TextMesh.text = "画质:极限";
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
