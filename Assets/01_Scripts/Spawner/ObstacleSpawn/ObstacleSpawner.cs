using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<ObstacleSpawnData> _obstacleSpawnDatas;

    private float _obstacleSpawnCoolTime;

    private void Awake()
    {
        CheckObstacleSpawnProbability();
    }

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

    private void CheckObstacleSpawnProbability()
    {
        int probability = 0;

        foreach (ObstacleSpawnData data in _obstacleSpawnDatas)
        {
            probability += data.SpawnProbability;
        }

        if (probability < 100)
        {
            Debug.LogWarning("probability less than 100");
        }
        else if (probability > 100)
        {
            Debug.LogWarning("probability greater than 100");
        }
    }
}
