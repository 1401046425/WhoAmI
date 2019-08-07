using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerManager : Singleton<PlayerManager>
{
  [SerializeField] private CinemachineVirtualCamera[] PlayerCameras;
  [SerializeField] private CharacterController2D[] PlayrControllers;
    [HideInInspector]public int NowPlayerindex;
    public void ChangePlayer(int index)
    {
        if (index > PlayrControllers.Length)
            return;
       var ActivePlayer= PlayrControllers[index].gameObject;
        ActivePlayer.SetActive(true);
        NowPlayerindex = index;
        for (int i = 0; i < PlayrControllers.Length; i++)
        {
            if (i != index)
            {
                PlayrControllers[i].gameObject.SetActive(false);
            }
        }
        if (index > PlayerCameras.Length)
            return;
        PlayerCameras[index].gameObject.SetActive(true);
        for (int i = 0; i < PlayerCameras.Length; i++)
        {
            if (i != index)
            {
                PlayerCameras[i].gameObject.SetActive(false);
            }
        }
        
    }
    public void ChangePlayerPos(int index,Vector2 pos)
    {
        if (index > PlayrControllers.Length)
            return;
        PlayrControllers[index].transform.position = pos;
    }
    public CharacterController2D GetNowActivePlayer()
    {
       return PlayrControllers[NowPlayerindex];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
