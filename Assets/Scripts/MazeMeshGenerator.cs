using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Logic for generating the mesh used for the maze
public class MazeMeshGenerator
{
    // Width of hallways
    public float width;
    // Height of hallways
    public float height;

    public MazeMeshGenerator()
    {
        width = 3.75f;
        height = 3.5f;
    }

    private void AddQuad(Matrix4x4 matrix, ref List<Vector3> newVertices, ref List<Vector2> newUVs, ref List<int> newTriangles)
    {
        int index = newVertices.Count;

        // Corners before transforming
        Vector3 vertex1 = new Vector3(-.5f, -.5f, 0);
        Vector3 vertex2 = new Vector3(-.5f, .5f, 0);
        Vector3 vertex3 = new Vector3(.5f, .5f, 0);
        Vector3 vertex4 = new Vector3(.5f, -.5f, 0);

        newVertices.Add(matrix.MultiplyPoint3x4(vertex1));
        newVertices.Add(matrix.MultiplyPoint3x4(vertex2));
        newVertices.Add(matrix.MultiplyPoint3x4(vertex3));
        newVertices.Add(matrix.MultiplyPoint3x4(vertex4));

        newUVs.Add(new Vector2(1, 0));
        newUVs.Add(new Vector2(1, 1));
        newUVs.Add(new Vector2(0, 1));
        newUVs.Add(new Vector2(0, 0));

        newTriangles.Add(index + 2);
        newTriangles.Add(index + 1);
        newTriangles.Add(index);

        newTriangles.Add(index + 3);
        newTriangles.Add(index + 2);
        newTriangles.Add(index);
    }

    public Mesh FromData(int[,] data)
    {
        Mesh maze = new Mesh();

        List<Vector3> newVertices = new List<Vector3>();
        List<Vector2> newUVs = new List<Vector2>();

        maze.subMeshCount = 2;
        List<int> floorTriangles = new List<int>();
        List<int> wallTriangles = new List<int>();

        int rowMax = data.GetUpperBound(0);
        int colMax = data.GetUpperBound(1);
        float halfHeight = height * .5f;

        for (int i = 0; i < rowMax; i++)
        {
            for (int j = 0; j < colMax; j++)
            {
                if (data[i, j] != 1)
                {
                    // Add floor for every cell
                    AddQuad(Matrix4x4.TRS(
                        new Vector3(j * width, 0, i * width),
                        Quaternion.LookRotation(Vector3.up),
                        new Vector3(width, width, 1)
                        ), ref newVertices, ref newUVs, ref floorTriangles);

                    // Add ceiling for every cell
                    AddQuad(Matrix4x4.TRS(
                        new Vector3(j * width, 0, i * width),
                        Quaternion.LookRotation(Vector3.down),
                        new Vector3(width, width, 1)
                        ), ref newVertices, ref newUVs, ref floorTriangles);

                    // Check if the cell should have walls on the sides
                    if (i - 1 < 0 || data[i - 1, j] == 1)
                        AddQuad(Matrix4x4.TRS(
                            new Vector3(j * width, halfHeight, (i - .5f) * width),
                            Quaternion.LookRotation(Vector3.forward),
                            new Vector3(width, height, 1)
                            ), ref newVertices, ref newUVs, ref wallTriangles);

                    if (j + 1 > colMax || data[i, j + 1] == 1)
                        AddQuad(Matrix4x4.TRS(
                            new Vector3((j + .5f) * width, halfHeight, i * width),
                            Quaternion.LookRotation(Vector3.left),
                            new Vector3(width, height, 1)
                            ), ref newVertices, ref newUVs, ref wallTriangles);

                    if (j - 1 < 0 || data[i, j - 1] == 1)
                        AddQuad(Matrix4x4.TRS(
                            new Vector3((j - .5f) * width, halfHeight, i * width),
                            Quaternion.LookRotation(Vector3.right),
                            new Vector3(width, height, 1)
                            ), ref newVertices, ref newUVs, ref wallTriangles);

                    if (i + 1 > rowMax || data[i + 1, j] == 1)
                        AddQuad(Matrix4x4.TRS(
                            new Vector3(j * width, halfHeight, (i + .5f) * width),
                            Quaternion.LookRotation(Vector3.back),
                            new Vector3(width, height, 1)
                            ), ref newVertices, ref newUVs, ref wallTriangles);
                }
            }
        }

        maze.vertices = newVertices.ToArray();
        maze.uv = newUVs.ToArray();

        maze.SetTriangles(floorTriangles.ToArray(), 0);
        maze.SetTriangles(wallTriangles.ToArray(), 1);

        maze.RecalculateNormals();

        return maze;
    }
}
