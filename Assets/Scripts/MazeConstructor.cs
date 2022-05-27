using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that handles Maze Construction: Physically builds a maze based on given parameters
public class MazeConstructor : MonoBehaviour
{
    private MazeDataGenerator mazeDataGenerator;
    private MazeMeshGenerator mazeMeshGenerator;

    [SerializeField]
    private Material mazeMaterial1;
    [SerializeField]
    private Material mazeMaterial2;
    [SerializeField]
    private Material startMaterial;
    [SerializeField]
    private Material treasureMaterial;

    public bool showDebug;
    public int[,] Data { get; private set; }

    private void DisplayMaze()
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.position = Vector3.zero;
        gameObject.name = "Procedural Maze";
        gameObject.tag = "Generated";

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mazeMeshGenerator.FromData(Data);

        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = meshFilter.mesh;

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.materials = new Material[2] { mazeMaterial1, mazeMaterial2 };
    }

    void Awake()
    {
        mazeDataGenerator = new MazeDataGenerator();
        mazeMeshGenerator = new MazeMeshGenerator();

        // 0 = open     1 = blocked
        // Initialise Data with a single empty cell surrounded by walls
        Data = new int[,]
        {
            {1, 1, 1 },
            {1, 0, 1 },
            {1, 1, 1 },
        };
    }

    // For debugging: Show a GUI representation of the current maze layout
    void OnGUI()
    {
        if (!showDebug)
            return;

        int[,] maze = Data;
        int rowMax = maze.GetUpperBound(0);
        int colMax = maze.GetUpperBound(1);

        string message = "";

        for (int i = rowMax; i >= 0; i--)
        {
            for (int j = 0; j <= colMax; j++)
            {
                // If the cell is empty
                if (maze[i, j] == 0)
                    message += "....";
                // else, the cell has a wall
                else
                    message += "==";
            }

            message += "\n";
        }
        // Draw the maze structure as a label 
        GUI.Label(new Rect(20, 20, 500, 500), message);
    }

    public void GenerateNewMaze(int rows, int cols)
    {
        if (rows % 2 == 0 && cols % 2 == 0)
            Debug.LogError("Odd numbers work better for dungeon size.");

        Data = mazeDataGenerator.FromDimensions(rows, cols);
        DisplayMaze();
    }
}
