using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : Player
{
    float chargeValue;

    public override void ChargeBoost(float value)
    {
        base.ChargeBoost(value);
        chargeValue = Mathf.Min(chargeValue + value / (10 - PlayerLevel), 1);

        if (chargeValue == 1 && CurruntHealth < MaxHealth)
        {
            chargeValue = 0;
            TakeHeal();
        }
    }
}
