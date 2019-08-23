using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextLevelCheck : MonoBehaviour
{
    [SerializeField] private string LevelName;

    [SerializeField] private string FinishText;

    [SerializeField] private Color FinishColor;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.INS.LevelHasUnLocked(LevelName))
        {
            try
            {
                var TextUGUI= GetComponent<TextMeshProUGUI>();
                TextUGUI.text = FinishText;
                TextUGUI.color = FinishColor;

            }
            catch (Exception e)
            {
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}