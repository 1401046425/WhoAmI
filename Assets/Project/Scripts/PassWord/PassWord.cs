using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PassWord : MonoBehaviour
{
    [HideInInspector] public string TargetPassword;

    [SerializeField] private uint bit;
    public Action startAction;
    public Action ShowPasswordAction;
    public Action OnPasswordTruth;
    [SerializeField] private UnityEvent OnPasswordFinish;
    private uint FinishBit=0;
  void InitPassword()
    {
        TargetPassword=string.Empty;
        for (int i = 0; i < bit; i++)
        {
            TargetPassword+=Random.Range(0, 9);
        }
        
    }
  
  private void Awake()
    {
        InitPassword();
        Debug.Log(TargetPassword);
    }

  public void StartPasswordRun()
  {
      startAction?.Invoke();
  }

  public void CheckPassWord(WordText text)
  {
      if (int.Parse(TargetPassword[text.Bit].ToString()) == (int) text.Password)
      {
          FinishBit++;
          text.IsFinish = true;
          OnPasswordTruth?.Invoke();
         // Debug.Log("检测成功密码");
      }

      if (FinishBit >= bit)
      {
          OnPasswordFinish?.Invoke();
      }


  }

  public void ShowPassWord()
  {
      ShowPasswordAction?.Invoke();
  }

  void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
