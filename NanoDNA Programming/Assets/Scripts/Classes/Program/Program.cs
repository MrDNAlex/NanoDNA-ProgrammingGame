using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;

[System.Serializable]
public class Program 
{
   
    public List<ProgramAction> list = new List<ProgramAction>();
    public int progLength;

    public Program (bool fillEmpty)
    {
        //Spawn a default list of empty commands

        if (fillEmpty)
        {
            for (int i = 0; i < 20; i++)
            {
               
                list.Add(ProgramAction.empty());
            }
        }
       
    }

    public void updateLength ()
    {
        progLength = 0;

        //Loop through all and check if viable lines are there
        foreach (ProgramAction action in list)
        {
            if (viableActionType(action))
            {
                progLength += 1;
            } 
        }
    }

    //Put this in a different script later
    public bool viableActionType (ProgramAction action)
    {
        switch (action.actionType)
        {
            case ActionType.Movement:

                switch (action.movementName)
                {
                    case MovementActionNames.None:
                        return false;
                    default:
                        return true;
                }
            case ActionType.Math:
                switch (action.mathName)
                {
                    case MathActionNames.None:
                        return false;
                    default:
                        return true;
                }
            case ActionType.Logic:
                switch (action.logicName)
                {
                    case LogicActionNames.None:

                        return false;
                    default:
                        return true;
                }
            case ActionType.Variable:
                switch (action.variableName)
                {
                    case VariableActionNames.None:
                        return false;
                    default:
                        return true;
                }
            default:
                return false;
              
        }
    }

   

}
