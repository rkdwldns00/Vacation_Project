using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager _instance;
    public static ObjectPoolManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject g = new GameObject("ObjectPool Manager");
                DontDestroyOnLoad(g);
                _instance = g.AddComponent<ObjectPoolManager>();
            }
            return _instance;
        }
    }

    // 생성할 오브젝트의 key값 저장을 위한 변수
    private GameObject poolObject;

    // 오브젝트풀들을 관리한 딕셔너리
    private Dictionary<GameObject, IObjectPool<GameObject>> _pool = new();

    private GameObject CreatePoolObject()
    {
        GameObject poolObject = Instantiate(this.poolObject);
        poolObject.GetComponent<ObjectPoolable>().Pool = _pool[this.poolObject];
        return poolObject;
    }

    private void OnGetPoolObject(GameObject poolGo)
    {
        poolGo.SetActive(true);
    }

    private void OnReleasePoolObject(GameObject poolGo)
    {
        poolGo.SetActive(false);
    }

    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }

    public GameObject GetPooledGameObject(GameObject prefab)
    {
        poolObject = prefab;

        if (_pool.ContainsKey(prefab) == false)
        {
            IObjectPool<GameObject> pool = new ObjectPool<GameObject>(CreatePoolObject, OnGetPoolObject, OnReleasePoolObject, OnDestroyPoolObject, true);

            _pool.Add(prefab, pool);
        }

        return _pool[prefab].Get();
    }
}
