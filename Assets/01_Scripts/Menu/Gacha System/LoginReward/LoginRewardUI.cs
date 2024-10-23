using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginRewardUI : ManagedUI
{
    [SerializeField] private GameObject _layer;
    [SerializeField] private List<LoginRewardBoxButton> _rewardButtons;
    [SerializeField] private Button _openUIButton;
    [SerializeField] private Button _closeUIButton;

    private bool isGetReward = false;

    private int RewardIndex
    {
        get => PlayerPrefs.GetInt("RewardIndex");
        set => PlayerPrefs.SetInt("RewardIndex", value);
    }

    public override void Awake()
    {
        base.Awake();

        RealtimeEventHandler.Instance.OnChangeDay += () => OpenUI(EUIType.Page);
        _openUIButton.onClick.AddListener(() => OpenUI(EUIType.Page));
        _closeUIButton.onClick.AddListener(CloseUI);
    }

    protected override void OnOpen()
    {
        for (int i = 0; i < _rewardButtons.Count; i++)
        {
            if (i == RewardIndex)
            {
                _rewardButtons[i].Button.onClick.AddListener(OnClickReward);
            }
            _rewardButtons[i].Button.interactable = i <= RewardIndex;
            _rewardButtons[i].Image.enabled = i >= RewardIndex;
        }

        isGetReward = false;

        _layer.SetActive(true);
    }

    protected override void OnClose()
    {
        _layer.SetActive(false);

        _openUIButton.gameObject.SetActive(!isGetReward);
    }

    private void OnClickReward()
    {
        _rewardButtons[RewardIndex].Image.enabled = false;

        if (RewardIndex == _rewardButtons.Count - 1)
        {
            Debug.Log("주간보상");

            RewardIndex = 0;
        }
        else
        {
            Debug.Log("일간보상");

            RewardIndex++;
        }

        isGetReward = true;
    }
}
