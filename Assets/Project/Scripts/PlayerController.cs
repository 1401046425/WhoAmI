
using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using Lean.Touch;
using UnityEngine.Experimental.PlayerLoop;

//第一人称控制需要刚体和碰撞器
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private float m_MoveSpeed;
    [SerializeField] float rotatespeed;
    [SerializeField] private bool CanControl=true;
    Vector3 m_Velocity;
    Rigidbody m_PlayrRigid;
    private Vector2 InputPos;
    private Vector2 RotatePos;
    Vector3 rot = Vector3.zero;
    [SerializeField] private Transform Camera;
    void Awake()
    {
        m_PlayrRigid = GetComponent<Rigidbody>();
        m_PlayrRigid.freezeRotation = true;
        LeanTouch.OnFingerSet += OnFingerSet;
        LeanTouch.OnFingerUp += OnFingerUp;
    }

    public void ActiveControl()
    {
        CanControl = true;
    }

    public void DesActiveControl()
    {
        CanControl = false;
    }

    private void OnFingerUp(LeanFinger obj)
    {
        InputPos = Vector2.zero;
        if(BaseLevelManager.INS!=null)
            BaseLevelManager.INS.StopSound("走路");
    }

    private void OnFingerSet(LeanFinger obj)
    {
        if(!CanControl)
            return;
       // if (obj.IsOverGui)
          //  return;
        if (obj.ScreenPosition.x > Screen.width / 2.5f)
        {
            RotatePos = obj.ScreenDelta.normalized;
            float MouseX = RotatePos.x * rotatespeed*Mathf.Abs(obj.ScreenDelta.x);
            float MouseY = RotatePos.y * rotatespeed*Mathf.Abs(obj.ScreenDelta.y);
            rot.x = rot.x - MouseY;
            rot.y = rot.y + MouseX;
            rot.z = 0; //锁定摄像头移动的角度z轴，防止左右倾斜
            //transform.eulerAngles = rot; //所有方向设定好后，摄像头的角度=rot
            if (rot.x >= 89)
                rot.x = 89;
            if (rot.x <= -89)
                rot.x = -89;
            transform.eulerAngles = new Vector3(0, rot.y, 0);
            Camera.eulerAngles=new Vector3(rot.x, rot.y, 0);
        }
        else
        {
            if (obj.ScreenDelta.normalized != Vector2.zero)
                InputPos = obj.ScreenDelta.normalized;
            if (BaseLevelManager.INS)
            {
                if (BaseLevelManager.INS.GetSoundState("走路")==BaseLevelManager.SoundType.Stoped)
                {
                    BaseLevelManager.INS.PlaySound("走路");
                }
            }

 
        }
    }
    void FixedUpdate()
    {
        if(!CanControl)
            return;
        // Vector3 targetVelocity = new Vector3(InputPos.x * 10f, m_PlayrRigid.velocity.y,InputPos.y*10f);
        // And then smoothing it out and applying it to the character

        // m_PlayrRigid.velocity = Vector3.SmoothDamp(m_PlayrRigid.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        transform.Translate(new Vector3(InputPos.x, m_PlayrRigid.velocity.y, InputPos.y) * m_MoveSpeed *
                                Time.fixedDeltaTime);
  
    }
}
