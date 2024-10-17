using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : ObjectPoolable, IObstacleResetable
{
    [SerializeField] private GameObject[] _barrierModelPrefabs;
    [SerializeField] private int _barrierCount;
    [SerializeField] private float _barrierModelSpawnStartXPos;
    [SerializeField] private float _barrierModelNextSpawnXDistance;

    public void ResetObstacle()
    {
        foreach(Transform t in GetComponentsInChildren<Transform>())
        {
            if (t != transform)
            {
                Destroy(t.gameObject);
            }
        }

        for (int i = 0; i < _barrierCount; i++)
        {
            int randBarrierIndex = Random.Range(0, _barrierModelPrefabs.Length);
            Vector3 spawnPos = new Vector3(_barrierModelSpawnStartXPos + _barrierModelNextSpawnXDistance * i, 0, transform.position.z);
            Transform t = Instantiate(_barrierModelPrefabs[randBarrierIndex], spawnPos, Quaternion.identity).transform;
            t.SetParent(transform);
        }
    }
}
