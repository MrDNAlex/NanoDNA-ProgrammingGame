using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelIconLedger: ScriptableObject
{

    public List<LevelIconInstance> icons;

    public bool validate;

    private void OnValidate()
    {
        Object[] idk = Resources.FindObjectsOfTypeAll(typeof(LevelIconInstance));
    }

}
