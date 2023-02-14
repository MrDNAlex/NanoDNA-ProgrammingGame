using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using System.Linq;
using UnityEditor;

[CreateAssetMenu]
public class TileLedger : ScriptableObject
{
    public List<TileInstance> tiles;

    public bool validate;

    private void OnValidate()
    {

        TileInstance[] idk  = (TileInstance[])Resources.FindObjectsOfTypeAll(typeof(TileInstance));

        tiles = idk.ToList();

    }


    
}
