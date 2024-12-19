using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* 코드 작성자
 * 부활, 공유, 보상 재화 구현 : 강지운
 * 게임 결과 UI 구현 : 이기환
 */
public class GameResultUI : MonoBehaviour
{
    public event Action OnOpen;
    public event Action OnClose;

    [SerializeField] private GameObject _layer;
    [SerializeField] private GameObject _bestScoreMassage;
    [SerializeField] private TextMeshProUGUI _distanceScoreText;
    [SerializeField] private TextMeshProUGUI _gemScoreText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _scoreGoldText;
    [SerializeField] private TextMeshProUGUI _ticketGoldText;
    [SerializeField] private TextMeshProUGUI _goldPlayerGoldText;
    [SerializeField] private TextMeshProUGUI _totalGoldText;
    [SerializeField] private TextMeshProUGUI _scoreCrystalText;
    [SerializeField] private TextMeshProUGUI _ticketCrystalText;
    [SerializeField] private TextMeshProUGUI _totalCrystalText;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _crystalResurrectionButton;
    [SerializeField] private Button _adResurrectionButton;
    [SerializeField] private Button _shareButton;

    private void Awake()
    {
        _closeButton.onClick.AddListener(CloseUI);
        _crystalResurrectionButton.onClick.AddListener(TryCrystalResurrection);
        _adResurrectionButton.onClick.AddListener(TryAdResurrection);
        _shareButton.onClick.AddListener(Share);

        OnClose += () => { SoundManager.Instance.StopBgm(); };
    }

    public void OpenUI()
    {
        _layer.SetActive(true);
        _distanceScoreText.text = "+ 거리 점수 : " + GameManager.Instance.DistanceScore;
        _gemScoreText.text = "+ 젬 점수 : " + GameManager.Instance.GemScore;
        _scoreText.text = "총 점수 : " + GameManager.Instance.Score;

        int scoreGold = (int)(GameManager.Instance.Score * GameManager.Instance.RewardGoldRate);
        int ticketGold = Currency.Ticket > 0 ? (int)((scoreGold + GameManager.Instance.GoldPlayerGoldAdded) * 0.5f) : 0;
        int goldPlayerGold = GameManager.Instance.GoldPlayerGoldAdded;
        int totalGold = scoreGold + ticketGold + goldPlayerGold;

        _scoreGoldText.text = "+ 점수 골드 획득 : " + scoreGold;
        _ticketGoldText.text = "+ 티켓 골드 획득 : " + ticketGold;
        _ticketGoldText.gameObject.SetActive(Currency.Ticket > 0);
        _goldPlayerGoldText.text = "+ 차량 골드 획득 : " + goldPlayerGold;
        _goldPlayerGoldText.gameObject.SetActive(goldPlayerGold > 0);
        _totalGoldText.text = "총 골드 획득 : " + totalGold;

        float scoreCrystal = GameManager.Instance.Score * GameManager.Instance.RewardCrystalRate;
        int ticketCrystal = (int)(scoreCrystal * 0.2f);
        int totalCrystal = (int)scoreCrystal + ticketCrystal;

        _scoreCrystalText.text = "+ 점수 크리스탈 획득 : " + (int)scoreCrystal;
        _ticketCrystalText.text = "+ 티켓 크리스탈 획득 : " + ticketCrystal;
        _ticketCrystalText.gameObject.SetActive(Currency.Ticket > 0);
        _totalCrystalText.text = "총 크리스탈 획득 : " + totalCrystal;

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
        //AdManager.Instance.ShowAds(Resurrection, ResurrectionFailed);
    }

    public void TryCrystalResurrection()
    {
        if (Currency.Crystal >= 30)
        {
            Currency.Crystal -= 30;
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
        PlayerSpawner.Instance.OnSpawned += (player) => player.ChargeBoost(1);
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

    private void Share()
    {
        PluginManager.Instance.Share("친구가 Street Racer 점수 " + GameManager.Instance.Score + "점을 기록했습니다!");
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
            GameManager.Instance.RewardGoldAdded += (int)(GameManager.Instance.RewardGold * 0.5f);
            GameManager.Instance.RewardCrystalAdded += (int)(GameManager.Instance.RewardCrystal * 0.2f);
        }
        Currency.Gold += GameManager.Instance.RewardGold;
        Currency.Crystal += GameManager.Instance.RewardCrystal;

        GameManager.Instance.RewardGoldAdded = 0;
        GameManager.Instance.GoldPlayerGoldAdded = 0;
        GameManager.Instance.RewardCrystalAdded = 0;
    }
}
