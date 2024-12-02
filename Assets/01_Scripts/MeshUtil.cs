using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 코드 작성자 : 강지운 */
public static class MeshUtil
{
    const float BOTTOM_Y = -10f;

    public static Mesh Merge(Mesh a, Mesh b)
    {
        return Merge(a, b, Vector3.zero);
    }

    public static Mesh Merge(Mesh a, Mesh b, Vector3 bPos)
    {
        CombineInstance[] combine = new CombineInstance[2];

        combine[0].mesh = a;
        combine[0].transform = Matrix4x4.identity;
        combine[1].mesh = b;
        combine[1].transform = Matrix4x4.Translate(bPos);

        Mesh result = new Mesh();
        result.CombineMeshes(combine);

        return result;
    }

    static VertexPointer[] vertices = new VertexPointer[60000];
    static List<Vertex> back = new();
    static List<Vertex> forward = new();
    static List<int> backTri = new();
    static List<int> forwardTri = new();

    /*const int RESULT_MESH_VERTICES_MAX_COUNT = 60000;
    static Vector3[] backVertices = new Vector3[RESULT_MESH_VERTICES_MAX_COUNT];
    static Vector3[] backNormals = new Vector3[RESULT_MESH_VERTICES_MAX_COUNT];
    static Vector2[] backUv = new Vector2[RESULT_MESH_VERTICES_MAX_COUNT];
    static Vector3[] forwardVertices = new Vector3[RESULT_MESH_VERTICES_MAX_COUNT];
    static Vector3[] forwardNormals = new Vector3[RESULT_MESH_VERTICES_MAX_COUNT];
    static Vector2[] forwardUv = new Vector2[RESULT_MESH_VERTICES_MAX_COUNT];*/

    public static (Mesh backMesh, Mesh forwordMesh) Cut(Mesh mesh, Vector3 cutterPoint, Vector3 cutterNormal)
    {
        return Cut(mesh, cutterPoint, cutterNormal, false, Vector2.zero);
    }

