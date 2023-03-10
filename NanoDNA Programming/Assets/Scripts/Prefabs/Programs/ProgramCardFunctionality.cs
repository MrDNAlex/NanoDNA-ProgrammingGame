using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;
using DNAStruct;
using DNASaveSystem;

public class ProgramCardFunctionality
{

    Flex Program;

    Scripts allScripts;

    Language lang;

    PlayerSettings playSettings;

    //Var Types
    UIWord Text = new UIWord("Text", "Texte");
    UIWord Number = new UIWord("Number", "Nombre");
    UIWord Decimal = new UIWord("Decimal", "Decimale");
    UIWord Bool = new UIWord("Boolean", "Booléen");

    UIWord Move = new UIWord("Move", "Bouge");

    public ProgramCardFunctionality()
    {
        allScripts = Camera.main.GetComponent<LevelScript>().allScripts;

        playSettings = SaveManager.loadPlaySettings();
        lang = playSettings.language;
    }

    //
    //Set UI
    //
    public Flex setUI(CardInfo info)
    {

        //Go to the function storing the flex for 
        switch (info.actionType)
        {
            case ActionType.Movement:
                return setUIMovement(info);
            case ActionType.Math:
                return setUIMovement(info);

            case ActionType.Logic:
                return setUIMovement(info);

            case ActionType.Variable:
                return setUIVariables(info);

            case ActionType.Action:
               // Debug.Log("Here");
                return setUIActions(info);

            default:
                Debug.Log("Here");
                return setUIMovement(info);
        }
    }

    public Flex setUIMovement(CardInfo info)
    {
        switch (info.movementName)
        {
            case MovementActionNames.Move:

                //Flex Variable Init
                Program = new Flex(info.rectTrans, 2);

                Flex Move = new Flex(Program.getChild(0), 1f);
                Flex Direction = new Flex(Program.getChild(1), 1);
                Flex Value = new Flex(Program.getChild(2), 1);

                //Set Flex Parameters

                Program.addChild(Direction);
                Program.addChild(Value);
                Program.addChild(Move);

                Program.setSpacingFlex(0.2f, 1);

                Program.setAllPadSame(0.2f, 1);
                break;
        }
        return Program;
    }

    public Flex setUIVariables(CardInfo info)
    {
        switch (info.variableName)
        {
            case VariableActionNames.Variable:

                //Flex Variable Init
                Program = new Flex(info.rectTrans, 2);

                Flex PublicAndType = new Flex(Program.getChild(0), 0.5f, Program);

                Debug.Log(Program.getChild(0).name);

                Flex Public = new Flex(PublicAndType.getChild(0), 1, PublicAndType);
                Flex VarType = new Flex(PublicAndType.getChild(1), 1, PublicAndType);

                Flex VarName = new Flex(Program.getChild(1), 1, Program);
                Flex VarSign = new Flex(Program.getChild(2), 0.5f, Program);
                Flex VarValue = new Flex(Program.getChild(3), 1, Program);

                PublicAndType.setSpacingFlex(0.05f, 1);

                Program.setSpacingFlex(0.5f, 1);

                Program.setAllPadSame(0.3f, 1);

                break;
        }
        return Program;
    }

    public Flex setUIActions(CardInfo info)
    {

        switch (info.actionName)
        {
            case ActionActionNames.Speak:

                Program = new Flex(info.rectTrans, 2);

                Flex SpeakType = new Flex(Program.getChild(0), 1);
                Flex SpeakMessage = new Flex(Program.getChild(1), 1);

                Program.addChild(SpeakType);
                Program.addChild(SpeakMessage);

                Program.setSpacingFlex(0.2f, 1);
                Program.setAllPadSame(0.2f, 1);

                break;
        }

        return Program;

    }


    //
    //Set Info
    //

    public void setInfo(CardInfo info)
    {

        switch (info.actionType)
        {
            case ActionType.Movement:

                setInfoMovement(info);
                break;
            case ActionType.Math:
                break;
            case ActionType.Logic:
                break;
            case ActionType.Variable:
                setInfoVariable(info);
                break;

            case ActionType.Action:
                setInfoAction(info);
                break;
            default:
                Debug.Log("Here");
                break;
        }
    }

