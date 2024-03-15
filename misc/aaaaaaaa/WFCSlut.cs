using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WFCSlot
{
    //all variables
    private List<TileBase> _domain;
    private float _entropy;
    private Vector3Int _pos;
    private TileBase _tile;
    private TileBase _unknown;

    //Get
    public Vector3Int Pos => _pos;
    public float Entropy => _entropy;
    public List<TileBase> Domain => _domain;
    public TileBase Tile
    {
        get
        {
            if (_domain.Count == 1)
            {
                // sloved slot
                return _domain[0];
            }
            else
            {
                // unsolved slot
                return _unknown;
            }
        }
    }

    //constructor
    public WFCSlot(Vector3Int pos, List<TileBase> tiles, TileBase unkownTile)
    {
        _pos = pos;
        _domain = new List<TileBase>(tiles);
        _entropy = _domain.Count - 1;
        _unknown = unkownTile;
    }

    //colapsing function
    public void ForceCollapse()
    {
        TileBase collapsedTile = _domain.OrderBy(t => Random.Range(0f, 1f)).First();

        _domain.Clear();
        _domain.Add(collapsedTile);

        _entropy = 0;
    }

    public bool SetNewDomain(List<TileBase> propagatedSlotDoamin)
    {
        List<TileBase> newDomain = _domain.Intersect(propagatedSlotDoamin).ToList();

        if (newDomain.Count <= 0)
        {
            Debug.LogWarning("domain poblem");
            return false;
        }

        bool changed = _domain.Count != newDomain.Count;
        _domain = new List<TileBase>(newDomain);
        _entropy = _domain.Count - 1;

        return changed;
    }
}