using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public Action OnGameInit;

    public bool IsFirstEnterGameToday;
    [HideInInspector] private string LastEnterLevelName;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        InitGame();
    }
    public void InitGame()
    {
        OnGameInit += InitunLockedLevel;
        OnGameInit();
        SceneManager.LoadSceneAsync("Lobby");
        IsFirstEnterGameToday = true;
    }
    private List<string> unLockedLevelName=new List<string>();
    /// <summary>
	/// 初始化GameFrameWork框架基础组建到GameManager中
	/// </summary>
    public List<string> GetAllUnLockLevelName()
    {
        return unLockedLevelName;
    }
    public string NowEnterLevel { get; set; }
    /// <summary>
	/// 初始化已经解锁到关卡
	/// </summary>
	public void InitunLockedLevel()
	{
        if (!ES3.KeyExists("LevelData"))
        {
            AddLevel("0.WhoAmI");
        }
        var LoudData = ES3.Load<List<string>>("LevelData");
       unLockedLevelName= LoudData;
	}
    /// <summary>
	/// 添加关卡
	/// </summary>
	/// <param name="LevelName"></param>
	public void AddLevel(string LevelName)
	{
        if (unLockedLevelName.Contains(LevelName))
            return;
		unLockedLevelName.Add(LevelName);
        ES3.Save<List<string>>("LevelData", unLockedLevelName);
	}
    /// <summary>
    /// 开始关卡
    /// </summary>
    public void StartLevel(string LevelName)
    {
	    LastEnterLevelName = LevelName;
	    SceneManager.LoadSceneAsync(LevelName);
    }

    public void RestartLastLevel()
    {
	    SceneManager.LoadSceneAsync(LastEnterLevelName);
    }

    public void QuitLevel()
    {
        SceneManager.LoadSceneAsync("Lobby");
    }
}