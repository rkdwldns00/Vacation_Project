using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "가챠 테이블", menuName = "가챠/가챠 테이블", order = int.MinValue)]
public class GachaRewardTable : ScriptableObject
{
    public List<RewardData> rewards;

    public RewardData GetRandomReward()
    {
        int sum = 0;
        foreach (var reward in rewards)
        {
            sum += reward.probability;
        }

        int random = UnityEngine.Random.Range(0, sum);

        sum = 0;
        foreach (var rewardData in rewards)
        {
            sum += rewardData.probability;
            if (sum > random)
            {
                return rewardData;
            }
        }

        return new RewardData();
    }
}

[Serializable]
public struct RewardData
{
    public GachaReward reward;
    public int probability;
    public int rewardRate;
}