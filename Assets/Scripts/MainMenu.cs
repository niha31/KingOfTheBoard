using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button helpButton;
    public Button aboutButton;
    public Button settingsButton;
    public Button exitButton;

    public Canvas thisCanvas;
    public Canvas helpCanvas;
    public Canvas aboutCanvas;
    public Canvas settingsCanvas;
    public Canvas newGameCanvas;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(PlayButtonClicked);
        helpButton.onClick.AddListener(HelpButtonClick);
        aboutButton.onClick.AddListener(AboutButtonClick);
        settingsButton.onClick.AddListener(SettingsButtonClick);
        exitButton.onClick.AddListener(ExitButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayButtonClicked()
    {

        newGameCanvas.gameObject.SetActive(true);
        thisCanvas.gameObject.SetActive(false);
    }

    void HelpButtonClick()
    {
        
        helpCanvas.gameObject.SetActive(true);
        thisCanvas.gameObject.SetActive(false);
    }

    void AboutButtonClick()
    {

        aboutCanvas.gameObject.SetActive(true);
        thisCanvas.gameObject.SetActive(false);
    }

    void SettingsButtonClick()
    {

        settingsCanvas.gameObject.SetActive(true);
        thisCanvas.gameObject.SetActive(false);
    }

    void ExitButtonClick()
    {
        Application.Quit();
    }
}
