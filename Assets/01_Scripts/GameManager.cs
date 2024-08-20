using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject g = new GameObject("GameManager");
                DontDestroyOnLoad(g);
                _instance = g.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    public int DistanceScore { get; set; }
    public int GemScore { get; set; }
    public int Score { get => DistanceScore + GemScore; }
    public float RewardGoldRate { set; private get; }
    public float RewardGoldAdded { set; private get; }
    public float RewardCrystalAdded { set; private get; }
    public int RewardGold => (int)(Score * RewardGoldRate + RewardGoldAdded);
    public int RewardCrystal => (int)(Score * 0.001f + RewardCrystalAdded);
    public bool isBestScore { get; set; }

    public int BestScore
    {
        get => PlayerPrefs.GetInt("BestScore");
        set => PlayerPrefs.SetInt("BestScore", value);
    }

    public int PlayerModelId
    {
        get => PlayerPrefs.GetInt("PlayerModelId");
        set => PlayerPrefs.SetInt("PlayerModelId", value);
    }
}
