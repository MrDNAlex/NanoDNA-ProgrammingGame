using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;

[System.Serializable]
public class ProgramAction
{
    //Maybe have a dictionary that stores all the information based off titles, then the program checks the type and grabs all the info it needs?

    //public string type;
    //public int value;
    //public string dir;

    public ActionType actionType;

    public MovementActionNames movementName;
    public MathActionNames mathName;
    public LogicActionNames logicName;
    public VariableActionNames variableName;

    //Maybe have a data type for all ActionTypes

    public MoveData moveData = new MoveData();
    public VariableData varData = new VariableData();


    //Make a Unique Constructore for each? or maybe I stuff the data into the CardInfo?
    public ProgramAction (CardInfo info, MoveData data)
    {
        this.actionType = info.actionType;
        this.movementName = info.movementName;
        this.mathName = info.mathName;
        this.logicName = info.logicName;
        this.variableName = info.variableName;

        this.moveData = data;
    }

    public ProgramAction(CardInfo info, VariableData data)
    {
        this.actionType = info.actionType;
        this.movementName = info.movementName;
        this.mathName = info.mathName;
        this.logicName = info.logicName;
        this.variableName = info.variableName;

        this.varData = data;
    }

    public ProgramAction ()
    {
        //Creates an Empty Program
        this.actionType = ActionType.Movement;
        this.movementName = MovementActionNames.None;
        this.mathName = MathActionNames.None;
        this.logicName = LogicActionNames.None;
        this.variableName = VariableActionNames.None;
        this.moveData = new MoveData();

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
        return actionType + " " + movementName;
    }

    
    public static ProgramAction empty()
    {
        return new ProgramAction();
    }

    
    






}
