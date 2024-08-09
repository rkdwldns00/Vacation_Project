using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Laser Spawn Data", menuName = "Obstacle Spawn Datas/Laser Spawn Data")]
public class LaserSpawnData : ObstacleSpawnData
{
    [Header("Laser Spawn Data")]
    [Space()]
    [SerializeField] private GameObject _laserObjectPrefab;

    public override void SpawnObstacle(Vector3 playerPos)
    {
        Vector3 spawnPos = new Vector3(0, 0, playerPos.z + _spawnDistance);
        Instantiate(_laserObjectPrefab, spawnPos, Quaternion.identity);
    }
}
