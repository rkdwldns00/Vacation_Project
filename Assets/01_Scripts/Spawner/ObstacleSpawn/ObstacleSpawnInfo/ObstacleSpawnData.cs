using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnData : ScriptableObject, IObstacleSpawnable
{
    [Header("Obstacle Spawn Data")]
    [Space()]
    public float NextSpawnCoolTime;
    public int SpawnProbability;
    public int SpawnScore;
    [SerializeField] protected float _spawnDistance;

    public virtual void SpawnObstacle(Vector3 playerPos) {}
}
