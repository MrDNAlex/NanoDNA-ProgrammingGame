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

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
