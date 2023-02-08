using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class TileInfo 
{
    //Work with this for now, if RAM usage and storage become a problem later we can find a way to compress it, foxus on MVP 
    TileBase tile;
    Vector2Int position;


    public TileInfo (TileBase tile, Vector2Int pos)
    {
        this.tile = tile;
        this.position = pos;

    }





}
