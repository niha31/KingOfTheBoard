using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Players
{
    private static int numberOfPlayers;
    static GameObject[] allPlayers;
    private static int currentPlayer;

    private static int[] numberofMovement = { 1,1,1,1};
    

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

    public static int[] NumberOfMovement
    {
        get
        {
            return numberofMovement;
        }
        set
        {
            numberofMovement = value;
        }
    }
}
