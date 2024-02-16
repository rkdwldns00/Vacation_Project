using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    private static InGameUIManager _instance;

    public static InGameUIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<InGameUIManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("InGameUIManager").AddComponent<InGameUIManager>();
                }
            }
            return _instance;
        }
    }

    [Header("Score UI")]
    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private GameObject _highScoreRecord;

    [Header("Player UI")]
    [SerializeField] private GameObject _playerHpPrefab;
    [SerializeField] private Transform _playerHpParent;
    [SerializeField] private List<GameObject> _playerHps = new List<GameObject>();

    [Header("Result UI")]
    [SerializeField] private GameResultUI _gameResultUI;

    private void Start()
    {
        for (int i=0; i<Player.Instance.MaxHealth; i++)
        {
            GameObject hp = Instantiate(_playerHpPrefab, _playerHpParent);
            _playerHps.Add(hp);
        }

        _gameResultUI.OnClose += () => SceneManager.LoadScene("MenuScene");
    }

    private void Update()
    {
        UpdateScoreUI();
        UpdatePlayerUI();
    }

    public void ActiveGameResultUI()
    {
        _gameResultUI.OpenUI();
    }

    private void UpdateScoreUI()
    {
        _currentScoreText.text = GameManager.Instance.Score + "m";

        if (GameManager.Instance.Score > GameManager.Instance.HighScore)
        {
            _highScoreRecord.SetActive(true);
        }
    }

    private void UpdatePlayerUI()
    {
        foreach (GameObject hp in _playerHps)
        {
            hp.SetActive(false);
        }

        for (int i=0; i<Player.Instance.CurruntHealth; i++)
        {
            _playerHps[i].SetActive(true);
        }
    }
}
