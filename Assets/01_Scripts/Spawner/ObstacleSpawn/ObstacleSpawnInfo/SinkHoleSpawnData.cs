using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SinkHole Spawn Data", menuName = "Obstacle Spawn Datas/SinkHole Spawn Data")]
public class SinkHoleSpawnData : ObstacleSpawnData
{
    [Header("Spike Spawn Data")]
    [Space()]
    [SerializeField] private GameObject _sinkHoleObjectPrefab;
    [SerializeField] private int _spawnCount;
    [SerializeField] private float _spawnXRange;
    [SerializeField] private float _spawnZRange;

    public override void SpawnObstacle(Vector3 playerPos)
    {
        Vector3 spawnPos = new Vector3(0, 0, playerPos.z + _spawnDistance);

        for (int i = 0; i < _spawnCount; i++)
        {
            float randomXRange = Random.Range(-_spawnXRange / 2, _spawnXRange / 2);
            float randomZRange = Random.Range(-_spawnZRange / 2, _spawnZRange / 2);
            Vector3 randomPos = new Vector3(randomXRange, 0, randomZRange);

            Instantiate(_sinkHoleObjectPrefab, spawnPos + randomPos, Quaternion.identity);
        }
    }
}
