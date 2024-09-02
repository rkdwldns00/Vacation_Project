using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private const float _itemRotateSpeed = 120f;

    protected abstract void UseItem(BuffSystem buffSystem);

    private void Update()
    {
        transform.Rotate(Vector3.up * _itemRotateSpeed * Time.deltaTime);
    }

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
