using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurveyUI : ManagedUI
{
    [SerializeField] private GameObject _surveyUILayer;
    [SerializeField] private GameObject _surveyRewardUILayer;
    [SerializeField] private GameObject _surveyAlreadyDidUILayer;
    [SerializeField] private Button _surveyUIActivateButton;
    [SerializeField] private Button _surveyUICloseButton;
    [SerializeField] private Button _surveyURLOpenButton;
    [SerializeField] private Button _surveyURLCloseButton;
    [SerializeField] private Button _surveyAlreadyDidCloseButton;
    [SerializeField] private string _surveyURL;
    [SerializeField] private int _surveyRewardCrystal;

    public override void Awake()
    {
        base.Awake();
        _surveyUIActivateButton.onClick.AddListener(() => OpenUI(EUIType.Page));

        _surveyUICloseButton.onClick.AddListener(CloseUI);

        _surveyURLCloseButton.onClick.AddListener(CloseUI);

        _surveyAlreadyDidCloseButton.onClick.AddListener(CloseUI);

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

    protected override void OnOpen()
    {
        if (!PlayerPrefs.HasKey("DidSurvey")) _surveyUILayer.SetActive(true);
        else _surveyAlreadyDidUILayer.SetActive(true);
    }

    protected override void OnClose()
    {
        _surveyUILayer.SetActive(false);
        _surveyAlreadyDidUILayer.SetActive(false);
        _surveyRewardUILayer.SetActive(false);
    }
}
