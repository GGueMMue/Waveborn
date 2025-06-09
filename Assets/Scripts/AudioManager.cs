using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmSource;
    AudioHighPassFilter bgmHighPassFilter;

    [Header("SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxSources;
    int channelIndex;

    public enum SFX { 
        DEAD = 0,
        HIT0, HIT1,
        LEVELUP,
        LOSE,
        MELEE0, MELEE1,
        RANGE,
        SELECT,
        WIN
    }

    private void Awake()
    {
        instance = this;
        Init();
    }

    public void Init()
    {
        GameObject bgmObj = new GameObject("BGMPlayer");
        bgmObj.transform.parent = transform;
        bgmSource = bgmObj.AddComponent<AudioSource>();
        bgmSource.playOnAwake = false;
        bgmSource.loop = true;
        bgmSource.volume = bgmVolume;
        bgmSource.clip = bgmClip;
        bgmHighPassFilter = Camera.main.GetComponent<AudioHighPassFilter>();

        GameObject sfxObj = new GameObject("SFXPlayer");
        sfxObj.transform.parent = transform;
        sfxSources = new AudioSource[channels];

        for(int i = 0; i < channels; i++)
        {
            sfxSources[i] = sfxObj.AddComponent<AudioSource>();
            sfxSources[i].playOnAwake=false;
            sfxSources[i].volume = sfxVolume;
            sfxSources[i].bypassListenerEffects = true;
        }
    }
    public void BGMEffect(bool isPlay)
    {
        bgmHighPassFilter.enabled = isPlay;
    }

    public void PlayBGM(bool isPlay)
    {
        if (isPlay)
        {
            bgmSource.Play();
        }
        else
        {
             bgmSource.Stop();
        }
    }
    public void PlaySFX(SFX sfx)
    {
        for(int i = 0;i < channels;i++)
        {
            int loopIndex = (i + channelIndex) % sfxSources.Length;
            if (sfxSources[loopIndex].isPlaying)
                continue;

            int randIndex = 0;
            if(sfx == SFX.HIT0 || sfx == SFX.HIT1)
            {
                randIndex = Random.Range((int)SFX.HIT0, (int)SFX.HIT1 + 1);
            }

            if (sfx == SFX.MELEE0 || sfx == SFX.MELEE1)
            {
                randIndex = Random.Range((int)SFX.MELEE0, (int)SFX.MELEE1 + 1);
            }

            channelIndex = loopIndex;

            sfxSources[loopIndex].clip = sfxClips[(int)sfx];
            sfxSources[loopIndex].Play();

            break;
        }
    }
}
