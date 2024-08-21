using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected abstract void UseItem(BuffSystem buffSystem);

    private void OnTriggerEnter(Collider other)
    {
        BuffSystem buffSystem = other.GetComponent<BuffSystem>();
        if (buffSystem != null)
        {
            UseItem(buffSystem);
            Destroy(gameObject);
        }
    }
}
