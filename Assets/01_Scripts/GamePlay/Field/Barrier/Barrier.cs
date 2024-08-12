using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] private GameObject[] _barrierModelPrefabs;
    [SerializeField] private int _barrierCount;
    [SerializeField] private float _barrierModelSpawnStartXPos;
    [SerializeField] private float _barrierModelNextSpawnXDistance;

    private void Awake()
    {
        for (int i = 0; i < _barrierCount; i++)
        {
            int randBarrierIndex = Random.Range(0, _barrierModelPrefabs.Length);
            Vector3 spawnPos = new Vector3(_barrierModelSpawnStartXPos + _barrierModelNextSpawnXDistance * i, 0, transform.position.z);
            Instantiate(_barrierModelPrefabs[randBarrierIndex], spawnPos, Quaternion.identity);
        }
    }
}