using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;

[System.Serializable]
public class SensorDataInfo
{
    public string name;

    public Vector3 pos;

    public Vector3 size;

    public Vector3 sensorSize;

    public List<VariableData> variables;

    public List<SensorSignal> signals;

    public LevelSensor.SensorType type;

    public bool eraseOnExit;


    public SensorDataInfo (LevelSensor data)
    {
        this.name = data.name;
        this.pos = data.pos;
        this.size = data.size;
        this.sensorSize = data.sensorSize;
        this.variables = data.variables;
        this.signals = data.signals;
        this.type = data.type;
        this.eraseOnExit = data.eraseOnExit;
    }


}
