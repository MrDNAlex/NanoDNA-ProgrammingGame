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

    // Scripts allScripts;

    Language lang;

    //Var Types
    UIWord Text = new UIWord("Text", "Texte");
    UIWord Number = new UIWord("Number", "Nombre");
    UIWord Decimal = new UIWord("Decimal", "Decimale");
    UIWord Bool = new UIWord("Boolean", "Booléen");

    UIWord Move = new UIWord("Move", "Bouge");

    public ProgramCardFunctionality()
    {
        //allScripts = Camera.main.GetComponent<LevelScript>().allScripts;

        // playSettings = SaveManager.loadPlaySettings();
        lang = PlayerSettings.language;
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

   // public Flex setUIMath (CardInfo info)
   // {

        //Maybe remove this and make it so that you click a button, it spawns a EditValuePanel, and have it multichoice, pick the operation. This would be a lot easier

        //Because the type of math operations are

        //Row 1
        //Addition
        //subtraction
        //Multiplication
        //Division

        //Row 2
        //Round
        //Modulus 

        //Row 3?
        //One special command where you can pick even more unique function (sin, cos, tan etc)


       
   // }

    public Flex setUIVariables(CardInfo info)
    {
        Flex VarName;
        Flex VarSign;
        switch (info.variableName)
        {

            case VariableActionNames.Variable:

                //Flex Variable Init
                Program = new Flex(info.rectTrans, 2);

                if (PlayerSettings.advancedVariables)
                {
                    Flex PublicAndType = new Flex(Program.getChild(0), 0.5f, Program);

                    Flex Public = new Flex(PublicAndType.getChild(0), 1, PublicAndType);
                    Flex VarType = new Flex(PublicAndType.getChild(1), 1, PublicAndType);

                    VarName = new Flex(Program.getChild(1), 1, Program);
                    VarSign = new Flex(Program.getChild(2), 0.5f, Program);
                    Flex VarValue = new Flex(Program.getChild(3), 1, Program);

                    PublicAndType.setSpacingFlex(0.05f, 1);
                }
                else
                {
                    VarName = new Flex(Program.getChild(0), 1, Program);
                    VarSign = new Flex(Program.getChild(1), 0.5f, Program);
                    Flex VarValue = new Flex(Program.getChild(2), 1, Program);
                }

                Program.setSpacingFlex(0.5f, 1);

                Program.setAllPadSame(0.3f, 1);

                break;
            case VariableActionNames.MathVariable:

                //Flex Variable Init
                Program = new Flex(info.rectTrans, 2);

                VarName = new Flex(Program.getChild(0), 1f, Program);
                VarSign = new Flex(Program.getChild(1), 0.5f, Program);

                //Section that will hold it's own Math Program
                Flex MathHolder = new Flex(Program.getChild(2), 2, Program);

                Program.setSpacingFlex(0.3f, 1);

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
          //  case ActionType.Math:
            //    break;
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
                    UIHelper.setText(info.rectTrans.GetChild(2).GetChild(0), Camera.main.GetComponent<ProgramManager>().getVariableName(info.action.moveData.refID), PlayerSettings.colourScheme.getBlackTextColor());
                }
                else
                {
                    UIHelper.setText(info.rectTrans.GetChild(2).GetChild(0), info.action.moveData.value, PlayerSettings.colourScheme.getBlackTextColor());
                }

                UIHelper.setText(info.rectTrans.GetChild(0), Move, PlayerSettings.colourScheme.getBlackTextColor());

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

                if (PlayerSettings.advancedVariables)
                {
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

                    //Variable Type
                    path = getVarTypeImage(info.action.varData.varType);

                    image = Resources.Load(path) as Texture2D;

                    info.transform.GetChild(0).GetChild(1).GetComponent<Button>().image.sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

                    UIHelper.setText(info.transform.GetChild(0).GetChild(1).GetChild(0), "", PlayerSettings.colourScheme.getBlackTextColor());

                    UIHelper.setText(info.transform.GetChild(1).GetChild(0), info.action.varData.name, PlayerSettings.colourScheme.getBlackTextColor());

                    //Check if type is bool, set image in that cases
                    if (info.action.varData.refID != 0)
                    {

                        //Set the value to the name of the reference variable
                        UIHelper.setText(info.transform.GetChild(3).GetChild(0), Camera.main.GetComponent<ProgramManager>().getVariableName(info.action.varData), PlayerSettings.colourScheme.getBlackTextColor());

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

                            UIHelper.setText(info.transform.GetChild(3).GetChild(0), "", PlayerSettings.colourScheme.getBlackTextColor());

                        }
                        else
                        {
                            path = "unity_builtin_extra/UISprite";

                            image = Resources.Load(path) as Texture2D;

                            info.transform.GetChild(3).GetComponent<Button>().image.sprite = null;

                            UIHelper.setText(info.transform.GetChild(3).GetChild(0), info.action.varData.value, PlayerSettings.colourScheme.getBlackTextColor());
                        }
                    }
                }
                else
                {

                    //Simple Variable
                    Texture2D image;

                    UIHelper.setText(info.transform.GetChild(0).GetChild(0), info.action.varData.name, PlayerSettings.colourScheme.getBlackTextColor());

                    //Check if type is bool, set image in that cases
                    if (info.action.varData.refID != 0)
                    {

                        //Set the value to the name of the reference variable
                        UIHelper.setText(info.transform.GetChild(2).GetChild(0), Scripts.programManager.getVariableName(info.action.varData), PlayerSettings.colourScheme.getBlackTextColor());

                        path = "unity_builtin_extra/UISprite";

                        image = Resources.Load(path) as Texture2D;

                        info.transform.GetChild(2).GetComponent<Button>().image.sprite = null;

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

                            info.transform.GetChild(2).GetComponent<Button>().image.sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

                            UIHelper.setText(info.transform.GetChild(2).GetChild(0), "", PlayerSettings.colourScheme.getBlackTextColor());

                        }
                        else
                        {
                            path = "unity_builtin_extra/UISprite";

                            image = Resources.Load(path) as Texture2D;

                            info.transform.GetChild(2).GetComponent<Button>().image.sprite = null;

                            UIHelper.setText(info.transform.GetChild(2).GetChild(0), info.action.varData.value, PlayerSettings.colourScheme.getBlackTextColor());
                        }
                    }
                }
                break;
            case VariableActionNames.MathVariable:

                //Make sure it can only get a reference

                //Set the box collider
                info.transform.GetChild(2).gameObject.AddComponent<BoxCollider2D>();

                info.transform.GetChild(2).GetComponent<BoxCollider2D>().size = info.transform.GetChild(2).GetComponent<FlexInfo>().flex.size;


                if (info.action.varData.refID != 0)
                {
                    UIHelper.setText(info.transform.GetChild(0).GetChild(0), Scripts.programManager.getVariableName(info.action.varData), PlayerSettings.colourScheme.getBlackTextColor());
                } 
                else
                {
                    //Get the first variable that can possible be grabbed and set the 
                    List<VariableData> varData = Scripts.programManager.getVariables(VariableType.Number);

                    info.action.varData = varData[0];

                    info.action.varData.refID = varData[0].id;

                    UIHelper.setText(info.transform.GetChild(0).GetChild(0), Scripts.programManager.getVariableName(info.action.varData), PlayerSettings.colourScheme.getBlackTextColor());
                }

                //Spawn the math block

                switch (info.action.varData.mathType)
                {
                    case MathTypes.None:

                        //Delete if there are 

                        break;
                    case MathTypes.Addition:

                        GameObject math = GameObject.Instantiate(Resources.Load("Prefabs/MathOperations/Addition") as GameObject, Program.getChild(2));

                        Flex flex = math.GetComponent<ProgramCard>().program;



                        break;
                }




                //Check the math operation/data variable, set the type

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
                    UIHelper.setText(info.transform.GetChild(1).GetChild(0), Camera.main.GetComponent<ProgramManager>().getVariableName(info.action.actData.refID), PlayerSettings.colourScheme.getBlackTextColor());
                }
                else
                {
                    UIHelper.setText(info.transform.GetChild(1).GetChild(0), info.action.actData.data, PlayerSettings.colourScheme.getBlackTextColor());
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
          //  case ActionType.Math:
           //     break;
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
                    if (noPanelOpen())
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

                     //   direction.GetComponent<EditValController>().setPanel(info, panel.transform, this);
                    }
                });

                info.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate
                {
                    if (noPanelOpen())
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

                       // direction.GetComponent<EditValController>().setPanel(info, panel.transform, this);
                    }
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
                int offset = 0;

                if (PlayerSettings.advancedVariables)
                {
                    offset = 1;

                    //Public Local
                    info.transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
                    {
                        if (noPanelOpen())
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

                          //  direction.GetComponent<EditValController>().setPanel(info, panel.transform, this);
                        }
                    });

                    //Variable Type
                    info.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
                    {
                        if (noPanelOpen())
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

                            //direction.GetComponent<EditValController>().setPanel(info, panel.transform, this);
                        }
                    });

                }
                else
                {
                    offset = 0;
                }

                //Name
                info.transform.GetChild(0 + offset).GetComponent<Button>().onClick.AddListener(delegate
                {
                    if (noPanelOpen())
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

                      //  direction.GetComponent<EditValController>().setPanel(info, panel.transform, this);
                    }
                });

                //Value
                info.transform.GetChild(2 + offset).GetComponent<Button>().onClick.AddListener(delegate
                {
                    if (noPanelOpen())
                    {
                        //Set Editable Variable Type
                        info.varType = info.action.varData.varType;

                        //Set Panel Type
                        info.editDataType = EditDataType.Value;

                        if (PlayerSettings.advancedVariables)
                        {
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
                        }
                        else
                        {
                            info.valEditType = ValueEditType.VariableSmartAssign;
                        }

                        GameObject panel = Camera.main.transform.GetChild(0).GetChild(2).gameObject;

                        panel.SetActive(true);

                        destroyChildren(panel);

                        string path = "";
                        path = "Prefabs/EditPanels/Value";

                        GameObject direction = GameObject.Instantiate(Resources.Load(path) as GameObject, panel.transform);

                    //    direction.GetComponent<EditValController>().setPanel(info, panel.transform, this);
                    }
                });

                break;

            case VariableActionNames.MathVariable:

                info.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
                {
                    if (noPanelOpen())
                    {
                        //Set Panel Type
                        info.editDataType = EditDataType.Variable;

                        //Set Editable Variable Type
                        info.varType = VariableType.Text;

                        //Set the Data type it will change
                        info.valEditType = ValueEditType.Name;

                        //Get the panel
                        GameObject panel = Camera.main.transform.GetChild(0).GetChild(2).gameObject;

                        panel.SetActive(true);

                        destroyChildren(panel);

                        GameObject direction = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/VariableList") as GameObject, panel.transform);

                     //   direction.GetComponent<EditValController>().setPanel(info, panel.transform, this);
                    }

                });

                info.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate
                {
                    if (noPanelOpen())
                    {
                        //Set Panel Type
                        info.editDataType = EditDataType.Multichoice;

                        //Set Editable Variable Type
                        info.varType = VariableType.Number;

                        //Set the Data type it will change
                        info.valEditType = ValueEditType.MathOperation;

                        //Get the panel
                        GameObject panel = Camera.main.transform.GetChild(0).GetChild(2).gameObject;

                        panel.SetActive(true);

                        destroyChildren(panel);

                        GameObject direction = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/MultiChoice") as GameObject, panel.transform);

                      //  direction.GetComponent<EditValController>().setPanel(info, panel.transform, this);
                    }
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
                    if (noPanelOpen())
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

                       // direction.GetComponent<EditValController>().setPanel(info, panel.transform, this);
                    }
                });

                info.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
                {
                    if (noPanelOpen())
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

                       // direction.GetComponent<EditValController>().setPanel(info, panel.transform, this);
                    }
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
                Debug.Log(info.actionType);
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
            case VariableActionNames.MathVariable:
                return new ProgramAction(info, info.action.varData);
            default:
                return null;
        }
    }

    public ProgramAction createActionAction(CardInfo info)
    {
        info.action.actData.character = Scripts.programSection.selectedCharacter.transform;
        switch (info.actionName)
        {
            case ActionActionNames.Speak:
                return new ProgramAction(info, info.action.actData);
            default:
               
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

    bool noPanelOpen ()
    {
        if (Camera.main.transform.GetChild(0).GetChild(2).childCount == 0)
        {
            return true;
        } else
        {
            return false;
        }
    }




}
