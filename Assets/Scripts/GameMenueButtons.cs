using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameMenueButtons : MonoBehaviour
{
    public Camera mainCamera;
    public TileMap map;

    public Canvas gameUI;
    public Canvas mainMenuConformation;
    public Canvas TechTreeCanvas;

    public Button endTurnButton;
    public Button mainMenuButton;
    public Button techTreeButton;
    public Button harvestButton;

    public Button yesButton;
    public Button noButton;

    public GameObject harvesterVisualPrefab;

    // Start is called before the first frame update
    void Start()
    {
        endTurnButton.onClick.AddListener(EndTurnButtonClicked);
        mainMenuButton.onClick.AddListener(MainMenuButtonClicked);
        techTreeButton.onClick.AddListener(TechTreeButtonClicked);
        harvestButton.onClick.AddListener(HarvestButtonCLicked);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void EndTurnButtonClicked()
    {
        Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().SetCanMove(true);
        int current = Players.CurrentPlayer;
        Players.CurrentPlayer = current + 1;

        this.gameObject.GetComponent<TouchInputs>().SetGameCameraAtStart();

        Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().CalculatePointsEarned();
    }

    void MainMenuButtonClicked()
    {
        this.gameObject.GetComponent<TouchInputs>().gamePaused = true;

        gameUI.gameObject.SetActive(false);
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

        this.gameObject.GetComponent<TouchInputs>().gamePaused = false;

        gameUI.gameObject.SetActive(true);
        mainMenuConformation.gameObject.SetActive(false);
    }

    void TechTreeButtonClicked()
    {
        this.gameObject.GetComponent<TouchInputs>().gamePaused = true;

        TechTreeCanvas.gameObject.SetActive(true);
        gameUI.gameObject.SetActive(false);
    }

    void HarvestButtonCLicked()
    {
        Vector3 playerPos = Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().GetPos();
        int [,] tileMap = map.GetTiles();

        //check if tile is harvestable
        if(map.tileTypes[tileMap[(int)playerPos.x, (int)playerPos.y]].harvestable)
        {
            string techNeeded = map.tileTypes[tileMap[(int)playerPos.x, (int)playerPos.y]].techNeeded;

            //check if tile owned by player
            if(map.tileOwnedBy[(int)playerPos.x,(int)playerPos.y] == -1)
            {
                //check if player has tech needed to harvest
                if (Players.AllPlayers[Players.CurrentPlayer].GetComponent<Tech>().HaveTech(techNeeded))
                {
                    Debug.Log("Tech Found");
                    Vector2 pos;
                    pos.x = playerPos.x;
                    pos.y = playerPos.y;
                    Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().tilesOwned.Add(pos);

                    map.tileOwnedBy[(int)pos.x, (int)pos.y] = Players.CurrentPlayer;

                    harvesterVisualPrefab.GetComponentInChildren<MeshRenderer>().material = Players.playerColours[Players.CurrentPlayer];
                    Instantiate(harvesterVisualPrefab, pos, Quaternion.identity);
                }
                else
                {
                    Debug.Log("no tech");
                }
            }
            else
            {
                Debug.Log("Tile already owned");
            }

            
        }
        else
        {
            Debug.Log("Tile cant be harvested");
        }

    }

}
