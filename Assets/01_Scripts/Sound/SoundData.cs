using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound Data", menuName = "Sound Data")]
public class SoundData : ScriptableObject
{
    [Header("BGM")]
    public AudioClip MenuBgm;
    public AudioClip GameBgm;
}
