using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.Tilemaps;

namespace DNAStruct
{
    [System.Serializable]
    public struct LevelMakerInfo
    {
        public Tilemap voidMap;
        public Tilemap backgroundMap;
        public Tilemap obstacleMap;
        public Tilemap decorationMap;
        public GameObject charHolder;

        public int maxLines;

    }


    public struct CardInfo
    {
        public ActionType actionType;
        public string cardType; //eventually replace this with the storeTag I think
        public UIWord cardName;


        public MovementActionNames movementName;
        public MathActionNames mathName;
        public LogicActionNames logicName;
        public VariableActionNames variableName;
        public ActionActionNames actionName;

        public EditDataType editDataType;

        //Program Related Stuff
        public Flex flex;
        public RectTransform rectTrans;
        public Transform transform;
        public ProgramAction action;
        public ProgramCard programCard;


        //CharData Object reference
        public Transform charDataTrans;


        public GameObject panel;

        public VariableType varType;

        public ValueEditType valEditType;

    }

    //
    //Program Action stuff
    //
    public enum ActionType
    {
        Movement,
        Math,
        Logic,
        Variable,
        Action
    }

    public enum MovementActionNames
    {
        None,
        Move,

    }

    public enum MathActionNames
    {
        None,
    }

    public enum LogicActionNames
    {
        None,
    }

    public enum VariableActionNames
    {
        None,
        Variable,

    }

    public enum ActionActionNames
    {
        None,
        Speak
    }


    public enum EditDataType
    {
        Direction,
        Value,
        NewValue,
        Multichoice,

    }

    public enum ValueEditType
    {
        Name,
        Value,
        Direction,
        VariableType,
        Public,
        Bool,
        Speak,

    }

    public enum VariableType
    {
        Text,
        Number,
        Decimal,
        Bool,
    }

    public enum ActionDescriptor
    {
        Yell,
        Talk,
        Whisper,

    }


    [System.Serializable]
    public enum Direction
    {
        Up,
        Left,
        Right,
        Down
    }



    //
    //Script Storage
    //

    public struct Scripts
    {
        public LevelScript levelScript;
        public LevelManager levelManager;
        public ProgramSection programSection;
        public MapDrag mapDrag;
        public StoreScript storeScript;
        public ProgramManager programManager;

    }


    //
    //Level Type
    //
    [System.Serializable]
    public enum LevelType
    {
        TopDown,
        SideView,

    }




    public enum Language
    {
        English,
        French
    }




    //
    //Word Storage
    //

    [System.Serializable]
    public class UIWord
    {
        public string french;
        public string english;

        public UIWord(string eng, string fren)
        {
            this.english = eng;
            this.french = fren;
        }


        public void setWords(string eng, string fren)
        {
            this.french = fren;
            this.english = eng;
        }

        public string getWord(Language lang)
        {
            switch (lang)
            {
                case Language.English:
                    return english;
                case Language.French:
                    return french;
                default:
                    return english;
            }

        }


    }

    [System.Serializable]
    public class PlayLevelWords
    {
        public UIWord name = new UIWord("Name", "Nom");
        public UIWord save = new UIWord("Save", "Sauve");
        public UIWord resize = new UIWord("Resize", "Redimensionner");
        public UIWord used = new UIWord("Used", "Utilisé");
        public UIWord debug = new UIWord("Debug", "Déboguer");
        public UIWord reset = new UIWord("Reset", "Réinitialiser");
        public UIWord collected = new UIWord("Collected", "Collecté");
        public UIWord complete = new UIWord("Complete", "Complété");     // Maybe replace this with something like finish
        public UIWord changeLang = new UIWord("Change Language", "Change Langue");


        public UIWord movement = new UIWord("Movement", "Mouvement");
        public UIWord math = new UIWord("Math", "Mathématique");
        public UIWord logic = new UIWord("Logic", "Logique");
        public UIWord variable = new UIWord("Variable", "Variable");
        public UIWord action = new UIWord("Action", "Action");

        public string getStoreTitle(ActionType type, Language lang)
        {
            switch (type)
            {
                case ActionType.Movement:
                    return movement.getWord(lang);
                case ActionType.Math:
                    return math.getWord(lang);
                case ActionType.Logic:
                    return logic.getWord(lang);
                case ActionType.Variable:
                    return variable.getWord(lang);
                case ActionType.Action:
                    return action.getWord(lang);
                default:
                    return movement.getWord(lang);
            }
        }

    }

    //
    //Action Data structs/classes
    //
    [System.Serializable]
    public class MoveData
    {
        public Direction dir;
        public string value;
        public int refID;
    }

    //
    //Variable Data
    //
    [System.Serializable]
    public class VariableData
    {
        public bool isPublic;
        public VariableType varType;
        public string name;
        public string value;
        public GameObject character;
        public int id;
        public int refID;

        public VariableData(bool pub, VariableType varType, string name, string value)
        {
            this.isPublic = pub;
            this.varType = varType;
            this.name = name;
            this.value = value;
        }

        public VariableData()
        {

        }

        public void setData(bool pub, VariableType varType, string name, string value)
        {
            this.isPublic = pub;
            this.varType = varType;
            this.name = name;
            this.value = value;

        }

        public void setParent(GameObject parent)
        {
            this.character = parent;
        }

        public void setID(int id)
        {
            this.id = id;
        }

        public void setValue(string value)
        {

            this.value = value;

        }
    }

    [System.Serializable]
    public class ActionData
    {
        public ActionActionNames actionName;
        public ActionDescriptor descriptor;
        public string data;

        public Transform character;

        public int refID;

        public void setData(string data)
        {
            this.data = data;
        }

    }





    //Settings Data

    public enum SettingEditType
    {
        MultiChoice,

    }

    public enum SettingValueType
    {
        Language,
        ColourScheme
    }

    public enum SettingColourScheme
    {
        Col1,
        Col2,
        Col3,
        Col4,
        Col5,
        Col6,
        Col7,
        Col8,

    }

    public enum SettingCardType
    {
        Button,
        Slider,


    }

    [System.Serializable]
    public struct StoreCardInfo
    {
        public ActionType actionType;

        public MovementActionNames movementName;
        public MathActionNames mathName;
        public LogicActionNames logicName;
        public VariableActionNames variableName;
        public ActionActionNames actionName;

    }








}