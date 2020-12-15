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

    public Canvas thisCanvas;
    public Canvas MainMenuCanvas;
    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(BackButtonClicked);
        playButton.onClick.AddListener(PlayButtonClicked);

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

    void PlayButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
    }
}
