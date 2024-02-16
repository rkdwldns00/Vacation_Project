using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private Image _playerHpImage;
    [SerializeField] private GameObject _highScoreRecord;

    [SerializeField] private GameResultUI _gameResultUI;

    private void Awake()
    {
        _gameResultUI.OnClose += () => SceneManager.LoadScene("MenuScene");
    }

    private void Update()
    {
        _currentScoreText.text = GameManager.Instance.Score + "m";

        if (GameManager.Instance.Score > GameManager.Instance.HighScore)
        {
            _highScoreRecord.SetActive(true);
        }
    }
}
