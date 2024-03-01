using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField] private GameObject[] _backGroundPrefabs;
    [SerializeField] private float _prefabSpacingZ;
    [SerializeField] private float _backGroundX;
    [SerializeField] private float _backGroundSpawnZ;

    private float LastSpawnedLeftZ;
    private float LastSpawnedRightZ;

    private void Update()
    {
        GenerateHandler();
    }

    private void GenerateHandler()
    {

    }

    private void SpawnPrefab(float x, float z)
    {

    }

    private void OnDrawGizmos()
    {
        DrawWireBox(_backGroundX);
        DrawWireBox(-_backGroundX);
    }

    private void DrawWireBox(float x)
    {
        Gizmos.DrawWireCube(new Vector3(x, 2.5f, _backGroundSpawnZ / 2f), new Vector3(0, 5, _backGroundSpawnZ));
        Gizmos.DrawLine(new Vector3(x, 0, 0), new Vector3(x, 5, _backGroundSpawnZ));
        Gizmos.DrawLine(new Vector3(x, 5, 0), new Vector3(x, 0, _backGroundSpawnZ));
    }
}
