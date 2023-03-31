using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

[CreateAssetMenu]
public class InteractableLedger : ScriptableObject
{

    public List<InteractableInstance> sprites;

    public bool validate;

    private void OnValidate()
    {
        Object[] idk = Resources.FindObjectsOfTypeAll(typeof(InteractableInstance));
    }
}
