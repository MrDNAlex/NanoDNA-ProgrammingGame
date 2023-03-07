using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using DNASaveSystem;

[CreateAssetMenu]
public class TileInstance : ScriptableObject
{

    public TileBase tile;
    public string id;


    public TileInstance (TileBase tile)
    {
        this.tile = tile;
        id = tile.name;
    }

    private void OnValidate()
    {
        if (tile != null)
        {
            this.id = tile.name;

            
        }

        loadLedger<TileLedger>();
       
    }

    void loadLedger<Type>()
    {
        Object[] ledgers = Resources.FindObjectsOfTypeAll(typeof(Type));
        //Debug.Log("Grabbed Ledgers");

        foreach (TileLedger ledger in ledgers)
        {
            //Add current instance to the ledger
            if (!ledger.tiles.Contains(this))
            {
                ledger.tiles.Add(this);
                //Debug.Log("Added this");
            }
        }
    }

}
