using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshUtil
{
    public static Mesh Merge(Mesh a, Mesh b)
    {
        Vector3[] vertices = new Vector3[a.vertexCount + b.vertexCount];
        Vector3[] normals = new Vector3[a.normals.Length + b.normals.Length];
        Vector2[] uv = new Vector2[a.uv.Length + b.uv.Length];
        int[] tri = new int[a.triangles.Length + b.triangles.Length];
        int index = 0;
        for (int i = 0; i < a.vertexCount; i++)
        {
            vertices[index] = a.vertices[i];
            normals[index] = a.normals[i];
            uv[index] = a.uv[i];
            index++;
        }
        for (int i = 0; i < b.vertexCount; i++)
        {
            vertices[index] = b.vertices[i];
            normals[index] = b.normals[i];
            uv[index] = b.uv[i];
            index++;
        }
        index = 0;
        for (int i = 0; i < a.triangles.Length; i++)
        {
            tri[index] = a.triangles[i];
            index++;
        }
        for (int i = 0; i < b.triangles.Length; i++)
        {
            tri[index] = b.triangles[i] + a.vertexCount;
            index++;
        }

        return new Mesh()
        {
            name = a.name,
            vertices = vertices,
            normals = normals,
            uv = uv,
            triangles = tri,
        };
    }

    public static (Mesh backMesh, Mesh forwordMesh) Cut(Mesh mesh, Vector3 cutterPoint, Vector3 cutterNormal)
    {
        //정점마다 절단면의 앞쪽인지 뒷쪽인지 확인
        VertexPointer[] vertices = new VertexPointer[mesh.vertexCount];
        int backCount = 0;
        int forwardCount = 0;

        for (int i = 0; i < mesh.vertexCount; i++)
        {
            vertices[i].isForwardMesh = Vector3.Dot(mesh.vertices[i] - cutterPoint, cutterNormal) > 0;
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
        List<Vertex> back = new();
        List<Vertex> forward = new();

        for (int i = 0; i < vertices.Length; i++)
        {
            if (vertices[i].isForwardMesh)
            {
                vertices[i].index = forward.Count;
                forward.Add(new()
                {
                    point = mesh.vertices[i],
                    normal = mesh.normals[i],
                    uv = mesh.uv[i]
                });
            }
            else
            {
                vertices[i].index = back.Count;
                back.Add(new()
                {
                    point = mesh.vertices[i],
                    normal = mesh.normals[i],
                    uv = mesh.uv[i]
                });
            }
        }


        //삼각형 계산
        List<int> forwardTri = new();
        List<int> backTri = new();
        int forwardTriIndex = 0;
        int backTriIndex = 0;

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

        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            VertexPointer a = vertices[mesh.triangles[i]];
            VertexPointer b = vertices[mesh.triangles[i + 1]];
            VertexPointer c = vertices[mesh.triangles[i + 2]];

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
                    Vertex vertex = new Vertex();
                    Vector3 nAB = (b.point - a.point).normalized;

                    vertex.point = a.point + nAB * Vector3.Dot(cutterNormal, (cutterPoint - a.point)) / Vector3.Dot(cutterNormal, nAB);
                    vertex.normal = (a.normal + b.normal).normalized;
                    vertex.uv = (a.uv + b.uv) / 2f;
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
