using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;

[System.Serializable]
public class LevelSensor : MonoBehaviour
{
    [System.Serializable]
    public enum SensorType
    {
        SoundSensor,
    }

    public string name;

    public Vector3 pos;

    public Vector3 size;

    public Vector3 sensorSize;

    public List<VariableData> variables;

    public SensorType type;

    public ISensor iSensor;

    //Makes the variables no longer accessible when trigger exits
    public bool eraseOnExit;

    public void setVariables ()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
