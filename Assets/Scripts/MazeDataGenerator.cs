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

    /*
     * Algorithm for generating a maze:
     * 1. Iterate through all rows
     *  2. Iterate through all columns
     *      3. Check if the current coordinate is an edge/border of the maze
     *          4. Add wall if it is
     *      5. Skip one cell and check the next one
     *          6. Add a wall
     *          7. Randomly select a cell adjacent to the current one
     *          8. Add a wall
     *      9. Repeat steps 3. - 8.
     */
    public int[,] FromDimensions(int rows, int cols)
    {
        int[,] maze = new int[rows, cols];

        int rowMax = maze.GetUpperBound(0);
        int colMax = maze.GetUpperBound(1);

        for (int i = 0; i <= rowMax; i++)
            for (int j = 0; j <= colMax; j++)
            {
                // Add walls to the surrounding maze space at the lower and upper bounds
                if (i == 0 || j == 0 || i == rowMax || j == colMax)
                    maze[i, j] = 1;

                // If i and j are even
                // Idea is to operate on every other cell i.e. skip one cell each time
                else if (i % 2 == 0 && j % 2 == 0)
                {
                    // Generate a random number and see if it is greater than the placementThreshold
                    if (Random.value > placementThreshold)
                    {
                        // If it is, add a wall
                        maze[i, j] = 1;

                        // Randomly select index of an adjacent cell
                        // Adjacent indices are either -1, 0 or 1
                        int a = Random.value < .5 ? 0 : (Random.value < .5 ? -1 : 1);
                        int b = a != 0 ? 0 : (Random.value < .5 ? -1 : 1);
                        // Add wall to selected adjacent cell
                        maze[i+a, j+b] = 1;
                    }
                }
            }

        return maze;
    }
}
