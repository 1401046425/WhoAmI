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
    [SerializeField] private Animator m_Animator;
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


        ItemManager.INS.OnItemDown += OnDargItem;
        ItemManager.INS.OnItemUp += OnDesDargItem;
        ItemManager.INS.Item_Grid.OnItemTakeOut += TakeOutItem;
        OnLandEvent.AddListener(OnLand);
        OnCrouchEvent.AddListener(OnCrouch);
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
    }

    private void TakeOutItem(Item _item)
    {
        if (TakeItem == null)
            return;
        if (_item.ItemName == TakeItem.GetComponent<Item>().ItemName)
        {
            TakeItem = null;
        }
    }
    private void ClearTakeItem()
    {
        if (TakeItem == null)
            return;
        TakeItem.transform.SetParent(null);
        TakeItem.gameObject.SetActive(false);
        TakeItem.transform.localScale = Vector3.one;
        TakeItem = null;
    }
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


    }
    private void OnDesUseItem()
    {
        OnActionEvent.RemoveAllListeners();
        TakeItem = null;
    }
    private void OnDargItem(Item item)
    {
        CanControl = false;
    }
    private void OnDesDargItem(Item item)
    {
        StartCoroutine(AwakeControl());
    }
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
        m_Animator.SetBool("OnLand", m_Grounded);
    }
    private void FixedUpdate()
    {
        Move(PlayerMovePos.x, Crouch, Jump);
        Jump = false;
        m_Animator.SetFloat("MoveSpeed", Mathf.Abs(PlayerMovePos.x));
        m_Animator.SetBool("IsCrouch", m_wasCrouching);


    }
    public void OnLand()
    {

    }
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
            if (finger.SwipeScreenDelta.x > 20)
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
    private void OnDestroy()
    {
        OnActionEvent.RemoveAllListeners();
    }
}
