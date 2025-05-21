using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class PlanetMeshGenerator : MonoBehaviour
{
    public float noiseStrength = 0.1f;
    public float noiseScale = 1.5f;

    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = vertices[i].normalized;
            float noise = Mathf.PerlinNoise(vertex.x * noiseScale, vertex.y * noiseScale);
            vertices[i] = vertex * (1 + noise * noiseStrength);
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}