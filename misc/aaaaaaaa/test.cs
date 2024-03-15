using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class test : MonoBehaviour
{
    
    [SerializeField] private Tilemap _map; 
    [SerializeField] private TileBase _base;
    [SerializeField] private TileBase _base2;

    [SerializeField] private Vector2Int _size;
    [SerializeField] private Vector2Int _center;
    [SerializeField] private Vector2 _perlinScale = new Vector2(1,1);
    
    public void Generate()
    {
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                float noiseCoordX = x/ (_size.x * 2f);
                float noiseCoordY = y/ (_size.y * 2f);

                float rnd = Mathf.PerlinNoise(noiseCoordX * _perlinScale.x, noiseCoordY * _perlinScale.y);

                if (rnd>0.5f)
                {
                    
                    _map.SetTile(new Vector3Int(_center.x+x - _size.x/2 ,_center.y+y - _size.y/2),_base2);
                }
                else
                {
                   
                    _map.SetTile(new Vector3Int(_center.x+x - _size.x/2 ,_center.y+y - _size.y/2),_base);
                }
                
            }
        }
    }
}
