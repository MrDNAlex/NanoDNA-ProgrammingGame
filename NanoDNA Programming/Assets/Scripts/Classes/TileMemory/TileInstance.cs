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
       
    }

}
