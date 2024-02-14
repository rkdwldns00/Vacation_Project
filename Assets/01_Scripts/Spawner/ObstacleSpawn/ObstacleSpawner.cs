using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<ObstacleSpawnData> _obstacleSpawnDatas;

    private float _obstacleSpawnCoolTime;

    private void Update()
    {
        if (_obstacleSpawnCoolTime <= Time.time)
        {
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        int spawnProbability = 0;

        foreach (ObstacleSpawnData data in _obstacleSpawnDatas)
        {
            spawnProbability += data.SpawnProbability;
        }

        int rand = Random.Range(0, spawnProbability);

        for (int i=0; i<_obstacleSpawnDatas.Count; i++)
        {
            if (rand <= spawnProbability)
            {
                _obstacleSpawnDatas[i].SpawnObstacle(Vector3.zero);
                _obstacleSpawnCoolTime = Time.time + _obstacleSpawnDatas[i].NextSpawnCoolTime;
                break;
            }

            spawnProbability += _obstacleSpawnDatas[i].SpawnProbability;
        }
    }
}
