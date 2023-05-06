using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using DNASaveSystem;

[CreateAssetMenu]
public class CollectableInstance : ScriptableObject
{

    public Sprite sprite;
    public string id;


    public CollectableInstance(Sprite sprite)
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

        loadLedger<CollectableLedger>();
    }

    void loadLedger<Type>()
    {
        Object[] ledgers = Resources.FindObjectsOfTypeAll(typeof(Type));
        //Debug.Log("Grabbed Ledgers");

        foreach (CollectableLedger ledger in ledgers)
        {
            //Add current instance to the ledger
            if (!ledger.sprites.Contains(this))
            {
                ledger.sprites.Add(this);
                //Debug.Log("Added this");
            }
        }
    }

}
