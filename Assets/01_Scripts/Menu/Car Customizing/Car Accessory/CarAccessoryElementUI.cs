using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CarAccessoryElementUI : MonoBehaviour
{
    [SerializeField] private GameObject _lockImage;
    [SerializeField] private GameObject _equippedImage;
    [SerializeField] private TextMeshProUGUI _carAccessoryNameText;
    [SerializeField] private Image _carAccessoryImage;

    public CarAccessoryData CarAccessoryData { get => _carAccessoryData; }
    private CarAccessoryData _carAccessoryData;
    private CarAccessoryCustomizingUI _carAccessoryCustomizingUI;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(SelectElement);

        StartCoroutine(SetCarAccessoryImageCoroutine());
    }

    private IEnumerator SetCarAccessoryImageCoroutine()
    {
        while (true)
        {
            if (_carAccessoryData != null)
            {
                Texture2D carAccessoryTexture = null;

                while (carAccessoryTexture == null)
                {
                    carAccessoryTexture = AssetPreview.GetAssetPreview(_carAccessoryData.AccessoryObjectPrefab);

                    yield return null;
                }

                Rect rect = new Rect(0, 0, carAccessoryTexture.width, carAccessoryTexture.height);
                _carAccessoryImage.sprite = Sprite.Create(carAccessoryTexture, rect, new Vector2(0.5f, 0.5f));

                yield break;
            }

            yield return null;
        }
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
