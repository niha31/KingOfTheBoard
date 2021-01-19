using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public TileType[] tileTypes;
    int[,] tiles;
    Node[,] graph;
    public GameObject playerVisualprefab;

    int mapSizeX = 10;
    int mapSizeY = 10;


    class Node
    {
        public List<Node> neighbours;

        public Node()
        {
            neighbours = new List<Node>();
        }
    }


    void Start()
    {
        tiles = new int[mapSizeX, mapSizeY];

        int maxTileType2 = 20;
        int currentTileType2 = 0;
        
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                int tile = Random.Range(0, 2);
                if(tile == 1 && currentTileType2 < maxTileType2)
                {
                    tiles[x, y] = tile;
                    currentTileType2++;
                }
                else
                {
                    tiles[x, y] = 0;
                }
            }
        }

        MakeMap();
        PlacePlayersOnMap();
        //GenoratePathFindingGraph();
    }

    void MakeMap()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                Instantiate(tileTypes[tiles[x, y]].tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);               
            }
        }

    }


    void PlacePlayersOnMap()
    {
        Players.NumberOfPlayers = SetUpGame.NoOfPlayers;
        GameObject[] allPlayer = null;

        for (int i = 0; i <= Players.NumberOfPlayers; i++)
        {
            Vector3 pos = new Vector3(Random.Range(0, 10), Random.Range(0, 10), 0);

            GameObject go = (GameObject)Instantiate(playerVisualprefab, pos, Quaternion.identity);

        }

        allPlayer = GameObject.FindGameObjectsWithTag("Player");
        Players.AllPlayers = allPlayer;
        Players.CurrentPlayer = 0;
      
        Camera.main.GetComponent<TouchInputs>().SetGameCameraAtStart();
    }

    

    //    void GenoratePathFindingGraph()
    //    {
    //        graph = new Node[mapSizeX, mapSizeY];

    //        for (int x = 0; x < mapSizeX; x++)
    //        {
    //            for (int y = 0; y < mapSizeY; y++)
    //            {
    //                if (x > 0)
    //                {
    //                    graph[x, y].neighbours.Add(graph[x - 1, y]);
    //                }
    //                if (x < mapSizeX - 1)
    //                {
    //                    graph[x, y].neighbours.Add(graph[x + 1, y]);
    //                }
    //                if (y > 0)
    //                {
    //                    graph[x, y].neighbours.Add(graph[x, y - 1]);
    //                }
    //                if (y < mapSizeY - 1)
    //                {
    //                    graph[x, y].neighbours.Add(graph[x, y + 1]);
    //                }
    //            }
    //        }
    //    }
}
