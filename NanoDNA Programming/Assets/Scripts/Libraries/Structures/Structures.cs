using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.Tilemaps;

namespace DNAStruct
{

    [System.Serializable]
    public struct SavedPlayerSettings
    {
        public Language language;
        public int volume;
        public ColourPaletteStorage colourScheme;
        public bool advancedVariables;

        public static SavedPlayerSettings CreateNewSettings ()
        {
            SavedPlayerSettings settings = new SavedPlayerSettings();

            settings.language = Language.English;
            settings.volume = 50;
            settings.advancedVariables = false;
            settings.colourScheme = new ColourPaletteStorage("Images/UIDesigns/Palettes/Palette 1");
            
            settings.colourScheme.textColorMain = Color.white;
            settings.colourScheme.textColorAccent = Color.black;

            return settings;
        }
    }

    [System.Serializable]
    public struct CurrentLevelLoader
    {
        public static string path;
        public static string name;
    }


    [System.Serializable]
    public struct LevelMakerInfo
    {
        public LevelIconInstance levelIcon;
        //From After Resource path
        public string levelPath;

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
        //public Math mathName;
        public LogicActionNames logicName;
        public VariableActionNames variableName;
        public ActionActionNames actionName;

       

        //Program Related Stuff
        public Flex flex;
        public RectTransform rectTrans;
        public Transform transform;
        public ProgramAction action;
        public ProgramCard programCard;

        //CharData Object reference
        public Transform charDataTrans;


        public GameObject panel;

        public EditDataType editDataType;

        public VariableType varType;

        public ValueEditType valEditType;

        public int progLineIndex;
    }

    //
    //Program Action stuff
    //
    public enum ActionType
    {
        Movement,
        Logic,
        Variable,
        Action
    }

    public enum MovementActionNames
    {
        None,
        Move,
    }

    public enum MathOperationTypes
    {
        None,
        Addition,
        Subtraction, 
        Multiplication,
        Division
    }

    public enum LogicActionNames
    {
        None,
    }

    public enum VariableActionNames
    {
        None,
        Variable,
        MathAddition,
        MathSubtraction,
        MathMultiplication,
        MathDivision,



    }

    public enum ActionActionNames
    {
        None,
        Speak
    }


    public enum EditDataType
    {
        Value,
        NewValue,
        Multichoice,
        Variable,
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
        VariableSmartAssign, 
        Value1, 
        Value2,
      //  MathOperation,
        LogicOperation,

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
        public static LevelScript levelScript;
        public static LevelManager levelManager;
        public static ProgramSection programSection;
        public static MapDrag mapDrag;
        public static StoreScript storeScript;
        public static ProgramManager programManager;

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

    public enum VariableActionType
    {
        Set, 
        ChangeVal,


    }

    [System.Serializable]
    public class VariableActionData
    {
        [System.Serializable]
        public struct VariableSetData
        {
            public bool isPublic;
            public VariableType varType;
            //public string name;
            public string value;
            public CharData charData;
            public int id;
            public int refID;

            

            public void setParent(CharData parentData)
            {
                this.charData = parentData;
            }

            public void setID(int id)
            {
                this.id = id;
            }

            public void setValue(string value)
            {

                this.value = value;

            }

            public void setRefID (int refID)
            {
                this.refID = refID;
            }

           
        }
        [System.Serializable]
        public struct VariableMathData
        {
            public string value1;
            public string value2;

            public int refID1;
            public int refID2;

            public VariableType varType;
            public MathOperationTypes operationType;


            public void setValue (string value, int index)
            {
                if (index == 0)
                {
                    this.value1 = value;
                } else
                {
                    this.value2 = value;
                }
            }

            public void setRefID (int refID, int index)
            {
                if (index == 0)
                {
                    this.refID1 = refID;
                }
                else
                {
                    this.refID2 = refID;
                }
            }
        }


        //Edit stuff

        public bool newVar;
        public string name;
        public int refID;
        public VariableActionType actionType;

        //Variable for the set Data
        public VariableSetData setData;
        //Variable for math data
        public VariableMathData mathData;

        public VariableActionData ()
        {

        }

        public VariableData getVarData()
        {
            VariableData data = new VariableData();

            data.isPublic = setData.isPublic;

            data.varType = setData.varType;
            data.name = name;
            data.value = setData.value;
            data.id = setData.id;
            data.refID = setData.refID;
            data.varType = setData.varType;
            data.charData = setData.charData;

            return data;
        }

        public void setVarData(bool pub, VariableType varType, string name, string value)
        {
            this.setData.isPublic = pub;
            this.setData.varType = varType;
            this.name = name;
            this.setData.value = value;

        }

    }
    
    //
    //Variable Data
    //
    
    [System.Serializable]
    public class VariableData
    {
        public bool isPublic;
        public bool isLevelVariable;
        //Determines if the true value can be read/added to all variables
        public bool isActivated;
        public bool hideVal;
        public VariableType varType;
        public string name;
        public string value;
       // public GameObject character;
        public CharData charData;
        public int id;
        public int refID;


        //public MathTypes mathType;
        

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

        public void setParent(CharData parentData)
        {
            this.charData = parentData;
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

    [System.Serializable]
    public class MathOperationData
    {
        public string value1;
        public int refID1;

        public string value2;
        public int refID2;

        public MathOperationTypes type;
        public VariableType varType;
    }

    //Settings Data

    public enum SettingEditType
    {
        MultiChoice,

    }

    public enum SettingValueType
    {
        Language,
        ColourScheme,
        AdvancedVariables
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
        public LogicActionNames logicName;
        public VariableActionNames variableName;
        public ActionActionNames actionName;

    }

    [System.Serializable]
    public enum InfoPanelType
    {
        Quit, 
        Complete,
        InfoTips,
        CollectibleDescription,
        LinesUsed,


    }

    [System.Serializable]
    public struct InfoPanelData
    {

    }

    [System.Serializable]
    public struct SensorSignal
    {
        //We don't need to have is activated, we can just send the signal, and it can read the id
        //public bool isActivated;
        public string value;
        public int id;
        public VariableType varType;
    }






}