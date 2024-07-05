using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    List<Tile> SelectableTiles = new List<Tile>();
    GameObject[] tiles;

    public int move = 3; // moevemnt range
    public float jumpHeight = 1;
    public float movementSpeed = 3;

    Stack<Tile> path = new Stack<Tile>();
    Tile currentTile;

    public bool isMoving = false;
    
    Vector3 velocity = new();
    Vector3 heading = new();

    float halfHeight = 0;
    protected void Init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        halfHeight = GetComponent<Collider>().bounds.extents.y;
    }

    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
    }

    public Tile GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        Tile tile = null;
        if(Physics.Raycast(target.transform.position, Vector3.down, out hit, 1))
        {
            tile = hit.collider.GetComponent<Tile>();
        }
        return tile;
    }

    public void ComputeAdjacencyList()
    {
        foreach(GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighbors(jumpHeight);
        }
    }

    public void FindSelectableTiles()
    {
        ComputeAdjacencyList();
        GetCurrentTile();

        Queue<Tile> process = new Queue<Tile>();
        process.Enqueue(currentTile);
        currentTile.visited = true;
        //While is not empty: continue process
        while(process.Count > 0)
        {
            Tile t = process.Dequeue();

            SelectableTiles.Add(t);
            t.selectable = true;
            if (t.distance < move)
            {
                
                foreach (Tile tile in t.adjacencyList)
                {
                    
                    if (!tile.visited)
                    {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                }
            }
        }
    }

    public void MoveToTile(Tile tile)
    {
        path.Clear();
        tile.target = true;
        isMoving = true;

        Tile next = tile;
        while(next != null)
        {
            path.Push(next);
            next = next.parent;

        }
    }

    public void Move()
    {
        if(path.Count > 0)
        {
            Tile t = path.Peek();
            Vector3 target = t.transform.position;

            //calculate the units pos on the top of the target tile
            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if(Vector3.Distance(transform.position, target) >= 0.05f)
            {
                CalculateHeading(target);
                SetHorizontalVelocity();
                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                // Tile center reach
                transform.position = target;
                path.Pop();
            }
        }
        else
        {
            RemoveSelectableTiles();
            isMoving = false;
        }
    }

    protected void RemoveSelectableTiles()
    {
        if(currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }
        foreach(Tile tile in SelectableTiles)
        {
            tile.ResetValues();
        }
        SelectableTiles.Clear();
    }

    public void CalculateHeading(Vector3 target)
    {
        heading = target = transform.position;
        heading.Normalize();
    }

    public void SetHorizontalVelocity()
    {
        velocity = heading * movementSpeed;
    }
}
