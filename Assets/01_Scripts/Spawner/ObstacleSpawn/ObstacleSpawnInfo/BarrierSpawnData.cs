using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Barrier Spawn Data", menuName = "Obstacle Spawn Datas/Barrier Spawn Data")]
public class BarrierSpawnData : ObstacleSpawnData
{
    [Header("Barrier Spawn Data")]
    [Space()]
    [SerializeField] private GameObject _barrierObjectPrefab;
    [SerializeField] private int _minSpawnCount;
    [SerializeField] private int _maxSpawnCount;
    [SerializeField] private float _spawnZDistance;

    public override void SpawnObstacle(Vector3 playerPos)
    {
        int spawnCount = Random.Range(_minSpawnCount, _maxSpawnCount + 1);

        for (int i=0; i< spawnCount; i++)
        {
            Vector3 spawnPos = new Vector3(0, 0, playerPos.z + _spawnDistance + _spawnZDistance * i);
            Instantiate(_barrierObjectPrefab, spawnPos, Quaternion.identity);
        }
    }
}
