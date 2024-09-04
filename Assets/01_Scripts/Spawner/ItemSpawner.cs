using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _itemPrefabs;
    [SerializeField] private float _itemSpawnPercent;
    [SerializeField] private int _obstacleScanerZ;
    [SerializeField] private int _itemSpawnZ;
    [SerializeField] private int _minX;
    [SerializeField] private int _maxX;

    private int _xSize => _maxX + 1 - _minX;
    private int _zSize => _obstacleScanerZ - _itemSpawnZ;

    private int _playerZWhenLastScan = -1;
    private ScanDataType[,] _scanDatas;

    private int _scanDataStartZ => Mathf.Max(0, _playerZWhenLastScan - _zSize + 1);

    private void Start()
    {
        _scanDatas = new ScanDataType[_xSize, _zSize];
        ScanHandler();
    }

    private void FixedUpdate()
    {
        ScanHandler();
    }

    private void ScanHandler()
    {
        while (Player.Instance != null && _playerZWhenLastScan < Player.Instance.transform.position.z + _obstacleScanerZ)
        {
            _playerZWhenLastScan++;

            bool allIsObstacle = true;
            int z = _playerZWhenLastScan % _zSize;

            for (int i = _minX; i < _maxX + 1; i++)
            {
                RaycastHit hit;
                bool isHit = Physics.Raycast(new Vector3(i, 10, _playerZWhenLastScan), Vector3.down, out hit, 11);
                bool isObstacle;
                if (isHit)
                {
                    isObstacle = hit.transform.gameObject.layer != LayerMask.NameToLayer("Road");
                }
                else
                {
                    isObstacle = true;
                }
                if (isHit)
                {
                    allIsObstacle = false;
                }
                _scanDatas[i - _minX, z] = isObstacle ? ScanDataType.Obstacle : ScanDataType.None;
            }
            if (allIsObstacle)
            {
                for (int i = _minX; i < _maxX + 1; i++)
                {
                    _scanDatas[i - _minX, z] = ScanDataType.WithoutGemSpace;
                }
            }

            SpawnItemHandler();
        }
    }

    private ScanDataType GetScanData(int x, int z)
    {
        return _scanDatas[x - _minX, (_scanDataStartZ % _zSize + z - _scanDataStartZ) % _zSize];
    }

    private void SpawnItemHandler()
    {
        if (Random.Range(0f, 100f) > _itemSpawnPercent)
        {
            return;
        }

        int z = _playerZWhenLastScan;
        int blankCount = 0;
        for (int x = _minX; x < _maxX + 1; x++)
        {
            if (GetScanData(x, z) == ScanDataType.None)
            {
                blankCount++;
            }
        }
        int random = Random.Range(0, blankCount);
        blankCount = 0;

        for (int x = _minX; x < _maxX + 1; x++)
        {
            if (GetScanData(x, z) == ScanDataType.None)
            {
                if (blankCount == random)
                {
                    SpawnItemPrefab(new Vector3(x, 0, z));
                    break;
                }
                blankCount++;
            }
        }
    }

    private void SpawnItemPrefab(Vector3 position)
    {
        int randIndex = Random.Range(0, _itemPrefabs.Length);
        GameObject spawnItem = ObjectPoolManager.Instance.GetPooledGameObject(_itemPrefabs[randIndex]);
        spawnItem.transform.position = position;
    }

    private enum ScanDataType
    {
        None,
        Obstacle,
        WithoutGemSpace
    }
}
