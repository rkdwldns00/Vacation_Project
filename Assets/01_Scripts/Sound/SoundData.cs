using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound Data", menuName = "Sound Data")]
public class SoundData : ScriptableObject
{
    [Header("BGM")]
    public AudioClip MenuBgm;
    public AudioClip GameBgm;

    [Header("SFX")]
    public AudioClip PlayerDamagedSfx;
    public AudioClip GetGemSfx;
    public AudioClip GetItemSfx;
    public AudioClip EngineSfx;
}
