using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : Player
{
    private float _chargeValue;

    public override void ChargeBoost(float value)
    {
        base.ChargeBoost(value);
        _chargeValue = Mathf.Min(_chargeValue + value / (10 - PlayerLevel), 1);

        if (_chargeValue == 1 && CurruntHealth < MaxHealth)
        {
            _chargeValue = 0;
            TakeHeal();
        }
    }
}
