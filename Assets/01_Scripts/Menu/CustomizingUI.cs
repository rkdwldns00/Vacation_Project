using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomizingUI : MonoBehaviour
{
    [SerializeField] private PlayerSetting _playerSetting;
    [SerializeField] private CustomizingCamera _customizingCamera;
    [SerializeField] private GameObject _layer;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private TextMeshProUGUI _upgradeCostText;
    [SerializeField] private TextMeshProUGUI _upgradeLevelText;
    [SerializeField] private TextMeshProUGUI _carInfoText;
    [SerializeField] private Button _beforeButton;
    [SerializeField] private Button _afterButton;
    [SerializeField] private Button _selectButton;
    [SerializeField] private Button _unlockButton;
    [SerializeField] private TextMeshProUGUI _unlockCostText;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Transform _menuPlayerModelTransform;

    public GameObject[] MenuPlayerModels { get => _menuPlayerModels; }
    private GameObject[] _menuPlayerModels;

    private int _curruntCarIndex;

    private void Awake()
    {
        _beforeButton.onClick.AddListener(ShowBeforeCar);
        _afterButton.onClick.AddListener(ShowAfterCar);
        _closeButton.onClick.AddListener(CloseUI);
        _selectButton.onClick.AddListener(SelectCar);
        _upgradeButton.onClick.AddListener(TryUpgradeCar);
        _unlockButton.onClick.AddListener(TryUnlockCar);
    }

    private void Start()
    {
        _menuPlayerModels = new GameObject[_playerSetting.PlayerModels.Length];
        for (int i = 0; i < _playerSetting.PlayerModels.Length; i++)
        {
            _menuPlayerModels[i] = Instantiate(_playerSetting.PlayerModels[i], _menuPlayerModelTransform);
            if (GameManager.Instance.PlayerModelId != i)
            {
                _menuPlayerModels[i].SetActive(false);
            }
        }

        CarColorCustomizingUI.Instance.InstantiateCarColorElements();
        CarAccessoryCustomizingUI.Instance.InstantiateCarAccessoryElements();
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

        for (int i = 0; i < _playerSetting.PlayerModels.Length; i++)
        {
            _menuPlayerModels[i].SetActive(GameManager.Instance.PlayerModelId == i);
        }
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

        UpdateCarInfo();
    }

    private void TryUnlockCar()
    {
        Player p = _playerSetting.playerPrefabs[_curruntCarIndex].GetComponent<Player>();
        if (p.UnlockCost <= Currency.Crystal && p.PlayerLevel == 0)
        {
            Currency.Crystal -= p.UnlockCost;
            p.PlayerLevel = 1;
            UpdateCarInfo();
        }
    }

    private void TryUpgradeCar()
    {
        Player p = _playerSetting.playerPrefabs[_curruntCarIndex].GetComponent<Player>();
        if (p.UpgradeCost <= Currency.Gold && p.IsUpgradable)
        {
            Currency.Gold -= p.UpgradeCost;
            p.PlayerLevel += 1;
            UpdateCarInfo();
        }
    }

    private void UpdateCarInfo()
    {
        Player p = _playerSetting.playerPrefabs[_curruntCarIndex].GetComponent<Player>();
        _unlockButton.gameObject.SetActive(p.PlayerLevel == 0);
        _upgradeButton.gameObject.SetActive(p.PlayerLevel > 0 && p.IsUpgradable);
        _selectButton.gameObject.SetActive(p.PlayerLevel > 0);
        _carInfoText.text = p.CarInfo;
        _unlockCostText.text = p.UnlockCost.ToString();
        _upgradeCostText.text = p.UpgradeCost.ToString();
        _upgradeLevelText.text = p.PlayerLevel.ToString();
    }

    private void SelectCar()
    {
        GameManager.Instance.PlayerModelId = _curruntCarIndex;
        OnChangeShowedCar();
    }
}
