using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleShieldItem : Item
{
    [SerializeField] private GameObject _shieldEffectPrefab;
    [SerializeField] private float _durationTime;

    protected override void UseItem(BuffSystem buffSystem)
    {
        ObstacleShieldBuff obstacleShieldBuff = new ObstacleShieldBuff(_shieldEffectPrefab, _durationTime);
        buffSystem.AddBuff(obstacleShieldBuff);
    }
}
