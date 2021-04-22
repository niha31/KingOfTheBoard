using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TechTree : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject map;

    public Canvas TechTreeCanvas;
    public Canvas gameUI;

    public Button closeButton;

    public Button forestryButton;
    public Button mineButton;
    public Button farmingButton;
    public Button huntingButton;

    public Sprite transparentButton;
    public Sprite solidButton;

    public Text textBox;

    Color solidColour = new Color(255, 255, 255, 255);
    Color transparentCoulor = new Color(255, 255, 255, 80);

    // Start is called before the first frame update
    void Start()
    {
        closeButton.onClick.AddListener(CloseButtonClicked);
        
        forestryButton.onClick.AddListener(ForestryButtonClicked);
        mineButton.onClick.AddListener(MineButtonClicked);
        farmingButton.onClick.AddListener(FarmButtonClicked);
        huntingButton.onClick.AddListener(HuntingButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if (Players.AllPlayers[Players.CurrentPlayer].GetComponent<Tech>().GetForestry())
        {
            forestryButton.image.sprite = solidButton;
        }
        else
        {
            forestryButton.image.sprite = transparentButton;
        }

        if (Players.AllPlayers[Players.CurrentPlayer].GetComponent<Tech>().GetFarming() == true)
        {
            farmingButton.image.sprite = solidButton;
        }
        else
        {
            farmingButton.image.sprite = transparentButton;
        }

        if (Players.AllPlayers[Players.CurrentPlayer].GetComponent<Tech>().GetMining() == true)
        {
            mineButton.image.sprite = solidButton;
        }
        else
        {
            mineButton.image.sprite = transparentButton;
        }

        if (Players.AllPlayers[Players.CurrentPlayer].GetComponent<Tech>().GetHunting() == true)
        {
            huntingButton.image.sprite = solidButton;
        }
        else
        {
            huntingButton.image.sprite = transparentButton;
        }
    }

    void CloseButtonClicked()
    {
        mainCamera.gameObject.GetComponent<TouchInputs>().gamePaused = false;

        gameUI.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    void ForestryButtonClicked()
    {
        int techPintsNeeded = map.GetComponent<TileMap>().tileTypes[3].harvestPoints;
        if (Players.AllPlayers[Players.CurrentPlayer].GetComponent<Tech>().GetForestry() == false && Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().techPoints >= techPintsNeeded)
        {
            Players.AllPlayers[Players.CurrentPlayer].GetComponent<Tech>().SetForestry(true);
            Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().techPoints = Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().techPoints - techPintsNeeded;
        }
        else if(Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().techPoints <= techPintsNeeded)
        {
            textBox.GetComponent<TextScript>().AddMessage("Not enough tech points", 1.5f, Color.red);
        }

    }
    void MineButtonClicked()
    {
        int techPintsNeeded = map.GetComponent<TileMap>().tileTypes[4].harvestPoints;
        if (Players.AllPlayers[Players.CurrentPlayer].GetComponent<Tech>().GetMining() == false && Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().techPoints >= techPintsNeeded)
        {
            Players.AllPlayers[Players.CurrentPlayer].GetComponent<Tech>().SetMining(true);
            Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().techPoints = Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().techPoints - techPintsNeeded;
        }
        else if (Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().techPoints <= techPintsNeeded)
        {
            textBox.GetComponent<TextScript>().AddMessage("Not enough tech points", 1.5f, Color.red);
        }
    }

    void FarmButtonClicked()
    {
        int techPintsNeeded = map.GetComponent<TileMap>().tileTypes[5].harvestPoints;
        if (Players.AllPlayers[Players.CurrentPlayer].GetComponent<Tech>().GetFarming() == false && Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().techPoints >= techPintsNeeded)
        {
            Players.AllPlayers[Players.CurrentPlayer].GetComponent<Tech>().SetFarming(true);
            Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().techPoints = Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().techPoints - techPintsNeeded;
        }
        else if (Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().techPoints <= techPintsNeeded)
        {
            textBox.GetComponent<TextScript>().AddMessage("Not enough tech points", 1.5f, Color.red);
        }
    }

    void HuntingButtonClicked()
    {
        int techPintsNeeded = map.GetComponent<TileMap>().tileTypes[6].harvestPoints;
        if (Players.AllPlayers[Players.CurrentPlayer].GetComponent<Tech>().GetHunting() == false && Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().techPoints >= techPintsNeeded)
        {
            Players.AllPlayers[Players.CurrentPlayer].GetComponent<Tech>().SetHunting(true);
            Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().techPoints = Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().techPoints - techPintsNeeded;
        }
        else if (Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().techPoints <= techPintsNeeded)
        {
            textBox.GetComponent<TextScript>().AddMessage("Not enough tech points", 1.5f, Color.red);
        }
    }
}