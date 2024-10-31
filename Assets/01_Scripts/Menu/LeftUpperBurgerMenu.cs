using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftUpperBurgerMenu : ManagedUI
{
    [SerializeField] private GameObject _layer;
    [SerializeField] private Button _burgerMenuActiveBtn;

    public override void Awake()
    {
        _burgerMenuActiveBtn.onClick.AddListener(() =>
        {
            if (!_layer.activeSelf) OpenUI();
            else CloseUI();
        });
    }

    protected override void OnOpen()
    {
        _layer.SetActive(true);
    }

    protected override void OnClose()
    {
        _layer.SetActive(false);
    }
}
