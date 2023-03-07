using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu]
public class EndLedger : ScriptableObject
{
    public List<EndInstance> sprites;

    public bool validate;

    private void OnValidate()
    {


    }
}
