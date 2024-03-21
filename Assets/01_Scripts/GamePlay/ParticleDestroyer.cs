using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    private void Awake()
    {
        float time = 0;

        if (GetComponent<ParticleSystem>()) time = GetComponent<ParticleSystem>().main.duration;
        else if (GetComponentInChildren<ParticleSystem>()) time = GetComponentInChildren<ParticleSystem>().main.duration;

        Destroy(gameObject, time);
    }
}
