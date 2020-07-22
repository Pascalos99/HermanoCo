using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class OneSidedCircleMeshGenerator : MonoBehaviour
{
    public MeshFilter filter;
    public int resolution = 100;
    public double radius = 1;

    Vector3[] vertices;
    Vector2[] UVs;
    int[] triangles;

    void Start()
    {
        if (filter == null) filter = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        filter.mesh = mesh;

        vertices = new Vector3[resolution + 1];
        UVs = new Vector2[resolution + 1];
        triangles = new int[3 * resolution];

        Vector3 origin = transform.position;
        vertices[resolution] = origin;

        UVs[resolution] = new Vector2(0.5f, 0.5f);
        float UVradius = 0.5f;

        double angle_partition = 2 * math.PI_DBL / resolution;
        for (int i = 0; i < resolution; i++)
        {
            double angle = angle_partition * i;
            double cos = math.cos(angle);
            double sin = math.sin(angle);
            Vector2 dir = new Vector2((float)cos, (float)sin);
            UVs[i] = UVs[resolution] + dir * UVradius;
            vertices[i] = origin + new Vector3((float)(cos * radius), (float)(sin * radius), 0);
            int pointB = (i + 1) % resolution;
            int j = i * 3;
            triangles[j + 0] = resolution;
            triangles[j + 1] = pointB;
            triangles[j + 2] = i;
        }

        mesh.vertices = vertices;
        mesh.uv = UVs;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        AssetDatabase.CreateAsset(mesh, "Assets\\Custom\\Meshes\\flatCircle" + resolution + "_" + ((int)(radius * 100)) + ".asset");
    }
}
