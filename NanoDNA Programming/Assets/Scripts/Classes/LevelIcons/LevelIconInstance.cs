using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class LevelIconInstance : ScriptableObject
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
        loadLedger<LevelIconLedger>();
    }

    void loadLedger<Type>()
    {
        Object[] ledgers = Resources.FindObjectsOfTypeAll(typeof(Type));
        //Debug.Log("Grabbed Ledgers");

        foreach (LevelIconLedger ledger in ledgers)
        {
            //Add current instance to the ledger
            if (!ledger.icons.Contains(this))
            {
                ledger.icons.Add(this);
                //Debug.Log("Added this");
            }
        }
    }
}
