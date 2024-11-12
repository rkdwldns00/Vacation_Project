using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : ManagedUI
{
    public static float MoveSensitivity
    {
        get => PlayerPrefs.GetFloat("MoveSensitivity");
        private set => PlayerPrefs.SetFloat("MoveSensitivity", value);
    }

    [SerializeField] private GameObject _settingUILayer;
    [SerializeField] private Button _settingUIActivateButton;
    [SerializeField] private Button _settingUICloseButton;
    [SerializeField] private Slider _moveSensitivitySlider;

    public override void Awake()
    {
        base.Awake();
        _moveSensitivitySlider.value = MoveSensitivity;

        _settingUIActivateButton.onClick.AddListener(() => OpenUI(EUIType.Page));
        _settingUICloseButton.onClick.AddListener(CloseUI);

        _moveSensitivitySlider.onValueChanged.AddListener((changedValue) =>
        {
            MoveSensitivity = changedValue;
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
