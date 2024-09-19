using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameResultUI : MonoBehaviour
{
    public event Action OnOpen;
    public event Action OnClose;

    [SerializeField] private GameObject _layer;
    [SerializeField] private GameObject _bestScoreMassage;
    [SerializeField] private TextMeshProUGUI _distanceScoreText;
    [SerializeField] private TextMeshProUGUI _gemScoreText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _crystalText;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _crystalResurrectionButton;
    [SerializeField] private Button _adResurrectionButton;

    private void Awake()
    {
        _closeButton.onClick.AddListener(CloseUI);
        _crystalResurrectionButton.onClick.AddListener(TryCrystalResurrection);
        _adResurrectionButton.onClick.AddListener(TryAdResurrection);
    }

    public void OpenUI()
    {
        _layer.SetActive(true);
        _distanceScoreText.text = "+ 거리 점수 : " + GameManager.Instance.DistanceScore;
        _gemScoreText.text = "+ 젬 점수 : " + GameManager.Instance.GemScore;
        _scoreText.text = "총 점수 : " + GameManager.Instance.Score;
        _goldText.text = "골드 획득 : " + GameManager.Instance.RewardGold;
        _crystalText.text = "크리스탈 획득 : " + GameManager.Instance.RewardCrystal;
        _bestScoreMassage.SetActive(GameManager.Instance.isBestScore);

        if (TutorialManager.isActive)
        {
            DisableResurrection();
        }

        OnOpen?.Invoke();
    }

    public void CloseUI()
    {
        _layer.SetActive(false);

        AddGameResult();

        OnClose?.Invoke();
    }

    public void TryAdResurrection()
    {

    }

    public void TryCrystalResurrection()
    {
        if (Currency.Crystal >= 5)
        {
            Currency.Crystal -= 5;
            Resurrection();
        }
        else
        {
            ResurrectionFailed();
        }
    }

    private void Resurrection()
    {
        DisableResurrection();

        _layer.SetActive(false);
        PlayerSpawner.Instance.OnSpawned += (player) => player.GetComponent<BuffSystem>().AddBuff(new ObstacleShieldBuff(null, 1));
        PlayerSpawner.Instance.SpawnPlayer();
    }

    private void ResurrectionFailed()
    {

    }

    private void DisableResurrection()
    {
        _adResurrectionButton.gameObject.SetActive(false);
        _crystalResurrectionButton.gameObject.SetActive(false);
    }

    private void AddGameResult()
    {
        GameManager.Instance.isBestScore = GameManager.Instance.Score > GameManager.Instance.BestScore;
        if (GameManager.Instance.isBestScore)
        {
            GameManager.Instance.BestScore = GameManager.Instance.Score;
        }
        if (Currency.Ticket > 0)
        {
            Currency.Ticket--;
            GameManager.Instance.RewardGoldAdded = (int)(GameManager.Instance.RewardGold * 0.5f);
            GameManager.Instance.RewardCrystalAdded = 1;
        }
        else
        {
            GameManager.Instance.RewardGoldAdded = 0;
            GameManager.Instance.RewardCrystalAdded = 0;
        }
        Currency.Gold += GameManager.Instance.RewardGold;
        Currency.Crystal += GameManager.Instance.RewardCrystal;
    }
}
