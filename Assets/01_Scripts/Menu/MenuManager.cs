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

    private void Start()
    {
        SetBestScoreText(GameManager.Instance.BestScore);

        if (PlayerPrefs.GetInt("PlayedTutorial") == 0)
        {
            foreach (GameObject go in _deleteOnTutorial)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in _activeOnTutorial)
            {
                go.SetActive(true);
            }
        }
    }

    [ContextMenu("튜토리얼 활성화")]
    public void SetActiveTutorial()
    {
        PlayerPrefs.SetInt("PlayedTutorial", 0);
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
