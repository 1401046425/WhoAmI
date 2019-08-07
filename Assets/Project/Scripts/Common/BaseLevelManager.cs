using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class BaseLevelManager : Singleton<BaseLevelManager>
{
    public PlayableDirector[] PLABDirectiors;
    public AudioClip[] BackGroundMusics;//背景音乐Clip
    public AudioClip[] Sounds;//音效Clip
    public TextAsset[] InfoTextAsset;//文本资源
    public TextMeshPro[] InfoText_View;//TextMeshPro文本显示
    public TextMeshProUGUI[] InfoTextUGUI_View;//TextMeshProUI文本显示
    private static AudioSource BGMPlayerINS;
    [SerializeField] private bool IsBGMLoop;
    public AudioSource BGM_Player {
        get {
            if (BGMPlayerINS == null)
            {
                BGMPlayerINS = new GameObject().AddComponent<AudioSource>();
                BGMPlayerINS.transform.name = string.Format("BGMAudioPlayer-NullMusic");
            }
            return BGMPlayerINS;
        }
    }//背景音乐播放器

    private void Awake()
    {

    }
    public void InitLevelManager()
    {
        
    }
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="Number"关卡管理器中的音乐序号></param>
    public void PlayBGM(int Number)
    {

        if (BGM_Player == null)
            return;
        if (BGM_Player.isPlaying)
            BGM_Player.Stop();
        if (BackGroundMusics.Length < Number)
            return;
        var clip = BackGroundMusics[Number];
        BGM_Player.clip = clip;
        if (BGM_Player.clip == null)
            return;
        BGM_Player.Play();
        if (IsBGMLoop)
            BGM_Player.loop = true;
        BGM_Player.transform.name= string.Format("BGMAudioPlayer-{0}",clip.name);
    }
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="Name">关卡管理器中的音乐名称</param>
    public void PlayBGM(string Name)
    {
        if (BGM_Player == null)
            return;
        if (BGM_Player.isPlaying)
            BGM_Player.Stop();
        AudioClip clip = null;
        foreach (var item in BackGroundMusics)
        {
            if (item.name == Name)
            {
                clip = item;
            }
        }
        BGM_Player.clip = clip;
        if (BGM_Player.clip == null)
            return;
        BGM_Player.Play();
        if (IsBGMLoop)
            BGM_Player.loop = true;
        BGM_Player.transform.name = string.Format("BGMAudioPlayer-{0}", clip.name);
    }
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="clip">音乐Clip</param>
    public void PlayBGM(AudioClip clip)
    {
        if (BGM_Player == null)
            return;
        if (BGM_Player.isPlaying)
            BGM_Player.Stop();

        if (clip = null)
            return;
        BGM_Player.clip = clip;
        if (BGM_Player.clip == null)
            return;
        BGM_Player.Play();
        if (IsBGMLoop)
            BGM_Player.loop = true;
        BGM_Player.transform.name = string.Format("BGMAudioPlayer-{0}", clip.name);
    }
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="clip">音效Clip</param>
    public void PlaySound(AudioClip clip)
    {
        var AudioSoundPlayer = new GameObject().AddComponent<AudioSource>();
        AudioSoundPlayer.transform.name = string.Format("AudioSound-{0}", clip.name);
        AudioSoundPlayer.playOnAwake = false;
        AudioSoundPlayer.clip = clip;
        AudioSoundPlayer.Play();
        Destroy(AudioSoundPlayer.gameObject,clip.length+1);
    }
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="Number">关卡管理器中的音效序号</param>
    public void PlaySound(int Number)
    {
        var AudioSoundPlayer = new GameObject().AddComponent<AudioSource>();
        if (Sounds.Length < Number)
            return;
        var clip = Sounds[Number];
        AudioSoundPlayer.transform.name = string.Format("AudioSound-{0}", clip.name);
        AudioSoundPlayer.playOnAwake = false;
        AudioSoundPlayer.clip = clip;
        AudioSoundPlayer.Play();
        Destroy(AudioSoundPlayer.gameObject, clip.length + 1);
    }
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="Name">关卡管理器中的音效名称</param>
    public void PlaySound(string Name)
    {
        if (Sounds == null||Sounds.Length<=0)
            return;
        AudioClip clip = null;
        foreach (var item in Sounds)
        {
            if (item.name == Name)
            {
                clip = item;
            }
        }
        var AudioSoundPlayer = new GameObject().AddComponent<AudioSource>();
        AudioSoundPlayer.transform.name = string.Format("AudioSound-{0}", clip.name);
        AudioSoundPlayer.playOnAwake = false;
        AudioSoundPlayer.clip = clip;
        AudioSoundPlayer.Play();
        Destroy(AudioSoundPlayer.gameObject, clip.length + 1);
    }

    IEnumerator PlayTextInfo(TextMeshPro textMesh, TextAsset infoAsset, Action CallBack)
    {
        if (textMesh == null)
            yield return null;
        var Info = infoAsset.text.Split('\n');
        foreach (var item in Info)
        {
            yield return new WaitForSeconds(item.Length * 0.25f);
            textMesh.text = item;
            yield return new WaitForSeconds(item.Length * 0.25f);
        }
        yield return new WaitForSeconds(1f);
        textMesh.text = null;
        CallBack?.Invoke();

    }
    private Coroutine PlayTextMeshInfo_Coroutine;
    public void PlayLevelTextInfo(TextMeshPro textMesh, TextAsset infoAsset, Action CallBack)
    {
        if (PlayTextMeshInfo_Coroutine != null)
        {
            StopCoroutine(PlayTextMeshInfo_Coroutine);
        }

        PlayTextMeshInfo_Coroutine = StartCoroutine(PlayTextInfo(textMesh, infoAsset, CallBack));
    }


    IEnumerator PlayTextInfo(TextMeshProUGUI textMesh, TextAsset infoAsset, Action CallBack)
    {
        if (textMesh == null)
            yield return null;
        var Info = infoAsset.text.Split('\n');
        foreach (var item in Info)
        {
            yield return new WaitForSeconds(item.Length * 0.25f);
            textMesh.text = item;
            yield return new WaitForSeconds(item.Length * 0.25f);
        }
        yield return new WaitForSeconds(1f);
        textMesh.text = null;
        CallBack?.Invoke();

    }
    private Coroutine PlayTextMeshUGUIInfo_Coroutine;
    public void PlayLevelTextInfo(TextMeshProUGUI textMesh, TextAsset infoAsset, Action CallBack)
    {
        if (PlayTextMeshUGUIInfo_Coroutine != null)
        {
            StopCoroutine(PlayTextMeshUGUIInfo_Coroutine);
        }

        PlayTextMeshUGUIInfo_Coroutine = StartCoroutine(PlayTextInfo(textMesh, infoAsset, CallBack));
    }
    //播放TimeLine
    public void PlayTimeLine(PlayableDirector director)
    {
        director.Play();
    }

    public void WaitCall(Action CallBack, float time)
    {
        StartCoroutine(DelayedCall(CallBack, time));
    }

    private IEnumerator DelayedCall(Action CallBack, float time)
    {
        yield return new  WaitForSecondsRealtime(time);
        CallBack?.Invoke();
    }
    /// <summary>
    /// 退出关卡
    /// </summary>
    public void QuitLevel()
    {
        GameManager.INS.QuitLevel();
    }

}
