using System;
using UnityEngine;
using UnityEngine.Events;

public class Clock: MonoBehaviour {
 
    public float speed;
    public Transform pointerM;
    public Transform pointerH;
    public Transform Center;
    public float Radius;
    private float angleM;
    private float LastangleM=360;
    private Vector3 oldVector_M;
    private Vector3 InputPos;
    private Vector3 selfScenePosition;
   [SerializeField] private Camera m_Camera;
   public Action<int> OnLenthChange;
   public int Lenth;
   public bool FrezeZero;
   [HideInInspector] public int m_Lenth
   {
       get { return Lenth; }
   }
   private bool _judge;
   
   private Vector2 _lastPoint;  //上一帧坐标
 
   void Update()
   {
      
   }
   /// <summary>
   /// 判断顺时针逆时针
   /// (顺正逆负
   /// </summary>
   /// <param name="current">当前坐标</param>
   /// <param name="last">上个坐标(ref 更新</param>
   /// <param name="anchor">锚点</param>
   /// <returns></returns>
   private float TouchJudge(Vector2 current,ref Vector2 last,Vector2 anchor)
   {
       Vector2 lastDir = (last - anchor).normalized;//上次方向
           Vector2 currentDir = (current - anchor).normalized;//当前方向
 
           float lastDot = Vector2.Dot(Vector2.right, lastDir);
       float currentDot = Vector2.Dot(Vector2.right, currentDir);
 
       float lastAngle = last.y < anchor.y//用y点判断上下扇面
           ? Mathf.Acos(lastDot)*Mathf.Rad2Deg
           : -Mathf.Acos(lastDot)*Mathf.Rad2Deg;
 
       float currentAngle = current.y < anchor.y
           ? Mathf.Acos(currentDot) * Mathf.Rad2Deg
           : -Mathf.Acos(currentDot) * Mathf.Rad2Deg;
 
       last = current;
       return currentAngle - lastAngle;
   }
   
   private void Start()
    {
        m_Camera=Camera.main;
        selfScenePosition = m_Camera.WorldToScreenPoint(transform.position);
    }

    private void FixedUpdate()
    {
        if(Input.GetMouseButton(0))
                DrugEvent();
    }
    public void DrugEvent()
    {
        oldVector_M = pointerM.localEulerAngles;
        Vector3 currentScenePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, selfScenePosition.z);
        InputPos= m_Camera.ScreenToWorldPoint(currentScenePosition);
        if((InputPos-Center.position).magnitude>Radius)
            return;
        

        //通过鼠标位置决定角度（因为Vector3.Angle不会大于180）
        if (InputPos.x >= Center.position.x)
        {
            angleM = 360 - Vector3.Angle(Vector3.up, new Vector3(InputPos.x - Center.position.x, InputPos.y - Center.position.y, 0));
        }
        else
        {
            angleM = Vector3.Angle(Vector3.up,new Vector3(InputPos.x - Center.position.x, InputPos.y - Center.position.y, 0));
        }
        
        pointerM.localEulerAngles = new Vector3(0, 0, angleM);

        if (Mathf.Abs(pointerM.localEulerAngles.z - oldVector_M.z) < 180)//判断是否经过12
        {
            pointerH.localEulerAngles += new Vector3(0, 0, (pointerM.localEulerAngles.z - oldVector_M.z) / 12);
        }
        else
        {
            if (InputPos.x > Center.position.x)//顺时针经过
            {
                pointerH.localEulerAngles += new Vector3(0, 0, (pointerM.localEulerAngles.z - oldVector_M.z - 360) / 12);
            }
            else//逆时针经过
            {
                pointerH.localEulerAngles += new Vector3(0, 0, (pointerM.localEulerAngles.z - oldVector_M.z + 360) / 12);
            }
        }
        
       var offset= TouchJudge(InputPos, ref _lastPoint, Center.position);
       if (Mathf.Abs(angleM - LastangleM) >= 30)
       {
           LastangleM = angleM;
           if (offset > 2)
           {      
               Lenth++;
               OnLenthChange?.Invoke(m_Lenth);
           }
           else if (offset < -2)
           {
               Lenth--;
               if (FrezeZero && Lenth < 0)
                   Lenth = 0; 
               OnLenthChange?.Invoke(m_Lenth);
           }

       }


    }
}