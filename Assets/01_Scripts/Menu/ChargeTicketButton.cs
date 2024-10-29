using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChargeTicketButton : MonoBehaviour
{
    private const int _ticketCost = 50;

    [SerializeField] private int _ticketCount;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private TextMeshProUGUI _costText;


    void Start()
    {
        _countText.text = "x" + _ticketCount;
        _costText.text = "★" + GetTicketChargeCost().ToString();
    }

    public int GetTicketChargeCost()
    {
        return _ticketCost * _ticketCount;
    }

    public void buyTicket()
    {
        if (Currency.Crystal >= GetTicketChargeCost())
        {
            Currency.Crystal -= GetTicketChargeCost();
            Currency.Ticket += _ticketCount;
        }
    }
}
