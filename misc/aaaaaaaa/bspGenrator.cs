using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class bspGenrator : MonoBehaviour
{
    [SerializeField] private Vector2Int _size;
    [SerializeField] private Tilemap _dungeonMap;
    [SerializeField] private TileBase _tile;
    [SerializeField] private TileBase _tile2;
    [SerializeField] private int minArea = 25;
    public enum CutDirection
    {
        Vertical,
        Horizontal
    }

     private CutDirection _direction;


    
    public void Generated()
    { 
        List<BoundsInt> rooms = null;
        Queue<BoundsInt> queue = new Queue<BoundsInt>();
        BoundsInt bounds = new BoundsInt();
        BoundsInt boundsa = new BoundsInt();
        BoundsInt boundsb = new BoundsInt();
        
        
        bounds.xMin =-1* Mathf.CeilToInt(_size.x / 2);
        bounds.xMax =1* Mathf.CeilToInt(_size.x / 2);
        bounds.yMin =-1* Mathf.CeilToInt(_size.x / 2);
        bounds.yMax =1* Mathf.CeilToInt(_size.x / 2);
        bounds.zMin = 0;
        bounds.zMax = 1;

        queue.Enqueue(bounds);
        if (_direction == CutDirection.Horizontal)
        {
            _direction = CutDirection.Vertical;
        }
        else
        {
            _direction = CutDirection.Horizontal;
        }
        while (queue.Count>0)
        {
            BoundsInt boundsInt = queue.Dequeue();
           
            
            Cut(bounds,_direction, out boundsa,out boundsb);
            if (boundsa.size.x*boundsa.size.y > minArea)
            {
                queue.Enqueue(boundsa);
            }
            else
            {
                rooms.Add(boundsa);
            }
            if (boundsb.size.x*boundsb.size.y > minArea)
            {
                queue.Enqueue(boundsb);
            }
            else
            {
                rooms.Add(boundsb);
            }
        }
        //PaintMap(bounds, _dungeonMap, _tile);
        
        _dungeonMap.ClearAllTiles();
        
        
        foreach (BoundsInt room in rooms)
        {
            PaintMap(room,_dungeonMap,_tile,Random.ColorHSV());
        }
        //PaintMap(boundsa,_dungeonMap,_tile);
        //PaintMap(boundsb,_dungeonMap,_tile2);
    }


    private void PaintMap(BoundsInt tiles, Tilemap map, TileBase tile, Color a)
    {
        foreach (Vector3Int pos in tiles.allPositionsWithin)
        {
            map.SetColor(new Vector3Int(),a);
            map.SetTile(pos,tile);
        }
    }

    public void Cut(BoundsInt inBounds,CutDirection direction,out BoundsInt boundsA,out BoundsInt boundsB ,float ratio = 0.5f )
    {
        boundsA = inBounds;
        boundsB = inBounds;
        
       

        switch (direction)
        {
            case CutDirection.Horizontal :
                boundsA.yMin = inBounds.yMin +Mathf.FloorToInt(inBounds.size.y * ratio) ;
                boundsB.yMax = inBounds.yMin +Mathf.CeilToInt(inBounds.size.y * ratio) ;
                break;
            case CutDirection.Vertical :
                boundsA.xMin = inBounds.xMin +Mathf.FloorToInt(inBounds.size.y * ratio) ;
                boundsB.xMax = inBounds.xMin +Mathf.CeilToInt(inBounds.size.y * ratio) ;
                break;
            default:
                break;
        }

    }
    
}
