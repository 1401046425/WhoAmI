using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
[RequireComponent(typeof (SpriteRenderer))]
public class Item : MonoBehaviour
{
    [SerializeField] public ItemType itemType;
    [SerializeField] public string ItemName;
    [SerializeField] public Sprite ItemImage;
    [SerializeField] public bool FreezeRotation;
    [SerializeField] public  bool TimeTaskItem;

    [HideInInspector]public bool IsTasked;
    
    private bool UseRigidBody2D;
    [SerializeField] private  bool AutoHide;
    [SerializeField] public bool Item_UseRigidBody2D
    {
        get {
            return UseRigidBody2D;
        }
        set {
            try
            {
                Item_RigidBody2D = GetComponent<Rigidbody2D>();
            }
            catch
            {
                Item_RigidBody2D = gameObject.AddComponent<Rigidbody2D>();
            }
            UseRigidBody2D = value;

        } }
    [SerializeField] public bool AutoAddInGrid;
   public UnityAction OnDesUseItem;
    private Collider2D  Item_Collider2D;
    [SerializeField] bool Item_IsTrigger {
        get {
            if (!Item_Collider2D)
            {
                Item_Collider2D = GetComponent<Collider2D>();
            }
            return Item_Collider2D.isTrigger;
        }
        set
        {
            if (!Item_Collider2D)
            {
                Item_Collider2D = GetComponent<Collider2D>();
            }
            Item_Collider2D.isTrigger = value;
        }
    }
    [SerializeField] public bool Item_UseAutoTrigger;
    Rigidbody2D rigidbody2D;
    private Rigidbody2D Item_RigidBody2D {
        get {
            if (!GetComponent<Rigidbody2D>())
                rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
            return rigidbody2D;
        }
        set {
            if (!GetComponent<Rigidbody2D>())
                rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
            if (rigidbody2D)
            {
                UseRigidBody2D = true;
            }
            rigidbody2D = value;
        }
    }
    private SpriteRenderer SpriteRenderer;
    public SpriteRenderer Item_SpriteRenderer {
        get {
            if (!SpriteRenderer)
            {
                SpriteRenderer = GetComponent<SpriteRenderer>();
            }
            return SpriteRenderer;
        }
        set {
            SpriteRenderer = value;
        }
    }
    public bool CanDrag;
    System.Type type;
    private void Awake()
    {
        type = this.GetType();
        ItemManager.INS.AddItemToManager(this);

        if (!GetComponent<SpriteRenderer>())
        {
            Item_SpriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        else
        {
            Item_SpriteRenderer = GetComponent<SpriteRenderer>();
        }
        Item_SpriteRenderer.sprite = ItemImage;
        if (transform.position.z != 0)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }
        if (GetComponent<Collider2D>() == null)
        {
            Item_Collider2D = gameObject.AddComponent<CircleCollider2D>();
        }

        try
        {
            Item_RigidBody2D = GetComponent<Rigidbody2D>();
        }
        catch
        {
        }
        if (FreezeRotation)
            Item_RigidBody2D.freezeRotation = FreezeRotation;
        if(AutoHide)
            gameObject.SetActive(false);


    }
    public virtual void OnMouseDonwItem()
    {
        ItemManager.INS.OnItemDown.Invoke(this);
        if (Item_UseAutoTrigger)
        {
            Item_IsTrigger = true;
        }
    }

    private void RebackToGrid()
    {
        ItemManager.INS.AddItemToGrid(this);
    }

    private void OnMouseDown()
    {
        OnMouseDonwItem();
    }
    private void OnMouseDrag()
    {
        if (CanDrag)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = pos;
        }
    }
    private void OnMouseUp()
    {
        if (Item_UseAutoTrigger)
        {
            Item_IsTrigger = false;
        }
        ItemManager.INS.OnItemUp.Invoke(this);
    }
    public virtual void OnUse()
    {
        OnDesUseItem?.Invoke();
        OnDesUseItem = null;
    }

    public void DesAllEvent()
    {
        OnDesUseItem = null;
    }
    private void OnDisable()
    {
        DesAllEvent();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (AutoAddInGrid)
        {
            ItemManager.INS.AddItemToGrid(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        if (OnDesUseItem != null)
             OnDesUseItem.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        CheckCraft();
    }

    private void CheckCraft()
    {
        var collders = Physics2D.OverlapCircleAll(transform.position, 5f);
        List<string> element=new List<string>();
        foreach (var VARIABLE in collders)
        {
            try
            {
                element.Add(VARIABLE.GetComponent<Item>().ItemName);
            }
            catch (Exception e)
            {

            }
        }
        if (element.Count > 1)
        {var itemname= CraftManager.INS.Craft(element);
            if (!string.IsNullOrWhiteSpace(itemname))//合成成功
            {
                ItemManager.INS.ShowItem(itemname,transform.position);
                foreach (var VARIABLE in element)
                {
                    ItemManager.INS.CloseItem(VARIABLE);
                }
                
            }
        }
        
    }

}
