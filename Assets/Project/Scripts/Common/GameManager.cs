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
    public GameStatus Status;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        InitGame();
    }
    public void InitGame()
    {
	    OnGameInit += InitAllLevel;
        OnGameInit += InitunLockedLevel;
        OnGameInit();
        IsFirstEnterGameToday = true;
        Status = GameStatus.Running;
    }
    private List<string> unLockedLevelName=new List<string>();
    private List<string> AllLevelName=new List<string>();

    private void InitAllLevel()
    {
	    AllLevelName.Add("Origin");
	    AllLevelName.Add("BigFlood");
    }

    /// <summary>
	/// 获取所有已经解锁的场景名称
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
            AddLevel("Origin");
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
        if(AllLevelName.Contains(LevelName))
			unLockedLevelName.Add(LevelName);
        ES3.Save<List<string>>("LevelData", unLockedLevelName);
	}
    /// <summary>
    /// 开始关卡
    /// </summary>
    public void StartLevel(string LevelName)
    {
	    GC.Collect();
	    LastEnterLevelName = LevelName;
	    SceneManager.LoadSceneAsync(LevelName);
    }

    public void RestartLastLevel()
    {
	    GC.Collect();
	    SceneManager.LoadSceneAsync(LastEnterLevelName);
    }

    public void QuitLevel()
    {
	    GC.Collect();
        SceneManager.LoadSceneAsync("Lobby");
    }

    public void PauseGame()
    {
	    Physics.autoSimulation = false;
	    Time.timeScale = 0;
	    Status = GameStatus.Pause;
    }

    public void UnPauseGame()
    {
	    Physics.autoSimulation = true;
	    Time.timeScale = 1;
	    Status = GameStatus.Running;
    }
}
public enum GameStatus {
	Stop,
	Pause,
	Running,
}