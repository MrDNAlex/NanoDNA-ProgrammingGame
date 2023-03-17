using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;
using System.IO;

public class ProgramPrefabs 
{

   public struct InstanceSearch
    {
       public ActionType type;

        public MovementActionNames moveName;
        public LogicActionNames logicName;
        public MathActionNames mathName;
        public VariableActionNames varName;
        public ActionActionNames actionName;

        public void setSearch (ProgramCard progCard)
        {
            this.type = progCard.actionType;

            this.moveName = progCard.movementName;
            this.logicName = progCard.logicName;
            this.mathName = progCard.mathName;
            this.varName = progCard.variableName;
            this.actionName = progCard.actionName;
        }

        public void setSearch(ProgramAction action)
        {
            this.type = action.actionType;

            this.moveName = action.movementName;
            this.logicName = action.logicName;
            this.mathName = action.mathName;
            this.varName = action.variableName;
            this.actionName = action.actionName;
        }

        public void setSearch (CardInfo info)
        {
            this.type = info.actionType;

            this.moveName = info.movementName;
            this.logicName = info.logicName;
            this.mathName = info.mathName;
            this.varName = info.variableName;
            this.actionName = info.actionName;
        }
    }

    public struct ProgramInstance
    {
        public string path;
        ActionType type;

        MovementActionNames moveName;
        LogicActionNames logicName;
        MathActionNames mathName;
        VariableActionNames varName;
        ActionActionNames actionName;


        public void setInstance (string path)
        {
            GameObject obj = Resources.Load(path) as GameObject;

            ProgramCard progCard = obj.GetComponent<ProgramCard>();

            this.path = path;

            this.type = progCard.actionType;

            this.moveName = progCard.movementName;
            this.logicName = progCard.logicName;
            this.mathName = progCard.mathName;
            this.varName = progCard.variableName;
            this.actionName = progCard.actionName;

            //Maybe we don't need this
            /*
            switch (this.type)
            {
                case ActionType.Movement:

                    break;
                case ActionType.Logic:
                    break;
                case ActionType.Variable:
                    break;
                case ActionType.Math:
                    break;
                case ActionType.Action:
                    break;
            }
            */
        }

        public bool isInstance (InstanceSearch search)
        {
            if (this.type == search.type)
            {
               
                if (this.moveName == search.moveName && search.moveName != MovementActionNames.None)
                {
                    return true;
                }
                else if (this.logicName == search.logicName && search.logicName != LogicActionNames.None)
                {
                    return true;
                }
                else if (this.mathName == search.mathName && search.mathName != MathActionNames.None)
                {
                    return true;
                }
                else if (this.varName == search.varName && search.varName != VariableActionNames.None)
                {
                    return true;
                }
                else if (this.actionName == search.actionName && search.actionName != ActionActionNames.None)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } else
            {
                return false;
            }
        }
    }



    //
    //
    //


    List<ProgramInstance> programs = new List<ProgramInstance>();

    public ProgramPrefabs ()
    {
        createList();
    }

    void createList ()
    {
        List<ActionType> tags = new List<ActionType>();
        tags.Add(ActionType.Movement);
        tags.Add(ActionType.Logic);
       // tags.Add(ActionType.Math);
        tags.Add(ActionType.Variable);
        tags.Add(ActionType.Action);

        foreach (ActionType tag in tags)
        {
            DirectoryInfo dir = new DirectoryInfo("Assets/Resources/" + folderPaths(tag));

            foreach (var file in dir.GetFiles())
            {
               // Debug.Log(file.FullName);

                if (!file.FullName.Contains(".meta"))
                {
                    string path = file.FullName.Substring(file.FullName.IndexOf("Prefabs"));

                    path = path.Replace(".prefab", "");

                    ProgramInstance instance = new ProgramInstance();

                    instance.setInstance(path);

                    programs.Add(instance);
                }
            }
        }
    }

    public static string folderPaths(ActionType tag)
    {
        switch (tag)
        {
            case ActionType.Movement:
                return "Prefabs/Programs/Movement";
            //case ActionType.Math:
               // return "Prefabs/Programs/Math";
            case ActionType.Logic:
                return "Prefabs/Programs/Logic";
            case ActionType.Variable:
                if (PlayerSettings.advancedVariables)
                {
                    return "Prefabs/Programs/Variable/Advanced";
                }
                else
                {
                    return "Prefabs/Programs/Variable/Simple";
                }
            case ActionType.Action:
                return "Prefabs/Programs/Action";
            default:
                return "";
        }
    }

    public static Object[] LoadAllPrefabs (ActionType tag)
    {
        return Resources.LoadAll(folderPaths(tag));
    }

    public GameObject getPrefab (InstanceSearch search)
    {
        GameObject obj = null;
        foreach (ProgramInstance inst in programs)
        {
            if (inst.isInstance(search))
            {
                obj = Resources.Load(inst.path) as GameObject;
               // Debug.Log("Obj " +obj);
            } 
        }

        return obj;
    }
}
