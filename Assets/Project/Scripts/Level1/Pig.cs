using System;
using System.Runtime.CompilerServices;
using UnityEngine;


public class Pig : SceneObject
    {
        public override void OnMutual()
        {
            gameObject.SetActive(false);
            TimeTaskManager.INS.AddPoint();
           ItemManager.INS.ShowItem("Pork");
        }
    }
