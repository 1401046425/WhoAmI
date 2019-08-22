using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cinemachine;
using Cinemachine.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

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

    /// <summary>
/// 新建一个故事区块
/// </summary>
/// <param name="BlockName"></param>
    public void CreateNewBlock(string BlockName)
    {
        if (string.IsNullOrWhiteSpace(BlockName))
            return;
        if (HasBlock(BlockName))
            return;
        var Block = new GameObject().AddComponent<StoryBlock>();
        Block.SetBlockName = BlockName;
        Block.transform.name = BlockName;
        Block.transform.SetParent(transform);
        StoryBlocks.Add(Block);
        var Camera = CreateVirtualCamera();
        Camera.transform.SetParent(Block.transform);
        Camera.m_Priority = 0;
        Camera.transform.position = new Vector3(0, 0, -10); 
        Camera.gameObject.AddComponent<CMCameraController>();

    }

    public static CinemachineVirtualCamera CreateVirtualCamera()
    {
        return InternalCreateVirtualCamera(
            "CM vcam", true, typeof(CinemachineComposer), typeof(CinemachineTransposer));
    }

    static CinemachineVirtualCamera InternalCreateVirtualCamera(
        string name, bool selectIt, params Type[] components)
    {
        // Create a new virtual camera
        CreateCameraBrainIfAbsent();
        GameObject go = InspectorUtility.CreateGameObject(
            GenerateUniqueObjectName(typeof(CinemachineVirtualCamera), name),
            typeof(CinemachineVirtualCamera));
        if (SceneView.lastActiveSceneView != null)
            go.transform.position = SceneView.lastActiveSceneView.pivot;
        Undo.RegisterCreatedObjectUndo(go, "create " + name);
        CinemachineVirtualCamera vcam = go.GetComponent<CinemachineVirtualCamera>();
        GameObject componentOwner = vcam.GetComponentOwner().gameObject;
        foreach (Type t in components)
            Undo.AddComponent(componentOwner, t);
        vcam.InvalidateComponentPipeline();
        if (selectIt)
            Selection.activeObject = go;
        return vcam;
    }

    public static string GenerateUniqueObjectName(Type type, string prefix)
    {
        int count = 0;
        UnityEngine.Object[] all = Resources.FindObjectsOfTypeAll(type);
        foreach (UnityEngine.Object o in all)
        {
            if (o != null && o.name.StartsWith(prefix))
            {
                string suffix = o.name.Substring(prefix.Length);
                int i;
                if (Int32.TryParse(suffix, out i) && i > count)
                    count = i;
            }
        }

        return prefix + (count + 1);
    }

    public static void CreateCameraBrainIfAbsent()
    {
        CinemachineBrain[] brains = UnityEngine.Object.FindObjectsOfType(
            typeof(CinemachineBrain)) as CinemachineBrain[];
        if (brains == null || brains.Length == 0)
        {
            Camera cam = Camera.main;
            if (cam == null)
            {
                Camera[] cams = UnityEngine.Object.FindObjectsOfType(
                    typeof(Camera)) as Camera[];
                if (cams != null && cams.Length > 0)
                    cam = cams[0];
            }

            if (cam != null)
            {
                Undo.AddComponent<CinemachineBrain>(cam.gameObject);
            }
        }
    }
}