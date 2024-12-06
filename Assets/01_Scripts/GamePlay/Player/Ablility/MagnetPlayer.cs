using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPlayer : Player
{
    public override int MaxLevel => 10;
    public override int UpgradeCost => base.UpgradeCost;
    public override int UnlockCost => 150;
    public override string CarInfo
    {
        get
        {
            if (PlayerLevel == 0)
            {
                return string.Format("젬 한칸당 최대 {0}m의 자력을 얻습니다.", GetMagnetStrength(MaxLevel));
            }
            else
            {
                return string.Format("젬 한칸당 {0}m의 자력을 얻습니다.", GetMagnetStrength(PlayerLevel));
            }
        }
    }

    private MagnetBuff _magnetBuff;

    private float GetMagnetStrength(int playerLevel)
    {
        return playerLevel * 0.4f;
    }

    protected override void Start()
    {
        base.Start();
        _magnetBuff = BuffSystem.AddBuff(new MagnetBuff(0)) as MagnetBuff;
    }

    protected override void Update()
    {
        base.Update();
        _magnetBuff.MagnetStrength = GetMagnetStrength(PlayerLevel) * (int)BoostGazy;
    }
}
