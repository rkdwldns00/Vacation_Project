using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bestScoreText;

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void SetBestScoreText(int score)
    {
        _bestScoreText.text = "Best Score : " + score; 
    }
}
