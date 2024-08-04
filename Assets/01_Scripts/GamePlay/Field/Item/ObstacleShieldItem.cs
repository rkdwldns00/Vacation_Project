using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleShieldItem : Item
{
    [SerializeField] private GameObject _obstacleShieldEffectPrefab;
    [SerializeField] private float _durationTime;

    protected override void UseItem(Player p)
    {
        ObstacleShield o = p.GetComponent<ObstacleShield>();
        if (o == null) o = p.AddComponent<ObstacleShield>();

        o.SetObstacleShieldData(_durationTime, _obstacleShieldEffectPrefab);
    }
}
