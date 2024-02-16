using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "Spike Spawn Data", menuName = "Obstacle Spawn Datas/Spike Spawn Data")]
public class SpikeSpawnData : ObstacleSpawnData
{
    [Header("Spike Spawn Data")]
    [Space()]
    [SerializeField] private GameObject _spikeObjectPrefab;
    [SerializeField] private int _minSpikeSpawnCount;
    [SerializeField] private int _maxSpikeSpawnCount;
    [SerializeField] private int _minLineSpawnCount;
    [SerializeField] private int _maxLineSpawnCount;
    [SerializeField] private float _lineDistance;

    public override void SpawnObstacle(Vector3 playerPos)
    {
        int lineSpawnCount = Random.Range(_minLineSpawnCount, _maxLineSpawnCount + 1);

        for (int i=0; i<lineSpawnCount; i++)
        {
            Vector3 spawnPos = new Vector3(0, 0, playerPos.z + _spawnDistance + _lineDistance * i);

            int spawnNumer = Random.Range(_minSpikeSpawnCount-1, _maxSpikeSpawnCount); // Numerator 분자
            int spawnDenom = 5;                                                        // Denominator 분모
            float spawnX = -4;

            for (int j=0; j<5; j++)
            {
                int rand = Random.Range(0, spawnDenom);

                if (spawnNumer >= rand)
                {
                    Instantiate(_spikeObjectPrefab, new Vector3(spawnX, 0, spawnPos.z), Quaternion.identity);
                    spawnNumer--;
                }

                spawnDenom--;
                spawnX += 2;
            }
        }
    }
}
