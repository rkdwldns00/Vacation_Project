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
            if(_instance == null)
            {
                GameObject g = new GameObject("GameManager");
                DontDestroyOnLoad(g);
                _instance = g.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    public int Score { get; set; }
    public bool isHighScore { get; set; }

    public int HighScore
    {
        get => PlayerPrefs.GetInt("HighScore");
        set => PlayerPrefs.SetInt("HighScore", value);
    }
}
