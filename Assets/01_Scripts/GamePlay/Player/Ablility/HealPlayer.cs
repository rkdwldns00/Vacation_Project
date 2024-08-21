using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : Player
{
    public override int MaxLevel => 10;
    public override int UpgradeCost => base.UpgradeCost;
    public override int UnlockCost => 15;
    public override string CarInfo
    {
        get
        {
            if (PlayerLevel == 0)
            {
                return string.Format("젬을 최소 {0}개 먹으면 체력이 회복됩니다.", Mathf.Ceil(1 / GetChargeValue(MaxLevel)));
            }
            else
            {
                return string.Format("젬을 {0}개 먹으면 체력이 회복됩니다.", Mathf.Ceil(1 / GetChargeValue(PlayerLevel)));
            }
        }
    }

    private float _chargeValue;

    private float GetChargeValue(int playerLevel)
    {
        return 0.01f + playerLevel / 1000f;
    }

    public override void ChargeBoost(float value)
    {
        base.ChargeBoost(value);
        _chargeValue = Mathf.Min(_chargeValue + value / GetChargeValue(PlayerLevel), 1);

        if (_chargeValue == 1 && CurruntHealth < MaxHealth)
        {
            _chargeValue = 0;
            TakeHeal();
        }
    }
}
