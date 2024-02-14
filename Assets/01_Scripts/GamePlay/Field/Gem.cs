using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private float _chargeValue;

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Player>().ChargeBoost(_chargeValue);
        Destroy(gameObject);
    }
}
