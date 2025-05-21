using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PlanetMeshGenerator : MonoBehaviour
{
    public float noiseStrength = 0.1f;
    public float noiseScale = 1.5f;

    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Color[] colors = new Color[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = vertices[i].normalized;
            float noise = Mathf.PerlinNoise(vertex.x * noiseScale, vertex.y * noiseScale);
            float height = 1 + noise * noiseStrength;
            vertices[i] = vertex * height;

            float t = Mathf.InverseLerp(1f, 1.1f, height); // Normalize height from 1 to 1.1

            // Interpolate between blue (low), green (mid), white (high)
            if (t < 0.5f)
                colors[i] = Color.Lerp(Color.blue, Color.green, t * 2f);
            else
                colors[i] = Color.Lerp(Color.green, Color.white, (t - 0.5f) * 2f);
        }

        mesh.vertices = vertices;
        mesh.colors = colors;
        mesh.RecalculateNormals();
    }
}