using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarColorCustomizingUI : MonoBehaviour
{
    [SerializeField] private CarColorData[] _carColorDatas;

    [Header("UI")]
    [SerializeField] private GameObject _carColorElementPrefab;
    [SerializeField] private Transform _carColorElementParent;
    [SerializeField] private Button _selectColorButton;
    [SerializeField] private Button _unselectColorButton;
    [SerializeField] private Button _unlockColorButton;
    [SerializeField] private GameObject _unlockCrystalCostImage;
    [SerializeField] private TextMeshProUGUI _unlockCrystalCostText;
    [SerializeField] private GameObject _unlockGoldCostImage;
    [SerializeField] private TextMeshProUGUI _unlockGoldCostText;

    [Header("Models")]
    [SerializeField] private GameObject[] _menuPlayerModels;
    [SerializeField] private GameObject[] _playerModelPrefabs;

    private void Awake()
    {
        InstantiateCarColorElements();
    }

    private void InstantiateCarColorElements()
    {
        foreach(Transform child in transform)
        {
            if (transform != child) Destroy(child.gameObject);
        }

        foreach(CarColorData carColorData in _carColorDatas)
        {
            if (PlayerPrefs.GetInt(carColorData.Name) == 1)
            {
                Instantiate(_carColorElementPrefab, _carColorElementParent).GetComponent<CarColorElementUI>().InitElementUI(carColorData, this);
            }
        }

        foreach (CarColorData carColorData in _carColorDatas)
        {
            if (PlayerPrefs.GetInt(carColorData.Name) == 0)
            {
                Instantiate(_carColorElementPrefab, _carColorElementParent).GetComponent<CarColorElementUI>().InitElementUI(carColorData, this);
            }
        }
    }

    public void SelectElement(CarColorData carColorData)
    {
        if (PlayerPrefs.GetInt(carColorData.Name) == 0)
        {
            _unlockGoldCostImage.SetActive(carColorData.UnlockGoldCost > 0);
            _unlockGoldCostText.text = carColorData.UnlockGoldCost > 0 ? carColorData.UnlockGoldCost.ToString() : "";
            _unlockCrystalCostImage.SetActive(carColorData.UnlockCrystalCost > 0);
            _unlockCrystalCostText.text = carColorData.UnlockCrystalCost > 0 ? carColorData.UnlockCrystalCost.ToString() : "";
        }
        else if (PlayerPrefs.GetInt(carColorData.Name) == 1)
        {

        }
    }
}
