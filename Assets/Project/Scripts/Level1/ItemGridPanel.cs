using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrameWork.BasePanel;
using UnityEngine.Events;

public class ItemGridPanel : BaseUIPanel
{
    [SerializeField] public Transform Content;
    List<ItemIcon> IconList=new List<ItemIcon>();
    private void Awake()
    {
        ItemManager.INS.Item_Grid.OnItemAdd += AddItemicon;
        ItemManager.INS.Item_Grid.OnItemTakeOut += TakeOutItem;
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        UpdateGrid();
    }
    public void UpdateGrid()
    {
        foreach (var item in ItemManager.INS.Item_Grid._Grid)
        {
            if (!HasIcon(item.ItemName))
            {
                AddItemicon(item);
            }
        }
    }
    private bool HasIcon(string Name)
    {
        foreach (var item in IconList)
        {
            if (item.ItemName == Name)
            {
                return true;
            }
        }
        return false;
    }
    private void AddItemicon(Item item)
    {
        var Icon = new GameObject();
        var trans=Icon.AddComponent<RectTransform>();
        Icon.name = item.ItemName;
       var ItemIcon= Icon.AddComponent<ItemIcon>();
        ItemIcon.ItemName = item.ItemName;
        ItemIcon.ItemImgae = item.ItemImage;
        ItemIcon.itemType = item.itemType;
        if (item.itemType!=ItemType.Hand)
        {
            ItemIcon.CanDragOut = true;
        }
        trans.SetParent(Content);
        trans.localPosition = Vector3.one;
        trans.localScale = Vector3.one;
    }
    private void TakeOutItem(Item item)
    {
        item.transform.SetParent(null);
        item.transform.localScale = Vector3.one;
        try
        {
            item.GetComponent<Rigidbody2D>().simulated = true;
        }
        catch
        {

        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
