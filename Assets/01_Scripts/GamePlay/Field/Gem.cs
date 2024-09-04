using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : ObjectPoolable
{
    [SerializeField] private float _chargeValue;
    [SerializeField] private GameObject _getGemEffect;

    private void Update()
    {
        if (transform.position.z + 12 < Player.Instance.transform.position.z)
        {
            ReleaseObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Player p = other.GetComponent<Player>();
        if (p != null)
        {
            p.ChargeBoost(_chargeValue);
            Instantiate(_getGemEffect, transform.position, Quaternion.identity);
            ReleaseObject();
        }
    }
}
