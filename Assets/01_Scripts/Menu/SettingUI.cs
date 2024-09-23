using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    public static float moveSensitivity;

    [SerializeField] private GameObject _settingUILayer;
    [SerializeField] private Button _settingUIActivateButton;
    [SerializeField] private Button _settingUICloseButton;
    [SerializeField] private Slider _moveSensitivitySlider;
    
    private void Awake()
    {
        moveSensitivity = _moveSensitivitySlider.value;

        _settingUIActivateButton.onClick.AddListener(() => _settingUILayer.SetActive(true));
        _settingUICloseButton.onClick.AddListener(() => _settingUILayer.SetActive(false));

        _moveSensitivitySlider.onValueChanged.AddListener((float changedValue) => moveSensitivity = changedValue);
    }
}
