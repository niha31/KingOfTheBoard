using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    struct MessageData
    {
        public string messageString;
        public float displayDuration;
        public Color messageColor;

        public MessageData(string messageString, float displayDuration, Color messageColor)
        {
            this.messageString = messageString;
            this.displayDuration = displayDuration;
            this.messageColor = messageColor;
        }
    }

    private bool messageActive = false;
    private float messageRemoveDelta = -1.0f;
    private List<MessageData> messageQueue = new List<MessageData>();

    public Text textBox;

    // Start is called before the first frame update
    void Start()
    {
        textBox = GetComponent<Text>();
        SetDefaultText();
    }

    // Update is called once per frame
    void Update()
    {
        // Messages are overriding the default text
        if (messageActive)
        {
            messageRemoveDelta -= Time.deltaTime;
            if (messageRemoveDelta < 0.0f)
            {
                if (messageQueue.Count > 0)
                {
                    SetActiveMessage(messageQueue[0]);
                    messageQueue.RemoveAt(0);
                }
                else
                {
                    SetDefaultText();
                }
            }
        }
    }

    void AddMessage(MessageData messageData)
    {
        if (messageActive)
        {
            messageQueue.Add(messageData);
        }
        else
        {
            SetActiveMessage(messageData);
        }
        
    }

    public void AddMessage(string messageString, float displayDuration, Color messageColor)
    {
        AddMessage(new MessageData(messageString, displayDuration, messageColor));
    }

    void SetActiveMessage(MessageData messageData)
    {
        messageActive = true;
        messageRemoveDelta = messageData.displayDuration;
        textBox.text = messageData.messageString;
        textBox.color = messageData.messageColor;
    }

    void SetDefaultText()
    {
        messageActive = false;
        messageRemoveDelta = 0.0f;

        int playerNumber = Players.CurrentPlayer + 1;
        int playerTechPoints = Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().techPoints;

        textBox.text = "Player " + playerNumber + " \nTech Score: " + playerTechPoints;
        textBox.color = Color.black;
    }

    void ClearMessageQueue()
    {
        messageQueue.Clear();
        messageRemoveDelta = 0.0f;
    }

    public void ClearAllMessages()
    {
        ClearMessageQueue();
        SetDefaultText();
    }
}
