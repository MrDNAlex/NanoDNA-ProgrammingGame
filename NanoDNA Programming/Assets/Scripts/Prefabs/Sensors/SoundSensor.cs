using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;

[System.Serializable]
public class SoundSensor : LevelSensor, ISensor
{

    private void Awake()
    {
        this.iSensor = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Calls the reference to the VirtualBox storage in program section and updates through there

        Debug.Log("Collison Enter");
        //Activate all variables it owns
        foreach (VariableData var in variables)
        {
            Scripts.programSection.virtualBox.activateSensorVariable(var.id);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Collison Exit");
        if (eraseOnExit)
        {
            //Deactivate Variables it owns
            foreach (VariableData var in variables)
            {
                Scripts.programSection.virtualBox.deactivateSensorVariable(var.id);
            }

        }
    }

    public void setInfo (SensorDataInfo info)
    {
        this.name = info.name;
        this.pos = info.pos;
        this.sensorSize = info.sensorSize;
        this.size = info.size;
        this.variables = info.variables;
        this.type = info.type;
        this.eraseOnExit = info.eraseOnExit;

        this.transform.localPosition = this.pos;
        this.transform.localScale = this.size;
        this.GetComponent<BoxCollider>().size = this.sensorSize;

       // foreach (VariableData var in variables)
     //   {
      //      Scripts.programManager.displayVariable(var);
      //  }

        Scripts.programManager.addSensorVariables(this.variables);

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
