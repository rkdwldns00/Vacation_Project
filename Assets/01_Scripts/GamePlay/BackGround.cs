using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField] private GameObject[] _buildingPrefabs;
    [SerializeField] private GameObject _groundPrefab;
    [SerializeField] private float _buildingSpacingZ;
    [SerializeField] private float _groundX;
    [SerializeField] private float _buildingX;
    [SerializeField] private float _backGroundStartZ;
    [SerializeField] private float _backGroundSpawnZ;

    private float _lastBuildingSpawnedLeftZ;
    private float _lastBuildingSpawnedRightZ;

    private float _lastGroundSpawnedZ;

    private Mesh _groundMesh;

    private void Start()
    {
        _groundMesh = _groundPrefab.GetComponent<MeshFilter>().sharedMesh;

        _lastBuildingSpawnedLeftZ = _backGroundStartZ;
        _lastBuildingSpawnedRightZ = _backGroundStartZ;
        _lastGroundSpawnedZ = _backGroundStartZ;
    }

    private void Update()
    {
        GenerateHandler();
    }

    private void GenerateHandler()
    {
        float f;
        if (Player.Instance == null)
        {
            f = transform.position.z + _backGroundSpawnZ;
        }
        else
        {
            f = Player.Instance.transform.position.z + _backGroundSpawnZ;
        }


        while (_lastBuildingSpawnedLeftZ < f)
        {
            _lastBuildingSpawnedLeftZ = SpawnBuilding(true, _lastBuildingSpawnedLeftZ + _buildingSpacingZ);
        }
        while (_lastBuildingSpawnedRightZ < f)
        {
            _lastBuildingSpawnedRightZ = SpawnBuilding(false, _lastBuildingSpawnedRightZ + _buildingSpacingZ);
        }

        while (_lastGroundSpawnedZ < f)
        {
            Vector3 pos = new Vector3(_groundX + _groundMesh.bounds.size.x / 2f, 0,
                _lastGroundSpawnedZ + _groundMesh.bounds.size.z / 2f + _buildingSpacingZ);

            GameObject ground1 = ObjectPoolManager.Instance.GetPooledGameObject(_groundPrefab);
            ground1.transform.position = pos;
            pos.x = -pos.x;
            GameObject ground2 = ObjectPoolManager.Instance.GetPooledGameObject(_groundPrefab);
            ground2.transform.position = pos;

            _lastGroundSpawnedZ += _groundMesh.bounds.size.z;
        }
    }

    private float SpawnBuilding(bool isLeft, float z)
    {
        GameObject prefab = _buildingPrefabs[Random.Range(0, _buildingPrefabs.Length)];
        Mesh mesh = prefab.GetComponentInChildren<MeshFilter>().sharedMesh;
        float meshZSize = mesh.bounds.size.z;
        float meshXSize = mesh.bounds.size.x;

        if (isLeft)
        {
            Instantiate(prefab, new Vector3(-_buildingX - meshXSize / 2f, 0f, z + meshZSize / 2f), Quaternion.identity);
        }
        else
        {
            Instantiate(prefab, new Vector3(_buildingX + meshXSize / 2f, 0f, z + meshZSize / 2f), Quaternion.identity);
        }

        return z + meshZSize;
    }

    private void OnDrawGizmos()
    {
        DrawWireBox(_buildingX, _groundX);
        DrawWireBox(-_buildingX, -_groundX);
    }

    private void DrawWireBox(float buildingX, float groundX)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3((groundX + buildingX) / 2f, 0, _backGroundSpawnZ / 2f), new Vector3(buildingX - groundX, 0, _backGroundSpawnZ));
        Gizmos.DrawLine(new Vector3(groundX, 0, 0), new Vector3(buildingX, 0, _backGroundSpawnZ));
        Gizmos.DrawLine(new Vector3(buildingX, 0, 0), new Vector3(groundX, 0, _backGroundSpawnZ));

        Gizmos.color = Color.gray;
        Gizmos.DrawWireCube(new Vector3(buildingX, 2.5f, _backGroundSpawnZ / 2f), new Vector3(0, 5, _backGroundSpawnZ));
        Gizmos.DrawLine(new Vector3(buildingX, 0, 0), new Vector3(buildingX, 5, _backGroundSpawnZ));
        Gizmos.DrawLine(new Vector3(buildingX, 5, 0), new Vector3(buildingX, 0, _backGroundSpawnZ));
    }
}
