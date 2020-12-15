using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class About : MonoBehaviour
{
    public Button backButton;

    public Canvas thisCanvas;
    public Canvas MainMenuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        backButton.onClick.AddListener(BackButtonClicked);
    }

    void BackButtonClicked()
    {
        MainMenuCanvas.gameObject.SetActive(true);
        thisCanvas.gameObject.SetActive(false);
    }
}
