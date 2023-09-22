using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;

[System.Serializable]
public class Program
{

    public List<ProgramAction> list = new List<ProgramAction>();
    public int progLength;

    public Program(bool fillEmpty)
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

    public void updateLength()
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

    public int getLength()
    {
        int length = 0;

        foreach (ProgramAction action in list)
        {
            if (viableActionType(action))
            {
                length += 1;
            }
        }

        return length;
    }

    public ActionType getProgramActionType (int index)
    {
        return list[index].actionType;
    }

    public MovementActionNames getMovementActionName (int index)
    {
        return list[index].movementName;
    }

    //Put this in a different script later
    public bool viableActionType(ProgramAction action)
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
            case ActionType.Action:
                switch (action.actionName)
                {
                    case ActionActionNames.None:
                        return false;
                    default:
                        return true;

                }
            default:
                return false;
        }
    }

    public void setAction(ProgramAction action, int index)
    {
       
        List<ProgramAction> prog = list;

        list.RemoveAt(index);
        list.Insert(index, action);

        list = prog;

        updateLength();
    }

    public void RemoveLine(int index)
    {
        list.RemoveAt(index);

        //Replace with an empty action
        list.Insert(index, new ProgramAction());
    }

    public void SwitchActions (int index1, int index2)
    {
        //Action 1 goes to index 2
        //Action 2 goes to Index 1

        Debug.Log("Swap: " + index1 + " And " + index2);

        ProgramAction action1 = list[index1];
        ProgramAction action2 = list[index2];

        list.RemoveAt(index1);
        list.Insert(index1, action2);

        list.RemoveAt(index2);
        list.Insert(index2, action1);

        //Check if the card even exists

        if (Scripts.programSection.transform.GetChild(index1).GetChild(1).childCount > 0)
        {
            Scripts.programSection.transform.GetChild(index1).GetChild(1).GetChild(0).GetComponent<ProgramCard>().getLineNumber();
        }

        if (Scripts.programSection.transform.GetChild(index2).GetChild(1).childCount > 0)
        {
            Scripts.programSection.transform.GetChild(index2).GetChild(1).GetChild(0).GetComponent<ProgramCard>().getLineNumber();
        }

      
        



    }

}
