using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using DNASaveSystem;

[CreateAssetMenu]
public class CharInstance : ScriptableObject
{
    public Sprite sprite;
    public string id;

    private void OnValidate()
    {
        if (sprite != null)
        {
            this.id = sprite.name;

        }
    }
}
