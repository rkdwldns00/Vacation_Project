using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoostItem : Item
{
    [SerializeField] private float _boostSpeed;
    [SerializeField] private float _durationTime;
    [SerializeField] private GameObject _shieldEffectPrefab;

    protected override void UseItem(BuffSystem buffSystem)
    {
        ObstacleShieldBuff obstacleShieldBuff = new ObstacleShieldBuff(_shieldEffectPrefab, _durationTime + 1);
        buffSystem.AddBuff(obstacleShieldBuff);

        BoostBuff boostBuff = new BoostBuff(_boostSpeed, _durationTime);
        buffSystem.AddBuff(boostBuff);
    }
}
