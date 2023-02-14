using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractableDataInfo 
{

    public string name;

    public Vector3 initPos;

    //Maybe eventuall add List of variables/accessible data

    //Maybe add a program that can be edited or not edited

    public InteractableDataInfo (InteractableData data)
    {

        this.name = data.name;
        this.initPos = data.initPos;

    }
    




}
