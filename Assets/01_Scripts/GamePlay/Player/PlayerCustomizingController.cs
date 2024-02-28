using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomizingController : MonoBehaviour
{
    [SerializeField] private GameObject[] _customModels;
    [SerializeField] private Transform _customModelPos;

    private GameObject playerModel => _customModels[GameManager.Instance.PlayerModelId];

    private void Awake()
    {
        Instantiate(playerModel, _customModelPos);
    }

    private void OnDrawGizmos()
    {
        Mesh mesh = _customModels[0].GetComponentInChildren<MeshFilter>().sharedMesh;
        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            Gizmos.DrawMesh(mesh, i, _customModelPos.position, _customModelPos.rotation, _customModelPos.lossyScale);
        }
    }
}
