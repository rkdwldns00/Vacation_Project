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
    [SerializeField] private GameObject _bestScoreRecord;

    [Header("Player UI")]
    [SerializeField] private GameObject _playerLossHpPrefab;
    [SerializeField] private Transform _playerLossHpParent;
    [SerializeField] private GameObject _playerHpPrefab;
    [SerializeField] private Transform _playerHpParent;
    [SerializeField] private List<GameObject> _playerHps = new List<GameObject>();

    [Header("Result UI")]
    [SerializeField] private GameResultUI _gameResultUI;

    private void Start()
    {
        for (int i=0; i<Player.Instance.MaxHealth; i++)
        {
            GameObject lossHp = Instantiate(_playerLossHpPrefab, _playerLossHpParent);
            GameObject hp = Instantiate(_playerHpPrefab, _playerHpParent);
            _playerHps.Add(hp);
        }

        Player.Instance.OnDamaged += UpdatePlayerHpUI;
        Player.Instance.OnDie += ActiveGameResultUI;

        _gameResultUI.OnClose += () => SceneManager.LoadScene("MenuScene");
    }

    private void Update()
    {
        UpdateScoreUI();
    }

    public void ActiveGameResultUI()
    {
        _gameResultUI.OpenUI();
    }

    private void UpdateScoreUI()
    {
        _currentScoreText.text = GameManager.Instance.Score + "m";

        if (GameManager.Instance.Score > GameManager.Instance.BestScore)
        {
            _bestScoreRecord.SetActive(true);
        }
    }

    private void UpdatePlayerHpUI()
    {
        for (int i=0; i<_playerHps.Count; i++)
        {
            _playerHps[i].SetActive(i < Player.Instance.CurruntHealth);
        }
    }
}
