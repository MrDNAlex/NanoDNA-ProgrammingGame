using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;
using FlexUI;
using UnityEngine.UI;
using DNASaveSystem;
using DNAMathAnimation;

public class EditValController : MonoBehaviour
{
    //Going to eventually have to check for key words as name 

    ProgramCardFunctionality func;

    CardInfo info;

    Flex Parent;
    Flex Holder;
    Flex ExitRow;
    Flex Empty;
    Flex Exit;

    Transform ParentTrans;

   // Scripts allScripts;

    MoveData moveData;
    VariableData varData;
    ActionData actData;

    Language lang;

    VariableType varType;
    string value = "";
    bool isPub;

    int globalIndex = 0;

    Vector3 OriginalPos;

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

    UIWord Up = new UIWord("Up", "Haut");
    UIWord Left = new UIWord("Left", "Gauche");
    UIWord Right = new UIWord("Right", "Droite");
    UIWord Down = new UIWord("Down", "Bas");

    UIWord Whisper = new UIWord("Whisper", "Chuchote");
    UIWord Talk = new UIWord("Talk", "Dit");
    UIWord Yell = new UIWord("Yell", "Crie");

    UIWord EntText = new UIWord("Enter Text...", "Entrez du Texte...");

    UIWord Addition = new UIWord("Addition", "Addition");
    UIWord Subtraction = new UIWord("Subtraction", "Soustraction");
    UIWord Multiplication = new UIWord("Multiplication", "Multiplication");
    UIWord Division = new UIWord("Division", "Division");


    List<UIWord> VariableTypes = new List<UIWord>();


    public void setPanel(CardInfo info, Transform parent, ProgramCardFunctionality func = null)
    {
        
        VariableTypes.Add(Text);
        VariableTypes.Add(Number);
        VariableTypes.Add(Decimal);
        VariableTypes.Add(Bool);

        this.info = info;

        if (func != null)
        {
            this.func = func;
        }

        //allScripts = Camera.main.GetComponent<LevelScript>().allScripts;

        lang = PlayerSettings.language;

        ParentTrans = parent;

        varType = info.varType;

        //Copy existing Data
        varData = info.action.varData;

        moveData = info.action.moveData;

        actData = info.action.actData;

        setUI();

        setColours();

        setControls(info);

        OriginalPos = ParentTrans.localPosition;

        Vector3 startPos = OriginalPos + new Vector3(0, -Screen.height, 0);

        ParentTrans.localPosition = startPos;

        StartCoroutine(DNAMathAnim.animateReboundRelocationLocal(ParentTrans, OriginalPos, 500, 1, true));

    }