    public static (Mesh backMesh, Mesh forwordMesh) Cut(Mesh mesh, Vector3 cutterPoint, Vector3 cutterNormal, bool showCuttedSlice, Vector2 cuttedSliceUV)
    {
        if (!mesh.isReadable)
        {
            Debug.LogError("자를 Mesh 에셋의 Read/Write 설정이 비활성화 되어있어 메시를 읽을 수 없습니다.");
            return (mesh, mesh);
        }

        Vector3[] meshVertices = mesh.vertices;
        Vector3[] meshNormals = mesh.normals;
        Vector2[] meshUv = mesh.uv;
        int[] meshTriangles = mesh.triangles;
        int meshVertexCount = mesh.vertexCount;

        //정점마다 절단면의 앞쪽인지 뒷쪽인지 확인
        int backCount = 0;
        int forwardCount = 0;

        for (int i = 0; i < meshVertexCount; i++)
        {
            vertices[i].isForwardMesh = Vector3.Dot(meshVertices[i] - cutterPoint, cutterNormal) > 0;
            if (vertices[i].isForwardMesh)
            {
                forwardCount++;
            }
            else
            {
                backCount++;
            }
        }


        //정점,normal,uv 계산
        back.Clear();
        forward.Clear();

        for (int i = 0; i < meshVertexCount; i++)
        {
            if (vertices[i].isForwardMesh)
            {
                vertices[i].index = forward.Count;
                forward.Add(new()
                {
                    point = meshVertices[i],
                    normal = meshNormals[i],
                    uv = meshUv[i]
                });
            }
            else
            {
                vertices[i].index = back.Count;
                back.Add(new()
                {
                    point = meshVertices[i],
                    normal = meshNormals[i],
                    uv = meshUv[i]
                });
            }
        }


        //삼각형 계산
        backTri.Clear();
        forwardTri.Clear();
        int backTriIndex = 0;
        int forwardTriIndex = 0;

        void AddTri(int a, int b, int c, bool isForward)
        {
            if (isForward)
            {
                forwardTri.Add(a);
                forwardTri.Add(b);
                forwardTri.Add(c);

                forwardTriIndex += 3;
            }
            else
            {
                backTri.Add(a);
                backTri.Add(b);
                backTri.Add(c);

                backTriIndex += 3;
            }
        }

        for (int i = 0; i < meshTriangles.Length; i += 3)
        {
            VertexPointer a = vertices[meshTriangles[i]];
            VertexPointer b = vertices[meshTriangles[i + 1]];
            VertexPointer c = vertices[meshTriangles[i + 2]];

            //정점이 모두 같은 방향에 있으면 삼각형 그대로 추가
            if (a.isForwardMesh == b.isForwardMesh && b.isForwardMesh == c.isForwardMesh)
            {
                AddTri(a.index, b.index, c.index, a.isForwardMesh);
            }
            //한 정점이 다른 방향에 있으면 삼각형 하나와 사각형(삼각형*2)으로 나눠서 추가
            else
            {
                VertexPointer point1;
                VertexPointer point2;
                VertexPointer crossedPoint;
                bool doubleVertexIsForward;

                if (a.isForwardMesh == b.isForwardMesh)
                {
                    doubleVertexIsForward = a.isForwardMesh;

                    point1 = a;
                    point2 = b;
                    crossedPoint = c;
                }
                else if (b.isForwardMesh == c.isForwardMesh)
                {
                    doubleVertexIsForward = b.isForwardMesh;

                    point1 = b;
                    point2 = c;
                    crossedPoint = a;
                }
                else
                {
                    doubleVertexIsForward = c.isForwardMesh;

                    point1 = c;
                    point2 = a;
                    crossedPoint = b;
                }

                //절단면과 선분의 교점 계산 함수
                Vertex GetMiddlePoint(Vertex a, Vertex b)
                {
                    Vector3 nAB = (b.point - a.point).normalized;

                    Vertex vertex = new Vertex()
                    {
                        point = a.point + nAB * Vector3.Dot(cutterNormal, (cutterPoint - a.point)) / Vector3.Dot(cutterNormal, nAB),
                        normal = (a.normal + b.normal).normalized,
                        uv = (a.uv + b.uv) / 2f,
                    };
                    return vertex;
                }

                Vertex middle1;
                Vertex middle2;

                List<Vertex> doublesList;
                List<Vertex> crossedList;

                if (doubleVertexIsForward)
                {
                    doublesList = forward;
                    crossedList = back;
                }
                else
                {
                    doublesList = back;
                    crossedList = forward;
                }

                middle1 = GetMiddlePoint(doublesList[point1.index], crossedList[crossedPoint.index]);
                middle2 = GetMiddlePoint(doublesList[point2.index], crossedList[crossedPoint.index]);

                //절단된 삼각형을 각 메시에 나눠서 추가
                int middle1Index = doublesList.Count;
                doublesList.Add(middle1);
                int middle2Index = doublesList.Count;
                doublesList.Add(middle2);
                int middle1CrossedIndex = crossedList.Count;
                crossedList.Add(middle1);
                int middle2CrossedIndex = crossedList.Count;
                crossedList.Add(middle2);

                AddTri(crossedPoint.index, middle1CrossedIndex, middle2CrossedIndex, !doubleVertexIsForward);
                AddTri(point1.index, point2.index, middle1Index, doubleVertexIsForward);
                AddTri(point2.index, middle2Index, middle1Index, doubleVertexIsForward);

                Vertex GetBottomVertex(Vertex a)
                {
                    Vertex bottom = a;
                    bottom.point = new Vector3(bottom.point.x, BOTTOM_Y, bottom.point.z);
                    return bottom;
                }

                //절단면 추가
                if (showCuttedSlice)
                {
                    middle1.uv = cuttedSliceUV;
                    middle2.uv = cuttedSliceUV;
                    Vertex bottom1 = GetBottomVertex(middle1);
                    Vertex bottom2 = GetBottomVertex(middle2);
                    bottom1.normal = cutterNormal;
                    bottom2.normal = cutterNormal;
                    middle1.normal = cutterNormal;
                    middle2.normal = cutterNormal;

                    middle1Index = doublesList.Count;
                    doublesList.Add(middle1);
                    middle2Index = doublesList.Count;
                    doublesList.Add(middle2);

                    middle1CrossedIndex = crossedList.Count;
                    crossedList.Add(middle1);
                    middle2CrossedIndex = crossedList.Count;
                    crossedList.Add(middle2);

                    int doubleBottom1Index = doublesList.Count;
                    doublesList.Add(bottom1);
                    int doubleBottom2Index = doublesList.Count;
                    doublesList.Add(bottom2);

                    int crossedBottom1Index = crossedList.Count;
                    crossedList.Add(bottom1);
                    int crossedBottom2Index = crossedList.Count;
                    crossedList.Add(bottom2);

                    AddTri(middle1CrossedIndex, middle2CrossedIndex, crossedBottom1Index, !doubleVertexIsForward);
                    AddTri(middle1CrossedIndex, crossedBottom1Index, middle2CrossedIndex, !doubleVertexIsForward);
                    AddTri(middle2CrossedIndex, crossedBottom2Index, crossedBottom1Index, !doubleVertexIsForward);
                    AddTri(middle2CrossedIndex, crossedBottom1Index, crossedBottom2Index, !doubleVertexIsForward);

                    AddTri(middle1Index, middle2Index, doubleBottom1Index, doubleVertexIsForward);
                    AddTri(middle1Index, doubleBottom1Index, middle2Index, doubleVertexIsForward);
                    AddTri(middle2Index, doubleBottom2Index, doubleBottom1Index, doubleVertexIsForward);
                    AddTri(middle2Index, doubleBottom1Index, doubleBottom2Index, doubleVertexIsForward);
                }
            }
        }

        //배열에 계산 결과 정리
        Vector3[] backVertices = new Vector3[back.Count];
        Vector3[] backNormals = new Vector3[back.Count];
        Vector2[] backUv = new Vector2[back.Count];
        for (int i = 0; i < back.Count; i++)
        {
            backVertices[i] = back[i].point;
            backNormals[i] = back[i].normal;
            backUv[i] = back[i].uv;
        }
        Vector3[] forwardVertices = new Vector3[forward.Count];
        Vector3[] forwardNormals = new Vector3[forward.Count];
        Vector2[] forwardUv = new Vector2[forward.Count];
        for (int i = 0; i < forward.Count; i++)
        {
            forwardVertices[i] = forward[i].point;
            forwardNormals[i] = forward[i].normal;
            forwardUv[i] = forward[i].uv;
        }

        //메쉬 제작 및 리턴
        Mesh backResult = new Mesh()
        {
            vertices = backVertices,
            normals = backNormals,
            uv = backUv,
            triangles = backTri.ToArray(),
            name = mesh.name
        };
        Mesh forwardResult = new Mesh()
        {
            vertices = forwardVertices,
            normals = forwardNormals,
            uv = forwardUv,
            triangles = forwardTri.ToArray(),
            name = mesh.name
        };
        return (backResult, forwardResult);
    }

    struct Vertex
    {
        public Vector3 point;
        public Vector3 normal;
        public Vector2 uv;
    }

    struct VertexPointer
    {
        public bool isForwardMesh;
        public int index;
    }
}
