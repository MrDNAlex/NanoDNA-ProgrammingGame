using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;
using FlexUI;
using UnityEngine.UI;

public class EditValController : MonoBehaviour
{

    ProgramCardFunctionality func;

    CardInfo info;


    Flex Parent;
    Flex Holder;
    Flex ExitRow;
    Flex Empty;
    Flex Exit;

    Transform ParentTrans;

    Scripts allScripts;

    Transform editSelection;

    //Variable var;

    VariableData varData;

    Language lang;

    VariableType varType;
    string value;
    bool isPub;


    //
    //
    //
    //Variable
    UIWord customTab = new UIWord("New", "Nouveau");
    UIWord premadeTab = new UIWord("Established", "Établi");
    UIWord variable = new UIWord("Variables", "Variables");
    UIWord newVariable = new UIWord("New Variable", "Nouvelle Variable");
    UIWord setbtn = new UIWord("Set", "Définer");
    UIWord error = new UIWord("Error : The variable inputted is not of the type ", "Erreur : La variable n'est pas du type ");

    //Var Types
    UIWord Text = new UIWord("Text", "Texte");
    UIWord Number = new UIWord("Number", "Nombre");
    UIWord Decimal = new UIWord("Decimal", "Decimale");
    UIWord Bool = new UIWord("Boolean", "Booléen");

    UIWord Public = new UIWord("Public", "Publique");
    UIWord Private = new UIWord("Private", "Privée");

    UIWord True = new UIWord("True", "Vrai");
    UIWord False = new UIWord("False", "Faux");

    List<UIWord> VariableTypes = new List<UIWord>();


