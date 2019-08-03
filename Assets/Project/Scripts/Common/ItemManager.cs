using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemManager : Singleton<ItemManager>
{
  [HideInInspector] public ItemGrid Item_Grid;
  [SerializeField] private List<ItemData> AllItem=new List<ItemData>();

    public UnityAction<Item> OnItemDown;
    public UnityAction<Item> OnItemUp;
    private void Awake()
    {
        Item_Grid = FindObjectOfType<ItemGrid>();
        if (!Item_Grid)
        {
            Debug.LogError("找不到ItemGrid组件的实例");
        }
    }
    public Item TakeItemfromGrid(string ItemName)
    {
        foreach (var item in Item_Grid._Grid)
        {
            if (item.ItemName == ItemName)
            {
                var get_item = item;
                Item_Grid._Grid.Remove(item);
                Item_Grid.OnItemTakeOut.Invoke(get_item);
                return get_item;
            }
        }
        return null;
    }
    public bool CheckItemInGrid(string ItemName)
    {
        foreach (var item in Item_Grid._Grid)
        {
            if (item.ItemName == ItemName)
            {
                return true;
            }
        }
        return false;
    }
    public void AddItemToGrid(Item _item)
    {
        if (string.IsNullOrWhiteSpace(_item.ItemName))
            return;
        if (GetItemFromGrid(_item.ItemName))
            return;
        Item_Grid._Grid.Add(_item);
        if(Item_Grid.OnItemAdd!=null)
            Item_Grid.OnItemAdd.Invoke(_item);
        _item.gameObject.SetActive(false);

    }
    public void AddItemToManager(Item _item)
    {
        if (string.IsNullOrWhiteSpace(_item.ItemName))
            return;
        var data = GetItemFromManager(_item);
        if (data == null)
        {
            AllItem.Add(new ItemData
            {
                item = _item,
                Count = 1
            });
        }
        else
        {
            data.Count++;
        }

    }
    public ItemData GetItemFromManager(Item _item)
    {
        foreach (var item in AllItem)
        {
            if (item.item == _item)
            {
                return item;
            }
        }
        return null;
    }
    public Item GetItemFromGrid(string Name)
    {
        foreach (var item in ItemGrid.INS._Grid)
        {
            if (item.ItemName == Name)
            {
                return item;
            }
        }
        return null;
    }
}
 public class ItemData
    {
   public Item item;
    public int Count;
    }
public enum ItemType
{
    Hand,
    Normal,
    Weapon,
}