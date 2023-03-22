using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;

[System.Serializable]
public class ProgramAction
{
    //Maybe have a dictionary that stores all the information based off titles, then the program checks the type and grabs all the info it needs?


    //Add a bool to say it's empty

    public ActionType actionType;

    public MovementActionNames movementName;
    //public MathActionNames mathName;
    public LogicActionNames logicName;
    public VariableActionNames variableName;
    public ActionActionNames actionName;

    //Maybe have a data type for all ActionTypes

    public MoveData moveData = new MoveData();
    public VariableData varData = new VariableData();
    public ActionData actData = new ActionData();


    //Make a Unique Constructore for each? or maybe I stuff the data into the CardInfo?
    public ProgramAction (CardInfo info, MoveData data)
    {
        this.actionType = info.actionType;
        this.movementName = info.movementName;
        //this.mathName = info.mathName;
        this.logicName = info.logicName;
        this.variableName = info.variableName;
        this.actionName = info.actionName;

        this.moveData = data;
    }

    public ProgramAction(ProgramCard.ActionInfo info, MoveData data)
    {
        this.actionType = info.actionType;
        this.movementName = info.movementName;
        this.logicName = info.logicName;
        this.variableName = info.variableName;
        this.actionName = info.actionName;

        this.moveData = data;
    }

    public ProgramAction(CardInfo info, VariableData data)
    {

        this.actionType = info.actionType;
        this.movementName = info.movementName;
       // this.mathName = info.mathName;
        this.logicName = info.logicName;
        this.variableName = info.variableName;
        this.actionName = info.actionName;

        this.varData = data;
    }

    public ProgramAction(ProgramCard.ActionInfo info, VariableData data)
    {

        this.actionType = info.actionType;
        this.movementName = info.movementName;
        this.logicName = info.logicName;
        this.variableName = info.variableName;
        this.actionName = info.actionName;

        this.varData = data;
    }

    public ProgramAction(CardInfo info, ActionData data)
    {

        this.actionType = info.actionType;
        this.movementName = info.movementName;
        //this.mathName = info.mathName;
        this.logicName = info.logicName;
        this.variableName = info.variableName;
        this.actionName = info.actionName;

        this.actData = data;
    }

    public ProgramAction(ProgramCard.ActionInfo info, ActionData data)
    {

        this.actionType = info.actionType;
        this.movementName = info.movementName;
        this.logicName = info.logicName;
        this.variableName = info.variableName;
        this.actionName = info.actionName;

        this.actData = data;

        this.actData.character = Scripts.programSection.selectedCharacter.transform;
    }

    public ProgramAction ()
    {
        //Creates an Empty Program
        this.actionType = ActionType.Movement;
        this.movementName = MovementActionNames.None;
        //this.mathName = MathActionNames.None;
        this.logicName = LogicActionNames.None;
        this.variableName = VariableActionNames.None;
        this.actionName = ActionActionNames.None;
        this.moveData = new MoveData();

    }

    public string dispAction ()
    {
        string str = "";

        switch (actionType)
        {
            case ActionType.Movement:
                str = actionType + " " + movementName;
                break;
            //case ActionType.Math:
              //  str = actionType + " " + mathName;
                break;
            case ActionType.Logic:
                str = actionType + " " + logicName;
                break;
            case ActionType.Variable:
                str = actionType + " " + variableName;
                break;
            case ActionType.Action:
                str = actionType + " " + actionName;
                break;
           
        }
        return str;
    }

    public string dispDetailedAction ()
    {
        string str = "";

        switch (actionType)
        {
            case ActionType.Movement:

                if (movementName == MovementActionNames.None)
                {
                    str = "";
                } else
                {
                    str = actionType + " " + movementName + " " + getDirection(moveData.dir) + " " + moveData.value + " " + moveData.refID;
                }
               
                break;
           // case ActionType.Math:
             //   str = actionType + " " + mathName;
                break;
            case ActionType.Logic:
                str = actionType + " " + logicName;
                break;
            case ActionType.Variable:
                str = actionType + " " + variableName + " "+ varData.isPublic + " " + varData.varType + " " + varData.name + " " + varData.value;
                break;
            case ActionType.Action:
                str = actionType + " " + actionName;
                break;

        }
        return str;
    }

    
    public static ProgramAction empty()
    {
        return new ProgramAction();
    }


    public string getDirection (Direction dir)
    {
        UIWord Up = new UIWord("Up", "Haut");
        UIWord Left = new UIWord("Left", "Gauche");
        UIWord Right = new UIWord("Right", "Droite");
        UIWord Down = new UIWord("Down", "Bas");

        switch (dir)
        {
            case Direction.Up:
                return Up.getWord(Language.English);
            case Direction.Left:
                return Left.getWord(Language.English);
            case Direction.Right:
                return Right.getWord(Language.English);
            case Direction.Down:
                return Down.getWord(Language.English);
            default:
                return "";
        }

       
    }
    
}
