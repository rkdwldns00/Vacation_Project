using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public Mesh originRoadMesh;
    public int roadMeshCount;
    public float playerBackSpaceLength;

    private MeshFilter meshFilter;
    private MeshCollider meshCollider;
    public Mesh curruntRoadMesh { get; private set; }
    private float originRoadMeshMinZ;
    private float originRoadMeshLength;

    private float lastSummonedMeshMinZ;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
    }

    private void Start()
    {
        if (!originRoadMesh.isReadable)
        {
            Debug.LogError("OriginRoadMesh 에셋의 Read/Write 설정이 비활성화 되어있어 메시를 읽을 수 없습니다.");
            Destroy(gameObject);
            return;
        }

        Mesh copiedOriginRoadMesh = new Mesh();
        copiedOriginRoadMesh.name = "Copyied originRoadMesh";
        copiedOriginRoadMesh.vertices = originRoadMesh.vertices;
        copiedOriginRoadMesh.triangles = originRoadMesh.triangles;
        copiedOriginRoadMesh.normals = originRoadMesh.normals;
        copiedOriginRoadMesh.uv = originRoadMesh.uv;
        originRoadMesh = copiedOriginRoadMesh;

        Vector3[] vertices = originRoadMesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            if (vertices[i].y > 0)
            {
                vertices[i].y = 0;
            }
        }
        originRoadMesh.vertices = vertices;


        transform.position = new Vector3(-originRoadMesh.bounds.center.x, transform.position.y, 0);

        originRoadMeshMinZ = originRoadMesh.vertices[0].z;
        float maxZ = originRoadMesh.vertices[0].z;
        for (int i = 1; i < originRoadMesh.vertexCount; i++)
        {
            float z = originRoadMesh.vertices[i].z;
            if (z > maxZ)
            {
                maxZ = z;
            }
            if (z < originRoadMeshMinZ)
            {
                originRoadMeshMinZ = z;
            }
        }
        originRoadMeshLength = maxZ - originRoadMeshMinZ;
        lastSummonedMeshMinZ = -playerBackSpaceLength;
        curruntRoadMesh = new Mesh() { name = "Road" };

        for (int i = 0; i < roadMeshCount; i++)
        {
            MoveMesh();
        }
    }

    private void Update()
    {
        if (Player.Instance != null && Player.Instance.transform.position.z - playerBackSpaceLength > lastSummonedMeshMinZ - originRoadMeshLength * roadMeshCount)
        {
            MoveMesh();
        }
    }

    private void MoveMesh()
    {
        curruntRoadMesh = MeshUtil.Merge(curruntRoadMesh, originRoadMesh, new Vector3(0, 0, lastSummonedMeshMinZ));
        lastSummonedMeshMinZ += originRoadMeshLength;

        (curruntRoadMesh, _) = MeshUtil.Cut(curruntRoadMesh, new Vector3(0, 0, lastSummonedMeshMinZ - originRoadMeshLength * roadMeshCount), Vector3.back);

        meshFilter.sharedMesh = curruntRoadMesh;
        meshCollider.sharedMesh = curruntRoadMesh;
    }

    public void SetMesh(Mesh mesh)
    {
        curruntRoadMesh = mesh;
        meshFilter.sharedMesh = curruntRoadMesh;
        meshCollider.sharedMesh = curruntRoadMesh;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!UnityEditor.EditorApplication.isPlaying)
        {
            for (int i = 0; i < 10; i++)
            {
                float roadLength = originRoadMesh.bounds.max.z - originRoadMesh.bounds.min.z;
                Gizmos.DrawMesh(originRoadMesh, new Vector3(-originRoadMesh.bounds.center.x, transform.position.y, roadLength * i));
            }
            Gizmos.DrawWireMesh(curruntRoadMesh, transform.position);
        }
    }
#endif
}
