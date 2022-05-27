using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MazeConstructor))]
public class GameController : MonoBehaviour
{
    private MazeConstructor constructor;
    
    [SerializeField]
    private int mazeRows = 13;
    [SerializeField]
    private int mazeCols = 15;

    void Start()
    {
        // Get reference to MazeConstructor object
        constructor = GetComponent<MazeConstructor>();
        
        constructor.GenerateNewMaze(mazeRows, mazeCols);
    }
}
