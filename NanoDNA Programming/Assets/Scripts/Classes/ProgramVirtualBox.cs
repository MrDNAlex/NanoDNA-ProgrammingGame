using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;

public class ProgramVirtualBox : MonoBehaviour
{
    //Kind of like a virtual box that stores the programs variables

    public List<VariableData> allVariables = new List<VariableData>();

    public List<VariableData> levelVariables = new List<VariableData>();

    public List<VariableData> sensorVariables = new List<VariableData>();

    public ProgramVirtualBox(ProgramManager.ProgramVariables vars)
    {
        this.allVariables = vars.allVariables;
        this.levelVariables = vars.levelVariables;
        this.sensorVariables = vars.sensorVariables;
    }

    public List<VariableData> getVariables(VariableType varType)
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

    public List<VariableData> getAllVariables()
    {
        List<VariableData> list = new List<VariableData>();

        foreach (VariableData var in allVariables)
        {
            list.Add(var);
        }
        return list;
    }

    public void activateSensorVariable(int id)
    {
        foreach (VariableData var in sensorVariables)
        {
            if (var.id == id)
            {
                Debug.Log("Activated Variable: " + var.name);
                var.isActivated = true;
            }
        }
    }

    public void deactivateSensorVariable(int id)
    {
        foreach (VariableData var in sensorVariables)
        {
            if (var.id == id)
            {
                Debug.Log("Deactivated Variable: " + var.name);
                var.isActivated = false;
            }
        }
    }

    public void updateVariables()
    {
        //Delete allVariables
        allVariables = new List<VariableData>();



        foreach (VariableData var in sensorVariables)
        {
            if (var.isLevelVariable == false)
            {
                if (var.isActivated)
                {
                    // displayAllSensorVariables();
                    // Debug.Log("Add Activated");
                    allVariables.Add(var);
                    // displayVariable(var);
                }
                else
                {
                    //  Debug.Log("Edit");
                    VariableData edVar = new VariableData();

                    //Make a shallow copy
                    edVar.isPublic = var.isPublic;
                    edVar.name = var.name;
                    edVar.varType = var.varType;
                    // edVar.value = var.value;
                    edVar.id = var.id;
                    // edVar.isPublic = var.isPublic;
                    //edVar.isPublic = var.isPublic;


                    switch (edVar.varType)
                    {
                        case VariableType.Text:
                            edVar.setValue("");
                            break;
                        case VariableType.Number:
                            edVar.setValue("0");
                            break;
                        case VariableType.Decimal:
                            edVar.setValue("0.0");
                            break;
                        case VariableType.Bool:
                            edVar.setValue("false");
                            break;
                    }
                    allVariables.Add(edVar);
                    //displayVariable(edVar);
                }

            }

        }

        //Add default variables first
        foreach (VariableData var in levelVariables)
        {
            if (var.isLevelVariable == false)
            {
                allVariables.Add(var);
            }
        }

        //Loop through all scripts and add all variables
        foreach (Transform child in Scripts.programSection.charHolder.transform)
        {
            if (child.GetComponent<CharData>() != null)
            {
                //Loop through the program to see if it has any variables
                //Debug.Log(child.name);

                foreach (ProgramAction action in child.GetComponent<CharData>().program.list)
                {
                    if (action.actionType == ActionType.Variable)
                    {
                        if (action.variableName == VariableActionNames.Variable)
                        {
                            //Debug.Log("Variable");
                            //Debug.Log(action.dispDetailedAction());
                            if (action.varActData.setData.id == 0)
                            {
                                //Gen new ID
                                action.varActData.setData.setID(genUniqueID());
                            }

                            action.varActData.setData.setParent(child.GetComponent<CharData>());

                            allVariables.Add(action.varActData.getVarData());
                        }
                    }
                }
            }
        }
        displayAllVariables();
    }

    public void displayAllVariables()
    {
        foreach (VariableData data in allVariables)
        {
            Debug.Log("Variable Type: " + data.varType + " Name: " + data.name + " Value:" + data.value + " ID: " + data.id + " REFID: " + data.refID);
        }
    }

    public void displayAllSensorVariables()
    {
        foreach (VariableData data in sensorVariables)
        {
            Debug.Log("Variable Type: " + data.varType + " Name: " + data.name + " Value:" + data.value + " ID: " + data.id + " REFID: " + data.refID);
        }
    }

    public void displayVariable(VariableData data)
    {
        Debug.Log("Variable Type: " + data.varType + " Name: " + data.name + " Value:" + data.value + " ID: " + data.id + " REFID: " + data.refID);
    }

    public int genUniqueID()
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

    public bool isSameID(int id)
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

    public void updateVariable(VariableActionData data)
    {
        //Search for same ID
        //  Debug.Log(data.setData.id);
        for (int i = 0; i < allVariables.Count; i++)
        {
            if (allVariables[i].id == data.setData.id)
            {
                //Check if they have a reference ID

                if (allVariables[i].refID == 0)
                {
                    //  Debug.Log("Here");
                    //Not referencing
                    allVariables[i].setValue(data.setData.value);
                }
                else
                {
                    //Search for the Ref ID
                    allVariables[i].setValue(allVariables.Find(val => val.id == data.setData.refID).value);
                    data.setData.setValue(allVariables.Find(val => val.id == data.setData.refID).value);

                    //Reset the ReferenceID (For VirtualBox)
                    allVariables[i].refID = 0;
                    data.refID = 0;
                    data.setData.refID = 0;
                }
            }
        }
    }

    public string getVariableName(VariableData data)
    {
        return allVariables.Find(val => val.id == data.refID).name;
    }

    public string getVariableName(int refID)
    {
        return allVariables.Find(val => val.id == refID).name;
    }

    public string getVariableValue(VariableData data)
    {
        return allVariables.Find(val => val.id == data.refID).value;
    }

    public string getVariableValue(int refID)
    {
        if (allVariables.Find(val => val.id == refID).refID != 0)
        {
            Debug.Log("Deep");
            return getVariableValue(allVariables.Find(val => val.id == refID).refID);
        }
        else
        {
            return allVariables.Find(val => val.id == refID).value;
        }
    }

    public VariableType getVariableType(int refID)
    {
        return allVariables.Find(val => val.id == refID).varType;
    }

    public VariableType getVariableType(VariableData data)
    {
        return allVariables.Find(val => val.id == data.refID).varType;
    }





}
