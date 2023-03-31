using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SensorInstance : ScriptableObject
{
    public Sprite sprite;
    public string id;

    public SensorInstance(Sprite sprite)
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

        loadLedger<SensorLedger>();
    }

    void loadLedger<Type>()
    {
        Object[] ledgers = Resources.FindObjectsOfTypeAll(typeof(Type));
        //Debug.Log("Grabbed Ledgers");

        foreach (SensorLedger ledger in ledgers)
        {
            //Add current instance to the ledger
            if (!ledger.sensors.Contains(this))
            {
                ledger.sensors.Add(this);
                //Debug.Log("Added this");
            }
        }
    }
}
