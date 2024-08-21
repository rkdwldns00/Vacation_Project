using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _crystalText;
    [SerializeField] private TextMeshProUGUI _ticketText;
    [SerializeField] private TextMeshProUGUI _ticketTimerText;

    private void Awake()
    {
        UpdateGoldUI(0);
        UpdateCrystalUI(0);
        UpdateTicketUI(0);

        Currency.OnChangedGold += UpdateGoldUI;
        Currency.OnChangedCrystal += UpdateCrystalUI;
        Currency.OnChangedTicket += UpdateTicketUI;
    }

    private void Update()
    {
        Currency.UpdateTicket();
    }

    private void OnDestroy()
    {
        Currency.OnChangedGold -= UpdateGoldUI;
        Currency.OnChangedCrystal -= UpdateCrystalUI;
        Currency.OnChangedTicket -= UpdateTicketUI;
    }

    private void UpdateGoldUI(int delta)
    {
        _goldText.text = Currency.Gold.ToString() + "G";
    }

    private void UpdateCrystalUI(int delta)
    {
        _crystalText.text = Currency.Crystal.ToString();
    }

    private void UpdateTicketUI(int delta)
    {
        _ticketText.text = string.Format("{0}/{1}", Currency.Ticket, Currency.MaxTicket);

        int remainTime = Currency.TicketHealTime - Currency.TicketHealingTimer;
        _ticketTimerText.text = string.Format("{0}:{1}", remainTime / 60, remainTime % 60);
        _ticketTimerText.gameObject.SetActive(Currency.Ticket < Currency.MaxTicket);
    }
}
