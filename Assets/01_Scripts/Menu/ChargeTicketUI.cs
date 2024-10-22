using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeTicketUI : ManagedUI
{
    [SerializeField] private GameObject _layer;
    [SerializeField] private Button _closeButton;

    public override void Awake()
    {
        base.Awake();
        _closeButton.onClick.AddListener(CloseUI);
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
