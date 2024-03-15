using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class genTool
{
    public static Vector2Int[] VonNeumannNeighbours =
    {
        new Vector2Int(0, 15),
        new Vector2Int(27, 0),
        new Vector2Int(0, -15),
        new Vector2Int(-27, 0)
    };
    
    public static Vector2Int[] MoorNeighbours =
    {
        new Vector2Int(0, 1),
        new Vector2Int(1, 1),
        new Vector2Int(1, 0),
        new Vector2Int(1, -1),
        new Vector2Int(0, -1),
        new Vector2Int(-1, -1),
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),
    };
    
    public static void DrawMap(Tilemap map,TileBase tile, HashSet<Vector2Int> positions)
    {
        map.ClearAllTiles();
        
        foreach (Vector2Int position in positions)
        {
            map.SetTile(new Vector3Int(position.x,position.y),tile);
        }
    }
    
    
}
