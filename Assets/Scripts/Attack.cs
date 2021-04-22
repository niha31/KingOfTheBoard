using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    TileMap map;
    Camera mainCamera;
    List<Node> neighbors;

    public bool canAttact = false;
    public bool attacking = false;
    public bool hasAttacked = false;

    float attackDamage = 25.0f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        map = this.GetComponent<PlayerScript>().map;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPos = this.GetComponent<PlayerScript>().pos;
        neighbors = map.graph[(int)playerPos.x, (int)playerPos.y].neighbours;

        if (attacking && Input.touchCount > 0 && !hasAttacked)
        {

            Ray ray = mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            if (hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "Castle")
            {
                hit.collider.gameObject.GetComponent<PlayerScript>().TakeDamage(attackDamage);
                attacking = false;
                hasAttacked = true;
            }
        }

        foreach (Node node in neighbors)
        {
            for (int i = 0; i <= Players.NumberOfPlayers; i++)
            {
                if (Players.CurrentPlayer == i)
                {
                    break;
                }

                Vector2 otherPlayerPos = Players.AllPlayers[i].GetComponent<PlayerScript>().pos;
                Vector2 nodePos;
                nodePos.x = node.x;
                nodePos.y = node.y;
                                
                if (otherPlayerPos == nodePos)
                {
                    canAttact = true;
                    break;
                }
                else if(nodePos == Players.AllPlayers[i].GetComponent<PlayerScript>().tilesOwned[0])
                {
                    canAttact = true;
                    break;
                }
                else
                {
                    canAttact = false;
                }
            }
        }      
    }

    
}
