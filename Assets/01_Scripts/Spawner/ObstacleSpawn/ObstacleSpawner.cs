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
                Vector3 playerPos = new Vector3(0, 0, Player.Instance.transform.position.z);
                _obstacleSpawnDatas[i].SpawnObstacle(playerPos);
                _obstacleSpawnCoolTime = Time.time + _obstacleSpawnDatas[i].NextSpawnCoolTime;
                break;
            }

            spawnProbability += _obstacleSpawnDatas[i].SpawnProbability;
        }
    }
}
