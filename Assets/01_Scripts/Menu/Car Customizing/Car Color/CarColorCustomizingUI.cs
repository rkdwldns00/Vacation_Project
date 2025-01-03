using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/* 코드 작성자 : 이기환 */
public class CarColorCustomizingUI : MonoBehaviour
{
    public static CarColorCustomizingUI Instance;
    public static Material EquippedColorMaterial;

    [SerializeField] private GameObject _layer;
    [SerializeField] private CarColorData[] _carColorDatas;
    [SerializeField] private CustomizingCamera _customizingCamera;
    [SerializeField] private CustomizingUI _customizingUI;
    [SerializeField] private PlayerSetting _playerSetting;
    [SerializeField] private Material _originMaterial;

    [Header("Car Color Element UI")]
    [SerializeField] private GameObject _carColorElementPrefab;
    [SerializeField] private Transform _carColorElementParent;

    [Header("Button")]
    [SerializeField] private Button _openUIButton;
    [SerializeField] private Button _equipColorButton;
    [SerializeField] private Button _unequipColorButton;
    [SerializeField] private Button _unlockColorButton;
    [SerializeField] private Button _exitUIButton;

    [Header("Unlock Cost")]
    [SerializeField] private GameObject _unlockCrystalCostImage;
    [SerializeField] private TextMeshProUGUI _unlockCrystalCostText;
    [SerializeField] private GameObject _unlockGoldCostImage;
    [SerializeField] private TextMeshProUGUI _unlockGoldCostText;

    private CarColorElementUI _equippedColorElement;
    private CarColorElementUI _selectedColorElement;

    private string _lastEquippedCarColor
    {
        get => PlayerPrefs.GetString("Last Car Color");
        set => PlayerPrefs.SetString("Last Car Color", value);
    }

    private void Awake()
    {
        Instance = this;

        _customizingCamera.ShowModels(_playerSetting.PlayerModels);

        _openUIButton.onClick.AddListener(() => _customizingCamera.ShowModels(_playerSetting.PlayerModels));
        _equipColorButton.onClick.AddListener(EquipColor);
        _unequipColorButton.onClick.AddListener(UnequipColor);
        _unlockColorButton.onClick.AddListener(UnlockColor);
        _exitUIButton.onClick.AddListener(CloseUI);

        EquippedColorMaterial = _originMaterial;
    }

    public void InstantiateCarColorElements()
    {
        foreach (CarColorData carColorData in _carColorDatas)
        {
            Instantiate(_carColorElementPrefab, _carColorElementParent).GetComponent<CarColorElementUI>().InitElementUI(carColorData, this);
        }

        foreach (Transform carColorElement in _carColorElementParent.transform)
        {
            if (carColorElement.GetComponent<CarColorElementUI>()?.CarColorData.Name == _lastEquippedCarColor)
            {
                _selectedColorElement = carColorElement.GetComponent<CarColorElementUI>();
                EquipColor();
            }
        }
    }

    public void SelectColorElement(CarColorElementUI selectedColorElement)
    {
        _selectedColorElement = selectedColorElement;
        CarColorData carColorData = selectedColorElement.CarColorData;

        ChangeCarColor(_selectedColorElement.CarColorData.ColorMaterial);

        if (PlayerPrefs.GetInt(carColorData.Name) == 0)
        {
            _equipColorButton.gameObject.SetActive(false);
            _unequipColorButton.gameObject.SetActive(false);
            _unlockColorButton.gameObject.SetActive(true);

            _unlockGoldCostImage.SetActive(carColorData.UnlockGoldCost > 0);
            _unlockGoldCostText.text = carColorData.UnlockGoldCost > 0 ? carColorData.UnlockGoldCost.ToString() : "";
            _unlockCrystalCostImage.SetActive(carColorData.UnlockCrystalCost > 0);
            _unlockCrystalCostText.text = carColorData.UnlockCrystalCost > 0 ? carColorData.UnlockCrystalCost.ToString() : "";
        }
        else if (PlayerPrefs.GetInt(carColorData.Name) == 1)
        {
            _unlockColorButton.gameObject.SetActive(false);
            _unequipColorButton.gameObject.SetActive(_equippedColorElement == _selectedColorElement);
            _equipColorButton.gameObject.SetActive(_equippedColorElement != _selectedColorElement);
        }
    }

    private void UnlockColor()
    {
        if (Currency.Crystal >= _selectedColorElement.CarColorData.UnlockCrystalCost && Currency.Gold >= _selectedColorElement.CarColorData.UnlockGoldCost)
        {
            PlayerPrefs.SetInt(_selectedColorElement.CarColorData.Name, 1);

            Currency.Crystal -= _selectedColorElement.CarColorData.UnlockCrystalCost;
            Currency.Gold -= _selectedColorElement.CarColorData.UnlockGoldCost;

            PlayerPrefs.Save();

            _unlockColorButton.gameObject.SetActive(false);
            EquipColor();

            _selectedColorElement.UnlockColorElement();
        }
    }

    private void EquipColor()
    {
        if (_selectedColorElement != null)
        {
            _equippedColorElement?.SetActiveEquippedImage(false);
            _equippedColorElement = _selectedColorElement;
            _equippedColorElement.SetActiveEquippedImage(true);

            ChangeCarColor(_equippedColorElement.CarColorData.ColorMaterial);

            _lastEquippedCarColor = _equippedColorElement.CarColorData.Name;
            PlayerPrefs.Save();

            _equipColorButton.gameObject.SetActive(false);
            _unequipColorButton.gameObject.SetActive(true);
        }
    }

    private void UnequipColor()
    {
        _equippedColorElement.SetActiveEquippedImage(false);

        _unequipColorButton.gameObject.SetActive(false);
        _equipColorButton.gameObject.SetActive(true);

        ChangeCarColor(_originMaterial);

        _lastEquippedCarColor = "";
        PlayerPrefs.Save();

        _equippedColorElement = null;
    }

    private void ChangeCarColor(Material colorMaterial)
    {
        EquippedColorMaterial = colorMaterial;

        foreach (GameObject model in _customizingCamera.Models)
        {
            model.GetComponent<CarColorMeshRenderer>().ChangeCarColor(colorMaterial);
        }

        foreach (GameObject model in _customizingUI.MenuPlayerModels)
        {
            model.GetComponent<CarColorMeshRenderer>().ChangeCarColor(colorMaterial);
        }
    }

    private void CloseUI()
    {
        _layer.SetActive(false);

        ChangeCarColor(_equippedColorElement ? _equippedColorElement.CarColorData.ColorMaterial : _originMaterial);
    }
}
