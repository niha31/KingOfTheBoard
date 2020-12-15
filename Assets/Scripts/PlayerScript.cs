using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject Player;
    Vector3 pos;

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

    
}
