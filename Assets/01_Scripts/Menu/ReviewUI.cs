using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviewUI : ManagedUI
{
    private static int _lastBestScore;

    [Header("Review Question UI")]
    [SerializeField] private GameObject _reviewQuestionUI;
    [SerializeField] private Button _reviewQuestionYesBtn;
    [SerializeField] private Button _reviewQuestionNoBtn;

    [Header("Review Request UI")]
    [SerializeField] private GameObject _reviewRequestUI;
    [SerializeField] private Button _reviewRequestYesBtn;
    [SerializeField] private Button _reviewRequestNoBtn;

    [Header("Survey Request UI")]
    [SerializeField] private SurveyUI _surveyUI;
    [SerializeField] private GameObject _surveyRequestUI;
    [SerializeField] private Button _surveyRequestYesBtn;
    [SerializeField] private Button _surveyRequestNoBtn;

    [Header("Did Review")]
    [SerializeField] private GameObject _didReviewUI;
    [SerializeField] private Button _didReviewCloseBtn;

    private int ReviewRequestScore
    {
        get => PlayerPrefs.GetInt("ReviewRequestScore");
        set => PlayerPrefs.SetInt("ReviewRequestScore", value);
    }

    private int DidReview
    {
        get => PlayerPrefs.GetInt("DidReview");
        set => PlayerPrefs.SetInt("DidReview", value);
    }

    private const string _reviewURL = "";

    public override void Awake()
    {
        base.Awake();

        _reviewQuestionYesBtn.onClick.AddListener(() =>
        {
            _reviewQuestionUI.SetActive(false);
            _reviewRequestUI.SetActive(true);
        });
        _reviewQuestionNoBtn.onClick.AddListener(() =>
        {
            _reviewQuestionUI.SetActive(false);
            _surveyRequestUI.SetActive(true);
        });

        _reviewRequestYesBtn.onClick.AddListener(() => StartCoroutine(OpenReviewURLCoroutine()));
        _reviewRequestNoBtn.onClick.AddListener(() => CloseUI());

        _surveyRequestYesBtn.onClick.AddListener(() => _surveyUI.OpenUI());
        _surveyRequestNoBtn.onClick.AddListener(() => CloseUI());

        _didReviewCloseBtn.onClick.AddListener(() => CloseUI());
    }

    private void Start()
    {
        if ((_lastBestScore < 30000 && GameManager.Instance.BestScore >= 30000 && ReviewRequestScore < 30000) ||
            (_lastBestScore < 50000 && GameManager.Instance.BestScore >= 50000 && ReviewRequestScore < 50000) ||
            (_lastBestScore < 70000 && GameManager.Instance.BestScore >= 70000 && ReviewRequestScore < 70000) ||
            (_lastBestScore != GameManager.Instance.BestScore && ReviewRequestScore > 70000) && DidReview != 1)
        {
            OpenUI();
        }

        _lastBestScore = GameManager.Instance.BestScore;
        ReviewRequestScore = GameManager.Instance.BestScore;
    }

    protected override void OnClose()
    {
        _reviewQuestionUI.SetActive(false);
        _reviewRequestUI.SetActive(false);
        _surveyRequestUI.SetActive(false);
        _didReviewUI.SetActive(false);
    }

    protected override void OnOpen()
    {
        _reviewQuestionUI.SetActive(true);
    }

    private IEnumerator OpenReviewURLCoroutine()
    {
        Application.OpenURL(_reviewURL);
        DidReview = 1;
        yield return new WaitForSeconds(1);
        _didReviewUI.SetActive(true);

        yield break;
    }

    [ContextMenu("리뷰 초기화")]
    public void ResetReview()
    {
        ReviewRequestScore = 0;
        DidReview = 0;
    }
}
