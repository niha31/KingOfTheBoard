using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Players
{
    private static int numberOfPlayers;
    static GameObject[] allPlayers;
    static GameObject[] allCastles;

    private static int currentPlayer;

    public static Material[] playerColours;

    public static void SetPlayerColours()
    {
        for(int i = 0; i <= NumberOfPlayers; i++)
        {
            allPlayers[i].GetComponentInChildren<MeshRenderer>().material = playerColours[i];
            allCastles[i].GetComponentInChildren<MeshRenderer>().material = playerColours[i];
        }
    }

    public static int NumberOfPlayers
    {
        get
        {
            return numberOfPlayers;
        }
        set
        {
            numberOfPlayers = value - 1;
        }
    }

    public static GameObject[] AllPlayers
    {
        get
        {
            return allPlayers;
        }
        set
        {
            allPlayers = value;
        }
    }

    public static GameObject[] AllCastles
    {
        get
        {
            return allCastles;
        }
        set
        {
            allCastles = value;
        }
    }

    public static int CurrentPlayer
    {
        get
        {
            return currentPlayer;
        }
        set
        {
            currentPlayer = value;
            if(currentPlayer > numberOfPlayers)
            {
                currentPlayer = 0;
            }
        }
    }

    public static Material[] PlayerColours
    { 
        get
        {
            return playerColours;
        }
        set
        {
            playerColours = value;
        }
    }

}
