using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    //public static SoundManager Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            GameObject g = new GameObject("Sound Manager");
    //            DontDestroyOnLoad(g);
    //            instance = g.AddComponent<SoundManager>();
    //        }
    //        return instance;
    //    }
    //}

    [Header("Audio Source")]
    [SerializeField] private AudioSource _bgmAudioSource;
    [SerializeField] private AudioSource _sfxAudioSource;

    [Header("Sound Data")]
    public SoundData SoundData;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip audioClip, SoundType soundType = SoundType.SFX, bool isLoop = false)
    {
        if (soundType == SoundType.SFX)
        {
            if (isLoop)
            {
                GameObject go = new GameObject("loopAudioSource");
                AudioSource source = go.AddComponent<AudioSource>();
                source.loop = true;
                source.clip = audioClip;
                source.Play();
            }
            else
            {
                _sfxAudioSource.PlayOneShot(audioClip);
            }
            
        }
        else if (soundType == SoundType.BGM)
        {
            if (_bgmAudioSource.isPlaying) _bgmAudioSource.Stop();
            _bgmAudioSource.clip = audioClip;
            _bgmAudioSource.Play();
        }
    }

    public void StopBgm()
    {
        _bgmAudioSource.Stop();
    }
}

public enum SoundType
{
    BGM,
    SFX
}
