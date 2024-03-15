using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WFCAnalysyer : MonoBehaviour
{
    [SerializeField] private Tilemap _map; 
    //[SerializeField] private WFCModuleSet _baseBackground;

    public void analyze()
    {

        TileBase[] tiles = _map.GetTilesBlock(_map.cellBounds);

        foreach (TileBase tile in tiles)
        {
            if (tile != null)
            {
                Debug.Log("module : "+tile.name);
            }
            
        }
    }
}
