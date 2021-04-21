using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    int mapSizeX = 10;
    int mapSizeY = 10;

    public TileType[] tileTypes;
    int[,] tiles;
    public int[,] tileOwnedBy;
    public Node[,] graph;

    public GameObject playerVisualprefab;
    public GameObject castleVisualPrefab;

    public Material material1;
    public Material material2;
    public Material material3;
    public Material material4;

    public Material[] materials;

    void Start()
    {
        tileOwnedBy = new int[mapSizeX, mapSizeY];

        materials = new Material[4];
        materials[0] = material1;
        materials[1] = material2;
        materials[2] = material3;
        materials[3] = material4;

        Players.playerColours = materials;

        tiles = new int[mapSizeX, mapSizeY];

        int maxAmountOfMountinTile = 10;
        int currentAmountOfMountinTile = 0;

        int maxAmountOfForestTIle = 10;
        int currentAmountOfForestTile = 0;

        int maxAmountOfMineTIle = 10;
        int currentAmountOfMineTile = 0;

        int maxAmountOfFarmTile = 10;
        int CurrentAmountOfFarmTile = 0;

        int maxAmountOfAnimalTile = 10;
        int CurrentAmountOfAnimalTile = 0;

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                int tile = Random.Range(2, 15);

                //mountine tile == 2
                if (tile == 2 && currentAmountOfMountinTile < maxAmountOfMountinTile)
                {
                    tiles[x, y] = tile;
                    currentAmountOfMountinTile++;
                }
                //forest tile == 3
                else if (tile == 3 && currentAmountOfForestTile < maxAmountOfForestTIle)
                {
                    tiles[x, y] = tile;
                    currentAmountOfForestTile++;
                }
                //mine tile == 4
                else if (tile == 4 && currentAmountOfMineTile < maxAmountOfMineTIle)
                {
                    tiles[x, y] = tile;
                    currentAmountOfMineTile++;
                } 
                //farm tile == 5
                else if(tile == 5 && CurrentAmountOfFarmTile < maxAmountOfFarmTile)
                {
                    tiles[x, y] = tile;
                    CurrentAmountOfFarmTile++;
                }
                //animal tile == 6
                else if(tile == 6 && CurrentAmountOfAnimalTile < maxAmountOfAnimalTile)
                {
                    tiles[x, y] = tile;
                    CurrentAmountOfAnimalTile++;
                }
                else
                {
                    //grass block == 1
                    tiles[x, y] = 1;
                }

                if (x == 0 && y == 0 || x == 9 && y == 9 || x == 0 && y == 9 || x == 9 && y == 0)
                {
                    //corner tiles are always default = 0
                    tiles[x, y] = 0;
                }


                tileOwnedBy[x, y] = -1;

            }
        }

        MakeMap();
        PlacePlayersOnMap();
        Players.SetPlayerColours();
        GenoratePathFindingGraph();
        Players.AllPlayers[Players.CurrentPlayer].GetComponent<PlayerScript>().CalculatePointsEarned();
    }

    public int[,] GetTiles()
    {
        return tiles;
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

        Camera.main.GetComponent<TouchInputs>().SetGameCameraAtStart();

        for(int i = 0; i <= Players.NumberOfPlayers; i++)
        {
            Players.AllPlayers[i].GetComponent<PlayerScript>().map = this;

            Players.AllPlayers[i].GetComponent<PlayerScript>().tilesOwned.Add(Players.AllPlayers[i].GetComponent<PlayerScript>().pos);
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
