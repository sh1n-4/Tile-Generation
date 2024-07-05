using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public bool current = false;
    public bool target = false;
    public bool selectable = false;
    public bool isWalkable = true;


    // variables needed for BFS
    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;

    public List<Tile> adjacencyList = new List<Tile>();
   
    void Update()
    {
        if (current)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (target)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (selectable)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void ResetValues()
    {
        adjacencyList.Clear();
        current = false;
        target = false;
        selectable = false;
        visited = false;
        parent = null;
        distance = 0;
    }

    public void FindNeighbors(float jumpHeight)
    {
        ResetValues();
        CheckTiles(Vector3.forward, jumpHeight);
        CheckTiles(Vector3.back, jumpHeight);
        CheckTiles(Vector3.left, jumpHeight);
        CheckTiles(Vector3.right, jumpHeight);

    }

    public void CheckTiles(Vector3 direction, float jumpHeight)
    {
        Vector3 halfExtents = new Vector3(0.25f, (1 + jumpHeight)/2, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);
        foreach(Collider item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if(tile != null && tile.isWalkable)
            {
                RaycastHit hit;
                // Code from the class recording: Physics.Raycast(tile.transform.position, Vector3.down, out hit, 1
                // The code above for some reason has raycast issues and wont detect surrounding tiles
                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1))
                {
                    adjacencyList.Add(tile);
                } else
                {
                    Debug.Log("No hit detected from: " + tile.transform.position + " in the down direction.");
                }
            }
        }
    }
}
