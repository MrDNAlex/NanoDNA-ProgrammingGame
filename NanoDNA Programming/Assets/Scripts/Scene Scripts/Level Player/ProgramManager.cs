using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;

public class ProgramManager : MonoBehaviour
{

    //Make 2 lists?

    //List for all variables
    List<VariableData> allVariables = new List<VariableData>();

    //Make a list for all the OG Variables when loaded


    //Add functions to load in pre existing / global variables from the level



    // Start is called before the first frame update
    void Start()
    {
        VariableData var1 = new VariableData();
        VariableData var2 = new VariableData();
        VariableData var3 = new VariableData();
        VariableData var4 = new VariableData();
        VariableData var5 = new VariableData();
        VariableData var6 = new VariableData();

        var1.setData(true, VariableType.Number, "Var1", 1.ToString());
        var2.setData(true, VariableType.Number, "Var2", 2.ToString());
        var3.setData(true, VariableType.Number, "Var3", 3.ToString());
        var4.setData(true, VariableType.Number, "Var4", 4.ToString());
        var5.setData(true, VariableType.Number, "Var5", 5.ToString());

        var6.setData(true, VariableType.Text, "Var6", "Message");

        allVariables.Add(var1);
        allVariables.Add(var2);
        allVariables.Add(var3);
        allVariables.Add(var4);
        allVariables.Add(var5);
        allVariables.Add(var6);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<VariableData> getVariables (VariableType varType)
    {
        List<VariableData> list = new List<VariableData>();

        foreach (VariableData var in allVariables)
        {
            if (var.varType == varType)
            {
                list.Add(var);
            }
        }
        return list;
    }



}
