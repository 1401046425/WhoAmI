using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemIcon : MonoBehaviour,IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    [HideInInspector] public string ItemName;
    [HideInInspector] public ItemType itemType;
    [HideInInspector] public bool CanDragOut;
    Image ImageView;
    [HideInInspector] public Sprite ItemImgae {
        get {
            if (!ImageView)
            {
                ImageView = gameObject.AddComponent<Image>();
            }
            return ImageView.sprite;
        }
        set
        {
            if (!ImageView)
            {
                ImageView = gameObject.AddComponent<Image>();
            }
            ImageView.sprite = value;
        }


    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CanDragOut)
        {
            transform.SetParent(GameObject.Find("UI").transform);
            transform.position = eventData.position;
            ItemGrid.INS.NowSlectItem = null;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (CanDragOut)
            transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (CanDragOut)
        {
            if (eventData.position.y >= Screen.height / 3)
            {
                Destroy(gameObject);
               ItemManager.INS.TakeItemfromGrid(ItemName);

            }
            else
            {
                var View = FindObjectOfType<ItemGridPanel>();
                transform.SetParent(View.Content);
            }
        }
        ItemGrid.INS.NowSlectItem = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemGrid.INS.NowSlectItem = ItemManager.INS.GetItemFromGrid(ItemName);
    }

}
