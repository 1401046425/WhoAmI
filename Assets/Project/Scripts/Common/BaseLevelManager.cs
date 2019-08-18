using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private bool IsBGMLoop;
    public AudioSource BGM_Player { get; set; }//背景音乐播放器

    private AudioSource CreateBGM_Player()
    {
        if (BGM_Player != null)
        {
            StartCoroutine(FadeOutBGM(BGM_Player));
        }
        BGM_Player = new GameObject().AddComponent<AudioSource>();
        BGM_Player.playOnAwake = false;
        if (IsBGMLoop)
            BGM_Player.loop = true;
    BGM_Player.transform.name = string.Format("BGMAudioPlayer-NullMusic");
    return BGM_Player;
    }

    private void Awake()
    {

    }

    public void InitLevelManager()
    {
        
    }

    IEnumerator FadeBGM(AudioSource Audio,AudioClip audioClip,Action callback)
    {
        while (Audio.volume>0)
        {
            Audio.volume -=  Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        Audio.clip = audioClip;
            callback.Invoke();
        Audio.volume = 0;
        while (Audio.volume<1)
        {
            Audio.volume += Time.fixedDeltaTime;   
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator FadeOutBGM(AudioSource Audio)
    {
        while (Audio.volume>0)
        {
            Audio.volume -=  Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        Destroy(Audio.gameObject);
    }
    IEnumerator FadeInBGM(AudioSource Audio,AudioClip audioClip,Action callback)
    {
        Audio.clip = audioClip;
        callback.Invoke();
        Audio.volume = 0;
        while (Audio.volume<1)
        {
            Audio.volume += Time.fixedDeltaTime;   
            yield return new WaitForFixedUpdate();
        }
    }
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="Number"关卡管理器中的音乐序号></param>
    public void PlayBGM(int Number)
    {
        if (BackGroundMusics.Length < Number)
            return;
        var clip = BackGroundMusics[Number];
        if (clip == null)
            return;
        StartCoroutine( FadeInBGM(CreateBGM_Player(),clip,BGM_Player.Play));

        BGM_Player.transform.name= string.Format("BGMAudioPlayer-{0}",clip.name);
    }
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="Name">关卡管理器中的音乐名称</param>
    public void PlayBGM(string Name)
    {

        AudioClip clip = null;
        foreach (var item in BackGroundMusics)
        {
            if (item.name == Name)
            {
                clip = item;
            }
        }
        if (clip == null)
            return;
        StartCoroutine( FadeInBGM(CreateBGM_Player(),clip,BGM_Player.Play));
        BGM_Player.transform.name = string.Format("BGMAudioPlayer-{0}", clip.name);
    }
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="clip">音乐Clip</param>
    public void PlayBGM(AudioClip clip)
    {
        if (clip == null)
            return;
        StartCoroutine( FadeInBGM(CreateBGM_Player(),clip,BGM_Player.Play));
        BGM_Player.transform.name = string.Format("BGMAudioPlayer-{0}", clip.name);
    }
/// <summary>
/// 关闭背景音乐
/// </summary>
    public void StopBGM()
    {
        StartCoroutine( FadeOutBGM(BGM_Player));
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
