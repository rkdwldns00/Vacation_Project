using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarAccessoryElementUI : MonoBehaviour
{
    [SerializeField] private GameObject _lockImage;
    [SerializeField] private GameObject _equippedImage;
    [SerializeField] private TextMeshProUGUI _carAccessoryNameText;

    public CarAccessoryData CarAccessoryData { get => _carAccessoryData; }
    private CarAccessoryData _carAccessoryData;
    private CarAccessoryCustomizingUI _carAccessoryCustomizingUI;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(SelectElement);
    }

    public void InitElementUI(CarAccessoryData carAccessoryData, CarAccessoryCustomizingUI carAccessoryCustomizingUI)
    {
        _carAccessoryData = carAccessoryData;
        _carAccessoryCustomizingUI = carAccessoryCustomizingUI;

        _lockImage.SetActive(PlayerPrefs.GetInt(_carAccessoryData.Name) == 0);
        _carAccessoryNameText.text = _carAccessoryData.Name;
    }

    private void SelectElement()
    {
        _carAccessoryCustomizingUI.SelectAccessoryElement(this);
    }

    public void SetActiveEquippedImage(bool active)
    {
        _equippedImage.SetActive(active);
    }

    public void UnlockAccessoryElement()
    {
        _lockImage.SetActive(false);
    }
}
