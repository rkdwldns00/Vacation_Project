using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private TMP_InputField _moveSensitivityInputField;

    public override void Awake()
    {
        base.Awake();
        _moveSensitivitySlider.value = MoveSensitivity;
        _moveSensitivityInputField.text = MoveSensitivity.ToString("0.00");

        _settingUIActivateButton.onClick.AddListener(() => OpenUI(EUIType.Page));
        _settingUICloseButton.onClick.AddListener(CloseUI);

        _moveSensitivitySlider.onValueChanged.AddListener((changedValue) =>
        {
            MoveSensitivity = changedValue;
            _moveSensitivityInputField.text = MoveSensitivity.ToString("0.00");
            PlayerPrefs.Save();
        });

        _moveSensitivityInputField.onEndEdit.AddListener((changedValue) =>
        {
            float changedFloatValue = float.Parse(changedValue);

            MoveSensitivity = Mathf.Clamp01(changedFloatValue);
            _moveSensitivitySlider.value = MoveSensitivity;
            _moveSensitivityInputField.text = MoveSensitivity.ToString("0.00");
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
