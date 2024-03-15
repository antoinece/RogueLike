using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DrunkYardGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int _stratPos; 
    [SerializeField] private TileBase _base;
    [SerializeField] private Tilemap _map;

    
    
    public void GeneratePath() 
    {
        
        Vector2Int position = _stratPos;
        
        HashSet<Vector2Int> positions = new HashSet<Vector2Int>();
        
        positions.Add(_stratPos);

        do
        {
            Vector2Int direction = genTool.VonNeumannNeighbours[Random.Range(0, genTool.VonNeumannNeighbours.Length)];
            for (int n = 0; n < 1; n++)
            {
                position += direction;
                positions.Add(position);
            }
        } while (positions.Count < 50);

        genTool.DrawMap(_map,_base,positions);
    }
    
    
}
