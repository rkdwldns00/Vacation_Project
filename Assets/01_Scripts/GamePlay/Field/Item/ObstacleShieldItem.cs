using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleShieldItem : Item
{
    [SerializeField] private GameObject _shieldEffectPrefab;
    [SerializeField] private float _durationTime;

    protected override void UseItem(Player p)
    {
        ObstacleShieldBuff obstacleShieldBuff = new ObstacleShieldBuff(_shieldEffectPrefab, _durationTime);
        p.GetComponent<BuffSystem>().AddBuff(obstacleShieldBuff);
    }
}
