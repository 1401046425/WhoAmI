using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockWall : MonoBehaviour
{
  [SerializeField] private Collider2D[] Walls;
  public virtual void UnLockAllWall()
  {
    foreach (var VARIABLE in Walls)
    {
      VARIABLE.enabled = false;
    }
    
  }
}
