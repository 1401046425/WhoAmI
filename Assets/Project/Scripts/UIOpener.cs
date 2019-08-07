using System;
using System.Collections;
using System.Collections.Generic;
using UIFrameWork;
using UnityEngine;

public class UIOpener : MonoBehaviour
{
    [SerializeField]
    private string UIName;

    private void OnMouseDown()
    {
        if(UIManager.GetPanel(UIName)==null)
            UIManager.ShowPanel(UIName);
    }

    public void OPenUIPanel()
    {
        if(UIManager.GetPanel(UIName)==null)
            UIManager.ShowPanel(UIName);
    }
}
