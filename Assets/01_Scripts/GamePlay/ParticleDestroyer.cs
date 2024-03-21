using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    private void Awake()
    {
        float destoryTime = 0;

        if (GetComponent<ParticleSystem>()) destoryTime = GetComponent<ParticleSystem>().main.duration;
        else if (GetComponentInChildren<ParticleSystem>()) destoryTime = GetComponentInChildren<ParticleSystem>().main.duration;

        Destroy(gameObject, destoryTime);
    }
}
