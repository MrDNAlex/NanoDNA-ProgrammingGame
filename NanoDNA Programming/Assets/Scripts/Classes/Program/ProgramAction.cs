using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;

[System.Serializable]
public class ProgramAction 
{
    //Maybe have a dictionary that stores all the information based off titles, then the program checks the type and grabs all the info it needs?

    public string type;
    public int value;
    public string dir;

    public ActionType actionType;

    public MovementActionName movementName;
    public MathActionName mathName;
    public LogicActionName logicName;
    public VariableActionName variableName;

    public object data;


    public ProgramAction(ActionType actionType, MovementActionName moveName, MathActionName mathName, LogicActionName logicName, VariableActionName varName, object data)
    {
        this.actionType = actionType;
        this.movementName = moveName;
        this.mathName = mathName;
        this.logicName = logicName;
        this.variableName = varName;
        this.data = data;
    }

    /*
    public ProgramAction (string type, string dir, int value)
    {
        this.type = type;
        this.dir = dir;
        this.value = value;
    }
    */

    public string dispAction ()
    {
        return type + " " + dir + " " + value;
    }

    /*
    public static ProgramAction empty()
    {
        return new ProgramAction();
    }
    */






}
