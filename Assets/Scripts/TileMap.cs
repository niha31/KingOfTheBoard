using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public TileType[] tileTypes;
    int[,] tiles;
    Node[,] graph;


    public GameObject playerVisualprefab;
    public GameObject castleVisualPrefab;

    int mapSizeX = 10;
    int mapSizeY = 10;

    public Material material1;
    public Material material2;
    public Material material3;
    public Material material4;

    public Material[] materials;

    void Start()
    {

        materials = new Material[4];
        materials[0] = material1;
        materials[1] = material2;
        materials[2] = material3;
        materials[3] = material4;

        Players.playerColours = materials;


        tiles = new int[mapSizeX, mapSizeY];

        int maxAmountOfTileType2 = 20;
        int currentAmountOfTileType2 = 0;
        
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                int tile = Random.Range(0, 2);                

                if (tile == 1 && currentAmountOfTileType2 < maxAmountOfTileType2)
                {
                    tiles[x, y] = tile;
                    currentAmountOfTileType2++;
                }
                else
                {
                    tiles[x, y] = 0;
                }
            }
        }

        MakeMap();
        PlacePlayersOnMap();
        Players.SetPlayerColours();
        GenoratePathFindingGraph();
        Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().map = this;

    }

    public Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, y, 0);
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

        Players.NumberOfPlayers = SetUpGame.NoOfPlayers;
        for (int i = 0; i <= Players.NumberOfPlayers; i++)
        {
            Vector3 pos = new Vector3();
            if (i == 0)
            {
                pos.Set(0, 0, 0);
            }
            else if (i == 1)
            {
                pos.Set(9, 9, 0);
            }
            else if (i == 2)
            {
                pos.Set(0, 9, 0);
            }
            else if(i == 3)
            {
                pos.Set(9, 0, 0);
            }

            Instantiate(castleVisualPrefab, pos , Quaternion.identity);           
        }

        GameObject[] allCastles = null;
        allCastles = GameObject.FindGameObjectsWithTag("Castle");
        Players.AllCastles = allCastles;
    }


    void PlacePlayersOnMap()
    {
        GameObject[] allPlayer = null;

        for (int i = 0; i <= Players.NumberOfPlayers; i++)
        {
            //Vector3 pos = new Vector3(Random.Range(0, 10), Random.Range(0, 10), 0);
            Vector3 pos = new Vector3(Players.AllCastles[i].transform.position.x, Players.AllCastles[i].transform.position.y, 0);

            Instantiate(playerVisualprefab, pos, Quaternion.identity);
        }

        allPlayer = GameObject.FindGameObjectsWithTag("Player");
        Players.AllPlayers = allPlayer;
        Players.CurrentPlayer = 0;

        //Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().pos = Players.AllPlayers[Players.CurrentPlayer].transform.position;

        Camera.main.GetComponent<TouchInputs>().SetGameCameraAtStart();

        for(int i = 0; i <= Players.NumberOfPlayers; i++)
        {
            Players.AllPlayers[i].GetComponent<PlayerScript>().map = this;
        }
    }



    void GenoratePathFindingGraph()
    {
        graph = new Node[mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                graph[x, y] = new Node();

                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }  
        
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                if (x > 0)
                {
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                }
                if (x < mapSizeX - 1)
                {
                    graph[x, y].neighbours.Add(graph[x + 1, y]);
                }
                if (y > 0)
                {
                    graph[x, y].neighbours.Add(graph[x, y - 1]);
                }
                if (y < mapSizeY - 1)
                {
                    graph[x, y].neighbours.Add(graph[x, y + 1]);
                }
            }
        }
    }

    public float CostToEnterTile(int x, int y)
    {
        TileType tt = tileTypes[tiles[x, y]];

        return tt.movementCost;
    }

    public void GenoratePathTo(int x, int y)
    {

        Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().currentPath = null;

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();

        int x1 = (int)(Players.AllPlayers[Players.CurrentPlayer].transform.position.x);
        int y1 = (int)(Players.AllPlayers[Players.CurrentPlayer].transform.position.y);
        
        Node source = graph[x1, y1];
        Node target = graph[x, y];


        dist[source] = 0;
        prev[source] = null;


        foreach (Node v in graph)
        {

            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }

            unvisited.Add(v);
        }
        while (unvisited.Count > 0)
        {

            Node u = null;

            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break;
            }

            unvisited.Remove(u);


            foreach (Node v in u.neighbours)
            {
                //float alt = dist[u] + u.DistanceTo(v);
                float alt = dist[u] + CostToEnterTile(v.x, v.y);

                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        if (prev[target] == null)
        {
            return;
        }

        List<Node> currentPath = new List<Node>();

        Node curr = target;

        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }
        currentPath.Reverse();
        Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().currentPath = currentPath;
    }    
}
