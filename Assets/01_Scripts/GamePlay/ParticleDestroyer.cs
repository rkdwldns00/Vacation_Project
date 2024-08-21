using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    private void Awake()
    {
        float destoryTime = 0;

        if (GetComponentInChildren<ParticleSystem>()) destoryTime = GetComponentInChildren<ParticleSystem>().main.duration;

        Destroy(gameObject, destoryTime);
    }
}
