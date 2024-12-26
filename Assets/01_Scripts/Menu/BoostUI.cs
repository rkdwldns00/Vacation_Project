using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostUI : ManagedUI
{
    [SerializeField] private GameObject _layer;
    [SerializeField] private Button _closeButton;
    [SerializeField] private SelectBoostDistanceButton _defaultBoostButton;
    private SelectBoostDistanceButton _selectedBoostButton;

    public override void Awake()
    {
        base.Awake();
        _closeButton.onClick.AddListener(CloseUI);
    }

    private void Start()
    {
        _selectedBoostButton = _defaultBoostButton;
        _defaultBoostButton.OnSelected();
    }

    protected override void OnOpen()
    {
        _layer.SetActive(true);
    }

    protected override void OnClose()
    {
        _layer.SetActive(false);
    }

    public void SelectButton(SelectBoostDistanceButton button)
    {
        if (_selectedBoostButton == button) return;

        _selectedBoostButton.OnUnselected();
        _selectedBoostButton = button;
        button.OnSelected();
    }
}
