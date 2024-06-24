using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    List<Tile> SelectableTiles = new();
    GameObject[] tiles;

    Stack<Tile> path = new();
    Tile currentTile;

    public int move = 5; // moevemnt range
    public float jumpHeight = 2;
    public float movementSpeed;
    Vector3 velocity = new();
    Vector3 heading = new();

    float halfHeight = 0;
    protected void Init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        halfHeight = GetComponent<Collider>().bounds.extents.y;
    }
}