    public void setPanel(CardInfo info, Transform parent, ProgramCardFunctionality func, Transform comp)
    {
        VariableTypes.Add(Text);
        VariableTypes.Add(Number);
        VariableTypes.Add(Decimal);
        VariableTypes.Add(Bool);

        this.info = info;
        this.func = func;

        allScripts = Camera.main.GetComponent<LevelScript>().allScripts;

        lang = allScripts.levelScript.lang;

        editSelection = comp;

        ParentTrans = parent;

        varType = info.varType;

        varData = info.action.varData;

        setUI();

        setControls(info);

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setUI()
    {

        Parent = new Flex(ParentTrans.GetComponent<RectTransform>(), 1);


        switch (info.editDataType)
        {
            case EditDataType.Direction:
                Holder = new Flex(this.GetComponent<RectTransform>(), 1);

                ExitRow = new Flex(Holder.getChild(0), 1);

                Empty = new Flex(ExitRow.getChild(0), 9);
                Exit = new Flex(ExitRow.getChild(1), 1);

                Flex Row1 = new Flex(Holder.getChild(1), 3);

                Flex R1Empty1 = new Flex(Row1.getChild(0), 1);
                Flex Up = new Flex(Row1.getChild(1), 1);
                Flex R1Empty2 = new Flex(Row1.getChild(2), 1);

                Flex Row2 = new Flex(Holder.getChild(2), 3);

                Flex Left = new Flex(Row2.getChild(0), 1);
                Flex R2Empty1 = new Flex(Row2.getChild(1), 1);
                Flex Right = new Flex(Row2.getChild(2), 1);


                Flex Row3 = new Flex(Holder.getChild(3), 3);

                Flex R3Empty1 = new Flex(Row3.getChild(0), 1);
                Flex Down = new Flex(Row3.getChild(1), 1);
                Flex R3Empty2 = new Flex(Row3.getChild(2), 1);

                //Add Children
                Parent.addChild(Holder);

                Holder.addChild(ExitRow);
                Holder.addChild(Row1);
                Holder.addChild(Row2);
                Holder.addChild(Row3);

                ExitRow.addChild(Empty);
                ExitRow.addChild(Exit);

                Row1.addChild(R1Empty1);
                Row1.addChild(Up);
                Row1.addChild(R1Empty2);

                Row2.addChild(Left);
                Row2.addChild(R2Empty1);
                Row2.addChild(Right);

                Row3.addChild(R3Empty1);
                Row3.addChild(Down);
                Row3.addChild(R3Empty2);

                //Holder.setAllPadSame(0.1f, 1);

                Holder.setHorizontalPadding(0.05f, 1, 0.05f, 1);
                Holder.setVerticalPadding(0.5f, 1, 0.5f, 1);

                Holder.setSpacingFlex(1f, 1);

                Row1.setSpacingFlex(0.3f, 1);
                Row2.setSpacingFlex(0.3f, 1);
                Row3.setSpacingFlex(0.3f, 1);

                Parent.setSize(new Vector2(1000, 700));

                break;
            case EditDataType.Value:

                setUIValue();


                break;

            case EditDataType.NewValue:

                Holder = new Flex(this.GetComponent<RectTransform>(), 1);

                ExitRow = new Flex(Holder.getChild(0), 0.7f);

                Empty = new Flex(ExitRow.getChild(0), 9);
                Exit = new Flex(ExitRow.getChild(1), 1);

                //Don't add these as children?
                Flex MakeVariable = new Flex(Holder.getChild(1), 3);

                //New Variable
                Flex NewVar = new Flex(MakeVariable.getChild(0), 1);

                Flex NewVarTitle = new Flex(NewVar.getChild(0), 1);
                Flex NewVarInput = new Flex(NewVar.getChild(1), 1);

                Flex ViewSet = new Flex(Holder.getChild(2), 2);

                Flex VariableExample = new Flex(ViewSet.getChild(0), 1.5f);

                Flex Type = new Flex(VariableExample.getChild(0), 1);
                Flex VarName = new Flex(VariableExample.getChild(1), 1);
                Flex Value = new Flex(VariableExample.getChild(2), 1);

                Flex SetBTN = new Flex(ViewSet.getChild(1), 1);

                Flex ErrorMSG = new Flex(ViewSet.getChild(2), 0.6f);


                Parent.addChild(Holder);

                Holder.addChild(ExitRow);

                Holder.addChild(MakeVariable);
                Holder.addChild(ViewSet);

                ExitRow.addChild(Empty);
                ExitRow.addChild(Exit);

                MakeVariable.addChild(NewVar);

                NewVar.addChild(NewVarTitle);
                NewVar.addChild(NewVarInput);


                ViewSet.addChild(VariableExample);
                ViewSet.addChild(SetBTN);
                ViewSet.addChild(ErrorMSG);

                VariableExample.addChild(Type);
                VariableExample.addChild(VarName);
                VariableExample.addChild(Value);

                Holder.setSpacingFlex(1f, 1);

                Holder.setHorizontalPadding(0.05f, 1, 0.05f, 1);

                Holder.setVerticalPadding(0.5f, 1, 0.5f, 1);

                NewVar.setAllPadSame(0.05f, 1);

                VariableExample.setSpacingFlex(1, 1);

                ViewSet.setHorizontalPadding(0.2f, 1, 0.2f, 1);

                Parent.setSize(new Vector2(1000, 700));

                break;

            case EditDataType.Multichoice:

                Holder = new Flex(this.GetComponent<RectTransform>(), 1);

                ExitRow = new Flex(Holder.getChild(0), 0.7f);

                Empty = new Flex(ExitRow.getChild(0), 9);
                Exit = new Flex(ExitRow.getChild(1), 1);

                Flex GridView = new Flex(Holder.getChild(1), 5);

                Flex GridRow1;
                Flex GridRow2;
                //Flex GridRow3;

                if (info.valEditType == ValueEditType.Public || info.valEditType == ValueEditType.Bool)
                {
                    GridRow1 = new Flex(GridView.getChild(0), 1);

                    GridView.addChild(GridRow1);

                    GridRow1.setSpacingFlex(0.2f, 1);
                } else
                {
                    GridRow1 = new Flex(GridView.getChild(0), 1);
                    GridRow2 = new Flex(GridView.getChild(1), 1);
                   // GridRow3 = new Flex(GridView.getChild(2), 1);

                    GridView.addChild(GridRow1);
                    GridView.addChild(GridRow2);
                   // GridView.addChild(GridRow3);

                    GridRow1.setSpacingFlex(0.2f, 1);
                    GridRow2.setSpacingFlex(0.2f, 1);
                   // GridRow3.setSpacingFlex(0.2f, 1);
                }

                Parent.addChild(Holder);

                Holder.addChild(ExitRow);
                Holder.addChild(GridView);
              
                ExitRow.addChild(Empty);
                ExitRow.addChild(Exit);

                Holder.setSpacingFlex(0.3f, 1);

                Holder.setHorizontalPadding(0.05f, 1, 0.05f, 1);

                Holder.setVerticalPadding(0.5f, 1, 0.5f, 1);

                GridView.setAllPadSame(0.05f, 1);

                GridView.setSpacingFlex(0.2f, 1);

                //Set gridview
                setGridView(GridView);

                Parent.setSize(new Vector2(1000, 700));

                

                break;
        }
    }

    public void setUIValue()
    {
        Holder = new Flex(this.GetComponent<RectTransform>(), 1);

        ExitRow = new Flex(Holder.getChild(0), 0.7f);

        Empty = new Flex(ExitRow.getChild(0), 9);
        Exit = new Flex(ExitRow.getChild(1), 1);

        Flex Tabs = new Flex(Holder.getChild(1), 1);

        Flex NewVarBTN = new Flex(Tabs.getChild(0), 1);
        Flex GlobalVarBTN = new Flex(Tabs.getChild(1), 1);


        //Don't add these as children?
        Flex MakeVariable = new Flex(Holder.getChild(2), 3);

        //New Variable
        Flex NewVar = new Flex(MakeVariable.getChild(0), 1);

        Flex NewVarTitle = new Flex(NewVar.getChild(0), 1);
        Flex NewVarInput = new Flex(NewVar.getChild(1), 1);

        //Premade
        Flex Premade = new Flex(MakeVariable.getChild(1), 1);

        Flex CustomTitle = new Flex(Premade.getChild(0), 1);
        Flex SV = new Flex(Premade.getChild(1), 4);
        Flex Viewport = new Flex(SV.getChild(0), 1);
        Flex Content = new Flex(Viewport.getChild(0), 1);

        Flex ViewSet = new Flex(Holder.getChild(3), 2);

        Flex VariableExample = new Flex(ViewSet.getChild(0), 1.5f);

        Flex Type = new Flex(VariableExample.getChild(0), 1);
        Flex VarName = new Flex(VariableExample.getChild(1), 1);
        Flex Value = new Flex(VariableExample.getChild(2), 1);

        Flex SetBTN = new Flex(ViewSet.getChild(1), 1);

        Flex ErrorMSG = new Flex(ViewSet.getChild(2), 0.6f);


        Parent.addChild(Holder);

        Holder.addChild(ExitRow);
        Holder.addChild(Tabs);
        Holder.addChild(MakeVariable);
        Holder.addChild(ViewSet);

        ExitRow.addChild(Empty);
        ExitRow.addChild(Exit);

        Tabs.addChild(NewVarBTN);
        Tabs.addChild(GlobalVarBTN);

        NewVar.addChild(NewVarTitle);
        NewVar.addChild(NewVarInput);

        Premade.addChild(CustomTitle);
        Premade.addChild(SV);

        SV.addChild(Viewport);
        Viewport.addChild(Content);

        ViewSet.addChild(VariableExample);
        ViewSet.addChild(SetBTN);
        ViewSet.addChild(ErrorMSG);

        VariableExample.addChild(Type);
        VariableExample.addChild(VarName);
        VariableExample.addChild(Value);


        //Make sure Premade is hidden
        Content.setChildMultiH(100);

        Premade.UI.gameObject.SetActive(false);

        Holder.setSpacingFlex(1f, 1);

        Holder.setHorizontalPadding(0.05f, 1, 0.05f, 1);

        Holder.setVerticalPadding(0.5f, 1, 0.5f, 1);

        Tabs.setHorizontalPadding(0.2f, 1, 0.2f, 1);

        NewVar.setAllPadSame(0.1f, 1);

        Premade.setAllPadSame(0.1f, 1);

        VariableExample.setSpacingFlex(1, 1);

        ViewSet.setHorizontalPadding(0.2f, 1, 0.2f, 1);

        //Add Content Children                                                                 maybe remove action
        List<VariableData> vars = Camera.main.GetComponent<ProgramManager>().getVariables(info.varType);

        foreach (VariableData varData in vars)
        {

            GameObject variable = Instantiate(Resources.Load("Prefabs/EditPanels/ValueDisp") as GameObject, Content.UI.transform);

            variable.GetComponent<ValueDisp>().setUI();

            variable.GetComponent<ValueDisp>().setInfo(varData);

            Content.addChild(variable.GetComponent<ValueDisp>().flex);

            variable.GetComponent<Button>().onClick.AddListener(delegate
            {
                this.varData = variable.GetComponent<ValueDisp>().varData;

                setView();

                Holder.getChild(3).GetChild(2).GetComponent<Text>().text = "";
            });

        }

        Parent.setSize(new Vector2(1000, 700));

        NewVar.setSize(MakeVariable.size);

        Premade.setSize(MakeVariable.size);
    }


    public void setControls(CardInfo info)
    {
        switch (info.editDataType)
        {
            case EditDataType.Direction:

                //Exit Button
                this.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
                {
                    ParentTrans.gameObject.SetActive(false);

                    Destroy(this.gameObject);
                });

                //Up
                this.transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
                {
                    setDirection(Direction.Up, info);
                });

                //Left
                this.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
                {
                    setDirection(Direction.Left, info);
                });

