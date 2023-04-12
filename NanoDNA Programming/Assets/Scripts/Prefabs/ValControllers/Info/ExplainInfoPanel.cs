using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;
using DNAMathAnimation;
using FlexUI;
using UnityEngine.UI;
using DNAScenes;
using UnityEngine.SceneManagement;

public class ExplainInfoPanel : InfoPanelController
{

    UIWord quitMessage = new UIWord("Are you sure you want to quit this level? All your progress will be lost.", "Etes vous certains que vous voulez quittez ce niveau? Toute votre progres seras perdue.");
    UIWord completeMessage = new UIWord("Are you sure your program is complete? Your program will be penalized for missing objectives.", "Etes vous certains que vous voulez soumettre votre programme? Votre program seras penaliser a chaque objectif manquer.");

    UIWord next = new UIWord("Next", "Prochain");
    UIWord prev = new UIWord("Previous", "Dernier");

    int pageNum;
    int totalPages;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setPanel(Transform parent, InfoPanelType type)
    {

        this.panelType = type;
        //  VariableTypes.Add(LangDictionary.Text);
        //  VariableTypes.Add(LangDictionary.Number);
        //  VariableTypes.Add(LangDictionary.Decimal);
        //  VariableTypes.Add(LangDictionary.Bool);

        //   this.panelInfo = progCard.panelInfo;
        //   this.actionInfo = progCard.actionInfo;

        //allScripts = Camera.main.GetComponent<LevelScript>().allScripts;

        // this.progCard = progCard;

        lang = PlayerSettings.language;

        ParentTrans = parent;

        //   varType = progCard.panelInfo.varType;

        //Copy existing Data
        //  varActData = progCard.action.varActData;

        //  moveData = progCard.action.moveData;

        //  actData = progCard.action.actDa
        //  ta;





        //Load the info data from the level



        setUI();

        setColours();

        setControls();

        OriginalPos = ParentTrans.localPosition;

        Vector3 startPos = OriginalPos + new Vector3(0, -Screen.height, 0);

        ParentTrans.localPosition = startPos;

        StartCoroutine(DNAMathAnim.animateReboundRelocationLocal(ParentTrans, OriginalPos, DNAMathAnim.getFrameNumber(0.75f), 1, true));
    }

    void setColours()
    {
        //Background
        UIHelper.setImage(Holder.UI, PlayerSettings.colourScheme.getAccent());

        UIHelper.setImage(Holder.getChild(1), PlayerSettings.colourScheme.getMain());

        UIHelper.setImage(Holder.getChild(1).GetChild(2).GetChild(0).GetChild(1), "Images/UIDesigns/Last");
        UIHelper.setImage(Holder.getChild(1).GetChild(2).GetChild(2).GetChild(1), "Images/UIDesigns/Next");

        //UIHelper.setImage(Holder.getChild(1).GetChild(1), PlayerSettings.colourScheme.getSecondary());

        switch (panelType)
        {
            case InfoPanelType.InfoTips:

                UIHelper.setImage(Holder.getChild(1).GetChild(2).GetChild(0), PlayerSettings.colourScheme.getSecondary());

                UIHelper.setImage(Holder.getChild(1).GetChild(2).GetChild(2), PlayerSettings.colourScheme.getSecondary());

                break;

        }

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

        Holder.getChild(1).GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
        {
            //reload the image and description associated with it
            setText();
        });

    }

    void setText()
    {
        string pages = pageNum + "/" + totalPages;
        UIHelper.setText(Holder.getChild(1).GetChild(2).GetChild(1), pages, PlayerSettings.colourScheme.getBlackTextColor());
    }

    void setUI()
    {
        Parent = new Flex(ParentTrans.GetComponent<RectTransform>(), 1);

        Holder = new Flex(this.GetComponent<RectTransform>(), 1, Parent);

        Flex ExitRow = new Flex(Holder.getChild(0), 1, Holder);
        Flex Exit = new Flex(ExitRow.getChild(0), 1, ExitRow);

        Flex PanelInfo = new Flex(Holder.getChild(1), 9, Holder);

        Flex Img = new Flex(PanelInfo.getChild(0), 6f, PanelInfo);
        Flex Description = new Flex(PanelInfo.getChild(1), 2, PanelInfo);
        Flex PageController = new Flex(PanelInfo.getChild(2), 1.5f, PanelInfo);

        Flex PrevBTN = new Flex(PageController.getChild(0), 1, PageController);
        Flex PrevBTNImg = new Flex(PrevBTN.getChild(1), 1, PrevBTN);
        Flex PageNum = new Flex(PageController.getChild(1), 1, PageController);
        Flex NextBTN = new Flex(PageController.getChild(2), 1, PageController);
        Flex NextBTNImg = new Flex(NextBTN.getChild(1), 1, NextBTN);


        Exit.setSquare();
        PrevBTNImg.setSquare();
        NextBTNImg.setSquare();

        

        PanelInfo.setSpacingFlex(0.5f, 1);

        Img.setSelfHorizontalPadding(0.1f, 1, 0.1f, 1);
        Description.setSelfHorizontalPadding(0.1f, 1, 0.1f, 1);

        ExitRow.setSelfHorizontalPadding(0, 1, 0.02f, 1);

        Holder.setVerticalPadding(0.1f, 1, 0.1f, 1);
        Holder.setHorizontalPadding(0.01f, 1, 0.01f, 1);
        Holder.setSpacingFlex(0.1f, 1);

        Parent.setSize(new Vector2(Screen.height * 0.7f * 1.3f, Screen.height * 0.7f));

    }





}
