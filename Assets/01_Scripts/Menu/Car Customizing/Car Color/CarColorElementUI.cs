using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarColorElementUI : MonoBehaviour
{
    [SerializeField] private Image _colorImage;
    [SerializeField] private GameObject _lockImage;
    [SerializeField] private GameObject _selectImage;

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

        if (PlayerPrefs.GetInt(_carColorData.Name) == 0)
        {
            _lockImage.SetActive(true);
        }
    }

    private void SelectElement()
    {
        _carColorCustomizingUI.SelectElement(_carColorData);
    }
}
