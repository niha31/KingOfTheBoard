using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public TileType[] tileTypes;
    int[,] tiles;

    int mapSizeX = 10;
    int mapSizeY = 10;


    void Start()
    {
        tiles = new int[mapSizeX, mapSizeY];
        
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                tiles[x, y] = Random.Range(0,2);
            }
        }

        MakeMap();
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
}