    public void setInfoMovement(CardInfo info)
    {
        switch (info.movementName)
        {
            case MovementActionNames.Move:

                string path = "";
                switch (info.action.moveData.dir)
                {
                    case Direction.Up:
                        path = "Images/EditControllerAssets/up";
                        break;
                    case Direction.Left:
                        path = "Images/EditControllerAssets/left";
                        break;
                    case Direction.Right:
                        path = "Images/EditControllerAssets/right";
                        break;
                    case Direction.Down:
                        path = "Images/EditControllerAssets/down";
                        break;
                }

                Texture2D image = Resources.Load(path) as Texture2D;

                info.rectTrans.GetChild(1).GetComponent<Button>().image.sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

                if (info.action.moveData.refID != 0)
                {
                    UIHelper.setText(info.rectTrans.GetChild(2).GetChild(0), Camera.main.GetComponent<ProgramManager>().getVariableName(info.action.moveData.refID), playSettings.colourScheme.getBlackTextColor());
                }
                else
                {
                    UIHelper.setText(info.rectTrans.GetChild(2).GetChild(0), info.action.moveData.value, playSettings.colourScheme.getBlackTextColor());
                }

                UIHelper.setText(info.rectTrans.GetChild(0), Move, playSettings.colourScheme.getBlackTextColor());

                //
                //Implement reference ID
                //

                break;
        }
    }

    public void setInfoVariable(CardInfo info)
    {
        string path = "";
        switch (info.variableName)
        {
            case VariableActionNames.Variable:

                //Check if public or local
                if (info.action.varData.isPublic)
                {
                    //Public
                    // Debug.Log("Public Path");
                    path = "Images/EditControllerAssets/Public";

                }
                else
                {
                    //Local
                    // Debug.Log("Private Path");
                    path = "Images/EditControllerAssets/Local";
                }

                Texture2D image = Resources.Load(path) as Texture2D;

                //Public / Private
                info.transform.GetChild(0).GetChild(0).GetComponent<Button>().image.sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

                path = getVarTypeImage(info.action.varData.varType);

                image = Resources.Load(path) as Texture2D;

                info.transform.GetChild(0).GetChild(1).GetComponent<Button>().image.sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

                info.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "";

                UIHelper.setText(info.transform.GetChild(1).GetChild(0), info.action.varData.name, playSettings.colourScheme.getBlackTextColor());
                
                //Check if type is bool, set image in that cases
                if (info.action.varData.refID != 0)
                {

                    //Set the value to the name of the reference variable
                    UIHelper.setText(info.transform.GetChild(3).GetChild(0), Camera.main.GetComponent<ProgramManager>().getVariableName(info.action.varData), playSettings.colourScheme.getBlackTextColor());

                    path = "unity_builtin_extra/UISprite";

                    image = Resources.Load(path) as Texture2D;

                    info.transform.GetChild(3).GetComponent<Button>().image.sprite = null;

                }
                else
                {

                    if (info.action.varData.varType == VariableType.Bool)
                    {
                        if (info.action.varData.value == "true")
                        {
                            path = "Images/EditControllerAssets/True";
                        }
                        else
                        {
                            path = "Images/EditControllerAssets/False";
                        }

                        image = Resources.Load(path) as Texture2D;

                        info.transform.GetChild(3).GetComponent<Button>().image.sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

                        UIHelper.setText(info.transform.GetChild(3).GetChild(0), "", playSettings.colourScheme.getBlackTextColor());

                    }
                    else
                    {
                        path = "unity_builtin_extra/UISprite";

                        image = Resources.Load(path) as Texture2D;

                        info.transform.GetChild(3).GetComponent<Button>().image.sprite = null;

                        UIHelper.setText(info.transform.GetChild(3).GetChild(0), info.action.varData.value, playSettings.colourScheme.getBlackTextColor());
                    }
                }
                break;
        }

    }

    public void setInfoAction(CardInfo info)
    {
        switch (info.actionName)
        {
            case ActionActionNames.Speak:

                string path = "";

                switch (info.action.actData.descriptor)
                {
                    case ActionDescriptor.Talk:
                        path = "Images/EditControllerAssets/Talk";
                        break;
                    case ActionDescriptor.Whisper:
                        path = "Images/EditControllerAssets/Whisper";
                        break;
                    case ActionDescriptor.Yell:
                        path = "Images/EditControllerAssets/Yell";
                        break;
                }
                Texture2D image = Resources.Load(path) as Texture2D;

                info.transform.GetChild(0).GetComponent<Button>().image.sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

                if (info.action.actData.refID != 0)
                {
                    //Make it equal to reference
                    UIHelper.setText(info.transform.GetChild(1).GetChild(0), Camera.main.GetComponent<ProgramManager>().getVariableName(info.action.actData.refID), playSettings.colourScheme.getBlackTextColor());
                }
                else
                {
                    UIHelper.setText(info.transform.GetChild(1).GetChild(0), info.action.actData.data, playSettings.colourScheme.getBlackTextColor());
                }
                break;
        }
    }


    //
    //Set Action
    //

