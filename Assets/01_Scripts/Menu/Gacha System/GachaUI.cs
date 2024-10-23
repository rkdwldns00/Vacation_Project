using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaUI : ManagedUI
{
    [SerializeField] GameObject _layer;
    [SerializeField] Image _rewardImage;
    [SerializeField] TextMeshProUGUI _rewardText;
    [SerializeField] Button _closeButton;

    private GachaReward _reward = null;

    public override void Awake()
    {
        base.Awake();
        _closeButton.onClick.AddListener(CloseUI);
    }

    public void SetReward(GachaReward reward)
    {
        _reward = reward;
    }

    protected override void OnOpen()
    {
        if (_reward != null)
        {
            _rewardImage.sprite = _reward.sprite;
            _rewardText.text = _reward.name + " 획득!";

            _layer.SetActive(true);
        }
    }

    protected override void OnClose()
    {
        _reward = null;
        _layer.SetActive(false);
    }
}
