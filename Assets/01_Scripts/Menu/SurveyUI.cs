using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurveyUI : MonoBehaviour
{
    [SerializeField] private GameObject _surveyUILayer;
    [SerializeField] private GameObject _surveyRewardUILayer;
    [SerializeField] private GameObject _surveyAlreadyDidUILayer;
    [SerializeField] private Button _surveyUIActivateButton;
    [SerializeField] private Button _surveyUICloseButton;
    [SerializeField] private Button _surveyURLOpenButton;
    [SerializeField] private string _surveyURL;
    [SerializeField] private int _surveyRewardCrystal;

    private void Awake()
    {
        _surveyUIActivateButton.onClick.AddListener(() =>
        {
            if (!PlayerPrefs.HasKey("DidSurvey")) _surveyUILayer.SetActive(true);
            else _surveyAlreadyDidUILayer.SetActive(true);
        });

        _surveyUICloseButton.onClick.AddListener(() => _surveyUILayer.SetActive(false));

        _surveyURLOpenButton.onClick.AddListener(() => 
        {
            _surveyUILayer.SetActive(false);
            _surveyRewardUILayer.SetActive(true);
            Currency.Crystal += _surveyRewardCrystal;
            PlayerPrefs.SetInt("DidSurvey", 1);
            PlayerPrefs.Save();
            Application.OpenURL(_surveyURL);
        });
    }
}
