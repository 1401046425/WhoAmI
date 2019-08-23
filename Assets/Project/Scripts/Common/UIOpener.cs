using System;
using System.Collections;
using System.Collections.Generic;
using UIFrameWork;
using UnityEngine;

public class UIOpener : MonoBehaviour
{
    [SerializeField]
    private string UIName;

    [SerializeField] private bool OpenAwake;
    private void OnMouseDown()
    {
        if(UIManager.GetPanel(UIName)==null)
            UIManager.ShowPanel(UIName);
    }

    public void OPenUIPanel()
    {
        UIManager.ShowPanel(UIName);
    }

    private void Awake()
    {
        if(OpenAwake)
            OPenUIPanel();
    }
}
