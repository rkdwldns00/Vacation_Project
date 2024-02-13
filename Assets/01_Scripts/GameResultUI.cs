using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResultUI : MonoBehaviour
{
    [SerializeField] private GameObject _layer;
    [SerializeField] private Text _highScoreMassage;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Button _closeButton;

    private void Awake()
    {
        _closeButton.onClick.AddListener(CloseUI);
    }

    public void OpenUI()
    {
        _layer.SetActive(true);
        _scoreText.text = GameManager.Instance.Score.ToString() + "m";
        _highScoreMassage.gameObject.SetActive(GameManager.Instance.isHighScore);
    }

    public void CloseUI()
    {
        _layer.SetActive(false);
    }
}
