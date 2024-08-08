using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Barrel Spawn Data", menuName = "Obstacle Spawn Datas/Barrel Spawn Data")]
public class BarrelSpawnData : ObstacleSpawnData
{
    [Header("Barrel Spawn Data")]
    [Space()]
    [SerializeField] GameObject _barrelObjectPrefab;
    [SerializeField] private int _minSpawnCount;
    [SerializeField] private int _maxSpawnCount;
    [SerializeField] private float _nextSpawnDistance;

    public override void SpawnObstacle(Vector3 playerPos)
    {
        int spawnCount = Random.Range(_minSpawnCount, _maxSpawnCount + 1);

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPos = new Vector3(0, 0, playerPos.z + _spawnDistance + _nextSpawnDistance * i);
            Instantiate(_barrelObjectPrefab, spawnPos, Quaternion.identity);
        }
    }
}
