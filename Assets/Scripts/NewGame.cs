using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public Button backButton;
    public Button playButton;
    public Button twoPlayerButton;
    public Button threePlayerButton;
    public Button fourPlayerButton;

    public Canvas thisCanvas;
    public Canvas MainMenuCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(BackButtonClicked);
        playButton.onClick.AddListener(PlayButtonClicked);
        twoPlayerButton.onClick.AddListener(TwoPlayerButtonClicked);
        threePlayerButton.onClick.AddListener(ThreePlayerButtonClicked);
        fourPlayerButton.onClick.AddListener(FourPlayerButtonClicked);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BackButtonClicked()
    {
        MainMenuCanvas.gameObject.SetActive(true);
        thisCanvas.gameObject.SetActive(false);
    }

    void TwoPlayerButtonClicked()
    {
        SetUpGame.NoOfPlayers = 2;
    }
    void ThreePlayerButtonClicked()
    {
        SetUpGame.NoOfPlayers = 3;

    }
    void FourPlayerButtonClicked()
    {
        SetUpGame.NoOfPlayers = 4;
    }

    void PlayButtonClicked()
    {
        if(SetUpGame.NoOfPlayers == -1)
        {
            return;
        }
        else
        {
            SceneManager.LoadScene("GameScene");
        }

    }

    
}