                //Right
                this.transform.GetChild(2).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate
                {
                    setDirection(Direction.Right, info);
                });

                //Down
                this.transform.GetChild(3).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
                {
                    setDirection(Direction.Down, info);
                });

                break;

            case EditDataType.Value:

                setText();

                Holder.getChild(3).GetChild(2).GetComponent<Text>().text = "";

                //Exit Button
                Holder.getChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
                {
                    ParentTrans.gameObject.SetActive(false);

                    Destroy(this.gameObject);
                });

                //Custom 
                Holder.getChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
                {
                    Holder.getChild(2).GetChild(0).gameObject.SetActive(true);
                    Holder.getChild(2).GetChild(1).gameObject.SetActive(false);
                });

                //Input

                Holder.getChild(2).GetChild(0).GetChild(1).GetComponent<InputField>().onValueChanged.AddListener(delegate
                {
                    value = Holder.getChild(2).GetChild(0).GetChild(1).GetComponent<InputField>().text;

                    Holder.getChild(3).GetChild(0).GetChild(0).GetComponent<Text>().text = getVarType(varType);

                    Holder.getChild(3).GetChild(0).GetChild(1).GetComponent<Text>().text = variable.getWord(lang);

                    Holder.getChild(3).GetChild(0).GetChild(2).GetComponent<Text>().text = value.ToString();

                    checkValueType();


                });

                //Premade/global
                Holder.getChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
                {
                    Holder.getChild(2).GetChild(0).gameObject.SetActive(false);
                    Holder.getChild(2).GetChild(1).gameObject.SetActive(true);

                });

                //Set
                Holder.getChild(3).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
                {
                    //Set Var Data
                    info.action.varData = varData;

                    //Set data
                    setData();

                    //Create and set the Action
                    info.programCard.action = func.createAction(info);

                    func.setInfo(info);

                    //Compile Program
                    allScripts.programSection.compileProgram();

                    ParentTrans.gameObject.SetActive(false);

                    Destroy(this.gameObject);

                });

                break;

            case EditDataType.NewValue:

                setText();

                Holder.getChild(2).GetChild(2).GetComponent<Text>().text = "";

                Holder.getChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = getVarType(varType);

                //Exit Button
                Holder.getChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
                {
                    ParentTrans.gameObject.SetActive(false);

                    Destroy(this.gameObject);
                });

                //Input
                Holder.getChild(1).GetChild(0).GetChild(1).GetComponent<InputField>().onValueChanged.AddListener(delegate
                {
                    value = Holder.getChild(1).GetChild(0).GetChild(1).GetComponent<InputField>().text;

                    Holder.getChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = getVarType(varType);

                    Holder.getChild(2).GetChild(0).GetChild(1).GetComponent<Text>().text = variable.getWord(lang);

                    Holder.getChild(2).GetChild(0).GetChild(2).GetComponent<Text>().text = value.ToString();

                    checkValueType();


                });

                //Set
                Holder.getChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
                {
                   
                    //Set Var Data
                    info.action.varData = varData;

                    //Set Data
                    setData();

                    //Create and set the Action
                    info.programCard.action = func.createAction(info);

                    func.setInfo(info);

                    //Compile Program
                    allScripts.programSection.compileProgram();

                    ParentTrans.gameObject.SetActive(false);

                    Destroy(this.gameObject);

                });

                break;

            case EditDataType.Multichoice:

                //Exit Button
                Holder.getChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
                {
                    ParentTrans.gameObject.SetActive(false);

                    Destroy(this.gameObject);
                });

                break;
        }
    }

    //
    //Set Info
    //
    public void setDirection(Direction dir, CardInfo info)
    {
        //Get Direction
        info.action.moveData.dir = dir;

        //Set Data
        //info.programCard.moveData = info.action.moveData;

        //Create and set the Action
        info.programCard.action = func.createAction(info);

        //Set Image
        string path = "";
        switch (dir)
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

        editSelection.GetComponent<Button>().image.sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

        //Compile Program
        allScripts.programSection.compileProgram();

        ParentTrans.gameObject.SetActive(false);

        Destroy(this.gameObject);
    }

    public void setView()
    {
        Holder.getChild(3).GetChild(0).GetChild(0).GetComponent<Text>().text = getVarType(varData.varType);

        Holder.getChild(3).GetChild(0).GetChild(1).GetComponent<Text>().text = varData.name.ToString();

        Holder.getChild(3).GetChild(0).GetChild(2).GetComponent<Text>().text = varData.value.ToString();

        varType = varData.varType;
        value = varData.value;
    }

    public void checkValueType()
    {
        int index = 0;
        switch (info.editDataType)
        {
            case EditDataType.Value:
                index = 3;
                break;
            case EditDataType.NewValue:
                index = 2;
                break;
        }


        switch (varType)
        {
            case VariableType.Text:
                //Anythign works for this
                //Do nothing
                Holder.getChild(index).GetChild(1).GetComponent<Button>().enabled = true;
                Holder.getChild(index).GetChild(2).GetComponent<Text>().text = "";
                break;
            case VariableType.Number:
                try
                {
                    int val = int.Parse((string)value);
                    Holder.getChild(index).GetChild(1).GetComponent<Button>().enabled = true;
                    Holder.getChild(index).GetChild(2).GetComponent<Text>().text = "";
                    varData.setData(varData.isPublic, varData.varType, varData.name, varData.value);
                }
                catch
                {
                    Holder.getChild(index).GetChild(2).GetComponent<Text>().text = error.getWord(lang) + getVarType(varType);

                    Holder.getChild(index).GetChild(1).GetComponent<Button>().enabled = false;

                }
                break;
            case VariableType.Decimal:
                try
                {
                    float val = float.Parse((string)value);
                    Holder.getChild(index).GetChild(1).GetComponent<Button>().enabled = true;
                    Holder.getChild(index).GetChild(2).GetComponent<Text>().text = "";
                    varData.setData(varData.isPublic, varData.varType, varData.name, varData.value);
                }
                catch
                {
                    Holder.getChild(index).GetChild(2).GetComponent<Text>().text = error.getWord(lang) + getVarType(varType);
                    Holder.getChild(index).GetChild(1).GetComponent<Button>().enabled = false;

                }
                break;
            case VariableType.Bool:

                try
                {
                    bool val = bool.Parse((string)value);
                    Holder.getChild(index).GetChild(1).GetComponent<Button>().enabled = true;
                    Holder.getChild(index).GetChild(2).GetComponent<Text>().text = "";
                    varData.setData(varData.isPublic, varData.varType, varData.name, varData.value);
                }
                catch
                {
                    Holder.getChild(index).GetChild(2).GetComponent<Text>().text = error.getWord(lang) + getVarType(varType);
                    Holder.getChild(index).GetChild(1).GetComponent<Button>().enabled = false;
                }
                break;
        }


    }

    public void setText()
    {

        switch (info.editDataType)
        {
            case EditDataType.Direction:
                break;

            case EditDataType.Value:

                Holder.getChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = customTab.getWord(lang);

                Holder.getChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = premadeTab.getWord(lang);

                Holder.getChild(2).GetChild(1).GetChild(0).GetComponent<Text>().text = variable.getWord(lang) + " (" + getVarType(varType) + ")";

                Holder.getChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = newVariable.getWord(lang);

                Holder.getChild(3).GetChild(1).GetChild(0).GetComponent<Text>().text = setbtn.getWord(lang);
                break;
            case EditDataType.NewValue:

                Holder.getChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = newVariable.getWord(lang);

                Holder.getChild(2).GetChild(1).GetChild(0).GetComponent<Text>().text = setbtn.getWord(lang);

                break;
        }

    }

    //Maybe make a language manager
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

    public void setData()
    {
        switch (info.actionType)
        {
            case ActionType.Movement:

                switch (info.movementName)
                {
                    case MovementActionNames.Move:

                        switch (info.valEditType)
                        {
                            case ValueEditType.Value:

                                //Get Direction
                                info.action.moveData.value = int.Parse(value.ToString());

                                break;
                        }

                        break;
                }

                break;
            case ActionType.Math:

                break;
            case ActionType.Logic:

                break;
            case ActionType.Variable:

                switch (info.variableName)
                {
                    case VariableActionNames.Variable:

                        switch (info.valEditType)
                        {
                            case ValueEditType.Name:

                                //Get Direction
                                info.action.varData.name = value.ToString();

                                break;
                            case ValueEditType.Value:

                                //Get Direction
                                info.action.varData.value = value.ToString();

                                break;
                            case ValueEditType.VariableType:
                                info.action.varData.varType = varType;

                                if (info.action.varData.varType == VariableType.Bool)
                                {
                                    info.action.varData.value = "false";
                                } else
                                {
                                    info.action.varData.value = "";
                                }

                                break;
                            case ValueEditType.Public:
                                info.action.varData.isPublic = isPub;
                                break;
                            case ValueEditType.Bool:
                                info.action.varData.value = isPub.ToString().ToLower();
                                break;
                        }

                        break;
                }
                break;
        }
    }


    public void setGridView(Flex parent)
    {

        //
        //Gonna have to redo this
        //


        List<string> paths = new List<string>();
        string path = "";
        switch (info.valEditType)
        {
            case ValueEditType.VariableType:

                paths.Clear();

                VariableTypes.Add(Text);
                VariableTypes.Add(Number);
                VariableTypes.Add(Decimal);
                VariableTypes.Add(Bool);

                //Add images for each later
                paths.Add("Images/EditControllerAssets/Text");
                paths.Add("Images/EditControllerAssets/Number");
                paths.Add("Images/EditControllerAssets/Decimal");
                paths.Add("Images/EditControllerAssets/Bool");

                int childrenPerRow = 2;
                //Row 
               for (int i = 0; i < 2; i ++)
                {
                    //Num of Children
                    for (int j = 0; j < childrenPerRow; j ++)
                    {
                        int index = i * childrenPerRow + j;

                        Texture2D image = Resources.Load(paths[index]) as Texture2D;

                        GameObject valDisp = Instantiate(Resources.Load("Prefabs/EditPanels/ValueDisp 2") as GameObject, parent.UI.GetChild(i).transform);

                        valDisp.transform.GetChild(1).GetComponent<Text>().text = VariableTypes[index].getWord(lang);

                        valDisp.transform.GetChild(0).GetComponent<Image>().sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

                        valDisp.GetComponent<Button>().onClick.AddListener(delegate
                        {
                            varType = GridViewVarType(index);

                            //varData.varType = varType;

                            //Set Var Data
                            info.action.varData = varData;

                            //Set data
                            setData();

                            //Create and set the Action
                            info.programCard.action = func.createAction(info);

                            func.setInfo(info);

                            //Compile Program
                            allScripts.programSection.compileProgram();

                            ParentTrans.gameObject.SetActive(false);

                            Destroy(this.gameObject);
                        });

                        Flex ValDisp = valDisp.GetComponent<ValueDisp>().flex;

                        ValDisp.setSquare();

                        parent.getChild(i).GetComponent<FlexInfo>().flex.addChild(ValDisp);
                    }
                }
                   
                break;

            case ValueEditType.Public:
                paths.Clear();

                paths.Add("Images/EditControllerAssets/Public");
                paths.Add("Images/EditControllerAssets/Local");

                foreach (string p in paths)
                {
                    //Public
                    path = p;
                    Texture2D image = Resources.Load(path) as Texture2D;

                    GameObject valDisp = Instantiate(Resources.Load("Prefabs/EditPanels/ValueDisp 2") as GameObject, parent.UI.GetChild(0).transform);

                    valDisp.transform.GetChild(0).GetComponent<Image>().sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

                    if (p == "Images/EditControllerAssets/Public")
                    {
                        valDisp.transform.GetChild(1).GetComponent<Text>().text = Public.getWord(lang);
                    }
                    else
                    {
                        valDisp.transform.GetChild(1).GetComponent<Text>().text = Private.getWord(lang);
                    }

                    valDisp.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        if (p == "Images/EditControllerAssets/Public")
                        {
                            isPub = true;
                        } else
                        {
                            isPub = false;
                        }
                       
                        //Set Var Data
                        info.action.varData = varData;

                        //Set data
                        setData();

                        //Create and set the Action
                        info.programCard.action = func.createAction(info);

                        func.setInfo(info);

                        //Compile Program
                        allScripts.programSection.compileProgram();

                        ParentTrans.gameObject.SetActive(false);

                        Destroy(this.gameObject);
                    });

                    Flex ValDisp = valDisp.GetComponent<ValueDisp>().flex;

                    ValDisp.setSquare();

                    parent.getChild(0).GetComponent<FlexInfo>().flex.addChild(ValDisp);
                }

                //Remove the last 3 children
                

                break;

            case ValueEditType.Bool:

                paths.Clear();

                paths.Add("Images/EditControllerAssets/True");
                paths.Add("Images/EditControllerAssets/False");

                foreach (string p in paths)
                {
                    //Public
                    path = p;
                    Texture2D image = Resources.Load(path) as Texture2D;

                    GameObject valDisp = Instantiate(Resources.Load("Prefabs/EditPanels/ValueDisp 2") as GameObject, parent.UI.GetChild(0).transform);

                    valDisp.transform.GetChild(0).GetComponent<Image>().sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

                    if (p == "Images/EditControllerAssets/True")
                    {
                        valDisp.transform.GetChild(1).GetComponent<Text>().text = True.getWord(lang);
                    }
                    else
                    {
                        valDisp.transform.GetChild(1).GetComponent<Text>().text = False.getWord(lang);
                    }

                    valDisp.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        //Use IsPub for setting the value
                        if (p == "Images/EditControllerAssets/True")
                        {
                            isPub = true;
                        }
                        else
                        {
                            isPub = false;
                        }

                        //Set Var Data
                        info.action.varData = varData;

                        //Set data
                        setData();

                        //Create and set the Action
                        info.programCard.action = func.createAction(info);

                        func.setInfo(info);

                        //Compile Program
                        allScripts.programSection.compileProgram();

                        ParentTrans.gameObject.SetActive(false);

                        Destroy(this.gameObject);
                    });

                    Flex ValDisp = valDisp.GetComponent<ValueDisp>().flex;

                    ValDisp.setSquare();

                    parent.getChild(0).GetComponent<FlexInfo>().flex.addChild(ValDisp);
                }

                break;
        }


    }

    public VariableType GridViewVarType (int index)
    {
        Debug.Log(index);

        switch (index)
        {
            case 0:
                return VariableType.Text;
            case 1:
                return VariableType.Number;
            case 2:
                return VariableType.Decimal;
            case 3:
                return VariableType.Bool;
            default:
                Debug.Log("here");
                return VariableType.Text;
        }


    }




}