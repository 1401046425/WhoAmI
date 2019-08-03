using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using UnityEngine.Events;
using System;

public class PlayerController2D : CharacterController2D
{
    public UnityEvent OnActionEvent;
    [Header("开发模式")]
    [Space]
    [SerializeField] private bool IsDevelopMod;
    [SerializeField] public Animator m_Animator;
    [SerializeField] private Transform m_HandPos;
    private bool m_Jump;
    private bool Jump { get {
            return m_Jump;
        } set{ m_Jump = value; if (m_Jump)
            {
                m_Animator.SetTrigger("IsJump");
            }  } }
    private bool Crouch;
    private bool CanControl = true;
    private Vector2 PlayerMovePos;
    LeanFingerSwipe Swipe;
    [SerializeField] public GameObject TakeItem;
    // Start is called before the first frame update
    void Start()
    {
            Lean.Touch.LeanTouch.OnFingerSet += OnTouch;
            Lean.Touch.LeanTouch.OnFingerUp += OnTouchUp;

            Swipe = FindObjectOfType<LeanFingerSwipe>();
            if (Swipe == null)//检查LeanFingerSwipe组件是否存在
            {
                Swipe = new GameObject().AddComponent<LeanFingerSwipe>();
                Swipe.transform.name = "LeanFingerSwipe";
                Swipe.IgnoreIsOverGui = false;
                Swipe.IgnoreStartedOverGui = false;
                Swipe.CheckAngle = false;
                Swipe.Angle = 0;
                Swipe.AngleThreshold = 90;
                Swipe.Clamp = LeanFingerSwipe.ClampType.None;
                Swipe.Multiplier = 1;
            }

            Swipe.OnSwipe.AddListener(OnTouchSwipe);



        OnLandEvent.AddListener(OnLand);
        OnCrouchEvent.AddListener(OnCrouch);

    }
    private void OnEnable()
    {
        ItemManager.INS.OnItemDown += OnDargItem;
        ItemManager.INS.OnItemUp += OnDesDargItem;
        ItemManager.INS.Item_Grid.OnItemTakeOut += TakeOutItem;
        ItemGrid.INS.OnItemSlect += ShowSlectItem;
    }
    private void OnCrouch(bool arg0)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, 5f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                if (colliders[i].GetComponent<Item>())
                {
                    ItemManager.INS.AddItemToGrid(colliders[i].GetComponent<Item>());
                }
            }
        }
    }//玩家蹲下

    private void TakeOutItem(Item _item)
    {
        if (TakeItem == null)
            return;
        if (_item.ItemName == TakeItem.GetComponent<Item>().ItemName)
        {
            TakeItem = null;
        }
    }//拿出物品
    private void ClearTakeItem()
    {
        if (TakeItem == null)
            return;
        OnActionEvent.RemoveAllListeners();
        TakeItem.transform.SetParent(null);
        TakeItem.gameObject.SetActive(false);
        TakeItem.transform.localScale = Vector3.one;
        TakeItem = null;
    }//清空手中物品
    void ShowSlectItem(Item _item)
    {
        if (_item == null)
            return;
        if (_item.itemType == ItemType.Hand)
        {
            ClearTakeItem();
            return;
        }
        if (TakeItem != null)
        {
            ClearTakeItem();
        }
        _item.gameObject.SetActive(true);
        _item.transform.SetParent(m_HandPos);
        _item.transform.localPosition = Vector2.one;
        if (_item.transform.localScale.x < 0)
        {

            _item.transform.localScale=new Vector3(Math.Abs(_item.transform.localScale.x),
                _item.transform.localScale.y,
                _item.transform.localScale.z);
        }
        try
        {
            _item.GetComponent<Rigidbody2D>().simulated = false;
        }
        catch
        {

        }
        OnActionEvent.RemoveAllListeners();
        OnActionEvent.AddListener(_item.OnUse);
        _item.OnDesUseItem += OnDesUseItem;
        TakeItem = _item.gameObject;


    }//展示选中物品
    private void OnDesUseItem()
    {
        OnActionEvent.RemoveAllListeners();
        TakeItem = null;
    }//取消使用物品
    private void OnDargItem(Item item)
    {
        CanControl = false;
    }//当鼠标拽动物品
    private void OnDesDargItem(Item item)
    {
        StartCoroutine(AwakeControl());
    }//当鼠标取消拽动物品
    IEnumerator AwakeControl()
    {
        yield return new WaitForFixedUpdate();
        CanControl = true;
    }

    private void Update()
    {
        if (IsDevelopMod)
        {
            var Hor = Input.GetAxis("Horizontal");
            PlayerMovePos.x = Hor;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
                Jump = true;
            }
        }

    }
    private void FixedUpdate()
    {
        Move(PlayerMovePos.x, Crouch, Jump);
        Jump = false;
        m_Animator.SetFloat("MoveSpeed", Mathf.Abs(PlayerMovePos.x));
        m_Animator.SetBool("IsCrouch", m_wasCrouching);

       m_Animator.SetBool("OnLand", m_Grounded);

    }
    public void OnLand()
    {

    }//当玩家踩在地面
    public void OnTouch(Lean.Touch.LeanFinger finger)
    {
        if (!finger.IsOverGui)
        {
            if (!CanControl)
                return;
            if (finger.ScreenPosition.x < Screen.width / 2)
            {

                PlayerMovePos = finger.SwipeScreenDelta.normalized;
            }
            else
            {
                if (finger.SwipeScreenDelta.y < -20)
                {
                    Crouch = true;
                }
            }
        }
    }
    private void OnTouchSwipe(LeanFinger finger)
    {
        if (!CanControl)
            return;
        if (finger.ScreenPosition.x < Screen.width / 2)
            return;
        if (!finger.IsOverGui)
        {
            if (finger.SwipeScreenDelta.y > 20)
            {
               Jump = true;
            }
            if (finger.SwipeScreenDelta.x > 20|| finger.SwipeScreenDelta.x < -20)
            {
                OnActionEvent.Invoke();
            }
        }

    }
    private void OnTouchUp(LeanFinger obj)
    {
        PlayerMovePos = Vector2.zero;
        Crouch = false;
    }
    private void OnDisable()
    {
        if (ItemManager.INS != null)
        {
            ItemManager.INS.OnItemDown -= OnDargItem;
            ItemManager.INS.OnItemUp -= OnDesDargItem;
            ItemManager.INS.Item_Grid.OnItemTakeOut -= TakeOutItem;
            ItemGrid.INS.OnItemSlect -= ShowSlectItem;
        }
    }
    private void OnDestroy()
    {
        OnActionEvent.RemoveAllListeners();
    }
}
