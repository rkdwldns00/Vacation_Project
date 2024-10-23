using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "골드보상", menuName = "가챠/가챠 보상/골드", order = int.MinValue)]
public class GoldReward : GachaReward
{
    public override string GetName(int rate) => rate.ToString() + " 골드";

    public override void GetReward(int rate)
    {
        Currency.Gold += rate;
    }
}
