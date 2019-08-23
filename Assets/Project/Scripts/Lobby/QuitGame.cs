using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private int Index;
    private void OnMouseDown()
    {
        Index++;
        try
        {
            GetComponent<TextMeshPro>().text = "[再次点击退出]";
        }
        catch (Exception e)
        {
        }
        if(Index==2)
        Application.Quit();
    }

    private void OnDisable()
    {
        Index=0;
        try
        {
            GetComponent<TextMeshPro>().text = "[退出游戏]";
        }
        catch (Exception e)
        {
        }
    }
}
