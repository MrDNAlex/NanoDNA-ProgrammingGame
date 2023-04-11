using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;
using FlexUI;
using UnityEngine.UI;
using DNASaveSystem;
using DNAMathAnimation;

public class VariableValController : EditValController
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

        setControls();

        OriginalPos = ParentTrans.localPosition;

        Vector3 startPos = OriginalPos + new Vector3(0, -Screen.height, 0);

        ParentTrans.localPosition = startPos;

        StartCoroutine(DNAMathAnim.animateReboundRelocationLocal(ParentTrans, OriginalPos, DNAMathAnim.getFrameNumber(1f), 1, true));

    }

    void setColours()
    {
        UIHelper.setImage(Holder.UI, PlayerSettings.colourScheme.getAccent());

        UIHelper.setImage(Holder.getChild(1), PlayerSettings.colourScheme.getMain());

        UIHelper.setImage(Holder.getChild(1).GetChild(0).GetChild(1), PlayerSettings.colourScheme.getSecondary());

        UIHelper.setImage(Holder.getChild(1).GetChild(1).GetChild(0), PlayerSettings.colourScheme.getSecondary());
    }

    void setUI()
    {
        Parent = new Flex(ParentTrans.GetComponent<RectTransform>(), 1);

        Holder = new Flex(this.GetComponent<RectTransform>(), 1, Parent);

        Flex ExitRow = new Flex(Holder.getChild(0), 1f, Holder);

        Flex Exit = new Flex(ExitRow.getChild(0), 1, ExitRow);

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

        Content.setChildMultiH(100);

        //Add Variables to Content
        //Loop through

        List<VariableData> vars = new List<VariableData>();

        vars.AddRange(Scripts.programManager.getVariables(DNAStruct.VariableType.Number));
        vars.AddRange(Scripts.programManager.getVariables(DNAStruct.VariableType.Decimal));
        vars.AddRange(Scripts.programManager.getVariables(DNAStruct.VariableType.Text));

        foreach (VariableData varData in vars)
        {

            bool display = false;


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


            if (display)
            {
                GameObject variable = Instantiate(Resources.Load("Prefabs/EditPanels/ValueDisp") as GameObject, Content.UI.transform);

                variable.GetComponent<ValueDisp>().setUI();

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
                            this.varActData.refID = variable.GetComponent<ValueDisp>().varData.id;
                           // this.varActData.setData.value = variable.GetComponent<ValueDisp>().varData.name;
                            this.value = variable.GetComponent<ValueDisp>().varData.name;
                            if (!PlayerSettings.advancedVariables)
                            {
                                this.varActData.setData.isPublic = true;
                            }
                            break;
                        case ActionType.Action:
                            this.actData.refID = variable.GetComponent<ValueDisp>().varData.id;
                            this.actData.data = variable.GetComponent<ValueDisp>().varData.name;
                            this.value = variable.GetComponent<ValueDisp>().varData.name;
                            break;

                    }

                    Scripts.programManager.displayAllVariables();

                    Holder.getChild(1).GetChild(1).GetChild(1).GetComponent<Text>().text = "";
                });
            }
        }

        Parent.setSize(new Vector2(Screen.height * 0.7f * 1.3f, Screen.height * 0.7f));
    }

    void setText()
    {
        UIHelper.setText(Holder.getChild(1).GetChild(1).GetChild(0).GetChild(0), LangDictionary.setbtn.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor());

        UIHelper.setText(Holder.getChild(1).GetChild(0).GetChild(0), LangDictionary.variable.getWord(lang) + " (" + getVarType(varType) + ")", PlayerSettings.colourScheme.getBlackTextColor());

        UIHelper.setText(Holder.getChild(1).GetChild(1).GetChild(1), "", PlayerSettings.colourScheme.getBlackTextColor());
    }

    void setControls()
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

        //Holder Button 
        Holder.getChild(1).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
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

}
