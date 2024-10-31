using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarColorElementUI : MonoBehaviour
{
    [SerializeField] private GameObject _lockImage;
    [SerializeField] private GameObject _equippedImage;
    [SerializeField] private TextMeshProUGUI _carColorNameText;

    public CarColorData CarColorData { get => _carColorData; }
    private CarColorData _carColorData;
    private CarColorCustomizingUI _carColorCustomizingUI;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(SelectElement);
    }

    public void InitElementUI(CarColorData carColorData, CarColorCustomizingUI carColorCustomizingUI)
    {
        _carColorData = carColorData;
        _carColorCustomizingUI = carColorCustomizingUI;

        _lockImage.SetActive(PlayerPrefs.GetInt(_carColorData.Name) == 0);
        _carColorNameText.text = _carColorData.Name;
    }

    private void SelectElement()
    {
        _carColorCustomizingUI.SelectColorElement(this);
    }

    public void SetActiveEquippedImage(bool active)
    {
        _equippedImage.SetActive(active);
    }

    public void UnlockColorElement()
    {
        _lockImage.SetActive(false);
    }
}
