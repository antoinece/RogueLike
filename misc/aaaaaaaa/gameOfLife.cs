using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;


public class gameOfLife : MonoBehaviour
{
    private BoundsInt _startZone;

    
    [SerializeField] private Tilemap _map; 
    [SerializeField] private TileBase _base;
    [Range(0,1)][SerializeField] private float idk;
    [SerializeField] private Vector2Int _size;
    [SerializeField] private Vector2Int _center;
    [SerializeField] private Vector2 _perlinScale = new Vector2(1,1);
    private HashSet<Vector2Int> tiles=new HashSet<Vector2Int>();
    
    public void Genetate()
    {
        init();
        genTool.DrawMap(_map,_base,tiles);
    }

    public void life()
    {
        GameOfLifeIteration();
        genTool.DrawMap(_map,_base,tiles);
    }

    private void Update()
    {
        GameOfLifeIteration();
        genTool.DrawMap(_map,_base,tiles);
    }

    private void init()
    {
        tiles.Clear();
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                float noiseCoordX = x/ (_size.x * 2f);
                float noiseCoordY = y/ (_size.y * 2f);

                float rnd = Mathf.PerlinNoise((noiseCoordX * _perlinScale.x), (noiseCoordY * _perlinScale.y));

                if (rnd>idk)
                {
                    //_map.SetTile(new Vector3Int(_center.x+x - _size.x/2 ,_center.y+y - _size.y/2),_base);
                    tiles.Add(new Vector2Int(x - _size.x/2 ,y - _size.y/2));
                }
                
            }
        }
    }

    private int NeighbouCount(Vector2Int _startPosition)
    {
        int count = 0;
        foreach (Vector2Int neighbour  in genTool.MoorNeighbours)
        {
            if (tiles.Contains(_startPosition + neighbour))
                count++;
        }

        return count;
    }
    
    private void GameOfLifeIteration()
    {
        HashSet<Vector2Int> aliveTiles = tiles;
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                Vector2Int cellPosition = new Vector2Int(x, y);
                int nbNeighbours = NeighbouCount(cellPosition);
                
                if (tiles.Contains(cellPosition))
                {
                    
                    if (nbNeighbours is < 2 or > 3)
                    {
                        aliveTiles.Remove(cellPosition);
                    }
                }
                else
                {
                    if (nbNeighbours == 3)
                    {
                        aliveTiles.Add(cellPosition);
                    }
                }
                
            }
        }

        tiles = aliveTiles;
    }
    
}
