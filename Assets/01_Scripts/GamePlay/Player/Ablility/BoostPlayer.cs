using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPlayer : Player
{
    public override int MaxLevel => 5;
    public override int UpgradeCost => 500;
    public override int UnlockCost => 150;
    public override string CarInfo
    {
        get
        {
            if (PlayerLevel == 0)
            {
                return string.Format("젬을 한칸 채울 때 마다\n최대 {0}초 부스트를 얻습니다.", GetBoostTime(MaxLevel));
            }
            else
            {
                return string.Format("젬을 한칸 채울 때 마다\n{0}초 부스트를 얻습니다.", GetBoostTime(PlayerLevel));
            }
        }
    }

    [SerializeField] private float _boostSpeed;
    [SerializeField] private GameObject _shieldEffect;
    private int _lastBoostGazy;


    protected override void Reset()
    {
        base.Reset();
        _buffSystem = GetComponent<BuffSystem>();
    }

    private float GetBoostTime(int playerLevel)
    {
        return playerLevel / 5f;
    }

    protected override void Update()
    {
        base.Update();

        if ((int)BoostGazy > _lastBoostGazy)
        {
            _buffSystem.AddBuff(new BoostBuff(_boostSpeed, GetBoostTime(PlayerLevel)));
            _buffSystem.AddBuff(new ObstacleShieldBuff(_shieldEffect, 1.2f));
        }
        _lastBoostGazy = (int)BoostGazy;
    }
}