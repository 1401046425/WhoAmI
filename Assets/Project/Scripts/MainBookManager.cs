using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBookManager : MonoBehaviour
{
  [SerializeField]  private BookPro Book;
  public static MainBookManager _S;

  private void Awake()
  {
    _S = this;
    UpdateLevel();
  }

  public bool interactable
  {
    get { return Book.interactable;}
    set { Book.interactable = value; }
  }

  void UpdateLevel()
  {
    var Index = GameManager.INS.GetAllUnLockLevelName().Count;
    Book.currentPaper = Index;
    Book.EndFlippingPaper = Index-1;
    Debug.Log(GameManager.INS.GetAllUnLockLevelName().Count);
  }
}
