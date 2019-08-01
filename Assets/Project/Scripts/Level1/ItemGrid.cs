using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemGrid : Singleton<ItemGrid>
{
    public List<Item> _Grid = new List<Item>();
    Item Item_NowSlect;
    public Item NowSlectItem { get { return NowSlectItem; } set {
            Item_NowSlect = value;
            if(OnItemSlect!=null)
            OnItemSlect.Invoke(Item_NowSlect);
        } }
    public UnityAction<Item> OnItemSlect;
    public UnityAction<Item> OnItemAdd;
    public UnityAction<Item> OnItemTakeOut;

    private void OnDestroy()
    {
        NowSlectItem = null;
    }
}