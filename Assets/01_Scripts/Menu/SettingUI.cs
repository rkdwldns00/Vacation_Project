using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : ManagedUI
{
    public static float moveSensitivity;

    [SerializeField] private GameObject _settingUILayer;
    [SerializeField] private Button _settingUIActivateButton;
    [SerializeField] private Button _settingUICloseButton;
    [SerializeField] private Slider _moveSensitivitySlider;

    private const string _moveSensitivityKey = "MoveSensitivity";

    public override void Awake()
    {
        base.Awake();
        _moveSensitivitySlider.value = PlayerPrefs.GetFloat(_moveSensitivityKey);

        _settingUIActivateButton.onClick.AddListener(() => OpenUI(EUIType.Page));
        _settingUICloseButton.onClick.AddListener(CloseUI);

        _moveSensitivitySlider.onValueChanged.AddListener((changedValue) =>
        {
            moveSensitivity = changedValue;
            PlayerPrefs.SetFloat(_moveSensitivityKey, changedValue);
            PlayerPrefs.Save();
        });
    }

    protected override void OnOpen()
    {
        _settingUILayer.SetActive(true);
    }

    protected override void OnClose()
    {
        _settingUILayer.SetActive(false);
    }
}
