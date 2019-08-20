using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VersionText : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        try
        {
            GetComponent<TextMeshPro>().text = string.Format("版本:{0}", Application.version);
        }
        catch (Exception e)
        {

        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
