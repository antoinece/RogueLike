using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using WFC;

[CreateAssetMenu(menuName = "WFC/Modules manager", fileName = "Module set", order = 0)]
public class WFCModuleManager : ScriptableObject
{
    //public variables
    public List<WFCModule> allModules;

    //private variables
    private List<TileBase> _tileSet = new List<TileBase>();

    public static Vector3Int[] Directions =
    {
        Vector3Int.up,
        Vector3Int.right,
        Vector3Int.down,
        Vector3Int.left
    };

    public void ResetTileSet()
    {
        _tileSet.Clear();
    }

    public List<TileBase> TileSet
    {
        //if no stuff in it -> put some
        get
        {
            if (_tileSet == null || _tileSet.Count <= 0)
            {
                foreach (var module in allModules)
                {
                    _tileSet.Add(module.baseTile);
                }
            }

            return _tileSet;
        }
    }

    public void AddModuleNeighbour(TileBase rootTile, TileBase moduleTile)
    {
        //create scriptable objects but also need to create assets to work :

        //get the path of something (derectory name for a directory not a path)
        var path = Path.GetDirectoryName(AssetDatabase.GetAssetPath(this));

        if (path != null)
        { 
            //scriptable object creation
            
            WFCModule freshModule = allModules.FirstOrDefault(moduleTile => moduleTile.baseTile == rootTile);
            if (freshModule == null)
            {
                freshModule = CreateInstance<WFCModule>();
                AssetDatabase.CreateAsset(freshModule, path + "/" + rootTile.name + ".asset");
                allModules.Add(freshModule);

                freshModule.baseTile = rootTile;
                freshModule.neighbours = new List<TileBase>();
                freshModule.neighbours.Add(rootTile);
            }

            /*if (!freshModule.neighbours.Contains(tile))
            {
                freshModule.neighbours.Add();
            }*/
            
        }


        //need to save the assets
        AssetDatabase.SaveAssets();
    }
}
