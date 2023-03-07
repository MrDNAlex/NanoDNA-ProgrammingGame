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

        loadLedger<EndLedger>();
    }

    void loadLedger<Type>()
    {
        Object[] ledgers = Resources.FindObjectsOfTypeAll(typeof(Type));
        //Debug.Log("Grabbed Ledgers");

        foreach (EndLedger ledger in ledgers)
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
