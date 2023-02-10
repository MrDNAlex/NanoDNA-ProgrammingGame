using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class TileInfo 
{
    //Work with this for now, if RAM usage and storage become a problem later we can find a way to compress it, foxus on MVP 
    public string id;
    public Vector2Int position;

    public TileInfo (string id, Vector2Int pos)
    {
        this.id = id;
        this.position = pos;

     
    }

}
