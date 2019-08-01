using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBang : MonoBehaviour
{
    ParticleSystem BigBangFX;
    public AudioClip BigBangAudioClip;
    private void Awake()
    {
        BigBangFX = GetComponent<ParticleSystem>();
    }
    public void StartBigBang()
    {
        BigBangFX.Play();
        Level0Manager.INS.PlaySound(BigBangAudioClip);
        Level0Manager.INS.PlayBGM(0);
        Level0Manager.INS.PlayLevelTextInfo(Level0Manager.INS.InfoText_View[0], Level0Manager.INS.InfoTextAsset[0],  ((Level0Manager)Level0Manager.INS).PlayerLookAtEarthAnimator);
        Destroy(gameObject, 5f);
        // GameManager.INS.Sound.PlaySound(Path,"Level0");
    }
    public void PlayeAudio()
    {
        var AudioPlayer = gameObject.AddComponent<AudioSource>();
        AudioPlayer.clip = BigBangAudioClip;
        AudioPlayer.Play();
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
