using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgramAction 
{

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






}
