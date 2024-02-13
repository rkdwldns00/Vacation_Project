using UnityEngine;

public class SinkHole : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minSize;
    public float maxSize;
    public float minDistance;
    public float maxDistance;
    public Material cuttedSliceMaterial;

    private void Start()
    {
        Collider[] cols = Physics.OverlapBox(transform.position, Vector3.one);
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
                    road.SetMesh(CutRoad(road.curruntRoadMesh,
                        Random.Range(0, 2) == 0,
                        transform.position.z - road.transform.position.z,
                        Random.Range(minDistance, maxDistance),
                        startX1, startX1 + size,
                        startX2, startX2 + size));

                    break;
                }
            }
        }
        Destroy(gameObject);
    }

    public Mesh CutRoad(Mesh mesh, bool isHole, float startZ, float HoleDistance, float startX1, float startX2, float endX1, float endX2)
    {
        Mesh before;
        Mesh middle;
        Mesh after;

        (before, middle) = MeshUtil.Cut(mesh, Vector3.forward * startZ, Vector3.forward);
        (middle, after) = MeshUtil.Cut(mesh, Vector3.forward * (startZ + HoleDistance), Vector3.forward);

        Mesh result = MeshUtil.Merge(before, after);
        Vector3 leftPoint = new Vector3(startX1, 0, startZ);
        Vector3 rightPoint = new Vector3(startX2, 0, startZ);
        Vector3 leftNormal = Vector3.Cross(new Vector3(startX1, 0, startZ) - new Vector3(endX1, 0, startZ + HoleDistance), Vector3.down);
        Vector3 rightNormal = Vector3.Cross(new Vector3(startX2, 0, startZ) - new Vector3(endX2, 0, startZ + HoleDistance), Vector3.up);

        if (isHole)
        {
            Mesh temp1, temp2;
            (_, temp1) = MeshUtil.Cut(middle, leftPoint, leftNormal);
            result = MeshUtil.Merge(result, temp1);
            (_, temp2) = MeshUtil.Cut(middle, rightPoint, rightNormal);
            result = MeshUtil.Merge(result, temp2);
        }
        else
        {
            Mesh temp;
            (temp, _) = MeshUtil.Cut(middle, leftPoint, leftNormal);
            (temp, _) = MeshUtil.Cut(temp, rightPoint, rightNormal);
            result = MeshUtil.Merge(result, temp);
        }

        return result;
    }
}
