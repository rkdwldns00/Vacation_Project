using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<ObstacleSpawnData> _obstacleSpawnDatas;

    private float _obstacleSpawnCoolTime;

    private void Update()
    {
        if (_obstacleSpawnCoolTime <= Time.time && Player.Instance)
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

        int rand = Random.Range(1, spawnProbability + 1);
        spawnProbability = 0;

        for (int i=0; i<_obstacleSpawnDatas.Count; i++)
        {
            spawnProbability += _obstacleSpawnDatas[i].SpawnProbability;

            if (rand <= spawnProbability && _obstacleSpawnDatas[i].SpawnScore <= GameManager.Instance.Score)
            {
                _obstacleSpawnDatas[i].SpawnObstacle(Player.Instance.transform.position);
                _obstacleSpawnCoolTime = Time.time + _obstacleSpawnDatas[i].NextSpawnCoolTime;
                break;
            }
        }
    }
}
