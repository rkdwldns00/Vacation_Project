using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 코드 작성자 : 이기환 */
public abstract class Item : ObjectPoolable
{
    private const float _itemRotateSpeed = 120f;

    protected abstract void UseItem(BuffSystem buffSystem);

    protected override void Update()
    {
        base.Update();

        transform.Rotate(Vector3.up * _itemRotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        BuffSystem buffSystem = other.GetComponent<BuffSystem>();
        if (buffSystem != null)
        {
            UseItem(buffSystem);
            SoundManager.Instance.PlaySound(SoundManager.Instance.SoundData.GetItemSfx);
            ReleaseObject();
        }
    }
}
