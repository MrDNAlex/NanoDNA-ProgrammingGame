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
        //Debug.Log("Validating");
        if (sprite != null)
        {
            this.id = sprite.name;

        }

        //Load The corrent ledgerLedger
        loadLedger<CharLedger>();
    }

    void loadLedger<Type> ()
    {
        Object[] ledgers = Resources.FindObjectsOfTypeAll(typeof(Type));
        //Debug.Log("Grabbed Ledgers");

        foreach (CharLedger ledger in ledgers)
        {
            //Add current instance to the ledger
            if (!ledger.chars.Contains(this))
            {
                ledger.chars.Add(this);
                //Debug.Log("Added this");
            }
        }
    }
}
