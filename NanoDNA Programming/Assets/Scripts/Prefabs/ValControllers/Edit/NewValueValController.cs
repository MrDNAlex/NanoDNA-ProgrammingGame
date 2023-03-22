using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;
using FlexUI;
using UnityEngine.UI;
using DNASaveSystem;
using DNAMathAnimation;

public class NewValueValController : EditValController
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
        varData = progCard.action.varData;

        moveData = progCard.action.moveData;

        actData = progCard.action.actData;

        setUI();

        setColours();

        setControls(progCard.panelInfo);

        OriginalPos = ParentTrans.localPosition;

        Vector3 startPos = OriginalPos + new Vector3(0, -Screen.height, 0);

        ParentTrans.localPosition = startPos;

        StartCoroutine(DNAMathAnim.animateReboundRelocationLocal(ParentTrans, OriginalPos, 200, 1, true));


    }

    void setColours()
    {
        UIHelper.setImage(Holder.UI, PlayerSettings.colourScheme.getAccent());

        UIHelper.setImage(Holder.getChild(1), PlayerSettings.colourScheme.getMain());

        UIHelper.setImage(Holder.getChild(1).GetChild(0).GetChild(0).GetChild(1), PlayerSettings.colourScheme.getSecondary());

        UIHelper.setImage(Holder.getChild(1).GetChild(1).GetChild(0), PlayerSettings.colourScheme.getSecondary());
    }

    void setUI()
    {
        Parent = new Flex(ParentTrans.GetComponent<RectTransform>(), 1);

        Holder = new Flex(this.GetComponent<RectTransform>(), 1);

        Flex ExitRow = new Flex(Holder.getChild(0), 1f, Holder);

        // Empty = new Flex(ExitRow.getChild(0), 9);
        Flex Exit = new Flex(ExitRow.getChild(0), 1, ExitRow);

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

    }

    void setText()
    {
        UIHelper.setText(Holder.getChild(1).GetChild(0).GetChild(0).GetChild(0), LangDictionary.newVariable.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor());

        UIHelper.setText(Holder.getChild(1).GetChild(1).GetChild(0).GetChild(0), LangDictionary.setbtn.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor());

        UIHelper.setText(Holder.getChild(1).GetChild(0).GetChild(0).GetChild(1).GetChild(0), LangDictionary.EntText.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor());

        UIHelper.setText(Holder.getChild(1).GetChild(0).GetChild(0).GetChild(1).GetChild(1), "", PlayerSettings.colourScheme.getBlackTextColor());
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
            progCard.action.varData = varData;

            //Set Data
            setData(0);

            //Create and set the Action
            progCard.action = progCard.Iprogram.createAction();

            //Set Info in ProgramCardFunc
            progCard.Iprogram.setInfo();

            //Set the action
            Scripts.programSection.selectedCharData.program.setAction(progCard.action, progCard.lineIndex);

            //Hide object and Destroy
            ParentTrans.gameObject.SetActive(false);
            Destroy(this.gameObject);

            Scripts.programManager.updateVariables();

        });


    }

   
}
