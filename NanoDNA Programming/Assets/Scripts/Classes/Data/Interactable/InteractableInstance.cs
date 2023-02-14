using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using DNASaveSystem;

[CreateAssetMenu]
public class InteractableInstance : ScriptableObject
{

    public Sprite sprite;
    public string id;


    public InteractableInstance(Sprite sprite)
    {
        this.sprite = sprite;
        id = sprite.name;
    }

    private void OnValidate()
    {
        if (sprite != null)
        {
            this.id = sprite.name;

        }
    }

}
