using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using WFC;
using Random = UnityEngine.Random;

public class WFCGenerator : MonoBehaviour
{
    //all Serializefield variables
    [SerializeField] private Vector2Int size;
    [SerializeField] private Tilemap map;
    [SerializeField] private TileBase unknown;
    [SerializeField] private WFCModuleManager moduleManager;

    //private variables
    private List<WFCSlot> _slots = new List<WFCSlot>();

    private void Start()
    {
        Initiate();
    }

    private void Update()
    {
        Step();
    }

    //initiate stuff
    public void Initiate()
    {
        map.ClearAllTiles();
        _slots.Clear();
        moduleManager.ResetTileSet();

        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);

                map.SetTile(pos, unknown);

                //---------------------------------
                _slots.Add(new WFCSlot(pos, moduleManager.TileSet, unknown));
            }
        }
    }

    //function to collapse a slot
    public void Step()
    {
        List<WFCSlot> collapsableSlots = _slots.Where(s => s.Entropy != 0).OrderBy(s => s.Entropy).ToList();

        if (collapsableSlots.Count > 0)
        {
            float minEntropy = collapsableSlots[0].Entropy;

            var slot = collapsableSlots.Where(s => s.Entropy == minEntropy).OrderBy(s => Random.Range(0f, 1f)).First();

            slot.ForceCollapse();

            map.SetTile(slot.Pos, slot.Tile);

            //Propagation
            Propagate(slot);
        }
        else
        {
            Debug.Log("no boucle");
        }
    }

    //propagate function
    private void Propagate(WFCSlot slot)
    {
        Stack<WFCSlot> slotStack = new Stack<WFCSlot>();
        List<WFCSlot> visitedSlots = new List<WFCSlot>();
        slotStack.Push(slot);

        while (slotStack.Count > 0)
        {
            WFCSlot propagatedSlot = slotStack.Pop();
            visitedSlots.Add(propagatedSlot);


            //system to see and calculate all neighbours in all directions
            foreach (var direction in WFCModuleManager.Directions)
            {
                var newSlot = _slots.FirstOrDefault(s => s.Pos == direction + propagatedSlot.Pos);

                if (newSlot != null && !visitedSlots.Contains(newSlot))
                {
                    List<TileBase> possibibleTiles = new List<TileBase>();
                    foreach (TileBase tile in propagatedSlot.Domain)
                    {
                        foreach (WFCModule module in moduleManager.allModules)
                        {
                            if (module.baseTile == tile)
                            {
                                foreach (TileBase possibleTile in module.neighbours)
                                {
                                    if (!possibibleTiles.Contains(possibleTile))
                                    {
                                        possibibleTiles.Add(possibleTile);
                                    }
                                }
                            }
                        }
                    }

                    //moduleManager.allModules.Where(m => m.baseTile == propagatedSlot.Domain);


                    if (!newSlot.SetNewDomain(possibibleTiles))
                    {
                        slotStack.Push(newSlot);
                    }

                    if (propagatedSlot.Entropy == 0)
                    {
                        map.SetTile(propagatedSlot.Pos, propagatedSlot.Tile);
                        Debug.Log("entropy == 0");
                    }

                    if (propagatedSlot.Entropy == -1)
                    {
                        
                        
                        
                        return;
                    }
                }
            }
        }
    }

    //clear
    public void Clear()
    {
        map.ClearAllTiles();
        _slots.Clear();
        moduleManager.ResetTileSet();
    }


    private void ClearAll()
    {
        map.ClearAllTiles();
        _slots.Clear();
        moduleManager.ResetTileSet();
    }
}

