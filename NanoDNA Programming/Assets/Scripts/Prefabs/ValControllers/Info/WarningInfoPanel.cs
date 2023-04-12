using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;
using DNAMathAnimation;
using FlexUI;
using UnityEngine.UI;
using DNAScenes;
using UnityEngine.SceneManagement;

public class WarningInfoPanel : InfoPanelController
{

    UIWord quitMessage = new UIWord("Are you sure you want to quit this level? All your progress will be lost.", "Etes vous certains que vous voulez quittez ce niveau? Toute votre progres seras perdue.");
    UIWord completeMessage = new UIWord("Are you sure your program is complete? Your program will be penalized for missing objectives.", "Etes vous certains que vous voulez soumettre votre programme? Votre program seras penaliser a chaque objectif manquer.");

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

        //  actData = progCard.action.actData;

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

        UIHelper.setImage(Holder.getChild(1).GetChild(1), PlayerSettings.colourScheme.getSecondary());

        switch (panelType)
        {
            case InfoPanelType.Quit:

                UIHelper.setImage(Holder.getChild(1).GetChild(1).GetChild(1), "Images/UIDesigns/ExitButton");

                break;

            case InfoPanelType.Complete:

                UIHelper.setImage(Holder.getChild(1).GetChild(1).GetChild(1), "Images/UIDesigns/CompleteButton");

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


        switch (panelType)
        {
            case InfoPanelType.Quit:

                Holder.getChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
                {
                    SceneManager.LoadScene(SceneConversion.GetScene(Scenes.Menu));

                });

                break;
            case InfoPanelType.Complete:

                Holder.getChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
                {
                    //Complete the level
                    //Run final program
                   Scripts.programSection.completeLevel();

                   // Debug.Log("Complete");

                    ParentTrans.gameObject.SetActive(false);

                    Destroy(this.gameObject);

                   
                   // Scripts.programSection.runFinalProgram();
                });

                break;
        }
    }

    void setText()
    {
        switch (panelType)
        {
            case InfoPanelType.Quit:

                UIHelper.setText(Holder.getChild(1).GetChild(0), quitMessage.getWord(lang), PlayerSettings.colourScheme.textColorBlack);

                break;
            case InfoPanelType.Complete:

                UIHelper.setText(Holder.getChild(1).GetChild(0), completeMessage.getWord(lang), PlayerSettings.colourScheme.textColorBlack);

                break;
        }

    }

    void setUI()
    {
        Parent = new Flex(ParentTrans.GetComponent<RectTransform>(), 1);

        Holder = new Flex(this.GetComponent<RectTransform>(), 1, Parent);

        Flex ExitRow = new Flex(Holder.getChild(0), 1, Holder);

        Flex Exit = new Flex(ExitRow.getChild(0), 1, ExitRow);

        Flex PanelInfo = new Flex(Holder.getChild(1), 9, Holder);

        Flex QuitText = new Flex(PanelInfo.getChild(0), 8, PanelInfo);

        Flex QuitButton = new Flex(PanelInfo.getChild(1), 2, PanelInfo);

        Flex QuitButtonIcon = new Flex(QuitButton.getChild(1), 1, QuitButton);

        Exit.setSquare();
        QuitButtonIcon.setSquare();

        ExitRow.setSelfHorizontalPadding(0, 1, 0.02f, 1);

        Holder.setVerticalPadding(0.1f, 1, 0.1f, 1);
        Holder.setHorizontalPadding(0.01f, 1, 0.01f, 1);

        QuitText.setSelfHorizontalPadding(0.1f, 1, 0.1f, 1);

        //PanelInfo.setHorizontalPadding(0.1f, 1, 0.1f, 1);
        //QuitButton.setSelfHorizontalPadding(0.5f, 1, 0.5f, 1);

        QuitButton.setVerticalPadding(0.05f, 1, 0.05f, 1);

        Holder.setSpacingFlex(0.1f, 1);

        Parent.setSize(new Vector2(Screen.height * 0.7f * 1.3f, Screen.height * 0.7f));

    }





}
