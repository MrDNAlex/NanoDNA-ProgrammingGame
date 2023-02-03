using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CharData : MonoBehaviour
{

    //Maybe add a system that checks if it's compiling for the right character?

    public string name;

    //public string name = "Bob";
    public Program program = new Program(true);

    //Past / Undo states
    public List<Program> programStates = new List<Program>();

    public void Start()
    {
        programStates.Add(program);
    }

    public ProgramAction getAction (int index)
    {
        if ((program.list.Count - 1) < index)
        {

            //Doesn't exist
           
            return ProgramAction.empty();
        } else
        {
            
           
            //Does exist
            return program.list[index];
        }
    }

    public void displayProgram ()
    {
        for (int i = 0; i < program.list.Count; i++)
        {
            if (program.list[i].type == "move")
            {
                Debug.Log(program.list[i].dispAction());
            }
        }
    }

    public void addPastState (Program program)
    {
        List<Program> states = new List<Program>();

       
            states.Add(program);

            int stateNum;

            if (programStates.Count <= 20)
            {
                stateNum = programStates.Count;
            }
            else
            {
                stateNum = 20;
            }

            for (int i = 0; i < stateNum; i++)
            {
                states.Add(programStates[i]);
            }

            programStates = states;
        

    }

    public Program undoState ()
    {
        //Grab last state
        Program newState = programStates[0];

        programStates.RemoveAt(0);

        return newState;

    }




}
