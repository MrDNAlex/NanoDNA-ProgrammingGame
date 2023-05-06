using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;
using FlexUI;
using UnityEngine.UI;
using DNASaveSystem;
using DNAMathAnimation;

public class ValueValController : EditValController
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setPanel(Transform parent, ProgramCard progCard)
    {
        VariableTypes.Add(LangDictionary.Text);
        VariableTypes.Add(LangDictionary.Number);
        VariableTypes.Add(LangDictionary.Decimal);
        VariableTypes.Add(LangDictionary.Bool);

        this.panelInfo = progCard.panelInfo;
        this.actionInfo = progCard.actionInfo;

        //allScripts = Camera.main.GetComponent<LevelScript>().allScripts;

        this.progCard = progCard;

        lang = PlayerSettings.language;

        ParentTrans = parent;

        varType = progCard.panelInfo.varType;

        //Copy existing Data
        varActData = progCard.action.varActData;

        moveData = progCard.action.moveData;

        actData = progCard.action.actData;

        setUI();

        setColours();

        setControls(progCard.panelInfo);

        OriginalPos = ParentTrans.localPosition;

        Vector3 startPos = OriginalPos + new Vector3(0, -Screen.height, 0);

        ParentTrans.localPosition = startPos;

        StartCoroutine(DNAMathAnim.animateReboundRelocationLocal(ParentTrans, OriginalPos, DNAMathAnim.getFrameNumber(0.75f), 1, true));


    }

    void setColours()
    {
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
    }

    void setUI()
    {
        Parent = new Flex(ParentTrans.GetComponent<RectTransform>(), 1);

        Holder = new Flex(this.GetComponent<RectTransform>(), 1, Parent);

        Flex ExitRow = new Flex(Holder.getChild(0), 1f, Holder);

        Flex Exit = new Flex(ExitRow.getChild(0), 1, ExitRow);

        Flex Background = new Flex(Holder.getChild(1), 9, Holder);

        Flex Tabs = new Flex(Background.getChild(0), 1.5f, Background);

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

        if (panelInfo.valEditType == ValueEditType.Bool)
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
        if (panelInfo.valEditType == ValueEditType.VariableSmartAssign)
        {
            vars = Camera.main.GetComponent<ProgramManager>().getAllVariables();
        }
        else
        {
            vars = Camera.main.GetComponent<ProgramManager>().getVariables(panelInfo.varType);
        }

        //Add the premade variables
        foreach (VariableData varData in vars)
        {

            bool display = false;

            if (PlayerSettings.advancedVariables)
            {
                if (varData.name != this.varActData.name)
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

                //variable.GetComponent<ValueDisp>().setUI();

                variable.GetComponent<ValueDisp>().setInfo(varData);

                Content.addChild(variable.GetComponent<ValueDisp>().flex);

                variable.GetComponent<Button>().onClick.AddListener(delegate
                {
                    //Set reference, set value equal to name

                    switch (actionInfo.actionType)
                    {
                        case ActionType.Movement:
                            this.moveData.refID = variable.GetComponent<ValueDisp>().varData.id;
                            this.moveData.value = variable.GetComponent<ValueDisp>().varData.name;
                            this.value = variable.GetComponent<ValueDisp>().varData.name;
                            break;

                        case ActionType.Variable:
                            this.varActData.setData.refID = variable.GetComponent<ValueDisp>().varData.id;
                            this.varActData.setData.value = variable.GetComponent<ValueDisp>().varData.name;
                            this.value = variable.GetComponent<ValueDisp>().varData.name;
                            if (!PlayerSettings.advancedVariables)
                            {
                                this.varActData.setData.isPublic = true;
                            }

                            //In the event that it is a math operation
                            if (panelInfo.valEditType == ValueEditType.Value1)
                            {
                                this.varActData.mathData.refID1 = variable.GetComponent<ValueDisp>().varData.id;
                                this.varActData.mathData.value1 = variable.GetComponent<ValueDisp>().varData.name;
                            }
                            else if (panelInfo.valEditType == ValueEditType.Value2)
                            {
                                this.varActData.mathData.refID2 = variable.GetComponent<ValueDisp>().varData.id;
                                this.varActData.mathData.value2 = variable.GetComponent<ValueDisp>().varData.name;
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

    void setText()
    {
        UIHelper.setText(Holder.getChild(1).GetChild(0).GetChild(0).GetChild(0), LangDictionary.customTab.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor(), PlayerSettings.getBigText());

        UIHelper.setText(Holder.getChild(1).GetChild(0).GetChild(1).GetChild(0), LangDictionary.premadeTab.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor(), PlayerSettings.getBigText());

        UIHelper.setText(Holder.getChild(1).GetChild(1).GetChild(1).GetChild(0), LangDictionary.variable.getWord(lang) + " (" + getVarType(varType) + ")", PlayerSettings.colourScheme.getBlackTextColor(), PlayerSettings.getBigText());

        UIHelper.setText(Holder.getChild(1).GetChild(1).GetChild(0).GetChild(0), LangDictionary.newVariable.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor(), PlayerSettings.getBigText());

        UIHelper.setText(Holder.getChild(1).GetChild(2).GetChild(0).GetChild(0), LangDictionary.setbtn.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor(), PlayerSettings.getMediumText());

        UIHelper.setText(Holder.getChild(1).GetChild(1).GetChild(0).GetChild(1).GetChild(0), LangDictionary.EntText.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor(), PlayerSettings.getMediumText());

        UIHelper.setText(Holder.getChild(1).GetChild(1).GetChild(0).GetChild(1).GetChild(1), "", PlayerSettings.colourScheme.getBlackTextColor(), PlayerSettings.getMediumText());
    }

    void setControls(ProgramCard.PanelInfo info)
    {
        setText();

        //Exit Button
        Holder.getChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
        {
            ParentTrans.gameObject.SetActive(false);

            Destroy(this.gameObject);

            Scripts.programManager.updateVariables();
        });


        //Unique to every type
        //
        //

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
            progCard.action.varActData = varActData;

            //Set data
            setData(globalIndex);

            //Create and set the Action
            progCard.action = progCard.Iprogram.createAction();

            progCard.Iprogram.setInfo();

            Scripts.programSection.selectedCharData.program.setAction(progCard.action, progCard.lineIndex);

            //Hide object and Destroy
            ParentTrans.gameObject.SetActive(false);
            Destroy(this.gameObject);

            Scripts.programManager.updateVariables();

        });

    }

    void setGridView(Flex parent)
    {
        switch (panelInfo.valEditType)
        {
            case ValueEditType.Direction:
                //
                //Row 1
                //

                instantiateEmpty(parent, 0);
                //Up
                instantiateDisplayCard("Images/EditControllerAssets/up", parent, LangDictionary.Up, 0, 0);
                instantiateEmpty(parent, 0);

                //
                //Row 2
                //

                //Left
                instantiateDisplayCard("Images/EditControllerAssets/left", parent, LangDictionary.Left, 1, 1);
                instantiateEmpty(parent, 1);
                //Right
                instantiateDisplayCard("Images/EditControllerAssets/right", parent, LangDictionary.Right, 2, 1);

                //
                //Row 3
                //
                instantiateEmpty(parent, 2);
                //Down
                instantiateDisplayCard("Images/EditControllerAssets/down", parent, LangDictionary.Down, 3, 2);
                instantiateEmpty(parent, 2);

                break;

            case ValueEditType.VariableType:

                //Text
                instantiateDisplayCard("Images/EditControllerAssets/Text", parent, LangDictionary.Text, 0, 0);

                //Number
                instantiateDisplayCard("Images/EditControllerAssets/Number", parent, LangDictionary.Number, 1, 0);

                //Decimal
                instantiateDisplayCard("Images/EditControllerAssets/Decimal", parent, LangDictionary.Decimal, 2, 1);

                //Bool
                instantiateDisplayCard("Images/EditControllerAssets/Bool", parent, LangDictionary.Bool, 3, 1);

                break;

            case ValueEditType.Public:

                //Public
                instantiateDisplayCard("Images/EditControllerAssets/Public", parent, LangDictionary.Public, 0, 0);

                //Local
                instantiateDisplayCard("Images/EditControllerAssets/Local", parent, LangDictionary.Private, 1, 0);

                break;

            case ValueEditType.Bool:

                //True
                instantiateDisplayCard("Images/EditControllerAssets/True", parent, LangDictionary.True, 0, 0);

                //False
                instantiateDisplayCard("Images/EditControllerAssets/False", parent, LangDictionary.False, 1, 0);

                break;

            case ValueEditType.Speak:

                //Whisper
                instantiateDisplayCard("Images/EditControllerAssets/Whisper", parent, LangDictionary.Whisper, 0, 0);

                //Talk
                instantiateDisplayCard("Images/EditControllerAssets/Talk", parent, LangDictionary.Talk, 1, 0);

                //Yell
                instantiateDisplayCard("Images/EditControllerAssets/Yell", parent, LangDictionary.Yell, 2, 0);

                break;
                /*
            case ValueEditType.MathOperation:

                instantiateDisplayCard("Images/EditControllerAssets/Addition", parent, LangDictionary.Addition, 0, 0);
                instantiateDisplayCard("Images/EditControllerAssets/Subtraction", parent, LangDictionary.Subtraction, 1, 0);
                instantiateDisplayCard("Images/EditControllerAssets/Multiplication", parent, LangDictionary.Multiplication, 2, 0);
                instantiateDisplayCard("Images/EditControllerAssets/Division", parent, LangDictionary.Division, 3, 0);

                break;
                */

        }
    }

    public void instantiateDisplayCard(string imagePath, Flex parent, UIWord word, int index, int rowIndex)
    {
        //Determine Image, Instantiate Object and Set Text
        Texture2D image = Resources.Load(imagePath) as Texture2D;

        GameObject valDisp = Instantiate(Resources.Load("Prefabs/EditPanels/ValueDispCard") as GameObject, parent.UI.GetChild(rowIndex).transform);

        valDisp.transform.GetChild(1).GetComponent<Text>().text = word.getWord(lang);

        valDisp.transform.GetChild(0).GetComponent<Image>().sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

        valDisp.GetComponent<Button>().onClick.AddListener(delegate
        {
            //Set reference, set value equal to name
            // this.varData.refID = variable.GetComponent<ValueDisp>().varData.id;
            //this.varData.value = variable.GetComponent<ValueDisp>().varData.name;

            if (index == 0)
            {
                varActData.setData.refID = 0;
                varActData.setData.value = "true";
            }
            else
            {
                varActData.setData.refID = 0;
                varActData.setData.value = "false";
            }

            globalIndex = index;

            Holder.getChild(3).GetChild(1).GetComponent<Text>().text = "";

            Scripts.programManager.updateVariables();
        });

        //Create Flex, set Square and Add as Child
        Flex ValDisp = valDisp.GetComponent<ValueDisp>().flex;

        ValDisp.setSquare();

        parent.getChild(rowIndex).GetComponent<FlexInfo>().flex.addChild(ValDisp);

    }
}
