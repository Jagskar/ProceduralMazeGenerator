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

    public Mesh FromData(int[,] data)
    {
        Mesh maze = new Mesh();

        List<Vector3> newVertices = new List<Vector3>();
        List<Vector2> newUVs = new List<Vector2>();
        List<int> newTriangles = new List<int>();

        // Quad face vertices
        Vector3 vertex1 = new Vector3(-.5f, -.5f, 0);
        Vector3 vertex2 = new Vector3(-.5f, .5f, 0);
        Vector3 vertex3 = new Vector3(.5f, .5f, 0);
        Vector3 vertex4 = new Vector3(.5f, -.5f, 0);

        newVertices.Add(vertex1);
        newVertices.Add(vertex2);
        newVertices.Add(vertex3);
        newVertices.Add(vertex4);

        // UV projected coordinates of the quad vertices x, y, z => u, v  
        newUVs.Add(new Vector2(1, 0));
        newUVs.Add(new Vector2(1, 1));
        newUVs.Add(new Vector2(0, 1));
        newUVs.Add(new Vector2(0, 0));

        // Triangle vertices are indexes in list of vertices
        // Quad can be split into 2 triangles
        // First triangle
        newTriangles.Add(2);
        newTriangles.Add(1);
        newTriangles.Add(0);

        // Second triangle
        newTriangles.Add(3);
        newTriangles.Add(2);
        newTriangles.Add(0);

        maze.vertices = newVertices.ToArray();
        maze.uv = newUVs.ToArray();
        maze.triangles = newTriangles.ToArray();

        return maze;
    }
}
