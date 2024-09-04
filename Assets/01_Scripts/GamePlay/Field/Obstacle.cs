using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void Update()
    {
        if (Player.Instance != null && transform.position.z + 20 < Player.Instance.transform.position.z)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Player>()?.TakeDamage();
    }
}
