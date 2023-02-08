using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgramAction 
{

    //Maybe have a dictionary that stores all the information based off titles, then the program checks the type and grabs all the info it needs?

    public string type;
    public int value;
    public string dir;


    public ProgramAction (string type, string dir, int value)
    {
        this.type = type;
        this.dir = dir;
        this.value = value;
    }

    public string dispAction ()
    {
        return type + " " + dir + " " + value;
    }

    public static ProgramAction empty()
    {
        return new ProgramAction("none", "up", 0);
    }






}
