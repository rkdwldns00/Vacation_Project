using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolable : MonoBehaviour
{
    public IObjectPool<GameObject> Pool { get; set; }

    protected void ReleaseObject()
    {
        Pool.Release(gameObject);
    }

    protected virtual void Update()
    {
        if (Player.Instance != null && transform.position.z + 20 < Player.Instance.transform.position.z)
        {
            ReleaseObject();
        }
    }
}
