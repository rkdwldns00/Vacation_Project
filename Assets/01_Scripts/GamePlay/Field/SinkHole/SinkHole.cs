using UnityEngine;

public class SinkHole : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minSize;
    public float maxSize;
    public float minDistance;
    public float maxDistance;
    public int minCount;
    public int maxCount;

    public Vector2 cuttedSliceUV;

    private void Update()
    {
        Collider[] cols = Physics.OverlapBox(transform.position + Vector3.forward * (maxDistance + 1f), Vector3.one);
        foreach (Collider col in cols)
        {
            if (col.CompareTag("Road"))
            {
                float size = Mathf.Min(Random.Range(minSize, maxSize), maxX - minX);
                float startX1 = Random.Range(minX, maxX - size);
                float startX2 = Random.Range(minX, maxX - size);

                Road road = col.GetComponent<Road>();
                if (road is not null)
                {
                    float totalDistance = 0;
                    bool isHole = Random.Range(0, 2) == 0;
                    int count = Random.Range(minCount, maxCount + 1);

                    for (int i = 0; i < count; i++)
                    {
                        float distance = Random.Range(minDistance, maxDistance);
                        road.SetMesh(CutRoad(road.curruntRoadMesh,
                            isHole,
                            transform.position.z - road.transform.position.z + totalDistance,
                            distance,
                            startX1, startX1 + size,
                            startX2, startX2 + size, i == 2));

                        startX1 = startX2;
                        startX2 = Random.Range(minX, maxX - size);
                        totalDistance += distance;
                    }

                    Destroy(gameObject);
                    break;
                }
            }
        }
    }

    public Mesh CutRoad(Mesh mesh, bool isHole, float startZ, float HoleDistance, float startX1, float startX2, float endX1, float endX2, bool generateFrontWall)
    {
        Vector3 centor = new Vector3(mesh.bounds.center.x, 0, 0);

        Mesh before;
        Mesh middle;
        Mesh after;

        (before, middle) = MeshUtil.Cut(mesh, centor + Vector3.forward * startZ, Vector3.forward);
        (middle, after) = MeshUtil.Cut(middle, centor + Vector3.forward * (startZ + HoleDistance), Vector3.forward, generateFrontWall, cuttedSliceUV);

        Mesh result = MeshUtil.Merge(before, after);
        Vector3 leftPoint = new Vector3(startX1, 0, startZ) + centor;
        Vector3 rightPoint = new Vector3(startX2, 0, startZ) + centor;
        Vector3 leftNormal = Vector3.Cross(leftPoint - new Vector3(endX1, 0, startZ + HoleDistance) - centor, Vector3.down);
        Vector3 rightNormal = Vector3.Cross(rightPoint - new Vector3(endX2, 0, startZ + HoleDistance) - centor, Vector3.up);

        if (isHole)
        {
            Mesh temp1, temp2;
            (_, temp1) = MeshUtil.Cut(middle, leftPoint, leftNormal, true, cuttedSliceUV);
            result = MeshUtil.Merge(result, temp1);
            (_, temp2) = MeshUtil.Cut(middle, rightPoint, rightNormal, true, cuttedSliceUV);
            result = MeshUtil.Merge(result, temp2);
        }
        else
        {
            Mesh temp;
            (temp, _) = MeshUtil.Cut(middle, leftPoint, leftNormal, true, cuttedSliceUV);
            (temp, _) = MeshUtil.Cut(temp, rightPoint, rightNormal, true, cuttedSliceUV);
            result = MeshUtil.Merge(result, temp);

            Mesh wallMesh = new Mesh();
            wallMesh.vertices = new Vector3[] {
                new Vector3(mesh.bounds.min.x,mesh.bounds.max.y,startZ),                //왼쪽 벽
                new Vector3(mesh.bounds.min.x,mesh.bounds.max.y,startZ+HoleDistance),
                new Vector3(mesh.bounds.min.x,-10,startZ),
                new Vector3(mesh.bounds.min.x,-10,startZ+HoleDistance)
                };
            wallMesh.triangles = new int[] { 0, 1, 2, 1, 3, 2 };
            wallMesh.normals = new Vector3[] { Vector3.right, Vector3.right, Vector3.right, Vector3.right };
            wallMesh.uv = new Vector2[] { cuttedSliceUV, cuttedSliceUV, cuttedSliceUV, cuttedSliceUV };
            result = MeshUtil.Merge(result, wallMesh);
        }

        Mesh floorMesh = new Mesh();
        floorMesh.vertices = new Vector3[] {
                new Vector3(mesh.bounds.min.x, -10, startZ),
                new Vector3(mesh.bounds.min.x, -10, startZ+HoleDistance),
                new Vector3(mesh.bounds.max.x, -10, startZ),
                new Vector3(mesh.bounds.max.x, -10, startZ+HoleDistance),
                };
        floorMesh.triangles = new int[] { 0, 1, 2, 1, 3, 2 };
        floorMesh.normals = new Vector3[] { Vector3.up, Vector3.up, Vector3.up, Vector3.up };
        floorMesh.uv = new Vector2[] { cuttedSliceUV, cuttedSliceUV, cuttedSliceUV, cuttedSliceUV };
        result = MeshUtil.Merge(result, floorMesh);


        return result;
    }
}