    private void OnDestroy()
    {
        ParentTrans.localPosition = OriginalPos;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void setColours()
    {
        switch (info.editDataType)
        {
            case EditDataType.Multichoice:

                //Background
                UIHelper.setImage(Holder.UI, PlayerSettings.colourScheme.getAccent());

                UIHelper.setImage(Holder.getChild(1), PlayerSettings.colourScheme.getMain());

                break;

            case EditDataType.Value:
                UIHelper.setImage(Holder.UI, PlayerSettings.colourScheme.getAccent());

                UIHelper.setImage(Holder.getChild(1), PlayerSettings.colourScheme.getMain());

                //Tabs
                UIHelper.setImage(Holder.getChild(1).GetChild(0).GetChild(0), PlayerSettings.colourScheme.getSecondary());
                UIHelper.setImage(Holder.getChild(1).GetChild(0).GetChild(1), PlayerSettings.colourScheme.getSecondary());

                //New Value (InputField) 
                UIHelper.setImage(Holder.getChild(1).GetChild(1).GetChild(0).GetChild(1), PlayerSettings.colourScheme.getSecondary());

                //Scroll View, Premade
                UIHelper.setImage(Holder.getChild(1).GetChild(1).GetChild(1).GetChild(1), PlayerSettings.colourScheme.getSecondary());
                UIHelper.setImage(Holder.getChild(1).GetChild(1).GetChild(1).GetChild(1).GetChild(0), PlayerSettings.colourScheme.getSecondary());

                //Set Button
                UIHelper.setImage(Holder.getChild(1).GetChild(2).GetChild(0), PlayerSettings.colourScheme.getSecondary());

                break;

            case EditDataType.NewValue:
                UIHelper.setImage(Holder.UI, PlayerSettings.colourScheme.getAccent());

                UIHelper.setImage(Holder.getChild(1), PlayerSettings.colourScheme.getMain());

                UIHelper.setImage(Holder.getChild(1).GetChild(0).GetChild(0).GetChild(1), PlayerSettings.colourScheme.getSecondary());

                UIHelper.setImage(Holder.getChild(1).GetChild(1).GetChild(0), PlayerSettings.colourScheme.getSecondary());
                break;
            case EditDataType.Variable:

                UIHelper.setImage(Holder.UI, PlayerSettings.colourScheme.getAccent());

                UIHelper.setImage(Holder.getChild(1), PlayerSettings.colourScheme.getMain());

                UIHelper.setImage(Holder.getChild(1).GetChild(0).GetChild(1), PlayerSettings.colourScheme.getSecondary());

                UIHelper.setImage(Holder.getChild(1).GetChild(1).GetChild(0), PlayerSettings.colourScheme.getSecondary());

                break;
        }
    }

    public void setUI()
    {
        Parent = new Flex(ParentTrans.GetComponent<RectTransform>(), 1);

        //Set types
        //MultiChoice
        //New Value
        //New Value With Tabs

        switch (info.editDataType)
        {
            case EditDataType.Multichoice:

                Holder = new Flex(this.GetComponent<RectTransform>(), 1);

                ExitRow = new Flex(Holder.getChild(0), 1f);

                Exit = new Flex(ExitRow.getChild(0), 1);

                Flex GridView = new Flex(Holder.getChild(1), 9);

                Flex GridRow1;
                Flex GridRow2;
                Flex GridRow3;
                //Flex GridRow3;

                if (info.valEditType == ValueEditType.Public || info.valEditType == ValueEditType.Bool)
                {
                    GridRow1 = new Flex(GridView.getChild(0), 1);

                    GridView.addChild(GridRow1);

                    GridRow1.setSpacingFlex(0.2f, 1);
                    GridRow1.UI.GetComponent<HorizontalLayoutGroup>().spacing = 10;
                }
                else
                {
                    GridRow1 = new Flex(GridView.getChild(0), 1);
                    GridRow2 = new Flex(GridView.getChild(1), 1);
                    GridRow3 = new Flex(GridView.getChild(2), 1);
                    // GridRow3 = new Flex(GridView.getChild(2), 1);

                    GridView.addChild(GridRow1);
                    GridView.addChild(GridRow2);
                    GridView.addChild(GridRow3);
                    // GridView.addChild(GridRow3);

                    GridRow1.UI.GetComponent<HorizontalLayoutGroup>().spacing = 10;
                    GridRow2.UI.GetComponent<HorizontalLayoutGroup>().spacing = 10;
                    GridRow3.UI.GetComponent<HorizontalLayoutGroup>().spacing = 10;
                    GridRow1.setSpacingFlex(0.2f, 1);
                    GridRow2.setSpacingFlex(0.2f, 1);
                    GridRow3.setSpacingFlex(0.2f, 1);
                    // GridRow3.setSpacingFlex(0.2f, 1);
                }

                Parent.addChild(Holder);

                Holder.setVerticalPadding(0.1f, 1, 0.1f, 1);
                Holder.setHorizontalPadding(0.01f, 1, 0.01f, 1);

                ExitRow.setSelfHorizontalPadding(0, 1, 0.02f, 1);

                Holder.addChild(ExitRow);
                Holder.addChild(GridView);

                ExitRow.addChild(Exit);

                Holder.setSpacingFlex(0.1f, 1);

                Exit.setSquare();

                GridView.setSpacingFlex(0.2f, 1);

                //Set gridview
                setGridView(GridView);

                Parent.setSize(new Vector2(Screen.height * 0.7f * 1.3f, Screen.height * 0.7f));


                GridRow1.UI.GetComponent<HorizontalLayoutGroup>().spacing = 10;

                break;

            case EditDataType.Value:

                setUIValue();

                break;

            case EditDataType.NewValue:

                Holder = new Flex(this.GetComponent<RectTransform>(), 1);

                ExitRow = new Flex(Holder.getChild(0), 1f, Holder);

                // Empty = new Flex(ExitRow.getChild(0), 9);
                Exit = new Flex(ExitRow.getChild(0), 1, ExitRow);

                Flex Background = new Flex(Holder.getChild(1), 9, Holder);

                //Don't add these as children?
                Flex MakeVariable = new Flex(Background.getChild(0), 6, Background);

                //New Variable
                Flex NewVar = new Flex(MakeVariable.getChild(0), 1, MakeVariable);

                Flex NewVarTitle = new Flex(NewVar.getChild(0), 1, NewVar);
                Flex NewVarInput = new Flex(NewVar.getChild(1), 4, NewVar);

                Flex ViewSet = new Flex(Background.getChild(1), 3, Background);

                Flex SetBTN = new Flex(ViewSet.getChild(0), 1, ViewSet);

                Flex ErrorMSG = new Flex(ViewSet.getChild(1), 1f, ViewSet);

                Parent.addChild(Holder);

                Holder.setSpacingFlex(0.1f, 1);

                Holder.setHorizontalPadding(0.01f, 1, 0.01f, 1);

                Holder.setVerticalPadding(0.1f, 1, 0.1f, 1);

                Exit.setSquare();

                NewVar.setAllPadSame(0.05f, 1);

                ViewSet.setHorizontalPadding(0.2f, 1, 0.2f, 1);

                Parent.setSize(new Vector2(Screen.height * 0.7f * 1.3f, Screen.height * 0.7f));

                break;

            case EditDataType.Variable:

                setUIVariable();

                break;
        }
    }

    public void setUIValue()
    {
        Holder = new Flex(this.GetComponent<RectTransform>(), 1, Parent);

        ExitRow = new Flex(Holder.getChild(0), 1f, Holder);

        Exit = new Flex(ExitRow.getChild(0), 1, ExitRow);

        Flex Background = new Flex(Holder.getChild(1), 9, Holder);

        Flex Tabs = new Flex(Background.getChild(0), 1, Background);

        Flex NewVarBTN = new Flex(Tabs.getChild(0), 1, Tabs);
        Flex GlobalVarBTN = new Flex(Tabs.getChild(1), 1, Tabs);

        //Make the Variables Section
        Flex MakeVariable = new Flex(Background.getChild(1), 6, Background);

        //New Variable
        Flex NewVar = new Flex(MakeVariable.getChild(0), 1, MakeVariable);

        Flex NewVarTitle = new Flex(NewVar.getChild(0), 1, NewVar);
        Flex NewVarInput = new Flex(NewVar.getChild(1), 4, NewVar);

        //Premade
        Flex Premade = new Flex(MakeVariable.getChild(1), 1, MakeVariable);

        Flex CustomTitle = new Flex(Premade.getChild(0), 1, Premade);
        Flex SV = new Flex(Premade.getChild(1), 4, Premade);
        Flex Viewport = new Flex(SV.getChild(0), 1, SV);
        Flex Content = new Flex(Viewport.getChild(0), 1, Viewport);

        //Multi Choice Grid
        Flex GridHolder = new Flex(MakeVariable.getChild(2), 1, MakeVariable);

        Flex Row1 = new Flex(GridHolder.getChild(0), 1, GridHolder);

        //View and Set
        Flex ViewSet = new Flex(Background.getChild(2), 2, Background);

        Flex SetBTN = new Flex(ViewSet.getChild(0), 1, ViewSet);

        Flex ErrorMSG = new Flex(ViewSet.getChild(1), 1f, ViewSet);

        //Make sure Premade is hidden
        Content.setChildMultiH(100);

        Premade.UI.gameObject.SetActive(false);

        if (info.valEditType == ValueEditType.Bool)
        {
            NewVar.UI.gameObject.SetActive(false);
        }
        else
        {
            GridHolder.UI.gameObject.SetActive(false);
        }

        Holder.setSpacingFlex(0.1f, 1);

        Holder.setHorizontalPadding(0.01f, 1, 0.01f, 1);

        Holder.setVerticalPadding(0.1f, 1, 0.1f, 1);

        //Tabs.setHorizontalPadding(0.2f, 1, 0.2f, 1);

        Exit.setSquare();

        NewVar.setAllPadSame(0.1f, 1);

        Premade.setAllPadSame(0.1f, 1);

        ViewSet.setHorizontalPadding(0.2f, 1, 0.2f, 1);

        Row1.setSpacingFlex(0.2f, 1);

        //Add all the preexisting Variables
        List<VariableData> vars;
        if (info.valEditType == ValueEditType.VariableSmartAssign)
        {
            vars = Camera.main.GetComponent<ProgramManager>().getAllVariables();
        }
        else
        {
            vars = Camera.main.GetComponent<ProgramManager>().getVariables(info.varType);
        }

        //Add the premade variables
        foreach (VariableData varData in vars)
        {

            bool display = false;

            if (PlayerSettings.advancedVariables)
            {
                if (varData.name != this.varData.name)
                {
                    if (varData.isPublic)
                    {
                        display = true;
                    }
                    else
                    {
                        //Check if the parent gameObject is the same as the variables
                        if (Scripts.programSection.selectedCharData == varData.charData)
                        {
                            display = true;
                        }
                    }
                }
            }
            else
            {
                display = true;
            }

            if (display)
            {
                GameObject variable = Instantiate(Resources.Load("Prefabs/EditPanels/ValueDisp") as GameObject, Content.UI.transform);

                variable.GetComponent<ValueDisp>().setUI();

                variable.GetComponent<ValueDisp>().setInfo(varData);

                Content.addChild(variable.GetComponent<ValueDisp>().flex);

                variable.GetComponent<Button>().onClick.AddListener(delegate
                {
                    //Set reference, set value equal to name

                    switch (info.actionType)
                    {
                        case ActionType.Movement:
                            this.moveData.refID = variable.GetComponent<ValueDisp>().varData.id;
                            this.moveData.value = variable.GetComponent<ValueDisp>().varData.name;
                            this.value = variable.GetComponent<ValueDisp>().varData.name;
                            break;

                        case ActionType.Variable:
                            this.varData.refID = variable.GetComponent<ValueDisp>().varData.id;
                            this.varData.value = variable.GetComponent<ValueDisp>().varData.name;
                            this.value = variable.GetComponent<ValueDisp>().varData.name;
                            if (!PlayerSettings.advancedVariables)
                            {
                                this.varData.isPublic = true;
                            }
                            break;
                        case ActionType.Action:
                            this.actData.refID = variable.GetComponent<ValueDisp>().varData.id;
                            this.actData.data = variable.GetComponent<ValueDisp>().varData.name;
                            this.value = variable.GetComponent<ValueDisp>().varData.name;
                            break;

                    }

                    Holder.getChild(1).GetChild(2).GetChild(1).GetComponent<Text>().text = "";
                });
            }
        }

        setGridView(GridHolder);

        Parent.setSize(new Vector2(Screen.height * 0.7f * 1.3f, Screen.height * 0.7f));

        NewVar.setSize(MakeVariable.size);

        Premade.setSize(MakeVariable.size);

        GridHolder.setSize(MakeVariable.size);
    }

    void setUIVariable ()
    {
        Holder = new Flex(this.GetComponent<RectTransform>(), 1, Parent);

        ExitRow = new Flex(Holder.getChild(0), 1f, Holder);

        // Empty = new Flex(ExitRow.getChild(0), 9);
        Exit = new Flex(ExitRow.getChild(0), 1, ExitRow);

        Flex ViewSetHolder = new Flex(Holder.getChild(1), 9, Holder);
        Flex VariableList = new Flex(ViewSetHolder.getChild(0), 6, ViewSetHolder);

        Flex VariableType = new Flex(VariableList.getChild(0), 1, VariableList);

        Flex ScrollView = new Flex(VariableList.getChild(1), 4, VariableList);
        Flex Viewport = new Flex(ScrollView.getChild(0), 1, ScrollView);
        Flex Content = new Flex(Viewport.getChild(0), 1, Viewport);

        Flex ViewAndSet = new Flex(ViewSetHolder.getChild(1), 3, ViewSetHolder);

        Flex SetButton = new Flex(ViewAndSet.getChild(0), 1, ViewAndSet);
        Flex ErrorMessage = new Flex(ViewAndSet.getChild(1), 1, ViewAndSet);

        //Edit the Flex
        Holder.setSpacingFlex(0.1f, 1);

        Holder.setHorizontalPadding(0.01f, 1, 0.01f, 1);

        Holder.setVerticalPadding(0.1f, 1, 0.1f, 1);

        Exit.setSquare();

        ViewSetHolder.setSpacingFlex(0.3f, 1);

        ViewSetHolder.setHorizontalPadding(0.1f, 1, 0.1f, 1);


        //Add Variables to Content

        Parent.setSize(new Vector2(Screen.height * 0.7f * 1.3f, Screen.height * 0.7f));

       // Parent.setSize(new Vector2(1000, 750));
    }

    public void setControls(CardInfo info)
    {

        setText();

        //Exit Button
        Holder.getChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
        {
            ParentTrans.gameObject.SetActive(false);

            Destroy(this.gameObject);

            Scripts.programManager.updateVariables();
        });

        switch (info.editDataType)
        {
            case EditDataType.Value:

                //Set Error Section to nothing
                Holder.getChild(1).GetChild(2).GetChild(1).GetComponent<Text>().text = "";

                //Custom 
                Holder.getChild(1).GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
                {

                    if (info.valEditType == ValueEditType.Bool)
                    {
                        Holder.getChild(2).GetChild(2).gameObject.SetActive(true);
                        Holder.getChild(2).GetChild(0).gameObject.SetActive(false);
                    }
                    else
                    {
                        Holder.getChild(2).GetChild(0).gameObject.SetActive(true);
                        Holder.getChild(2).GetChild(2).gameObject.SetActive(false);
                    }
                    Holder.getChild(1).GetChild(1).GetChild(1).gameObject.SetActive(false);
                    Holder.getChild(1).GetChild(1).GetChild(0).gameObject.SetActive(true);
                });

                //Premade/global
                Holder.getChild(1).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
                {
                    if (info.valEditType == ValueEditType.Bool)
                    {
                        Holder.getChild(2).GetChild(0).gameObject.SetActive(false);
                        Holder.getChild(2).GetChild(2).gameObject.SetActive(false);
                    }
                    else
                    {
                        Holder.getChild(2).GetChild(0).gameObject.SetActive(false);
                        Holder.getChild(2).GetChild(2).gameObject.SetActive(false);
                    }
                    Holder.getChild(1).GetChild(1).GetChild(1).gameObject.SetActive(true);
                    Holder.getChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);
                });

                //Input

                Holder.getChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<InputField>().onValueChanged.AddListener(delegate
                {
                    value = Holder.getChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<InputField>().text;
                   
                    checkValueType();
                });

                //Set
                Holder.getChild(1).GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
                {
                    //Set Var Data
                    info.action.varData = varData;

                    //Set data
                    setData(globalIndex);

                    //Create and set the Action
                    info.programCard.action = func.createAction(info);

                    func.setInfo(info);

                    Scripts.programSection.selectedCharData.program.setAction(info.programCard.action, info.progLineIndex);

                    //Hide object and Destroy
                    ParentTrans.gameObject.SetActive(false);
                    Destroy(this.gameObject);

                    Scripts.programManager.updateVariables();

                });

                break;

            case EditDataType.NewValue:

               

                //Set Error to nothing 
                Holder.getChild(1).GetChild(1).GetChild(1).GetComponent<Text>().text = "";

                //Input
                Holder.getChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponent<InputField>().onValueChanged.AddListener(delegate
                {
                    value = Holder.getChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponent<InputField>().text;

                    checkValueType();
                });

                //Set
                Holder.getChild(1).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
                {

                    //Set Var Data
                    info.action.varData = varData;

                    //Set Data
                    setData(0);

                    //Create and set the Action
                    info.programCard.action = func.createAction(info);

                    //Set Info in ProgramCardFunc
                    func.setInfo(info);

                    //Set the action
                    Scripts.programSection.selectedCharData.program.setAction(info.programCard.action, info.progLineIndex);

                    //Hide object and Destroy
                    ParentTrans.gameObject.SetActive(false);
                    Destroy(this.gameObject);

                    Scripts.programManager.updateVariables();

                });
                break;
        }
    }

    public void checkValueType()
    {
        int index = 0;
        switch (info.editDataType)
        {
            case EditDataType.Value:
                index = 2;
                break;
            case EditDataType.NewValue:
                index = 1;
                break;
        }

        switch (info.valEditType)
        {
            case ValueEditType.VariableSmartAssign:
                try
                {
                    bool val = bool.Parse((string)value);
                    varData.setData(true, VariableType.Bool, varData.name, varData.value);
                    resetRefID(info.actionType);
                    Debug.Log("Var type is Bool");
                }
                catch
                {
                    //Try Int
                    try
                    {
                        int val = int.Parse((string)value);
                        Debug.Log(varData.value);
                        varData.setData(true, VariableType.Number, varData.name, varData.value);

                        resetRefID(info.actionType);
                        Debug.Log("Var type is Integer");
                    }
                    catch
                    {
                        //Try Float
                        try
                        {
                            float val = float.Parse((string)value);
                            varData.setData(true, VariableType.Decimal, varData.name, varData.value);
                            resetRefID(info.actionType);
                            Debug.Log("Var type is Decimal");
                        }
                        catch
                        {
                            //Default to Text
                            varData.setData(true, VariableType.Text, varData.name, varData.value);
                            resetRefID(info.actionType);
                            Debug.Log("Var type is Text");
                        }
                    }
                }
                break;
            default:

                switch (varType)
                {
                    case VariableType.Text:
                        //Do Nothing
                        Holder.getChild(1).GetChild(index).GetChild(0).GetComponent<Button>().enabled = true;
                        Holder.getChild(1).GetChild(index).GetChild(1).GetComponent<Text>().text = "";
                        resetRefID(info.actionType);
                        break;
                    case VariableType.Number:
                        try
                        {
                            int val = int.Parse((string)value);
                            Holder.getChild(1).GetChild(index).GetChild(0).GetComponent<Button>().enabled = true;
                            Holder.getChild(1).GetChild(index).GetChild(1).GetComponent<Text>().text = "";
                            varData.setData(varData.isPublic, varData.varType, varData.name, varData.value);
                            resetRefID(info.actionType);
                        }
                        catch
                        {
                            Holder.getChild(1).GetChild(index).GetChild(0).GetComponent<Button>().enabled = false;
                            Holder.getChild(1).GetChild(index).GetChild(1).GetComponent<Text>().text = error.getWord(lang) + getVarType(varType);

                        }
                        break;
                    case VariableType.Decimal:
                        try
                        {
                            float val = float.Parse((string)value);
                            Holder.getChild(1).GetChild(index).GetChild(0).GetComponent<Button>().enabled = true;
                            Holder.getChild(1).GetChild(index).GetChild(1).GetComponent<Text>().text = "";
                            varData.setData(varData.isPublic, varData.varType, varData.name, varData.value);
                            resetRefID(info.actionType);
                        }
                        catch
                        {
                            Holder.getChild(1).GetChild(index).GetChild(0).GetComponent<Button>().enabled = false;
                            Holder.getChild(1).GetChild(index).GetChild(1).GetComponent<Text>().text = error.getWord(lang) + getVarType(varType);
                        }
                        break;
                    case VariableType.Bool:

                        try
                        {
                            bool val = bool.Parse((string)value);
                            Holder.getChild(1).GetChild(index).GetChild(0).GetComponent<Button>().enabled = true;
                            Holder.getChild(1).GetChild(index).GetChild(1).GetComponent<Text>().text = "";
                            varData.setData(varData.isPublic, varData.varType, varData.name, varData.value);
                            resetRefID(info.actionType);
                        }
                        catch
                        {
                            Holder.getChild(1).GetChild(index).GetChild(0).GetComponent<Button>().enabled = false;
                            Holder.getChild(1).GetChild(index).GetChild(1).GetComponent<Text>().text = error.getWord(lang) + getVarType(varType);
                        }
                        break;
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

                UIHelper.setText(Holder.getChild(1).GetChild(0).GetChild(0).GetChild(0), customTab.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor());

                UIHelper.setText(Holder.getChild(1).GetChild(0).GetChild(1).GetChild(0), premadeTab.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor());

                UIHelper.setText(Holder.getChild(1).GetChild(1).GetChild(1).GetChild(0), variable.getWord(lang) + " (" + getVarType(varType) + ")", PlayerSettings.colourScheme.getBlackTextColor());

                UIHelper.setText(Holder.getChild(1).GetChild(1).GetChild(0).GetChild(0), newVariable.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor());

                UIHelper.setText(Holder.getChild(1).GetChild(2).GetChild(0).GetChild(0), setbtn.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor());

                UIHelper.setText(Holder.getChild(1).GetChild(1).GetChild(0).GetChild(1).GetChild(0), EntText.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor());

                UIHelper.setText(Holder.getChild(1).GetChild(1).GetChild(0).GetChild(1).GetChild(1), "", PlayerSettings.colourScheme.getBlackTextColor());

                break;
            case EditDataType.NewValue:
                UIHelper.setText(Holder.getChild(1).GetChild(0).GetChild(0).GetChild(0), newVariable.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor());

                UIHelper.setText(Holder.getChild(1).GetChild(1).GetChild(0).GetChild(0), setbtn.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor());

                UIHelper.setText(Holder.getChild(1).GetChild(0).GetChild(0).GetChild(1).GetChild(0), EntText.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor());

                UIHelper.setText(Holder.getChild(1).GetChild(0).GetChild(0).GetChild(1).GetChild(1), "", PlayerSettings.colourScheme.getBlackTextColor());

                break;

            case EditDataType.Variable:

                UIHelper.setText(Holder.getChild(1).GetChild(1).GetChild(0).GetChild(0), setbtn.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor());

                UIHelper.setText(Holder.getChild(1).GetChild(0).GetChild(0), variable.getWord(lang) + " (" + getVarType(varType) + ")", PlayerSettings.colourScheme.getBlackTextColor());

                UIHelper.setText(Holder.getChild(1).GetChild(1).GetChild(1), "", PlayerSettings.colourScheme.getBlackTextColor());

                break;
        }
    }

    public void setData(int index)
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

                                info.action.moveData.value = value;

                               // info.action.moveData.value.ToString();
                                break;
                            case ValueEditType.Direction:
                                info.action.moveData.dir = indexToDir(index);
                                break;
                        }
                        break;
                }
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

                                //Set Value
                                info.action.varData.name = value.ToString();

                                break;
                            case ValueEditType.Value:

                                //Set Value
                                info.action.varData.value = value.ToString();

                                break;
                            case ValueEditType.VariableType:

                                info.action.varData.varType = GridViewVarType(index);

                                if (info.action.varData.varType == VariableType.Bool)
                                {
                                    info.action.varData.value = "false";
                                }
                                else
                                {
                                    info.action.varData.value = "";
                                }

                                break;
                            case ValueEditType.Public:

                                if (index == 0)
                                {
                                    info.action.varData.isPublic = true;
                                }
                                else
                                {
                                    info.action.varData.isPublic = false;
                                }
                                break;
                            case ValueEditType.Bool:
                                if (index == 0)
                                {
                                    info.action.varData.value = "true";
                                }
                                else
                                {
                                    info.action.varData.value = "false";
                                }
                                break;

                            case ValueEditType.VariableSmartAssign:
                                //Set Value
                                info.action.varData.value = value.ToString();
                                break;
                        }
                        break;
                    case VariableActionNames.MathVariable:

                        switch (info.valEditType)
                        {
                            case ValueEditType.Name:
                                info.action.varData.name = value.ToString();
                                break;
                            case ValueEditType.MathOperation:

                                switch (index)
                                {
                                    case 0:
                                        info.action.varData.mathType = MathTypes.Addition;
                                        break;
                                    case 1:
                                        info.action.varData.mathType = MathTypes.Subtraction;
                                        break;
                                    case 2:
                                        info.action.varData.mathType = MathTypes.Multiplication;
                                        break;
                                    case 3:
                                        info.action.varData.mathType = MathTypes.Division;
                                        break;
                                }
                                break;
                        }
                        break;
                }
                break;
            case ActionType.Action:

                switch (info.actionName)
                {
                    case ActionActionNames.Speak:

                        switch (info.valEditType)
                        {
                            case ValueEditType.Speak:

                                switch (index)
                                {
                                    case 0:
                                        info.action.actData.descriptor = ActionDescriptor.Whisper;
                                        break;
                                    case 1:
                                        info.action.actData.descriptor = ActionDescriptor.Talk;
                                        break;
                                    case 2:
                                        info.action.actData.descriptor = ActionDescriptor.Yell;
                                        break;
                                }
                                break;

                            case ValueEditType.Value:

                                info.action.actData.data = value.ToString();

                                break;

                                //Message
                        }

                        break;
                }

                break;
        }
    }

    public void instantiateEmpty(Flex parent, int rowIndex)
    {
        GameObject valDisp = Instantiate(Resources.Load("Prefabs/EditPanels/Empty") as GameObject, parent.UI.GetChild(rowIndex).transform);

        Flex ValDisp = new Flex(valDisp.GetComponent<RectTransform>(), 1);

        ValDisp.setSquare();

        parent.getChild(rowIndex).GetComponent<FlexInfo>().flex.addChild(ValDisp);

    }
    public void instantiateDisplayCard(string imagePath, Flex parent, UIWord word, int index, int rowIndex)
    {
        //Determine Image, Instantiate Object and Set Text
        Texture2D image = Resources.Load(imagePath) as Texture2D;

        GameObject valDisp = Instantiate(Resources.Load("Prefabs/EditPanels/ValueDispCard") as GameObject, parent.UI.GetChild(rowIndex).transform);

        valDisp.transform.GetChild(1).GetComponent<Text>().text = word.getWord(lang);

        valDisp.transform.GetChild(0).GetComponent<Image>().sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

        if (info.editDataType == EditDataType.Multichoice)
        {
            //On Listener
            valDisp.GetComponent<Button>().onClick.AddListener(delegate
            {
                //Set Var Data
                info.action.varData = varData;

                //Set data
                setData(index);

                Debug.Log(info.action.varData.mathType);

                //Create and set the Action
                info.programCard.action = func.createAction(info);

                //Set Info
                func.setInfo(info);

                Scripts.programSection.selectedCharData.program.setAction(info.programCard.action, info.progLineIndex);

                //Compile Program
                //allScripts.programSection.compileProgram();
                //allScripts.programSection.renderProgram();

                ParentTrans.gameObject.SetActive(false);

                Destroy(this.gameObject);

               Scripts.programManager.updateVariables();
            });
        }
        else
        {
            valDisp.GetComponent<Button>().onClick.AddListener(delegate
            {
                //Set reference, set value equal to name
                // this.varData.refID = variable.GetComponent<ValueDisp>().varData.id;
                //this.varData.value = variable.GetComponent<ValueDisp>().varData.name;

                if (index == 0)
                {
                    varData.refID = 0;
                    varData.value = "true";
                }
                else
                {
                    varData.refID = 0;
                    varData.value = "false";

                }

                globalIndex = index;

                // setView();

                Holder.getChild(3).GetChild(1).GetComponent<Text>().text = "";

               Scripts.programManager.updateVariables();
            });
        }

        //Create Flex, set Square and Add as Child
        Flex ValDisp = valDisp.GetComponent<ValueDisp>().flex;

        ValDisp.setSquare();

        parent.getChild(rowIndex).GetComponent<FlexInfo>().flex.addChild(ValDisp);

    }

    public void setGridView(Flex parent)
    {

        switch (info.valEditType)
        {
            case ValueEditType.Direction:
                //
                //Row 1
                //

                instantiateEmpty(parent, 0);
                //Up
                instantiateDisplayCard("Images/EditControllerAssets/up", parent, Up, 0, 0);
                instantiateEmpty(parent, 0);

                //
                //Row 2
                //

                //Left
                instantiateDisplayCard("Images/EditControllerAssets/left", parent, Left, 1, 1);
                instantiateEmpty(parent, 1);
                //Right
                instantiateDisplayCard("Images/EditControllerAssets/right", parent, Right, 2, 1);

                //
                //Row 3
                //
                instantiateEmpty(parent, 2);
                //Down
                instantiateDisplayCard("Images/EditControllerAssets/down", parent, Down, 3, 2);
                instantiateEmpty(parent, 2);

                break;

            case ValueEditType.VariableType:

                //Text
                instantiateDisplayCard("Images/EditControllerAssets/Text", parent, Text, 0, 0);

                //Number
                instantiateDisplayCard("Images/EditControllerAssets/Number", parent, Number, 1, 0);

                //Decimal
                instantiateDisplayCard("Images/EditControllerAssets/Decimal", parent, Decimal, 2, 1);

                //Bool
                instantiateDisplayCard("Images/EditControllerAssets/Bool", parent, Bool, 3, 1);

                break;

            case ValueEditType.Public:

                //Public
                instantiateDisplayCard("Images/EditControllerAssets/Public", parent, Public, 0, 0);

                //Local
                instantiateDisplayCard("Images/EditControllerAssets/Local", parent, Private, 1, 0);

                break;

            case ValueEditType.Bool:

                //True
                instantiateDisplayCard("Images/EditControllerAssets/True", parent, True, 0, 0);

                //False
                instantiateDisplayCard("Images/EditControllerAssets/False", parent, False, 1, 0);

                break;

            case ValueEditType.Speak:

                //Whisper
                instantiateDisplayCard("Images/EditControllerAssets/Whisper", parent, Whisper, 0, 0);

                //Talk
                instantiateDisplayCard("Images/EditControllerAssets/Talk", parent, Talk, 1, 0);

                //Yell
                instantiateDisplayCard("Images/EditControllerAssets/Yell", parent, Yell, 2, 0);

                break;
            case ValueEditType.MathOperation:

                instantiateDisplayCard("Images/EditControllerAssets/Addition", parent, Addition, 0, 0);
                instantiateDisplayCard("Images/EditControllerAssets/Subtraction", parent, Subtraction, 1, 0);
                instantiateDisplayCard("Images/EditControllerAssets/Multiplication", parent, Multiplication, 2, 0);
                instantiateDisplayCard("Images/EditControllerAssets/Division", parent, Division, 3, 0);

                break;
        }
    }

    public VariableType GridViewVarType(int index)
    {
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

    public Direction indexToDir(int index)
    {
        Direction dir = Direction.Up;
        switch (index)
        {
            case 0:
                dir = Direction.Up;
                break;
            case 1:
                dir = Direction.Left;
                break;
            case 2:
                dir = Direction.Right;
                break;
            case 3:
                dir = Direction.Down;
                break;
        }
        return dir;
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

    public void resetRefID(ActionType type)
    {
        switch (type)
        {
            case ActionType.Movement:
                moveData.refID = 0;
                break;
            case ActionType.Variable:
                varData.refID = 0;
                break;
            case ActionType.Action:
                actData.refID = 0;
                break;
        }
    }

}