using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : AIMovement
{
    public void Start()
    {
        Init();
    }

    public void Update()
    {
        if (!isMoving)
        {
            FindSelectableTiles();
            CheckMouse();
        }
        else
        {
            Move();
        }
        
    }

    void CheckMouse()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "Tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();
                    if(t.selectable)
                    {
                        MoveToTile(t);
                    }
                }
            }
        }
    }
}
