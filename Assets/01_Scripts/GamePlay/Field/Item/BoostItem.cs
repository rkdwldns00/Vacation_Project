using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoostItem : Item
{
    [SerializeField] private float _boostSpeed;
    [SerializeField] private float _durationTime;
    [SerializeField] private GameObject _shieldEffectPrefab;

    protected override void UseItem(Player p)
    {
        ObstacleShieldBuff obstacleShieldBuff = new ObstacleShieldBuff(_shieldEffectPrefab, _durationTime + 1);
        p.GetComponent<BuffSystem>().AddBuff(obstacleShieldBuff);

        BoostBuff boostBuff = new BoostBuff(_boostSpeed, _durationTime);
        p.GetComponent<BuffSystem>().AddBuff(boostBuff);
    }
}
