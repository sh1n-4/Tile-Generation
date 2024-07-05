using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    public int width, height;
    public GameObject tilePrefab;
    //test
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        //GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateGrid()
    {
        if(tilePrefab == null)
        {
            Debug.LogError("No prefab assigned");
            return;
        }
        //Loop through gridPos
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                //calc the pos for each cube
                Vector3 position = new Vector3(x, 0.1f, y); //z=y bcuz y=y is vertical
                //Instantiate at calc pos
                GameObject newTile = Instantiate(tilePrefab, position, Quaternion.identity);
                newTile.transform.parent = transform;
                newTile.tag = "Tile";
            }
        }
    }

    public void ClearGrid()
    {
        //find all gameobject tagged as Tile
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach(GameObject tile in tiles)
        {
            DestroyImmediate(tile);
        }
    }

    public void AssignMaterials()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        //Material material = Resources.Load<Material>("Tile");
        Material m = material;
        foreach(GameObject tile in tiles)
        {
            tile.GetComponent<Renderer>().material = m;
        }
    }

    public void AssignTileScript()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        
        foreach (GameObject tile in tiles)
        {
            tile.AddComponent<Tile>();
        }
    }
}
