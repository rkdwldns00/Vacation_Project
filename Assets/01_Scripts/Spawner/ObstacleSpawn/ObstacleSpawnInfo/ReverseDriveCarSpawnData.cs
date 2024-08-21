using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reverse Drive Car Spawn Data", menuName = "Obstacle Spawn Datas/Reverse Drive Car Spawn Data")]
public class ReverseDriveCarSpawnData : ObstacleSpawnData
{
    [Header("Reverse Drive Car Spawn Data")]
    [Space()]
    [SerializeField] private GameObject _reverseDriveCarObjectPrefab;
    [SerializeField] private int _spawnCount;
    [SerializeField] private float _nextSpawnXDistance;
    [SerializeField] private float _spawnStartXPos;

    public override void SpawnObstacle(Vector3 playerPos)
    {
        for (int i=0; i< _spawnCount; i++)
        {
            Vector3 spawnPos = new Vector3(_spawnStartXPos + _nextSpawnXDistance * i, 0, playerPos.z + _spawnDistance);
            Instantiate(_reverseDriveCarObjectPrefab, spawnPos, Quaternion.identity);
        }
    }
}
