using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DNAStruct;


[System.Serializable]
public class CharData : MonoBehaviour
{
    //
    //Maybe increase efficiency using this https://blog.unity.com/technology/how-on-demand-rendering-can-improve-mobile-performance
    //

    //Maybe add a system that checks if it's compiling for the right character?

    public UIWord name;

    //public string name = "Bob";
    public Program program = new Program(true);

    //Past / Undo states
    public List<Program> programStates = new List<Program>();

    //Eventually replace this with vector2int, referring to tile it would be on, it would then have to align itself to said tile
    public Vector3 initPos;

    //Maybe delete this
    public int charID;


    public void Start()
    {
        programStates.Add(program);

        OnDemandRendering.renderFrameInterval = 12;
    }

    public CharData(CharDataInfo info)
    {
        this.name = info.name;
        this.program = info.program;
        this.programStates = info.programStates;
        this.initPos = info.initPos;
    }

    public ProgramAction getAction(int index)
    {
        if ((program.list.Count - 1) < index)
        {
            //Doesn't exist
            return ProgramAction.empty();
        }
        else
        {
            //Does exist
            return program.list[index];
        }
    }

    public void displayProgram(bool detailed = false)
    {
        // Debug.Log(program.list.Count);

        for (int i = 0; i < program.list.Count; i++)
        {
            if (detailed)
            {
                Debug.Log((i + 1) + ": " + program.list[i].dispDetailedAction());
            }
            else
            {
                Debug.Log((i + 1) + ": " + program.list[i].dispAction());
            }
        }
    }

    public void addPastState(Program program)
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

    public Program undoState()
    {
        //Grab last state
        Program newState = programStates[0];

        programStates.RemoveAt(0);

        return newState;

    }

    //Create a function that adds a new program action, it then calls to update program length

    public void addAction(ProgramAction action)
    {
        program.list.Add(action);

        program.updateLength();
    }

    void genID()
    {
        charID = Random.Range(0, 1000000);
    }

}
