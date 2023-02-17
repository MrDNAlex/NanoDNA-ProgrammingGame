using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;
using DNAStruct;

public class ProgramCardFunctionality
{

    Flex Program;

    Scripts allScripts;

    Language lang;

    //Var Types
    UIWord Text = new UIWord("Text", "Texte");
    UIWord Number = new UIWord("Number", "Nombre");
    UIWord Decimal = new UIWord("Decimal", "Decimale");
    UIWord Bool = new UIWord("Boolean", "Booléen");

    public ProgramCardFunctionality()
    {
        allScripts = Camera.main.GetComponent<LevelScript>().allScripts;
        lang = allScripts.levelScript.lang;
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
                Program.addChild(Move);
                Program.addChild(Direction);
                Program.addChild(Value);

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

                Flex Public = new Flex(Program.getChild(0), 1);
                Flex VarType = new Flex(Program.getChild(1), 1);
                Flex VarName = new Flex(Program.getChild(2), 1);
                Flex VarSign = new Flex(Program.getChild(3), 0.5f);
                Flex VarValue = new Flex(Program.getChild(4), 1);

                //Set Flex Parameters
                Program.addChild(Public);
                Program.addChild(VarType);
                Program.addChild(VarName);
                Program.addChild(VarSign);
                Program.addChild(VarValue);

                Program.setSpacingFlex(0.5f, 1);

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

                info.rectTrans.GetChild(2).GetChild(0).GetComponent<Text>().text = "" + info.action.moveData.value;
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
                    path = "Images/EditControllerAssets/Public";

                }
                else
                {
                    //Local
                    path = "Images/EditControllerAssets/Local";
                }

                Texture2D image = Resources.Load(path) as Texture2D;

                info.transform.GetChild(0).GetComponent<Button>().image.sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

                path = getVarTypeImage(info.action.varData.varType);

                image = Resources.Load(path) as Texture2D;

                info.transform.GetChild(1).GetComponent<Button>().image.sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

                info.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "";

                info.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = info.action.varData.name;


                //Check if type is bool, set image in that cases

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

                    info.transform.GetChild(4).GetComponent<Button>().image.sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

                    info.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "";

                }
                else
                {
                    info.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = info.action.varData.value;

                    path = "unity_builtin_extra/UISprite";

                    image = Resources.Load(path) as Texture2D;

                    info.transform.GetChild(4).GetComponent<Button>().image.sprite = null;

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
                    info.editDataType = EditDataType.Direction;

                    //Set Editable Variable Type
                    info.varType = VariableType.Number;

                    //Set the Data type it will change
                    info.valEditType = ValueEditType.Direction;

                    GameObject panel = Camera.main.transform.GetChild(0).GetChild(2).gameObject;

                    panel.SetActive(true);

                    destroyChildren(panel);

                    // GameObject dir1 = Resources.Load<GameObject>("Prefabs/EditPanels/Direction.prefab") as GameObject;

                    GameObject dir = Resources.Load("Prefabs/EditPanels/Direction") as GameObject;

                    GameObject direction = GameObject.Instantiate(dir, panel.transform);

                    direction.GetComponent<EditValController>().setPanel(info, panel.transform, this, info.transform.GetChild(1));


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

                    direction.GetComponent<EditValController>().setPanel(info, panel.transform, this, info.transform.GetChild(2));

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
                info.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
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

                    direction.GetComponent<EditValController>().setPanel(info, panel.transform, this, info.transform.GetChild(2));
                });

                //Variable Type
                info.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
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

                    direction.GetComponent<EditValController>().setPanel(info, panel.transform, this, info.transform.GetChild(2));
                });

                //Name
                info.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate
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

                    direction.GetComponent<EditValController>().setPanel(info, panel.transform, this, info.transform.GetChild(2));


                });

                //Value
                info.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(delegate
                {
                    //Set Editable Variable Type
                    info.varType = info.action.varData.varType;

                    if (info.varType == VariableType.Bool)
                    {
                        //Set Panel Type
                        info.editDataType = EditDataType.Multichoice;
                    }
                    else
                    {
                        //Set Panel Type
                        info.editDataType = EditDataType.Value;
                    }

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
                    if (info.varType == VariableType.Bool)
                    {
                        path = "Prefabs/EditPanels/MultiChoice";
                    }
                    else
                    {
                        path = "Prefabs/EditPanels/Value";
                    }
                    GameObject direction = GameObject.Instantiate(Resources.Load(path) as GameObject, panel.transform);

                    direction.GetComponent<EditValController>().setPanel(info, panel.transform, this, info.transform.GetChild(2));
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
