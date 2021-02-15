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

    public Canvas mainMenuConformation;

    public Button endTurnButton;
    public Button mainMenuButton;
    public Button yesButton;
    public Button noButton;

    bool gamePaused = false;

    // Start is called before the first frame update
    void Start()
    {
        endTurnButton.onClick.AddListener(EndTurnButtonClicked);
        mainMenuButton.onClick.AddListener(MainMenuButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
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

            if(Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().GetCanMove())
            {
                gameStatus.text = "Player " + (Players.CurrentPlayer + 1) + "'s turn.";
            }
            else
            {
                gameStatus.text = "Out of Moves";
            }

            SetGameCameraAtStart();
        }
    }

    void EndTurnButtonClicked()
    {
        Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().SetCanMove(true);
        int current = Players.CurrentPlayer;
        Players.CurrentPlayer = current + 1;

        SetGameCameraAtStart();
    }

    void MainMenuButtonClicked()
    {
        Debug.Log("menu button clicked");
        
        gamePaused = true;
        mainMenuConformation.gameObject.SetActive(true);

        yesButton.onClick.AddListener(YesButtonClicked);
        noButton.onClick.AddListener(NoButtonClicked);
    }

    void YesButtonClicked()
    {
        SceneManager.LoadScene("MainMenueScene");
    }

    void NoButtonClicked()
    {
        gamePaused = false;
        mainMenuConformation.gameObject.SetActive(false);
    }

    public void SetGameCameraAtStart()
    {
        Vector2 pos = new Vector2(Players.AllPlayers[Players.CurrentPlayer].transform.position.x, Players.AllPlayers[Players.CurrentPlayer].transform.position.y);
        StartCoroutine(cameraScript.SetCameraToPlayerPos(pos.x, pos.y));
    }
}