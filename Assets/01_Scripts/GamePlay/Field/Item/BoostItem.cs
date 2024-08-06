using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoostItem : Item
{
    [SerializeField] private float _boostSpeed;
    [SerializeField] private float _durationTime;
    [SerializeField] private GameObject _obstacleShieldEffectPrefab;

    protected override void UseItem(Player p)
    {
        ObstacleShield o = p.GetComponent<ObstacleShield>();
        if (o == null) o = p.AddComponent<ObstacleShield>();

        o.SetObstacleShieldData(_durationTime + 1, _obstacleShieldEffectPrefab);

        BoostBuff boostBuff = new BoostBuff(_boostSpeed, _durationTime);
        p.GetComponent<BuffSystem>().AddBuff(boostBuff);
    }
}
