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

        if (o == null)
        {
            o = p.AddComponent<ObstacleShield>();
        }

        o.AddDurationTime(_durationTime + 1, _obstacleShieldEffectPrefab);

        Boost b = p.GetComponent<Boost>();

        if (b == null)
        {
            b = p.AddComponent<Boost>();
        }

        b.SetBoostData(_boostSpeed, _durationTime);
    }
}
