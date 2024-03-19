using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TorusGenerator : MonoBehaviour
{
    public int rows = 16;
    public int columns = 24;
    public float tubeRadius = 0.1f;
    public float horizontalCircumference = 360f;
    public float verticalCircumference = 360f;
    public bool smooth = true;

    private void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = GenerateTorus(rows, columns, tubeRadius, horizontalCircumference, verticalCircumference);
    }

    private Mesh GenerateTorus(int rows, int columns, float tubeRadius, float hCircumference, float vCircumference)
    {
        Mesh mesh = new Mesh();

        int vertexCount = (rows + 1) * (columns + 1);
        Vector3[] vertices = new Vector3[vertexCount];
        Vector2[] uv = new Vector2[vertexCount];
        int[] triangles = new int[rows * columns * 6];

        float rowStep = vCircumference / rows * Mathf.Deg2Rad;
        float columnStep = hCircumference / columns * Mathf.Deg2Rad;
        float rowRadius = 0.5f; // The main radius of the torus

        // Calculate vertices and UVs
        for (int row = 0, i = 0; row <= rows; row++)
        {
            float rowAngle = row * rowStep;
            for (int col = 0; col <= columns; col++, i++)
            {
                float colAngle = col * columnStep;
                float x = Mathf.Cos(colAngle) * (rowRadius + tubeRadius * Mathf.Cos(rowAngle));
                float y = Mathf.Sin(rowAngle) * tubeRadius;
                float z = Mathf.Sin(colAngle) * (rowRadius + tubeRadius * Mathf.Cos(rowAngle));
                vertices[i] = new Vector3(x, y, z);
                uv[i] = new Vector2((float)row / rows, (float)col / columns);
            }
        }

        // Calculate triangles
        for (int row = 0, i = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++, i += 6)
            {
                int current = row * (columns + 1) + col;
                int next = current + columns + 1;

                triangles[i] = current;
                triangles[i + 1] = next;
                triangles[i + 2] = current + 1;

                triangles[i + 3] = current + 1;
                triangles[i + 4] = next;
                triangles[i + 5] = next + 1;
            }
        }

        // Assign to mesh
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        if (smooth)
        {
            mesh.RecalculateNormals();
        }
        else
        {
            // To avoid smooth shading, calculate normals manually for a flat-shaded look
        }

        return mesh;
    }
}
