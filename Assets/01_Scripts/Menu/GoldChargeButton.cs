using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldChargeButton : MonoBehaviour
{
    public void TryShowGoldAd()
    {
        //AdManager.Instance.ShowAds(GiveReward,Fail);
    }

    private void GiveReward()
    {
        Currency.Gold += 1000;
    }

    private void Fail()
    {

    }
}
