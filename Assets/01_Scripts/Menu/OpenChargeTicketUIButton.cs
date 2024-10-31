using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenChargeTicketUIButton : MonoBehaviour
{
    [SerializeField] private ChargeTicketUI _ticketChargeUI;
    [SerializeField] private Button button;

    private void Awake()
    {
        button.onClick.AddListener(() => { _ticketChargeUI.OpenUI(EUIType.Page); });
    }
}
