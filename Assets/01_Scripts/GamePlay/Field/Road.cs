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
        lastSummonedMeshMinZ = originRoadMeshMinZ;
        curruntRoadMesh = new Mesh() { name = "Road" };

        for (int i = 0; i < roadMeshCount; i++)
        {
            MoveMesh();
        }
    }

    private void Update()
    {
        if (Player.Instance.transform.position.z - playerBackSpaceLength > lastSummonedMeshMinZ - originRoadMeshLength * roadMeshCount)
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
}
