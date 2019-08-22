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
                    BlockManager.CreateNewBlock(InputInfo);  
                }
  
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
