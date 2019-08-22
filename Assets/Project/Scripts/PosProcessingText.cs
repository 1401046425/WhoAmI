using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PosProcessingText : MonoBehaviour
{
    private bool OnOff=true;
    private TextMeshPro TextMesh;

   [SerializeField] private MobileColorGrading ColorGradingFX;
    // Start is called before the first frame update
    private void Awake()
    {
        TextMesh = GetComponent<TextMeshPro>();
        if (ES3.KeyExists("PostProcessing"))
            OnOff= ES3.Load<bool>("PostProcessing");
        SetPostState();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnMouseDown()
    {
        OnOff = !OnOff;
        SetPostState();
    }

    private void SetPostState()
    {
        ColorGradingFX.enabled = OnOff;
        switch (OnOff)
        {
            case true:
                TextMesh.text = "后期处理:开";
                break;
            case false:
                TextMesh.text = "后期处理:关";
                break;
        }

        ES3.Save<bool>("PostProcessing",OnOff); 
    }
}
