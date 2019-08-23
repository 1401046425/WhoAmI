using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryBlockManager : MonoBehaviour
{
    public List<StoryBlock> StoryBlocks = new List<StoryBlock>();
    private StoryBlock NowPlayingBlock; //当前播放的区块
    public StoryBlock _NowPlayingBlock { get; }
    private int PlayBlockIndex = -1;

    public int _PlayBlockIndex
    {
        get { return PlayBlockIndex; }
    }

    public static StoryBlockManager INS;

    public StoryBlock GetBlock(string Name) //获取故事区块
    {
        foreach (var VARIABLE in StoryBlocks)
        {
            if (VARIABLE._BlockName == name)
                return VARIABLE;
        }

        return null;
    }

    private void Awake()
    {
        INS = this;
        foreach (var VARIABLE in StoryBlocks)
        {
            VARIABLE.OnInit();
        }
    }

    private void Start()
    {
        PlayNextBlock();
        //JumpBlock(16);
    }

    public void ClearEmptyStoryBlock()
    {
        for (int i = 0; i < StoryBlocks.Count; i++)
        {
            if (StoryBlocks[i] == null)
                    StoryBlocks.Remove(StoryBlocks[i]);
        }
  
    }

    public void JumpBlock(int Index)
    {
        NowPlayingBlock.OnExit();
        PlayBlockIndex = Index;
        NowPlayingBlock = StoryBlocks[_PlayBlockIndex];
        NowPlayingBlock.OnEnter();
    }

    public void PlayNextBlock() //播放下一个故事区块
    {
        if (_PlayBlockIndex > StoryBlocks.Count)
            return;
        PlayBlockIndex++;
        NowPlayingBlock = StoryBlocks[_PlayBlockIndex];
        NowPlayingBlock.OnEnter();
    }

    public void PlayNextBlock(float WaitTime) //延时播放下一个故事区块
    {
        StartCoroutine(DelayedCall(PlayNextBlock, WaitTime));
    }

    public void StopNowBlock() //停止现在的故事区块
    {
        NowPlayingBlock.OnExit();
        PlayNextBlock();
    }

    public void StopNowBlock(float WaitTime) //延时停止现在的故事区块
    {
        StartCoroutine(DelayedCall(StopNowBlock, WaitTime));
    }

    IEnumerator DelayedCall(Action CallBack, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        CallBack?.Invoke();
    }

    public void EndStoryBlock()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

    private bool HasBlock(string BlockName)
    {
        if (string.IsNullOrWhiteSpace(BlockName))
            return false;
        foreach (var VARIABLE in StoryBlocks)
        {
            if (VARIABLE._BlockName == BlockName)
                return true;
        }

        return false;
    }

    public void AddBlock(StoryBlock Block)
    {
        if(StoryBlocks.Contains(Block))
            return;
        if(HasBlock(Block._BlockName))
            return;
        StoryBlocks.Add(Block);
    }



 
}