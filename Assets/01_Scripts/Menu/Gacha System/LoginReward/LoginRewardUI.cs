using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginRewardUI : ManagedUI
{
    [Header("보상")]
    [SerializeField] private GachaRewardTable dailyReward;
    [SerializeField] private GachaRewardTable weeklyReward;
    [Header("내부 UI")]
    [SerializeField] private GameObject _layer;
    [SerializeField] private List<LoginRewardBoxButton> _rewardButtons;
    [SerializeField] private Button _openUIButton;
    [SerializeField] private Button _closeUIButton;
    [Header("외부 UI 연결")]
    [SerializeField] private GachaUI _gachaUI;

    private int RewardIndex
    {
        get => PlayerPrefs.GetInt("RewardIndex");
        set => PlayerPrefs.SetInt("RewardIndex", value);
    }

    private bool GetRewardToday
    {
        get => PlayerPrefs.GetInt("GetRewardToday") == 1;
        set => PlayerPrefs.SetInt("GetRewardToday", value ? 1 : 0);
    }


    public override void Awake()
    {
        base.Awake();

        RealtimeEventHandler.Instance.OnChangeDay += OnChangeDay;
        _openUIButton.onClick.AddListener(() => OpenUI(EUIType.Page));
        _closeUIButton.onClick.AddListener(CloseUI);
    }

    private void Start()
    {
        if (!GetRewardToday)
        {
            OpenUI(EUIType.Page);
        }
    }

    private void OnChangeDay()
    {
        GetRewardToday = false;
    }

    protected override void OnOpen()
    {
        for (int i = 0; i < _rewardButtons.Count; i++)
        {
            if (i == RewardIndex && !GetRewardToday)
            {
                _rewardButtons[i].Button.onClick.RemoveAllListeners();
                _rewardButtons[i].Button.onClick.AddListener(OnClickReward);
            }
            _rewardButtons[i].Button.interactable = i <= RewardIndex - (GetRewardToday ? 1 : 0);
            _rewardButtons[i].Image.enabled = i >= RewardIndex;
        }

        _layer.SetActive(true);
    }

    protected override void OnClose()
    {
        _layer.SetActive(false);
    }

    private void OnClickReward()
    {
        _rewardButtons[RewardIndex].Image.enabled = false;

        if (RewardIndex == _rewardButtons.Count - 1)
        {
            _gachaUI.SetReward(weeklyReward.GetRandomReward());
            _gachaUI.OpenUI(EUIType.Popup);
            CloseUI();

            RewardIndex = 0;
        }
        else
        {
            _gachaUI.SetReward(dailyReward.GetRandomReward());
            _gachaUI.OpenUI(EUIType.Popup);
            CloseUI();

            RewardIndex++;
        }

        GetRewardToday = true;
    }
}
