using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class TouchInputs : MonoBehaviour
{
    public Camera mainCamera;
    public CameraScript cameraScript;

    public GameObject map;

    public Text gameStatus;

    public bool gamePaused = false;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.deltaTime;
        

        if (!gamePaused)
        {
            if (Input.touchCount > 0 && Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().GetCanMove())
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);

                if (hit.collider.gameObject.tag == "Map")
                {
                    map.GetComponent<TileMap>().GenoratePathTo((int)hit.transform.position.x, (int)hit.transform.position.y);

                    Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().MoveNextTile();

                    //Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().MovePlayerTo(hit.transform.position.x, hit.transform.position.y);

                    //Players.NumberOfMovement[Players.CurrentPlayer] -= 1;
                }
            }

            if (Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().GetCanMove())
            {
                gameStatus.text = "Player " + (Players.CurrentPlayer + 1) + "'s turn.";

                //Wait(10);
                
                //gameStatus.text = " ";
            }
            else
            {
                //gameStatus.text = "Out of Moves";

                //Wait(2);

                gameStatus.text = " ";
            }

           

            SetGameCameraAtStart();
        }
    }

    public void SetGameCameraAtStart()
    {
        Vector2 pos = new Vector2(Players.AllPlayers[Players.CurrentPlayer].transform.position.x, Players.AllPlayers[Players.CurrentPlayer].transform.position.y);
        StartCoroutine(cameraScript.SetCameraToPlayerPos(pos.x, pos.y));
    }
}
