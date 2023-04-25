using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

[CreateAssetMenu]
public class CollectableLedger : ScriptableObject
{

    public List<CollectableInstance> sprites;

    public bool validate;

    private void OnValidate()
    {
        Object[] idk = Resources.FindObjectsOfTypeAll(typeof(CollectableInstance));
    }
}
