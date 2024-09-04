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
}
