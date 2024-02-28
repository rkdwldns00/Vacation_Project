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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!UnityEditor.EditorApplication.isPlaying)
        {
            MeshFilter filter = _customModels[0].GetComponentInChildren<MeshFilter>();
            Mesh mesh = filter.sharedMesh;
            for (int i = 0; i < mesh.subMeshCount; i++)
            {
                Vector3 scale = filter.transform.lossyScale;
                scale.x *= _customModelPos.lossyScale.x;
                scale.y *= _customModelPos.lossyScale.y;
                scale.z *= _customModelPos.lossyScale.z;
                Gizmos.DrawMesh(mesh, i, _customModelPos.position, _customModelPos.rotation, scale);
            }
        }
    }
#endif
}
