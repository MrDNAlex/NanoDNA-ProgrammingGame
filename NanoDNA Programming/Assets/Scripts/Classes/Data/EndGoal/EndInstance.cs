using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EndInstance : ScriptableObject
{
    public Sprite sprite;
    public string id;

    public EndInstance(Sprite sprite)
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
