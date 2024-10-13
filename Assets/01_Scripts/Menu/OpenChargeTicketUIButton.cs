using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChargeTicketUIButton : MonoBehaviour
{
    [SerializeField] private ChargeTicketUI _ticketChargeUI;

    public void OpenTicketChargeUI()
    {
        _ticketChargeUI.OpenUI();
    }
}
