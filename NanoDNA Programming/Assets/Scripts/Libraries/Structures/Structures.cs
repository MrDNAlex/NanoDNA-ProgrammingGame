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

        public EditDataType editDataType;

        //Program Related Stuff
        public Flex flex;
        public RectTransform rectTrans;
        public Transform transform;
        public ProgramAction action;
        public ProgramCard programCard;

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
        Variable
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

    }

    public enum VariableType
    {
        Text,
        Number,
        Decimal,
        Bool,
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
    //Action Data structs
    //
    [System.Serializable]
    public struct MoveData
    {
        public Direction dir;
        public int value;
    }
    [System.Serializable]
    public struct VariableData
    {
        public bool isPublic;
        public VariableType varType;
        public string name;
        public string value;

        public void setData(bool pub, VariableType varType, string name, string value)
        {
            this.isPublic = pub;
            this.varType = varType;
            this.name = name;
            this.value = value;
        }
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

    }








}