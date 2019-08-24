using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StoryBlockEditor))]
[Serializable]

public  class StoryBlockEditor:EditorWindow
{
    private  StoryBlockManager BlockManager;
    private  GameObject BlockPrefab;
    private  string InputInfo;
    private int BlockIndex;
    [MenuItem("Tools/StoryBlockEditor")]
    public static void ConfigDialog()
    {
//GetWindow创建
       EditorWindow.GetWindow(typeof(StoryBlockEditor));
    }
    void OnGUI()
    {
        GUILayout.Label("故事区块编辑器", EditorStyles.boldLabel);
        //通过EditorGUILayout.ObjectField可以接受Object类型的参数进行相关操作
        //Button
       // BlockManager = EditorGUILayout.ObjectField("故事区块管理器",BlockManager, typeof(StoryBlockManager), true)as StoryBlockManager;
        BlockManager = GameObject.FindObjectOfType<StoryBlockManager>();
        if (BlockManager != null)
        {
            InputInfo = EditorPrefs.GetString("BlockName");
            BlockPrefab = EditorGUILayout.ObjectField("故事区块预制物",BlockPrefab, typeof(GameObject), true)as GameObject;
            InputInfo= EditorGUILayout.TextField("区块名称",InputInfo);
            EditorPrefs.SetString("BlockName",InputInfo);
            if (GUILayout.Button("新建一个故事区块"))
            {
                if (BlockPrefab != null)
                {
                    DuplicateBlock(BlockPrefab);
                }
                else
                {
                    CreateNewBlock(InputInfo);  
                }
  
            }

            BlockIndex = EditorPrefs.GetInt("BlockIndex");
            BlockIndex= EditorGUILayout.IntField("区块序号",BlockIndex);
            EditorPrefs.SetInt("BlockIndex",BlockIndex);
            if (GUILayout.Button(String.Format("跳跃到{0}区块", BlockIndex)))
            {
                BlockManager.JumpBlock(BlockIndex);
            }
            EditorGUILayout.LabelField(String.Format("区块总数:{0}",BlockManager.StoryBlocks.Count),EditorStyles.boldLabel);
            for (int i = 0; i < BlockManager.StoryBlocks.Count; i++)
            {
                if (BlockManager.StoryBlocks[i] != null)
                {
                    BlockManager.StoryBlocks[i]= EditorGUILayout.ObjectField(String.Format("故事区块{1}——{0}", BlockManager.StoryBlocks[i]._BlockName,i) , BlockManager.StoryBlocks[i], typeof(StoryBlock), true)as StoryBlock;
                    BlockManager.StoryBlocks[i].transform.SetAsLastSibling();
                }

            }

        }
        else
        {
            if (GUILayout.Button("新建一个故事区块管理器"))
            {

              var Manager= new GameObject().AddComponent<StoryBlockManager>();
              Manager.transform.name = "StoryBlockManager";
            }
        }



    }
    /// <summary>
    /// 新建一个故事区块
    /// </summary>
    /// <param name="BlockName"></param>
    public void CreateNewBlock(string BlockName)
    {
        if (string.IsNullOrWhiteSpace(BlockName))
            return;
        if (BlockManager.StoryBlocks.Contains(BlockManager.GetBlock(BlockName)))
            return;
        var Block = new GameObject().AddComponent<StoryBlock>();
        Block.SetBlockName = BlockName;
        Block.transform.name = BlockName;
        Block.transform.SetParent(BlockManager.transform);
        BlockManager.StoryBlocks.Add(Block);
        var Camera = CreateVirtualCamera();
        Camera.transform.SetParent(Block.transform);
        Camera.m_Priority = 0;
        Camera.transform.position = new Vector3(0, 0, -10); 
        Camera.gameObject.AddComponent<CMCameraController>();
    }
public static Cinemachine.CinemachineVirtualCamera CreateVirtualCamera()
    {
        return InternalCreateVirtualCamera(
            "CM vcam", true, typeof(Cinemachine.CinemachineComposer), typeof(Cinemachine.CinemachineTransposer));
    }

    static Cinemachine.CinemachineVirtualCamera InternalCreateVirtualCamera(
        string name, bool selectIt, params Type[] components)
    {
        // Create a new virtual camera
        CreateCameraBrainIfAbsent();
        GameObject go = Cinemachine.Editor.InspectorUtility.CreateGameObject(
            GenerateUniqueObjectName(typeof(Cinemachine.CinemachineVirtualCamera), name),
            typeof(Cinemachine.CinemachineVirtualCamera));
        if (SceneView.lastActiveSceneView != null)
            go.transform.position = SceneView.lastActiveSceneView.pivot;
        Undo.RegisterCreatedObjectUndo(go, "create " + name);
        Cinemachine.CinemachineVirtualCamera vcam = go.GetComponent<Cinemachine.CinemachineVirtualCamera>();
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
        Cinemachine.CinemachineBrain[] brains = UnityEngine.Object.FindObjectsOfType(
            typeof(Cinemachine.CinemachineBrain)) as Cinemachine.CinemachineBrain[];
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
                Undo.AddComponent<Cinemachine.CinemachineBrain>(cam.gameObject);
            }
        }
    }
    private  void DuplicateBlock(GameObject obj)
    {
        var BlockObj= Instantiate(obj);
        BlockObj.transform.SetParent(BlockManager.transform);
        var StoryBlockComp = BlockObj.GetComponent<StoryBlock>();
        StoryBlockComp.SetBlockName = InputInfo;
        BlockObj.transform.name = StoryBlockComp._BlockName;
        BlockManager.AddBlock(StoryBlockComp);
    }

    private void OnInspectorUpdate()
    {
        if(BlockManager!=null)
        BlockManager.ClearEmptyStoryBlock();
    }
}

public class StoryBlockManagerEditor : Editor
{
    [InitializeOnLoadMethod]
    static void Start()
    {
        EditorApplication.hierarchyWindowItemOnGUI = OnHierarchyGUI;
    }

    static void OnHierarchyGUI(int instanceID, Rect selectionRect)
    {
        var Manager= FindObjectsOfType<StoryBlockManager>();
        if (Manager.Length >= 2)
        {
            for (int i = 0; i < Manager.Length-1; i++)
            {
                DestroyImmediate(Manager[i]);
            }
        }
    }
}
