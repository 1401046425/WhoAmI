using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBookManager : MonoBehaviour
{
  [SerializeField]  private BookPro Book;
  public static MainBookManager INS;

  private void Awake()
  {
    INS = this;
  }

  public bool interactable
  {
    get { return Book.interactable;}
    set { Book.interactable = value; }
  }
}
