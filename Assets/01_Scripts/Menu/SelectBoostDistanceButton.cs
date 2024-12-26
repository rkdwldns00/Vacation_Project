using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectBoostDistanceButton : MonoBehaviour
{
    [SerializeField] private BoostUI _boostUI;
    [SerializeField] private float _boostDistance;
    [SerializeField] private int _boostCost;
    [SerializeField] private TextMeshProUGUI _distanceText;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private Button _button;
    [SerializeField] private Image _backGroundImage;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _unselectedColor;

    private void Awake()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void Start()
    {
        _distanceText.text = _boostDistance.ToString() + "m";
        _costText.text = _boostCost.ToString() + "G";
    }

    private void OnClick()
    {
        if (Currency.Gold > _boostCost)
        {
            _boostUI.SelectButton(this);
        }
    }

    public void OnSelected()
    {
        StartBoostManager.BoostCost = _boostCost;
        StartBoostManager.BoostDistance = _boostDistance;

        _backGroundImage.color = _selectedColor;
    }

    public void OnUnselected()
    {
        _backGroundImage.color = _unselectedColor;
    }
}
