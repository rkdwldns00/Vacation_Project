using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeTicketUI : MonoBehaviour
{
    [SerializeField] private GameObject _layer;

    public void OpenUI()
    {
        _layer.SetActive(true);
    }

    public void CloseUI()
    {
        _layer.SetActive(false);
    }
}
