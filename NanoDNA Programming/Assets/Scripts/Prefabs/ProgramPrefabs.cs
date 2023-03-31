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
        public VariableActionNames varName;
        public ActionActionNames actionName;

        public void setSearch (ProgramCard progCard)
        {
            this.type = progCard.actionInfo.actionType;

            this.moveName = progCard.actionInfo.movementName;
            this.logicName = progCard.actionInfo.logicName;
            this.varName = progCard.actionInfo.variableName;
            this.actionName = progCard.actionInfo.actionName;
        }

        public void setSearch(ProgramAction action)
        {
            this.type = action.actionType;

            this.moveName = action.movementName;
            this.logicName = action.logicName;
            this.varName = action.variableName;
            this.actionName = action.actionName;
        }

        public void setSearch (CardInfo info)
        {
            this.type = info.actionType;

            this.moveName = info.movementName;
            this.logicName = info.logicName;
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
        VariableActionNames varName;
        ActionActionNames actionName;

        public GameObject obj;

        public void setInstance (string path)
        {
            GameObject obj = Resources.Load(path) as GameObject;

            ProgramCard progCard = obj.GetComponent<ProgramCard>();

            this.path = path;

            this.type = progCard.actionInfo.actionType;

            this.moveName = progCard.actionInfo.movementName;
            this.logicName = progCard.actionInfo.logicName;
            this.varName = progCard.actionInfo.variableName;
            this.actionName = progCard.actionInfo.actionName;

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

        public void setInstance(GameObject obj)
        {
            this.obj = obj;

            ProgramCard progCard = obj.GetComponent<ProgramCard>();

            //this.path = path;

            this.type = progCard.actionInfo.actionType;

            this.moveName = progCard.actionInfo.movementName;
            this.logicName = progCard.actionInfo.logicName;
            this.varName = progCard.actionInfo.variableName;
            this.actionName = progCard.actionInfo.actionName;

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
        tags.Add(ActionType.Variable);
        tags.Add(ActionType.Action);


        //Do a resource.loadall. save the path that it got those things from and then 


        foreach (ActionType tag in tags)
        {
            //DirectoryInfo dir = new DirectoryInfo("Assets/Resources/" + folderPaths(tag));

            GameObject[] objects = Resources.LoadAll<GameObject>(folderPaths(tag));

            foreach (GameObject obj in objects)
            {
                ProgramInstance instance = new ProgramInstance();

                instance.setInstance(obj);

                programs.Add(instance);
            }

            //Scripts.levelScript.LiveDebug(dir.FullName);
            /*
            foreach (var file in dir.GetFiles())
            {
                Scripts.levelScript.LiveDebug("Getting Files");
                // Debug.Log(file.FullName);

                if (!file.FullName.Contains(".meta"))
                {
                    string path = file.FullName.Substring(file.FullName.IndexOf("Prefabs"));

                    path = path.Replace(".prefab", "");

                    ProgramInstance instance = new ProgramInstance();

                    instance.setInstance(path);

                    Scripts.levelScript.LiveDebug(path);

                    programs.Add(instance);
                }
            }
            */
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
       // Debug.Log("Get Prefab");
        GameObject obj = null;
        foreach (ProgramInstance inst in programs)
        {
            if (inst.isInstance(search))
            {
                obj = inst.obj;
               // Debug.Log("Obj " +obj);
            } 
        }

        return obj;
    }
}
