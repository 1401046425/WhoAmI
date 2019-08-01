using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.Playables;

public class Level0Manager : BaseLevelManager
{
    public PlayableDirector[] LevelAnimationDirector;
    public GameObject Logo;
    public void PlayerLookAtEarthAnimator()
    {
        PlayerLevelAnimation(0, PlayerWordOrinAnimator, 4f);
    }
    public void PlayerWordOrinAnimator() 
    {
        PlayLevelTextInfo(InfoText_View[1], InfoTextAsset[1], PlayerTruthAnimator);
    }
    public void PlayerTruthAnimator()
    {
        PlayerLevelAnimation(1, null, 0f);
    }
    public void PlayerLevel0Over()
    {
        PlayerLevelAnimation(2, ShowLogo, 7f);
    }
    public void ShowLogo()
    {
        Logo.SetActive(true);
    }
    public void PlayerLevelAnimation(int Number,Action action,float WaitSecondCallBack)
    {
        if (LevelAnimationDirector.Length <= 0||LevelAnimationDirector==null)
            return;
        LevelAnimationDirector[Number].Play();
        StartCoroutine(AnimationCallBack(action, WaitSecondCallBack));
        
    }
    IEnumerator AnimationCallBack(Action CallBack,float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        CallBack?.Invoke();
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
