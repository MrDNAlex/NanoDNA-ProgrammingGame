using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;

public class MotionSensor : LevelSensor, ISensor
{

    //Future Ideas
    //Keep a trigger count, once it's exceeded it sends a signal
    private void Awake()
    {
        this.iSensor = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //Calls the reference to the VirtualBox storage in program section and updates through there

        //Debug.Log("Collison Enter");
        //Activate all variables it owns
        foreach (VariableData var in variables)
        {
            Scripts.programSection.virtualBox.activateSensorVariable(var.id);
        }

        //Send signals
        foreach (SensorSignal signal in this.signals)
        {
            Scripts.programSection.virtualBox.activateSignal(signal);
        }

    }

    private void OnTriggerExit(Collider other)
    {
       // Debug.Log("Collison Exit");
        if (eraseOnExit)
        {
            //Deactivate Variables it owns
            foreach (VariableData var in variables)
            {
                Scripts.programSection.virtualBox.deactivateSensorVariable(var.id);
            }

        }
    }


}
