using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ObjectPoolable
{
    private const float _itemRotateSpeed = 120f;

    protected abstract void UseItem(BuffSystem buffSystem);

    private void Update()
    {
        transform.Rotate(Vector3.up * _itemRotateSpeed * Time.deltaTime);

        if (transform.position.z + 12 < Player.Instance.transform.position.z)
        {
            ReleaseObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        BuffSystem buffSystem = other.GetComponent<BuffSystem>();
        if (buffSystem != null)
        {
            UseItem(buffSystem);
            ReleaseObject();
        }
    }
}
