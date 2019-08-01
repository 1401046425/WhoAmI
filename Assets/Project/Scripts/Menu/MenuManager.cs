using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrameWork;

public class MenuManager : MonoBehaviour
{
    public static MenuManager INS;
    private void Awake()
    {
        INS = this;
        InitMenu();
    }
    public void InitMenu()
    {
        if (GameManager.INS == null)
            return;
        if (GameManager.INS.IsFirstEnterGameToday)
        {
           UIManager.ShowPanel("StartMenu");
        }
        else
        {
            UIManager.ShowPanel("LevelPanel");
            UIManager.ClosePanel("StartMenu");
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
