using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Falling Obstacle Spawn Data", menuName = "Obstacle Spawn Datas/Falling Obstacle Spawn Data")]
public class FallingObstacleSpawnData : ObstacleSpawnData
{
    [Header("Falling Obstacle Spawn Data")]
    [Space()]
    [SerializeField] private GameObject _fallingObstaclePrefab;
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

            GameObject obstacle = ObjectPoolManager.Instance.GetPooledGameObject(_fallingObstaclePrefab);
            obstacle.transform.position = spawnPos + randomPos;
            obstacle.GetComponent<IObstacleResetable>().ResetObstacle();
        }
    }
}
