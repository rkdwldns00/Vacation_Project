using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* 코드 작성자 : 이기환 */
public class CarAccessoryCustomizingUI : MonoBehaviour
{
    public static CarAccessoryCustomizingUI Instance;
    public static CarAccessoryData EquippedCarAccessoryData;

    [SerializeField] private GameObject _layer;
    public CarAccessoryData[] CarAccessoryDatas { get => _carAccessoryDatas; }
    [SerializeField] private CarAccessoryData[] _carAccessoryDatas;
    [SerializeField] private CustomizingCamera _customizingCamera;
    [SerializeField] private CustomizingUI _customizingUI;
    [SerializeField] private PlayerSetting _playerSetting;

    [Header("Car Accessory Element UI")]
    [SerializeField] private GameObject _carAccessoryElementPrefab;
    [SerializeField] private Transform _carAccessoryElementParent;

    [Header("Button")]
    [SerializeField] private Button _openUIButton;
    [SerializeField] private Button _equipAccessoryButton;
    [SerializeField] private Button _unequipAccessoryButton;
    [SerializeField] private Button _unlockAccessoryButton;
    [SerializeField] private Button _exitUIButton;

    [Header("Unlock Cost")]
    [SerializeField] private GameObject _unlockCrystalCostImage;
    [SerializeField] private TextMeshProUGUI _unlockCrystalCostText;
    [SerializeField] private GameObject _unlockGoldCostImage;
    [SerializeField] private TextMeshProUGUI _unlockGoldCostText;

    private CarAccessoryElementUI _equippedAccessoryElement;
    private CarAccessoryElementUI _selectedAccessoryElement;

    private string _lastEquippedCarAccessory
    {
        get => PlayerPrefs.GetString("Last Car Accessory");
        set => PlayerPrefs.SetString("Last Car Accessory", value);
    }

    private void Awake()
    {
        Instance = this;

        _customizingCamera.ShowModels(_playerSetting.PlayerModels);

        _openUIButton.onClick.AddListener(() => _customizingCamera.ShowModels(_playerSetting.PlayerModels));
        _equipAccessoryButton.onClick.AddListener(EquipAccessory);
        _unequipAccessoryButton.onClick.AddListener(UnequipAccessory);
        _unlockAccessoryButton.onClick.AddListener(UnlockAccessory);
        _exitUIButton.onClick.AddListener(CloseUI);
    }

    public void InstantiateCarAccessoryElements()
    {
        foreach (CarAccessoryData carAccessoryData in _carAccessoryDatas)
        {
            Instantiate(_carAccessoryElementPrefab, _carAccessoryElementParent).GetComponent<CarAccessoryElementUI>().InitElementUI(carAccessoryData, this);
        }

        foreach (Transform carAccessoryElement in _carAccessoryElementParent.transform)
        {
            if (carAccessoryElement.GetComponent<CarAccessoryElementUI>()?.CarAccessoryData.Name == _lastEquippedCarAccessory)
            {
                _selectedAccessoryElement = carAccessoryElement.GetComponent<CarAccessoryElementUI>();
                EquipAccessory();
            }
        }
    }

    public void SelectAccessoryElement(CarAccessoryElementUI selectedAccessoryElement)
    {
        _selectedAccessoryElement = selectedAccessoryElement;
        CarAccessoryData carAccessoryData = selectedAccessoryElement.CarAccessoryData;

        ChangeCarAccessory(_selectedAccessoryElement.CarAccessoryData);

        if (PlayerPrefs.GetInt(carAccessoryData.Name) == 0)
        {
            _equipAccessoryButton.gameObject.SetActive(false);
            _unequipAccessoryButton.gameObject.SetActive(false);
            _unlockAccessoryButton.gameObject.SetActive(true);

            _unlockGoldCostImage.SetActive(carAccessoryData.UnlockGoldCost > 0);
            _unlockGoldCostText.text = carAccessoryData.UnlockGoldCost > 0 ? carAccessoryData.UnlockGoldCost.ToString() : "";
            _unlockCrystalCostImage.SetActive(carAccessoryData.UnlockCrystalCost > 0);
            _unlockCrystalCostText.text = carAccessoryData.UnlockCrystalCost > 0 ? carAccessoryData.UnlockCrystalCost.ToString() : "";
        }
        else if (PlayerPrefs.GetInt(carAccessoryData.Name) == 1)
        {
            _unlockAccessoryButton.gameObject.SetActive(false);
            _unequipAccessoryButton.gameObject.SetActive(_equippedAccessoryElement == _selectedAccessoryElement);
            _equipAccessoryButton.gameObject.SetActive(_equippedAccessoryElement != _selectedAccessoryElement);
        }
    }

    private void UnlockAccessory()
    {
        if (Currency.Crystal >= _selectedAccessoryElement.CarAccessoryData.UnlockCrystalCost && Currency.Gold >= _selectedAccessoryElement.CarAccessoryData.UnlockGoldCost)
        {
            PlayerPrefs.SetInt(_selectedAccessoryElement.CarAccessoryData.Name, 1);

            Currency.Crystal -= _selectedAccessoryElement.CarAccessoryData.UnlockCrystalCost;
            Currency.Gold -= _selectedAccessoryElement.CarAccessoryData.UnlockGoldCost;

            PlayerPrefs.Save();

            _unlockAccessoryButton.gameObject.SetActive(false);
            EquipAccessory();

            _selectedAccessoryElement.UnlockAccessoryElement();
        }
    }

    private void EquipAccessory()
    {
        if (_selectedAccessoryElement != null)
        {
            _equippedAccessoryElement?.SetActiveEquippedImage(false);
            _equippedAccessoryElement = _selectedAccessoryElement;
            _equippedAccessoryElement.SetActiveEquippedImage(true);

            ChangeCarAccessory(_equippedAccessoryElement.CarAccessoryData);

            _lastEquippedCarAccessory = _equippedAccessoryElement.CarAccessoryData.Name;
            PlayerPrefs.Save();

            _equipAccessoryButton.gameObject.SetActive(false);
            _unequipAccessoryButton.gameObject.SetActive(true);
        }
    }

    private void UnequipAccessory()
    {
        _equippedAccessoryElement.SetActiveEquippedImage(false);

        _unequipAccessoryButton.gameObject.SetActive(false);
        _equipAccessoryButton.gameObject.SetActive(true);

        ChangeCarAccessory(null);

        _lastEquippedCarAccessory = "";
        PlayerPrefs.Save();

        _equippedAccessoryElement = null;
    }

    private void ChangeCarAccessory(CarAccessoryData carAccessoryData)
    {
        EquippedCarAccessoryData = carAccessoryData;

        foreach (GameObject model in _customizingCamera.Models)
        {
            model.GetComponentInChildren<CarAccessoryPositioner>().SetCarAccessoryObject(carAccessoryData);
        }

        foreach (GameObject model in _customizingUI.MenuPlayerModels)
        {
            model.GetComponentInChildren<CarAccessoryPositioner>().SetCarAccessoryObject(carAccessoryData);
        }
    }

    private void CloseUI()
    {
        _layer.SetActive(false);

        ChangeCarAccessory(_equippedAccessoryElement != null ? _equippedAccessoryElement.CarAccessoryData : null);
    }

    public void GenerateCarAccessoryImage(IEnumerator generateCoroutine)
    {
        StartCoroutine(generateCoroutine);
    }
}
