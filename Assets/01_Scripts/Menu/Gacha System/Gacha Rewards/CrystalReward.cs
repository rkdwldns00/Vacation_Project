using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "크리스탈보상", menuName = "가챠/가챠 보상/크리스탈", order = int.MinValue)]
public class CrystalReward : GachaReward
{
    public override void GetReward(int rate)
    {
        Currency.Crystal += rate;
    }
}
