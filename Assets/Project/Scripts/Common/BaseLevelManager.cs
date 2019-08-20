using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class BaseLevelManager : MonoBehaviour
{
    public PlayableDirector[] PLABDirectiors;
    public AudioClip[] BackGroundMusics;//背景音乐Clip
    public AudioClip[] Sounds;//音效Clip
    public TextAsset[] InfoTextAsset;//文本资源
    public TextMeshPro[] InfoText_View;//TextMeshPro文本显示
    public TextMeshProUGUI[] InfoTextUGUI_View;//TextMeshProUI文本显示
    [SerializeField] private bool IsBGMLoop;
    public static BaseLevelManager INS;
    public AudioSource BGM_Player { get; set; }//背景音乐播放器
private Dictionary<string,AudioSource>SoundPlayer=new Dictionary<string, AudioSource>();
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
        INS = this;
        GameManager.INS.OnGameStateChange += OnStatusChange;
    }

    private void OnStatusChange(GameStatus obj)
    {
        if (obj == GameStatus.Pause)
        {
            PauseBGM();
        }

        if (obj == GameStatus.Running)
        {
            UnPauseBGM();
        }
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
    IEnumerator PauseBGM(AudioSource Audio)
    {
        if (!Audio)
            yield return null;
        while (Audio.volume>0)
        {
            Audio.volume -=  0.1f;
            yield return new WaitForSecondsRealtime(0.03f);
        }
        Audio.Pause();
    }
    IEnumerator UnPauseBGM(AudioSource Audio)
    {
        Audio.volume = 0;
        while (Audio.volume<1)
        {
            Audio.volume += 0.1f;   
            yield return new WaitForSecondsRealtime(0.03f);
        }
        Audio.UnPause();
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
        if(BGM_Player)
         StartCoroutine( FadeOutBGM(BGM_Player));
    }
/// <summary>
/// 暂停背景音乐
/// </summary>
public void PauseBGM()
 {
     if(BGM_Player)
        StartCoroutine(PauseBGM(BGM_Player));
 }
public void UnPauseBGM()
{
    if(BGM_Player)
        StartCoroutine(UnPauseBGM(BGM_Player));
}
/// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="clip">音效Clip</param>
    public void PlaySound(AudioClip clip)
    {
        AudioSource AudioSoundPlayer=null;
        try
        {
            AudioSoundPlayer = SoundPlayer[clip.name];
        }
        catch (Exception e)
        {
            AudioSoundPlayer  = new GameObject().AddComponent<AudioSource>();
        }
         
        AudioSoundPlayer.transform.name = string.Format("AudioSound-{0}", clip.name);
        AudioSoundPlayer.playOnAwake = false;
        AudioSoundPlayer.clip = clip;
        AudioSoundPlayer.Play();
        if(!SoundPlayer.ContainsKey(clip.name))
            SoundPlayer.Add(clip.name,AudioSoundPlayer);
       // Destroy(AudioSoundPlayer.gameObject,clip.length+1);
    }
public enum SoundType
{
    Playing,
    Stoped,
}
/// <summary>
/// 获取音效播放状态
/// </summary>
/// <param name="Name"></param>
/// <returns></returns>
    public SoundType GetSoundState(string Name)
{
    if (SoundPlayer.ContainsKey(Name))
    {
        var SDPlayer = SoundPlayer[Name];
        if (SDPlayer.isPlaying)
            return SoundType.Playing;
    }
    return SoundType.Stoped;
}

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="Number">关卡管理器中的音效序号</param>
    public void PlaySound(int Number)
    {

        if (Sounds.Length < Number)
            return;
        var clip = Sounds[Number];
        AudioSource AudioSoundPlayer=null;
        try
        {
            AudioSoundPlayer = SoundPlayer[clip.name];
        }
        catch (Exception e)
        {
            AudioSoundPlayer  = new GameObject().AddComponent<AudioSource>();
        }
        AudioSoundPlayer.transform.name = string.Format("AudioSound-{0}", clip.name);
        AudioSoundPlayer.playOnAwake = false;
        AudioSoundPlayer.clip = clip;
        AudioSoundPlayer.Play();
        if(!SoundPlayer.ContainsKey(clip.name))
            SoundPlayer.Add(clip.name,AudioSoundPlayer);
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
        
        AudioSource AudioSoundPlayer=null;
        try
        {
            AudioSoundPlayer = SoundPlayer[clip.name];
        }
        catch (Exception e)
        {
            AudioSoundPlayer  = new GameObject().AddComponent<AudioSource>();
        }
        AudioSoundPlayer.transform.name = string.Format("AudioSound-{0}", clip.name);
        AudioSoundPlayer.playOnAwake = false;
        AudioSoundPlayer.clip = clip;
        AudioSoundPlayer.Play();
        if(!SoundPlayer.ContainsKey(clip.name))
            SoundPlayer.Add(clip.name,AudioSoundPlayer);
    }
/// <summary>
/// 停止音效
/// </summary>
/// <param name="Name"></param>
    public void StopSound(string Name)
    {
        if(SoundPlayer.ContainsKey(Name))
            SoundPlayer[Name].Stop();
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
