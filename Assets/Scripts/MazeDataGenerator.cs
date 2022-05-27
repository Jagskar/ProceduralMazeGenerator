using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that generates the maze structure itself: Where the walls are and where the empty cells are
// Passes this data to MazeConstructor which will then build the maze based on this structure
public class MazeDataGenerator
{
    // Chance of having an empty space
    // Can tune this value as needed
    public float placementThreshold;

    public MazeDataGenerator()
    {
        placementThreshold = .1f;
    }

    public int[,] FromDimensions(int rows, int cols)
    {
        int[,] maze = new int[rows, cols];

        return maze;
    }
}
