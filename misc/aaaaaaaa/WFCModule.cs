using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WFC
{
    [CreateAssetMenu(menuName = "WFC/Modules", fileName = "module_1")]
    public class WFCModule : ScriptableObject
    {
        //public variables
        public TileBase baseTile;
        public List<TileBase> neighbours;

    }
}