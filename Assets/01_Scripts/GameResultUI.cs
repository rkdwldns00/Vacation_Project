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
    [SerializeField] private Button _closeButton;

    private void Awake()
    {
        _closeButton.onClick.AddListener(CloseUI);
    }

    public void OpenUI()
    {
        _layer.SetActive(true);
        _distanceScoreText.text = "+ 거리 점수 : " + GameManager.Instance.DistanceScore;
        _gemScoreText.text = "+ 젬 점수 : " + GameManager.Instance.GemScore;
        _scoreText.text = "총 점수 : " + GameManager.Instance.Score;
        _bestScoreMassage.SetActive(GameManager.Instance.isBestScore);

        OnOpen?.Invoke();
    }

    public void CloseUI()
    {
        _layer.SetActive(false);

        OnClose?.Invoke();
    }
}
