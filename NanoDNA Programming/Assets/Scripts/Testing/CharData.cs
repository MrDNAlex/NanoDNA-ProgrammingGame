using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CharData : MonoBehaviour
{
    //public string name = "Bob";
    public Program program = new Program(true);


    public List<Program> programStates = new List<Program>();

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




}
