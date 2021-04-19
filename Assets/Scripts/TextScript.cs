using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextScript : MonoBehaviour
{

    private float messageRemoveDelta = -1.0f;
    private bool messageActive = false;

    Text textBox;

    // Start is called before the first frame update
    void Start()
    {
        textBox = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (messageActive)
        {
            messageRemoveDelta -= Time.deltaTime;
            if (messageRemoveDelta < 0.0f)
            {
                messageActive = false;
                textBox.text = " ";
            }
        }
    }

    //string MessageToDisplay()
    //{

    //}
}
