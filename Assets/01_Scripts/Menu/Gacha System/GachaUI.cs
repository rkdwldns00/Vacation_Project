using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* 코드 작성자 : 강지운 */
public class GachaUI : ManagedUI
{
    [SerializeField] GameObject _layer;
    [SerializeField] Image _rewardImage;
    [SerializeField] TextMeshProUGUI _rewardText;
    [SerializeField] Button _closeButton;

    private RewardData _reward;

    public override void Awake()
    {
        base.Awake();
        _closeButton.onClick.AddListener(CloseUI);
    }

    public void SetReward(RewardData reward)
    {
        _reward = reward;
    }

    protected override void OnOpen()
    {
        if (_reward.reward != null)
        {
            _rewardImage.sprite = _reward.reward.sprite;
            _rewardText.text = _reward.reward.GetName(_reward.rewardRate) + " 획득!";

            _layer.SetActive(true);
        }
    }

    protected override void OnClose()
    {
        _reward.reward = null;
        _layer.SetActive(false);
    }
}
