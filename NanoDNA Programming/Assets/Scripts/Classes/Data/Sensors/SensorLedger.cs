using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SensorLedger : ScriptableObject
{
    public List<SensorInstance> sensors;

    public bool validate;

}
