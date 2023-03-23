using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;
using FlexUI;
using UnityEngine.UI;
using DNASaveSystem;
using DNAMathAnimation;

public class MultiChoiceValController : EditValController
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

        StartCoroutine(DNAMathAnim.animateReboundRelocationLocal(ParentTrans, OriginalPos, 200, 1, true));

        
    }

    void setColours()
    {
        //Background
        UIHelper.setImage(Holder.UI, PlayerSettings.colourScheme.getAccent());

        UIHelper.setImage(Holder.getChild(1), PlayerSettings.colourScheme.getMain());
    }

    void setUI()
    {
        Parent = new Flex(ParentTrans.GetComponent<RectTransform>(), 1);

        Holder = new Flex(this.GetComponent<RectTransform>(), 1, Parent);

        Flex ExitRow = new Flex(Holder.getChild(0), 1f);

        Flex Exit = new Flex(ExitRow.getChild(0), 1);

        Flex GridView = new Flex(Holder.getChild(1), 9);

        Flex GridRow1;
        Flex GridRow2;
        Flex GridRow3;
        //Flex GridRow3;

        if (panelInfo.valEditType == ValueEditType.Public || panelInfo.valEditType == ValueEditType.Bool)
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

    }

    void setText()
    {

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

     
            //On Listener
            valDisp.GetComponent<Button>().onClick.AddListener(delegate
            {
                //Set Var Data
                progCard.action.varActData = varActData;

                //Set data
                setData(index);

                progCard.action = progCard.Iprogram.createAction();

                //Set Info
                progCard.Iprogram.setInfo();

                Scripts.programSection.setAction(progCard.action, progCard.lineIndex);

                ParentTrans.gameObject.SetActive(false);

                Destroy(this.gameObject);

                Scripts.programManager.updateVariables();
            });
      
        //Create Flex, set Square and Add as Child
        Flex ValDisp = valDisp.GetComponent<ValueDisp>().flex;

        ValDisp.setSquare();

        parent.getChild(rowIndex).GetComponent<FlexInfo>().flex.addChild(ValDisp);

    }

}
