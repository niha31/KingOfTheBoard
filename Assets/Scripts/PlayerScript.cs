using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject Player;
    public Vector3 pos;

    public TileMap map;

    public List<Node> currentPath = null;

    int movement = 2;
    bool canMove = true;


    void Start()
    {
        pos = transform.position;
    }

    public void MovePlayerTo(float x, float y)
    {
        Player.transform.position = new Vector3(x, y, 0);
        pos = new Vector3(x, y, 0);
    }

    public Vector3 GetPos()
    {
        return pos;
    }

    public void Update()
    {
        if(currentPath != null)
        {
            int currNode = 0;

            while (currNode < currentPath.Count -1 )
            {
                //Vector3 start = map.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].y) + new Vector3(0,0, -1.0f);
                //Vector3 end = map.TileCoordToWorldCoord(currentPath[currNode + 1].x, currentPath[currNode + 1].y) + new Vector3(0, 0, -1.0f);
                Vector3 start = new Vector3(currentPath[currNode].x, currentPath[currNode].y, -1.0f);
                Vector3 end = new Vector3(currentPath[currNode + 1].x, currentPath[currNode + 1].y, -1.0f);

                Debug.DrawLine(start, end, Color.red);
                Debug.Log("Update");
                currNode++;
            }
        }
    }

    public void MoveNextTile()
    {
        float remainingMovement = movement;

        while (remainingMovement > 0)
        {
            if (currentPath == null)
            {
                return;
            }
            
            remainingMovement -= map.CostToEnterTile(currentPath[1].x, currentPath[1].y);
            //Player.transform.position = new Vector3(currentPath[1].x, currentPath[1].y, 0);
            MovePlayerTo(currentPath[1].x, currentPath[1].y);

            currentPath.RemoveAt(0);

            if (currentPath.Count == 1)
            {
                currentPath = null;
            }
        }

        SetCanMove(false);
    }
    
    public void SetCanMove(bool set)
    {
        canMove = set;
    }

    public bool GetCanMove()
    {
        return canMove;
    }
}
