using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "추격자 설정", menuName = "Chaser Data")]
public class ChaserData : ScriptableObject
{
    [Header("추격자 주기 레벨 스케일링")]
    public float hitTimeDecreaseValue;
    public float hitTimeDecreaseScore;
    public int hitTimeDecreaseCount;
}
