using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePlayer : Player
{
    public override int MaxLevel => 10;
    public override int UpgradeCost => base.UpgradeCost;
    public override int UnlockCost => 125;
    public override string CarInfo
    {
        get
        {
            if (PlayerLevel == 0)
            {
                return string.Format("점수 획득량이 최대 {0}% 늘어납니다.", Mathf.Round(GetBonus(MaxLevel) * 100 - 100));
            }
            else
            {
                return string.Format("점수 획득량이 {0}% 늘어납니다.", Mathf.Round(GetBonus(PlayerLevel) * 100 - 100));
            }
        }
    }

    float GetBonus(int playerLevel)
    {
        return 1f + playerLevel * 0.07f;
    }

    public override void ChargeBoost(float value)
    {
        BoostGazy += value;
        GameManager.Instance.GemScore += (int)(10 * GetBonus(PlayerLevel));
        RunOnChangedBoostGazy(value);
    }

    protected override void UpdateDistanceScore()
    {
        GameManager.Instance.DistanceScore = (int)(transform.position.z * GetBonus(PlayerLevel));
    }
}
