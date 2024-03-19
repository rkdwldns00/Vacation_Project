using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "플레이어 설정",menuName = "Player Setting",order = int.MinValue)]
public class PlayerSetting : ScriptableObject
{
    public float _minX;
    public float _maxX;
    public float _fallingSensorY;
}