    public void setAction(CardInfo info)
    {
        switch (info.actionType)
        {
            case ActionType.Movement:

                setActionMovement(info);
                break;
            case ActionType.Math:
                break;
            case ActionType.Logic:
                break;
            case ActionType.Variable:
                setActionVariable(info);
                break;
            case ActionType.Action:
                setActionAction(info);
                break;
            default:
                Debug.Log("Here");
                break;
        }

    }

    public void setActionMovement(CardInfo info)
    {

        switch (info.movementName)
        {
            case MovementActionNames.Move:

                info.programCard.action = createAction(info);

                //Direction
                info.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
                {
                    
                    //Set Panel Type
                    info.editDataType = EditDataType.Multichoice;

                    //Set Editable Variable Type
                    info.varType = VariableType.Number;

                    //Set the Data type it will change
                    info.valEditType = ValueEditType.Direction;

                    GameObject panel = Camera.main.transform.GetChild(0).GetChild(2).gameObject;

                    panel.SetActive(true);

                    destroyChildren(panel);

                    // GameObject dir1 = Resources.Load<GameObject>("Prefabs/EditPanels/Direction.prefab") as GameObject;

                    GameObject dir = Resources.Load("Prefabs/EditPanels/MultiChoice") as GameObject;

                    GameObject direction = GameObject.Instantiate(dir, panel.transform);

                    direction.GetComponent<EditValController>().setPanel(info, panel.transform, this);


                });


                info.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate
                {
                    
                    //Set Panel Type
                    info.editDataType = EditDataType.Value;

                    //Set Editable Variable Type
                    info.varType = VariableType.Number;

                    //Set the Data type it will change
                    info.valEditType = ValueEditType.Value;

                    GameObject panel = Camera.main.transform.GetChild(0).GetChild(2).gameObject;

                    panel.SetActive(true);

                    destroyChildren(panel);

                    // GameObject dir1 = Resources.Load<GameObject>("Prefabs/EditPanels/Direction.prefab") as GameObject;

                    GameObject dir = Resources.Load("Prefabs/EditPanels/Value") as GameObject;

                    GameObject direction = GameObject.Instantiate(dir, panel.transform);

                    direction.GetComponent<EditValController>().setPanel(info, panel.transform, this);

                });

