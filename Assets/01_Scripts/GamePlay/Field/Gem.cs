using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private float _chargeValue;

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Player>().ChargeBoost(_chargeValue);
    }
}
