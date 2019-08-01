using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIFrameWork;
using UIFrameWork.BasePanel;
public class UI_Mneu :BaseUIPanel 
{
    // Start is called before the first frame update

    /// <summary>
    /// 显示游戏关卡菜单界面
    /// </summary>
    public void ShowLevelPanel()
    {
        UIManager.ClosePanel("StartMenu");
        UIManager.ShowPanel("LevelPanel");
        GameManager.INS.IsFirstEnterGameToday = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
