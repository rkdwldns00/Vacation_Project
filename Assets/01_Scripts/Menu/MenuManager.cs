using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private GameObject[] _deleteOnTutorial;
    [SerializeField] private GameObject[] _activeOnTutorial;
    [SerializeField] private PlayerSetting _playerSetting;

    private void Awake()
    {
        if (_playerSetting.playerPrefabs[0].GetComponent<Player>().PlayerLevel == 0)
        {
            GameManager.Instance.PlayerModelId = 0;
        }
    }

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        SetBestScoreText(GameManager.Instance.BestScore);
        Application.targetFrameRate = 120;

        if (_playerSetting.playerPrefabs[0].GetComponent<Player>().PlayerLevel == 0)
        {
            foreach (GameObject go in _deleteOnTutorial)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in _activeOnTutorial)
            {
                go.SetActive(true);
            }

            GameManager.Instance.PlayerModelId = 0;
        }

        SoundManager.Instance.PlaySound(SoundManager.Instance.SoundData.MenuBgm, SoundType.BGM);
    }

    [ContextMenu("튜토리얼 활성화")]
    public void SetActiveTutorial()
    {
        _playerSetting.playerPrefabs[0].GetComponent<Player>().PlayerLevel = 0;
    }

    [ContextMenu("재화 무한")]
    public void SetCurrencyInfinity()
    {
        Currency.Crystal = 1000;
        Currency.Gold = 100000;
    }

    [ContextMenu("재화 초기화")]
    public void ClearCurrency()
    {
        Currency.Crystal = 0;
        Currency.Gold = 0;
    }

    [ContextMenu("차량 레벨 초기화")]
    public void ClearCarLevel()
    {
        for (int i = 1; i < _playerSetting.playerPrefabs.Length; i++)
        {
            _playerSetting.playerPrefabs[i].GetComponent<Player>().PlayerLevel = 0;
        }
    }

    public void StartGame()
    {
        TutorialManager.isActive = false;
        SceneManager.LoadScene("GameScene");
    }

    public void StartTutorial()
    {
        TutorialManager.isActive = true;
        SceneManager.LoadScene("GameScene");
    }

    private void SetBestScoreText(int score)
    {
        _bestScoreText.text = "최고 점수 : " + score;
    }
}
