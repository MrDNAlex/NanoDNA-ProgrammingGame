using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EndDataInfo
{
    public string name;

    public Vector3 pos;

    public Vector3 size;

    //Maybe eventuall add List of variables/accessible data

    //Maybe add a program that can be edited or not edited

    public EndDataInfo(EndData data)
    {

        this.name = data.name;
        this.pos = data.pos;
        this.size = data.size;

    }
}
