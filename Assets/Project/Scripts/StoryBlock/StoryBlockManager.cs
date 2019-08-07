using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = UnityEngine.Object;

public class StoryBlockManager : Singleton<StoryBlockManager>
{
   public List<StoryBlock> StoryBlocks=new List<StoryBlock>();
   private StoryBlock NowPlayingBlock;//当前播放的区块
   public StoryBlock _NowPlayingBlock { get;}
   private int PlayBlockIndex=-1;
   public  int _PlayBlockIndex
   {
       get { return PlayBlockIndex; }
   }

   public StoryBlock GetBlock(string Name)//获取故事区块
   {
       foreach (var VARIABLE in StoryBlocks)
       {
           if (VARIABLE._BlockName == name)
               return VARIABLE;
       }

       return null;
   }

   private void Start()
   {
       PlayNextBlock();
   }

   public void PlayNextBlock()//播放下一个故事区块
   {
       PlayBlockIndex++;
       NowPlayingBlock = StoryBlocks[_PlayBlockIndex];
       NowPlayingBlock.OnEnter();
   }
   public void PlayNextBlock(float WaitTime)//延时播放下一个故事区块
   {
       StartCoroutine(DelayedCall(PlayNextBlock, WaitTime));
   }
   public void StopNowBlock()//停止现在的故事区块
   {
       NowPlayingBlock.OnExit();
       PlayNextBlock();
   }
   public void StopNowBlock(float WaitTime)//延时停止现在的故事区块
   {
       StartCoroutine(DelayedCall(StopNowBlock, WaitTime));
   }
   IEnumerator DelayedCall(Action CallBack, float time)
   {
       yield return new  WaitForSecondsRealtime(time);
       CallBack?.Invoke();
   }
}
