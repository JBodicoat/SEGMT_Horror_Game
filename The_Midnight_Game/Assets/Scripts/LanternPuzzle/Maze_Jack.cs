// Jack 16/03/2020 created script
// Morgan 24/03/2020 Added nav mesh layer to ignore list

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets the maze to ignore collision with all layers but the lanterns.
/// </summary>
public class Maze_Jack : MonoBehaviour
{
    void Awake()
    {
        Physics.IgnoreLayerCollision(0, 11);
        Physics.IgnoreLayerCollision(1, 11);
        Physics.IgnoreLayerCollision(2, 11);
        Physics.IgnoreLayerCollision(3, 11);
        Physics.IgnoreLayerCollision(4, 11);
        Physics.IgnoreLayerCollision(5, 11);
        Physics.IgnoreLayerCollision(8, 11);
        Physics.IgnoreLayerCollision(9, 11);
        Physics.IgnoreLayerCollision(13, 11);
    }
}
