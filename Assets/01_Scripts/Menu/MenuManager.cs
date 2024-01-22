using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestScoreText;

    public void StartGame()
    {
        //SceneManager.LoadScene("");
    }

    private void SetBestScoreText(int score)
    {
        bestScoreText.text = "Best Score : " + score; 
    }
}
