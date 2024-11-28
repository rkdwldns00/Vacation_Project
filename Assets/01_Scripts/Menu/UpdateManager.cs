using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

/* 코드 작성자 : 강지운 */

public class UpdateManager : MonoBehaviour
{
    private void Awake()
    {
        RealtimeEventHandler.Instance.OnUpdate += OnUpdate;
    }

    private void OnUpdate(float preVersion)
    {
        //차량 리워크 업데이트
        if (preVersion <= 0.119)
        {
            //리워크 이전 차량 가격
            int[] unlockCost = { 0, 200, 150, 150, 50, 200, 100, 50 };
            Func<int, int>[] upgradeCost =
            {
                (level) => 0,
                (level) => level * unlockCost[1],
                (level) => level * unlockCost[2],
                (level) => level * unlockCost[3],
                (level) => level * unlockCost[4],
                (level) => level * unlockCost[5],
                (level) => 500,
                (level) => level * unlockCost[7]
            };

            for (int i = 1; i < 8; i++)
            {
                int level = PlayerPrefs.GetInt("Player" + (i + 1) + "Level");
                if (level > 0)
                {
                    Currency.Crystal += unlockCost[i];
                }

                for (int j = 1; j < level; j++)
                {
                    Currency.Gold += upgradeCost[i](j);
                }

                PlayerPrefs.SetInt("Player" + (i + 1) + "Level", 0);
            }
        }

        GameManager.Instance.PlayerModelId = 0;
    }
}
