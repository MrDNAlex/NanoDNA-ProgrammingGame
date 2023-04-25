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
        MotionSensor, 

    }

    public string name;

    public Vector3 pos;

    public Vector3 size;

    public Vector3 sensorSize;

    //List of variables that the player can grab from the item (When in range)
    public List<VariableData> variables;

    public List<SensorSignal> signals;

    public SensorType type;

    public ISensor iSensor;

    //Makes the variables no longer accessible when trigger exits (One time use)
    public bool eraseOnExit;


    public void setInfo(SensorDataInfo info)
    {
        this.name = info.name;
        this.pos = info.pos;
        this.sensorSize = info.sensorSize;
        this.size = info.size;
        this.variables = info.variables;
        this.signals = info.signals;
        this.type = info.type;
        this.eraseOnExit = info.eraseOnExit;

        this.transform.localPosition = this.pos;
        this.transform.localScale = this.size;


        this.GetComponent<BoxCollider>().size = this.sensorSize;

        Scripts.programManager.addSensorVariables(this.variables);


    }


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
