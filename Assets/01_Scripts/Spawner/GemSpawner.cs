using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _gemPrefab;
    [SerializeField] private float _gemSpawnPercent;
    [SerializeField] private int _obstacleScanerZ;
    [SerializeField] private int _gemSpawnZ;
    [SerializeField] private int _minX;
    [SerializeField] private int _maxX;

    private int _xSize => _maxX + 1 - _minX;
    private int _zSize => _obstacleScanerZ - _gemSpawnZ;

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

    private void OnDrawGizmosSelected()
    {
        for (int z = _scanDataStartZ; z < _scanDataStartZ + _zSize; z++)
        {
            for (int i = 0; i < _xSize; i++)
            {
                Color color = Color.white;
                var a = GetScanData(i + _minX, z);
                if (a == ScanDataType.None)
                {
                    color = Color.black;
                }
                else if (a == ScanDataType.Obstacle)
                {
                    color = Color.red;
                }
                else if (a == ScanDataType.WithoutGemSpace)
                {
                    color = Color.blue;
                }
                Gizmos.color = color;
                Gizmos.DrawCube(new Vector3(_minX + i, 0, z), Vector3.one);
            }
        }
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
                bool isObstacle = Physics.Raycast(new Vector3(i, 10, _playerZWhenLastScan), Vector3.down, out hit, 11, ~(1 << LayerMask.NameToLayer("Road")));
                if (!isObstacle)
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

            SpawnGemHandler();
        }
    }

    private ScanDataType GetScanData(int x, int z)
    {
        return _scanDatas[x - _minX, (_scanDataStartZ % _zSize + z - _scanDataStartZ) % _zSize];
    }

    private void SpawnGemHandler()
    {
        if (Random.Range(0f, 100f) > _gemSpawnPercent)
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
                    SpawnGemPrefab(new Vector3(x, 0, z));
                    break;
                }
                blankCount++;
            }
        }
    }

    private void SpawnGemPrefab(Vector3 position)
    {
        Instantiate(_gemPrefab, position, Quaternion.identity);
    }

    private enum ScanDataType
    {
        None,
        Obstacle,
        WithoutGemSpace
    }
}
