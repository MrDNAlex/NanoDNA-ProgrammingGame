using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Program 
{
   
    public List<ProgramAction> list = new List<ProgramAction>();

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

   

}
