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
    [SerializeField] private GameObject _playerLossBoostGazyPrefab;
    [SerializeField] private Transform _playerLossBoostGazyParent;
    [SerializeField] private GameObject _playerBoostGazyPrefab;
    [SerializeField] private Transform _playerBoostGazyParent;
    [SerializeField] private List<Image> _playerBoostGazys = new List<Image>();


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
        player.OnChangedHealth += UpdatePlayerHpUI;
        player.OnDamaged += ShowDamagedEffect;
        player.OnDie += ActiveGameResultUI;
        player.OnChangedBoostGazy += UpdatePlayerBoostGazyUI;

        for (int i = 0; i < Player.Instance.MaxHealth; i++)
        {
            GameObject lossHp = Instantiate(_playerLossHpPrefab, _playerLossHpParent);
            GameObject hp = Instantiate(_playerHpPrefab, _playerHpParent);
            _playerHps.Add(hp);
        }
        for (int i = 0; i < Player.Instance.MaxBoostGazy; i++)
        {
            GameObject lossBoost = Instantiate(_playerLossBoostGazyPrefab, _playerLossBoostGazyParent);
            GameObject Boost = Instantiate(_playerBoostGazyPrefab, _playerBoostGazyParent);
            _playerBoostGazys.Add(Boost.GetComponent<Image>());
        }

        player.OnDie += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        foreach (var hp in _playerHps)
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
        _currentScoreText.text = "Score : " + GameManager.Instance.Score;

        if (GameManager.Instance.Score > GameManager.Instance.BestScore)
        {
            _bestScoreRecord.SetActive(true);
        }
    }

    private void UpdatePlayerBoostGazyUI(float delta)
    {
        for (int i = 0; i < _playerBoostGazys.Count; i++)
        {
            _playerBoostGazys[i].fillAmount = Player.Instance.BoostGazy - i;
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
