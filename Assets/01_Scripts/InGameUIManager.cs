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
    [SerializeField] private Image _playerDamagedEffectImg;

    [Header("Result UI")]
    [SerializeField] private GameResultUI _gameResultUI;

    private void Awake()
    {
        PlayerSpawner.Instance.OnSpawned += OnPlayerSpawned;

        _gameResultUI.OnClose += () => SceneManager.LoadScene("MenuScene");
    }

    private void Update()
    {
        UpdateScoreUI();
    }

    private void OnPlayerSpawned(Player player)
    {
        player.OnDamaged += UpdatePlayerHpUI;
        player.OnDamaged += ShowDamagedEffect;
        player.OnDie += ActiveGameResultUI;

        for (int i = 0; i < Player.Instance.MaxHealth; i++)
        {
            GameObject lossHp = Instantiate(_playerLossHpPrefab, _playerLossHpParent);
            GameObject hp = Instantiate(_playerHpPrefab, _playerHpParent);
            _playerHps.Add(hp);
        }

        player.OnDie += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        foreach(var hp in _playerHps)
        {
            Destroy(hp);
        }
        _playerHps.Clear();
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
        for (int i = 0; i < _playerHps.Count; i++)
        {
            _playerHps[i].SetActive(i < Player.Instance.CurruntHealth);
        }
    }

    private void ShowDamagedEffect()
    {
        StartCoroutine(ShowDamagedEffectCoroutine());
    }

    private IEnumerator ShowDamagedEffectCoroutine()
    {
        _playerDamagedEffectImg.color = new Color(1, 0, 0, 0.5f);

        while (_playerDamagedEffectImg.color.a > 0)
        {
            _playerDamagedEffectImg.color = new Color(1, 0, 0, _playerDamagedEffectImg.color.a - Time.deltaTime / 1.5f);
            yield return null;
        }

        yield break;
    }
}
