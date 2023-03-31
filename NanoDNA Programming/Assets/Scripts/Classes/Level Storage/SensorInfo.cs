using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SensorInfo 
{
    public string id;

    public SensorDataInfo data;

    public SensorInfo(SensorDataInfo data, string id)
    {
        this.data = data;
        this.id = id;
    }
}