                break;
        }
    }

    public void setActionVariable(CardInfo info)
    {
        switch (info.variableName)
        {
            case VariableActionNames.Variable:

                info.programCard.action = createAction(info);

                //Public Local
                info.transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
                {
                    //Set Panel Type
                    info.editDataType = EditDataType.Multichoice;

                    //Set Editable Variable Type
                    info.varType = info.action.varData.varType;

                    //Set the Data type it will change
                    info.valEditType = ValueEditType.Public;

                    GameObject panel = Camera.main.transform.GetChild(0).GetChild(2).gameObject;

                    panel.SetActive(true);

                    destroyChildren(panel);

                    GameObject direction = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/MultiChoice") as GameObject, panel.transform);

                    direction.GetComponent<EditValController>().setPanel(info, panel.transform, this);
                });

                //Variable Type
                info.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
                {
                    //Set Panel Type
                    info.editDataType = EditDataType.Multichoice;

                    //Set Editable Variable Type
                    info.varType = info.action.varData.varType;

                    //Set the Data type it will change
                    info.valEditType = ValueEditType.VariableType;

                    GameObject panel = Camera.main.transform.GetChild(0).GetChild(2).gameObject;

                    panel.SetActive(true);

                    destroyChildren(panel);

                    GameObject direction = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/MultiChoice") as GameObject, panel.transform);

                    direction.GetComponent<EditValController>().setPanel(info, panel.transform, this);
                });

                //Name
                info.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
                {
                    //Set Panel Type
                    info.editDataType = EditDataType.NewValue;

                    //Set Editable Variable Type
                    info.varType = VariableType.Text;

                    //Set the Data type it will change
                    info.valEditType = ValueEditType.Name;

                    GameObject panel = Camera.main.transform.GetChild(0).GetChild(2).gameObject;

                    panel.SetActive(true);

                    destroyChildren(panel);

                    GameObject direction = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/NewValue") as GameObject, panel.transform);

                    direction.GetComponent<EditValController>().setPanel(info, panel.transform, this);


                });

                //Value
                info.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate
                {
                    //Set Editable Variable Type
                    info.varType = info.action.varData.varType;

                    //Set Panel Type
                    info.editDataType = EditDataType.Value;

                    if (info.varType == VariableType.Bool)
                    {
                        //Set the Data type it will change
                        info.valEditType = ValueEditType.Bool;
                    }
                    else
                    {
                        //Set the Data type it will change
                        info.valEditType = ValueEditType.Value;
                    }

                    GameObject panel = Camera.main.transform.GetChild(0).GetChild(2).gameObject;

                    panel.SetActive(true);

                    destroyChildren(panel);

                    string path = "";
                    path = "Prefabs/EditPanels/Value";

                    GameObject direction = GameObject.Instantiate(Resources.Load(path) as GameObject, panel.transform);

                    direction.GetComponent<EditValController>().setPanel(info, panel.transform, this);
                });

                break;
        }
    }

    public void setActionAction(CardInfo info)
    {

        switch (info.actionName)
        {
            case ActionActionNames.Speak:

                info.programCard.action = createAction(info);

                info.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
                {
                    //Yell, Talk, Whipser
                    //Set Panel Type
                    info.editDataType = EditDataType.Multichoice;

                    //Set Editable Variable Type
                    info.varType = VariableType.Number;

                    //Set the Data type it will change
                    info.valEditType = ValueEditType.Speak;

                    GameObject panel = Camera.main.transform.GetChild(0).GetChild(2).gameObject;

                    panel.SetActive(true);

                    destroyChildren(panel);

                    GameObject direction = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/MultiChoice") as GameObject, panel.transform);

                    direction.GetComponent<EditValController>().setPanel(info, panel.transform, this);
                });

                info.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
                {
                    //Message

                    //Set Editable Variable Type
                    info.varType = VariableType.Text;

                    //Set Panel Type
                    info.editDataType = EditDataType.Value;

                    //Set the Data type it will change
                    info.valEditType = ValueEditType.Value;

                    GameObject panel = Camera.main.transform.GetChild(0).GetChild(2).gameObject;

                    panel.SetActive(true);

                    destroyChildren(panel);

                    string path = "Prefabs/EditPanels/Value";

                    GameObject direction = GameObject.Instantiate(Resources.Load(path) as GameObject, panel.transform);

                    direction.GetComponent<EditValController>().setPanel(info, panel.transform, this);
                });
                break;
        }

    }

    //
    //Create Action
    //

    public ProgramAction createAction(CardInfo info)
    {

        switch (info.actionType)
        {
            case ActionType.Movement:
                return createMovementAction(info);
            case ActionType.Variable:
                return createVariableAction(info);
            case ActionType.Action:
                return createActionAction(info);

            default:
                Debug.Log("Here");
                return null;

        }

    }

    public ProgramAction createMovementAction(CardInfo info)
    {
        switch (info.movementName)
        {
            case MovementActionNames.Move:
                //This may go wrong
                return new ProgramAction(info, info.action.moveData);
            //return new ProgramAction(info, info.programCard.moveData);
            //Return new Program Action
            default:
                Debug.Log("Here");
                return null;
        }

    }

    public ProgramAction createVariableAction(CardInfo info)
    {
        switch (info.variableName)
        {
            case VariableActionNames.Variable:

                return new ProgramAction(info, info.action.varData);
            default:
                Debug.Log("Here");
                return null;
        }
    }

    public ProgramAction createActionAction(CardInfo info)
    {
        info.action.actData.character = allScripts.programSection.selectedCharacter.transform;
        switch (info.actionName)
        {
            case ActionActionNames.Speak:
                return new ProgramAction(info, info.action.actData);
            default:
                Debug.Log("Here");
                return null;
        }
    }


    //
    //Converters
    //

    public Direction getDirec(string dir)
    {
        switch (dir)
        {
            case "up":
                return Direction.Up;

            case "left":
                return Direction.Left;

            case "right":
                return Direction.Right;

            case "down":
                return Direction.Down;
            default:
                return Direction.Up;

        }
    }

    public void destroyChildren(GameObject Obj)
    {
        //Only deletes children under the one referenced
        foreach (Transform child in Obj.transform)
        {
            //Safe to delete
            GameObject.Destroy(child.gameObject);
        }
    }

    public string getVarType(VariableType varType)
    {
        switch (varType)
        {
            case VariableType.Text:
                return Text.getWord(lang);
            case VariableType.Number:
                return Number.getWord(lang);
            case VariableType.Decimal:
                return Decimal.getWord(lang);
            case VariableType.Bool:
                return Bool.getWord(lang);
            default:
                return Text.getWord(lang);

        }

    }

    public string getVarTypeImage(VariableType varType)
    {
        switch (varType)
        {
            case VariableType.Text:
                return "Images/EditControllerAssets/Text";
            case VariableType.Number:
                return "Images/EditControllerAssets/Number";
            case VariableType.Decimal:
                return "Images/EditControllerAssets/Decimal";
            case VariableType.Bool:
                return "Images/EditControllerAssets/Bool";
            default:
                return "Images/EditControllerAssets/Text";
        }
    }

   


}
