using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//sing GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance
    {
        get
        {
            instance = new GameObject("AdManager").AddComponent<AdManager>();
            DontDestroyOnLoad(instance);
            instance.InitAds();
            return instance;
        }
    }
    private static AdManager instance;

    //private RewardedAd rewardedAd;
    private Action adSucceedCallBack;

    private void InitAds()
    {
        string adUnitId;

#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            adUnitId = "unexpected_platform";
#endif

        //RewardedAd.Load(adUnitId, new AdRequest(), LoadCallback);
    }

    /*private void LoadCallback(RewardedAd rewardedAd, LoadAdError loadAdError)
    {
        if (rewardedAd != null)
        {
            this.rewardedAd = rewardedAd;
            Debug.Log("광고 로드 성공");
        }
        else
        {
            Debug.Log(loadAdError.GetMessage());
        }

    }*/

    public void ShowAds(Action succeedCallBack,Action failedCallBack)
    {
        /*if (rewardedAd.CanShowAd())
        {
            adSucceedCallBack = succeedCallBack;
            rewardedAd.Show(GetReward);
        }
        else
        {*/
            failedCallBack?.Invoke();
        //}
    }

    /*private void GetReward(Reward reward)
    {
        adSucceedCallBack?.Invoke();
        adSucceedCallBack = null;
        InitAds();
    }*/
}
