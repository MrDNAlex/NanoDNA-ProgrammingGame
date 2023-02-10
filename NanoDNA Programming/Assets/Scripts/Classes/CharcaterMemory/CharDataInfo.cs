using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharDataInfo
{

    public string name;

    //public string name = "Bob";
    public Program program = new Program(true);

    //Past / Undo states
    public List<Program> programStates = new List<Program>();

    //Eventually replace this with vector2int, referring to tile it would be on, it would then have to align itself to said tile
    public Vector3 initPos;


    public CharDataInfo (CharData data)
    {
        this.name = data.name;
        this.program = data.program;
        this.programStates = data.programStates;
        this.initPos = data.initPos;

    }

}
