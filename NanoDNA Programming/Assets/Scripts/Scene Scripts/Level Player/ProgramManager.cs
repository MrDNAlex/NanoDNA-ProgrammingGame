using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;

public class ProgramManager : MonoBehaviour
{

    //Make 2 lists?

    //List for all variables
    List<VariableData> allVariables = new List<VariableData>();

    List<VariableData> defaultVariables = new List<VariableData>();

    //Make a list for all the OG Variables when loaded
    Scripts allScripts;

    //Add functions to load in pre existing / global variables from the level

    private void Awake()
    {
        Camera.main.GetComponent<LevelScript>().allScripts.programManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        VariableData var1 = new VariableData();
        VariableData var2 = new VariableData();
        VariableData var3 = new VariableData();
        VariableData var4 = new VariableData();
        VariableData var5 = new VariableData();
        VariableData var6 = new VariableData();
        VariableData var7 = new VariableData();

        var1.setData(true, VariableType.Number, "Var1", 1.ToString());
        var2.setData(true, VariableType.Number, "Var2", 2.ToString());
        var3.setData(true, VariableType.Number, "Var3", 3.ToString());
        var4.setData(true, VariableType.Number, "Var4", 4.ToString());
        var5.setData(true, VariableType.Number, "Var5", 5.ToString());
        var6.setData(true, VariableType.Text, "Var6", "Message");
        var7.setData(true, VariableType.Bool, "Var7", "true");

        var1.setID(genUniqueID());
        var2.setID(genUniqueID());
        var3.setID(genUniqueID());
        var4.setID(genUniqueID());
        var5.setID(genUniqueID());
        var6.setID(genUniqueID());
        var7.setID(genUniqueID());

        defaultVariables.Add(var1);
        defaultVariables.Add(var2);
        defaultVariables.Add(var3);
        defaultVariables.Add(var4);
        defaultVariables.Add(var5);
        defaultVariables.Add(var6);
        defaultVariables.Add(var7);

        allScripts = Camera.main.GetComponent<LevelScript>().allScripts;

        updateVariables();
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

    public void updateVariables ()
    {
        //Delete allVariables
        allVariables = new List<VariableData>();

        //Add default variables first
        foreach (VariableData var in defaultVariables)
        {
            allVariables.Add(var);
        }

        //Loop through all scripts and add all variables
        foreach (Transform child in allScripts.programSection.charHolder.transform)
        {
           

            if (child.GetComponent<CharData>() != null)
            {
                //Loop through the program to see if it has any variables

                foreach (ProgramAction action in child.GetComponent<CharData>().program.list)
                {
                    if (action.actionType == ActionType.Variable)
                    {
                        if (action.varData.id == 0)
                        {
                            //Gen new ID
                            action.varData.setID(genUniqueID());
                        }

                        action.varData.setParent(child.gameObject);

                        allVariables.Add(action.varData);
                    }
                }

            }

        }

        //displayAllVariables();

    }

    public void displayAllVariables ()
    {
        foreach (VariableData data in allVariables)
        {
            Debug.Log("Variable Type: " + data.varType + " Name: " + data.name + " Value:" + data.value + " ID: " + data.id + " REFID: " + data.refID);
        }
    }

   public int genUniqueID ()
    {
        
        int rngID = 0;

        bool sameID = true;

        while (sameID)
        {
            rngID = Random.Range(0, 1000);

            sameID = isSameID(rngID);
        }
        return rngID;

    }

    public bool isSameID (int id)
    {
        bool verdict = false;
        foreach (VariableData var in allVariables)
        {
            if (var.id == id)
            {
                verdict = true;
            }
        }

        return verdict;
    }

    public void updateVariable (VariableData data)
    {
        //Search for same ID
       
        for (int i = 0; i < allVariables.Count; i ++)
        {
           if (allVariables[i].id == data.id)
            {
                //Check if they have a reference ID

                if (allVariables[i].refID == 0)
                {
                    //Not referencing
                    allVariables[i].setValue(data.value);
                } else
                {
                    //Search for the Ref ID
                    //Debug.Log(allVariables[i].value);
                    allVariables[i].setValue(allVariables.Find(val => val.id == data.refID).value);
                    data.setValue(allVariables.Find(val => val.id == data.refID).value);
                   // Debug.Log(allVariables[i].value);

                   // Debug.Log(allVariables.Find(val => val.id == data.refID).value);
                   // Debug.Log("Value Updated");

                   

                }
            }
        }

    }

    public string getVariableName (VariableData data)
    {
        return allVariables.Find(val => val.id == data.refID).name;
    }

    public string getVariableName (int refID)
    {
        return allVariables.Find(val => val.id == refID).name;
    }

    public string getVariableValue(VariableData data)
    {
        return allVariables.Find(val => val.id == data.refID).value;
    }

    public string getVariableValue(int refID)
    {
        return allVariables.Find(val => val.id == refID).value;
    }

}
