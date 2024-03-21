using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizingUI : MonoBehaviour
{
    [SerializeField] private PlayerSetting _playerSetting;
    [SerializeField] private CustomizingCamera _customizingCamera;
    [SerializeField] private GameObject _layer;
    [SerializeField] private Button _beforeButton;
    [SerializeField] private Button _afterButton;
    [SerializeField] private Button _selectButton;
    [SerializeField] private Button _closeButton;

    private int _curruntCarIndex;

    private void Awake()
    {
        _beforeButton.onClick.AddListener(ShowBeforeCar);
        _afterButton.onClick.AddListener(ShowAfterCar);
        _closeButton.onClick.AddListener(CloseUI);
        _selectButton.onClick.AddListener(SelectCar);
    }

    public void OpenUI()
    {
        _curruntCarIndex = GameManager.Instance.PlayerModelId;
        OnChangeShowedCar();

        _customizingCamera.ShowModels(_playerSetting.PlayerModels);

        _layer.SetActive(true);
    }

    public void CloseUI()
    {
        _layer.SetActive(false);
    }

    private void ShowBeforeCar()
    {
        _curruntCarIndex = Mathf.Max(0, _curruntCarIndex - 1);
        _customizingCamera.ShowCar(_curruntCarIndex);
        OnChangeShowedCar();
    }

    private void ShowAfterCar()
    {
        _curruntCarIndex = Mathf.Min(_curruntCarIndex + 1, _playerSetting.PlayerModels.Length - 1);
        _customizingCamera.ShowCar(_curruntCarIndex);
        OnChangeShowedCar();
    }

    private void OnChangeShowedCar()
    {
        _beforeButton.interactable = _curruntCarIndex > 0;
        _afterButton.interactable = _curruntCarIndex < _playerSetting.PlayerModels.Length - 1;
        _selectButton.interactable = _curruntCarIndex != GameManager.Instance.PlayerModelId;
    }

    private void SelectCar()
    {
        GameManager.Instance.PlayerModelId = _curruntCarIndex;
        OnChangeShowedCar();
    }
}
