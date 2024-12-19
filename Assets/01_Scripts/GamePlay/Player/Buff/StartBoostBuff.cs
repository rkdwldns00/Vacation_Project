using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoostBuff : BoostBuff
{
    private float _durationDistance;
    private ObstacleShieldBuff _obstacleShieldBuff;

    public StartBoostBuff(float boostSpeed, float durationDistance) : base(boostSpeed, 0)
    {
        _durationDistance = durationDistance;
    }

    public override void StartBuff(BuffSystem buffSystem)
    {
        base.StartBuff(buffSystem);
        _obstacleShieldBuff = buffSystem.AddBuff(new ObstacleShieldBuff(null, 1000)) as ObstacleShieldBuff;
    }

    public override void EndBuff(BuffSystem buffSystem)
    {
        base.EndBuff(buffSystem);
        buffSystem.RemoveBuff(_obstacleShieldBuff);
    }

    protected override void DurationTimeCheckHandler()
    {
        if (GameManager.Instance.Score >= _durationDistance)
        {
            buffSystem.RemoveBuff(this);
        }
    }
}
