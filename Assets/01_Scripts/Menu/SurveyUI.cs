using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 코드 작성자
 * ManagedUI 상속 적용 : 강지운
 * 설문조사 기능 구현 : 이기환
 */
public class SurveyUI : ManagedUI
{
    [SerializeField] private GameObject _surveyUILayer;
    [SerializeField] private GameObject _surveyDidUILayer;
    [SerializeField] private Button _surveyUIActivateButton;
    [SerializeField] private Button _surveyUICloseButton;
    [SerializeField] private Button _surveyURLOpenButton;
    [SerializeField] private Button _surveyURLCloseButton;
    [SerializeField] private string _surveyURL;
    [SerializeField] private int _surveyRewardCrystal;

    private const string _didSurveyKey = "DidSurvey";

    public override void Awake()
    {
        base.Awake();
        _surveyUIActivateButton.onClick.AddListener(() => OpenUI(EUIType.Page));

        _surveyUICloseButton.onClick.AddListener(CloseUI);

        _surveyURLCloseButton.onClick.AddListener(CloseUI);

        _surveyURLOpenButton.onClick.AddListener(() => StartCoroutine(OpenSurveyURLCoroutine()));
    }

    protected override void OnOpen()
    {
        _surveyUILayer.SetActive(true);
        _surveyDidUILayer.SetActive(false);
    }

    protected override void OnClose()
    {
        _surveyUILayer.SetActive(false);
        _surveyDidUILayer.SetActive(false);
    }

    private IEnumerator OpenSurveyURLCoroutine()
    {
        Application.OpenURL(_surveyURL);

        yield return new WaitForSeconds(1f);

        _surveyUILayer.SetActive(false);
        _surveyDidUILayer.SetActive(true);

        if (!PlayerPrefs.HasKey(_didSurveyKey))
        {
            Currency.Crystal += _surveyRewardCrystal;
            PlayerPrefs.SetInt(_didSurveyKey, 1);
            PlayerPrefs.Save();
        }

        yield break;
    }
}
