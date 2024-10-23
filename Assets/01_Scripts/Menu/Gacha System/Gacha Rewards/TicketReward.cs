using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "티켓보상", menuName = "가챠/가챠 보상/티켓", order = int.MinValue)]
public class TicketReward : GachaReward
{
    public override string GetName(int rate) => rate + " 티켓";

    public override void GetReward(int rate)
    {
        Currency.Ticket += rate;
    }
}
