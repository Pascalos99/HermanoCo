using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CircleMeshGenerator : MonoBehaviour
{
    public MeshFilter filter;
    public int resolution = 100;
    public double radius = 1;
    public float thickness = 0.2f;

    Vector3[] vertices;
    Vector2[] UVs;
    int[] triangles;

    void Start()
    {
        if (filter == null) filter = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        filter.mesh = mesh;

        vertices = new Vector3[2 * resolution + 2];
        UVs = new Vector2[2 * resolution + 2];
        triangles = new int[12 * resolution];

        Vector3 origin = transform.position;
        Vector3 offset = new Vector3(0, 0, -thickness);
        vertices[resolution] = origin;
        vertices[2 * resolution + 1] = origin + offset;

        UVs[resolution] = new Vector2(0.25f, 0.5f);
        UVs[2 * resolution + 1] = new Vector2(0.75f, 0.5f);
        Vector2 UVoffset = new Vector2(0.5f, 0);
        float UVradius = 0.25f;

        double angle_partition = 2 * math.PI_DBL / resolution;
        for (int i = 0; i < resolution; i++)
        {
            double angle = angle_partition * i;
            double cos = math.cos(angle);
            double sin = math.sin(angle);
            Vector2 dir = new Vector2((float)cos, (float)sin);
            UVs[i] = UVs[resolution] + dir * UVradius;
            UVs[resolution + i + 1] = UVs[i] + UVoffset;
            vertices[i] = origin + new Vector3((float)(cos * radius), (float)(sin * radius), 0);
            vertices[resolution + i + 1] = vertices[i] + offset;
            int pointB = (i + 1) % resolution;
            int j = i * 12;
            triangles[j + 0]  = i;
            triangles[j + 1]  = pointB;
            triangles[j + 2]  = resolution;
            triangles[j + 3]  = 2 * resolution + 1;
            triangles[j + 4]  = pointB + resolution + 1;
            triangles[j + 5]  = i + resolution + 1;
            triangles[j + 6]  = i + resolution + 1;
            triangles[j + 7]  = pointB;
            triangles[j + 8]  = i;
            triangles[j + 9]  = pointB + resolution + 1;
            triangles[j + 10] = pointB;
            triangles[j + 11] = i + resolution + 1;
        }

        mesh.vertices = vertices;
        mesh.uv = UVs;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        AssetDatabase.CreateAsset(mesh, "Assets\\Custom\\Meshes\\circle"+resolution+"_"+((int)(thickness * 100))+".asset");
    }
}
