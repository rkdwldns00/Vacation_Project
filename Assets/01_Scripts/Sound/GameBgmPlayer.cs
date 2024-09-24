using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBgmPlayer : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.SoundData.GameBgm, SoundType.BGM);
    }
}
