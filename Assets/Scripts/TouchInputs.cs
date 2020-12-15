using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TouchInputs : MonoBehaviour
{
    public Camera mainCamera;
    public CameraScript cameraScript;

    public Text gameStatus;

    public Button endTurnButton;

    // Start is called before the first frame update
    void Start()
    {
        endTurnButton.onClick.AddListener(EndTurnButtonClicked);

    }

    // Update is called once per frame
    void Update()
    {
        if (Players.NumberOfMovement[Players.CurrentPlayer] == 0)
        {
            gameStatus.text = "Out of Movement";
        }
        else
        {
            gameStatus.text = "Player " + (Players.CurrentPlayer + 1) + "'s turn.";
        }

        if (Input.touchCount > 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            if (hit.collider.gameObject.tag == "Map" && Players.NumberOfMovement[Players.CurrentPlayer] != 0)
            {
                Debug.Log("map tile touched");

                Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().MovePlayerTo(hit.transform.position.x, hit.transform.position.y);

                Vector2 pos = new Vector2(Players.AllPlayers[Players.CurrentPlayer].transform.position.x, Players.AllPlayers[Players.CurrentPlayer].transform.position.y);
                StartCoroutine(cameraScript.SetCameraToPlayerPos(pos.x, pos.y));
            }

            
        }
    }

    void EndTurnButtonClicked()
    {
        Players.NumberOfMovement[Players.CurrentPlayer] = 1;
        Players.CurrentPlayer = Players.CurrentPlayer + 1;
    }

    public void SetGameCameraAtStart()
    {
        Vector2 pos = new Vector2(Players.AllPlayers[Players.CurrentPlayer].transform.position.x, Players.AllPlayers[Players.CurrentPlayer].transform.position.y);
        StartCoroutine(cameraScript.SetCameraToPlayerPos(pos.x, pos.y));
    }
}