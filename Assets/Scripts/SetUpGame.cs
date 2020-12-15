using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class SetUpGame
{
    private static int noOfPlayers = -1;

    public static int NoOfPlayers
    {
        get
        {
            return noOfPlayers;
        }
        set
        {
            noOfPlayers = value;
        }
    }

}
