using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SinkHole Spawn Data", menuName = "Obstacle Spawn Datas/SinkHole Spawn Data")]
public class SinkHoleSpawnData : ObstacleSpawnData
{
    [Header("Spike Spawn Data")]
    [Space()]
    [SerializeField] private GameObject _sinkHoleObjectPrefab;
    [SerializeField] private float _spawnXRange;
    [SerializeField] private float _spawnZRange;

    public override void SpawnObstacle(Vector3 playerPos)
    {
        Vector3 spawnPos = new Vector3(0, 0, playerPos.z + _spawnDistance);

        Instantiate(_sinkHoleObjectPrefab, spawnPos, Quaternion.identity);

    }
}
