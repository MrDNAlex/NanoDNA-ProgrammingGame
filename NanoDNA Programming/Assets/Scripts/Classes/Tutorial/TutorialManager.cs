using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;
using DNAStruct;

public class TutorialManager : MonoBehaviour
{

    [SerializeField] public RectTransform tutorialHolder;

    [SerializeField] GameObject lineObj;

    DialogueScript script;


    Flex Background;
    Flex GridView;
    Flex Zoom;
    Flex Resize;

    Flex ExitButton;
    Flex InteracName;
    Flex InfoButton;

    Flex Content;

    Flex StoreSection;

    Flex CompleteLevel;
    Flex CollectedBackground;
    Flex CollectedProgressBar;
    Flex UsedBackground;
    Flex UsedProgressBar;

    Flex DebugBTN;

    Flex ProgSpeed;


    UIWord tutSc1 = new UIWord("Welcome young programmer!  My name is Mr. Penguin and as you can see I’m in need of a little help.", "");
    UIWord tutSc2 = new UIWord("Although time is of the essence I’m sure you need to get accustomed to the environment.", "");
    UIWord tutSc3 = new UIWord("First if you look in the top right corner of your device you’ll see a satellite view of me and the environment.", "");
    UIWord tutSc4 = new UIWord("As you notice it may be hard to see me from the satellites distance. Luckily it has optical zoom.", "");
    UIWord tutSc5 = new UIWord("The zoom is modified by this slider, by pinching your screen to zoom, or by using your mouse wheel.", "");
    UIWord tutSc6 = new UIWord("Similarly you can move the screen by clicking and dragging. Try and zoom in closer to see me.", "");
    UIWord tutSc7 = new UIWord("Awesome! I can’t see you but I imagine you see my beautiful feathers! You can reset the view by using the highlighted button below the slider.", "");
    UIWord tutSc8 = new UIWord("Now, to the left of the view, you will see a list of blocks. This is where you will program and send us instructions. We will come back to it later.", "");
    UIWord tutSc9 = new UIWord("The first part of the program line displays the line number and allows you to drag to switch lines.", "");
    UIWord tutSc10 = new UIWord("Next is the program holder, this is where you drag the program card into.", "");
    UIWord tutSc11 = new UIWord("Last is the delete icon, once clicked it deletes the program on the line. You can also drag the card to the left to do the same.", "");
    UIWord tutSc12 = new UIWord("Below the map view you will see the store section. This is where you will get your programming blocks to complete levels.", "");
    UIWord tutSc13 = new UIWord("The first section of the store are the tabs. Each tab has program blocks of a certain category.", "");
    UIWord tutSc14 = new UIWord("Below that are all the cards that belong to the category.", "");
    UIWord tutSc15 = new UIWord("Beside the Store we have the constraints section. It focuses on 3 things.", "");
    UIWord tutSc16 = new UIWord("First the number of objects you collected. Your goal is to fill this bar by the end of the level.", "");
    UIWord tutSc17 = new UIWord("Second, how many lines you are using. This bar needs to be as empty as possible. If it’s full your program won’t run.", "");
    UIWord tutSc18 = new UIWord("And last of all the complete button.", "");
    UIWord tutSc19 = new UIWord("Now it’s time to make your first program.", "");
    UIWord tutSc20 = new UIWord("Start by dragging a “Move” block into the first line of my program.", "");
    UIWord tutSc21 = new UIWord("Like a true natural! Now the direction and distance must be changed.", "");
    UIWord tutSc22 = new UIWord("To change the direction click on the button with the arrow and select (direction for the level).", "");
    UIWord tutSc23 = new UIWord("Next, to change the distance, click the button displaying 0, click to enter text, and input (value). After that click “Set”.", "");
    UIWord tutSc24 = new UIWord("Perfect! Your first program is now ready to test! We need to make sure there are no errors.", "");
    UIWord tutSc25 = new UIWord("To do so click the debug button.", "");
    UIWord tutSc26 = new UIWord("Success! I received the instructions and executed them!", "");
    UIWord tutSc27 = new UIWord("Click the debug button again to put me back to the start.", "");
    UIWord tutSc28 = new UIWord("Now that we know it works we can try the next step.", "");
    UIWord tutSc29 = new UIWord("Add another “Move” block in the second line of the program. Make sure it goes (direction) for (value) distance.", "");
    UIWord tutSc30 = new UIWord("Awesome! Now before debugging again, let’s increase the speed of the program to 4x. Click the button a few times to do so.", "");
    UIWord tutSc31 = new UIWord("Now we will debug it.", "");
    UIWord tutSc32 = new UIWord("Perfect! Your program worked! Before you complete your first day I will show you other helpful things.", "");
    UIWord tutSc33 = new UIWord("Click the debugging button again.", "");
    UIWord tutSc34 = new UIWord("Now I’ll have you try to manipulate your program.", "");
    UIWord tutSc35 = new UIWord("Try switching the order of the program. To do so click and drag the icon beside the line number and slide it underneath the second line.", "");
    UIWord tutSc36 = new UIWord("Awesome! Switching the order of your program can sometimes result in new results so take advantage.", "");
    UIWord tutSc37 = new UIWord("Let’s switch it back to the correct order.", "");
    UIWord tutSc38 = new UIWord("Now let’s try deleting a program block.", "");
    UIWord tutSc39 = new UIWord("Start by placing a new move block below your current program.", "");
    UIWord tutSc40 = new UIWord("Now let’s pretend we didn’t need it, to delete it, click the trash icon, or slide the program block to the left.", "");
    UIWord tutSc41 = new UIWord("Congratulations! That’s everything you need to know to become the world’s best programmer!", "");
    UIWord tutSc42 = new UIWord("If you ever need hints or help you can click the info icon at the top.", "");
    UIWord tutSc43 = new UIWord("And if you want to leave the level you can use the exit button.", "");
    UIWord tutSc44 = new UIWord("Now you can complete the level by clicking the complete button.", "");
    UIWord tutSc45 = new UIWord("I wish you luck in your future endeavors, I hope we meet again when you become the world’s best!", "");


    //Section 1
    Flex MapArea;

    private void Awake()
    {
        Scripts.tutorialManager = this;
        setUI();

        //Create script
        script = new DialogueScript();

        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc1));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc2));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc3));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc4));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc5));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc6));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc7));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc8));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc9));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc10));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc11));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc12));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc13));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc14));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc15));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc16));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc17));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc18));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc19));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc20));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc21));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc22));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc23));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc24));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc25));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc26));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc27));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc28));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc29));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc30));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc31));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc32));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc33));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc34));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc35));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc36));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc37));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc38));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc39));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc40));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc41));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc42));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc43));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc44));
        script.addDialogue(new Dialogue("Images/Dialogue/Tutorial/Tutorial", tutSc45));
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setUI()
    {
        Background = new Flex(tutorialHolder, 1);

        Flex Reg1 = new Flex(Background.getChild(0), 4f, Background);
        Flex Header = new Flex(Reg1.getChild(0), 1, Reg1);
        Flex List = new Flex(Reg1.getChild(1), 8, Reg1);
        Flex SV = new Flex(List.getChild(0), 1, List);
        Flex VP = new Flex(SV.getChild(0), 10, SV);

        Flex Controls = new Flex(Header.getChild(0), 1.5f, Header);

        Flex ControlHolder = new Flex(Controls.getChild(0), 1, Controls);

        ExitButton = new Flex(ControlHolder.getChild(0), 1, ControlHolder);

        InteracName = new Flex(ControlHolder.getChild(1), 4, ControlHolder);

        InfoButton = new Flex(ControlHolder.getChild(2), 1, ControlHolder);

        Flex ScriptsTabs = new Flex(Header.getChild(1), 1);

        Flex Reg2 = new Flex(Background.getChild(1), 6f, Background);
        Flex MapView = new Flex(Reg2.getChild(0), 2f, Reg2);

        if (((float)Screen.width / Screen.height) >= 1.8f)
        {
            MapArea = new Flex(MapView.getChild(0), 14, MapView);
        }
        else
        {
            MapArea = new Flex(MapView.getChild(0), 11, MapView);
        }

        Flex UIHolder = new Flex(MapView.getChild(1), 1, MapView);

        Zoom = new Flex(UIHolder.getChild(0), 6, UIHolder);
        Flex Buttons = new Flex(UIHolder.getChild(1), 3, UIHolder);
        ProgSpeed = new Flex(Buttons.getChild(1), 1, Buttons);
        Resize = new Flex(Buttons.getChild(0), 1, Buttons);
        DebugBTN = new Flex(Buttons.getChild(2), 1, Buttons);

        Flex Reg3 = new Flex(Reg2.getChild(1), 1f, Reg2);

        Flex Constraints = new Flex(Reg3.getChild(1), 1f, Reg3);

        Flex ProgressHolder = new Flex(Constraints.getChild(0), 2, Constraints);

        Flex CollectedHolder = new Flex(ProgressHolder.getChild(0), 1, ProgressHolder);
        CollectedBackground = new Flex(CollectedHolder.getChild(0), 1, CollectedHolder);
        CollectedProgressBar = new Flex(CollectedHolder.getChild(1), 2f, CollectedHolder);

        Flex UsedHolder = new Flex(ProgressHolder.getChild(1), 1, ProgressHolder);
        UsedBackground = new Flex(UsedHolder.getChild(0), 1, UsedHolder);
        UsedProgressBar = new Flex(UsedHolder.getChild(1), 2f, UsedHolder);

        CompleteLevel = new Flex(Constraints.getChild(1), 1, Constraints);

        //Add Children
        Content = new Flex(VP.getChild(0), 1, VP);
        Content.setChildMultiH((Screen.height * 0.9f) / 5.5f);

        addChildren(Content);

        StoreSection = storeSection(Reg3);

        Reg3.addChild(StoreSection);

        //Squares
        //ProgSpeed.setSquare();
        //Resize.setSquare();
        //DebugBTN.setSquare();

        //Calculate leftover height, and fix the size of the Zoom slider
        Background.setSize(new Vector2(Screen.width, Screen.height));

        MapView.setSize(new Vector2(MapView.size.x, MapView.size.x * ((float)Screen.height / Screen.width)));

        Reg3.setSize(new Vector2(Reg3.size.x, Screen.height - MapView.size.y));

        Zoom.setSize(new Vector2(Zoom.size.x, Scripts.levelScript.MapView.getChild(0).GetChild(0).GetComponent<FlexInfo>().flex.size.y + Scripts.levelScript.MapView.getChild(0).GetComponent<VerticalLayoutGroup>().spacing + 10));
        Buttons.setSize(new Vector2(Buttons.size.x, MapArea.size.y - Zoom.size.y));


        CollectedBackground.setSize(new Vector2(CollectedBackground.size.x, CollectedBackground.size.x));
        UsedBackground.setSize(new Vector2(UsedBackground.size.x, UsedBackground.size.x));

        CollectedProgressBar.setSize(new Vector2(CollectedProgressBar.size.x, CollectedHolder.size.y - CollectedHolder.size.x));
        UsedProgressBar.setSize(new Vector2(UsedProgressBar.size.x, UsedHolder.size.y - UsedHolder.size.x));

        ExitButton.setSize(new Vector2(ExitButton.size.y, ExitButton.size.y));
        InfoButton.setSize(new Vector2(InfoButton.size.y, InfoButton.size.y));

        InteracName.setSize(new Vector2(ControlHolder.size.x - (ControlHolder.size.y * 2) + (ControlHolder.size.y - ExitButton.size.y), InteracName.size.y));

        //Set Images
        /*
        UIHelper.setImage(ExitButton.UI, PlayerSettings.colourScheme.getAccent());
        UIHelper.setImage(InfoButton.UI, PlayerSettings.colourScheme.getAccent());

        UIHelper.setImage(Header.UI, PlayerSettings.colourScheme.getSecondary(true));
        UIHelper.setImage(Constraints.UI, PlayerSettings.colourScheme.getSecondary(true));
        UIHelper.setImage(List.UI, PlayerSettings.colourScheme.getMain(true));

        UIHelper.setImage(ProgSpeed.UI, PlayerSettings.colourScheme.getMain());
        UIHelper.setImage(Resize.UI, PlayerSettings.colourScheme.getMain());
        UIHelper.setImage(DebugBTN.UI, PlayerSettings.colourScheme.getMain());

        UIHelper.setImage(Zoom.UI.GetChild(0), PlayerSettings.colourScheme.getMain());
        UIHelper.setImage(Zoom.UI.GetChild(1).GetChild(0), PlayerSettings.colourScheme.getSecondary());
        UIHelper.setImage(Zoom.UI.GetChild(2).GetChild(0), PlayerSettings.colourScheme.getAccent());

        UIHelper.setImage(Reg3.getChild(1).GetChild(1).GetChild(0).GetChild(0), PlayerSettings.colourScheme.getMain(true));
        UIHelper.setImage(Reg3.getChild(1).GetChild(1).GetChild(0), PlayerSettings.colourScheme.getMain(true));
        UIHelper.setImage(Reg3.getChild(1).GetChild(1), PlayerSettings.colourScheme.getMain(true));

        UIHelper.setImage(CompleteLevel.UI, PlayerSettings.colourScheme.getAccent());

        //UIHelper.setImage(Save.UI, PlayerSettings.colourScheme.getAccent());
        // UIHelper.setImage(Undo.UI, PlayerSettings.colourScheme.getAccent());

        UIHelper.setImage(UsedBackground.UI, PlayerSettings.colourScheme.getAccent());
        UIHelper.setImage(CollectedBackground.UI, PlayerSettings.colourScheme.getAccent());

        UIHelper.setImage(UsedProgressBar.UI, PlayerSettings.colourScheme.getAccent());
        UIHelper.setImage(CollectedProgressBar.UI, PlayerSettings.colourScheme.getAccent());

        //Set background
        UIHelper.setImage(UsedProgressBar.getChild(0), PlayerSettings.colourScheme.getMain());
        UIHelper.setImage(UsedProgressBar.getChild(1), PlayerSettings.colourScheme.getMain());
        UIHelper.setImage(CollectedProgressBar.getChild(0), PlayerSettings.colourScheme.getMain());
        UIHelper.setImage(CollectedProgressBar.getChild(1), PlayerSettings.colourScheme.getMain());
        */

        GridView.UI.GetComponent<GridLayoutGroup>().cellSize = new Vector2(((GridView.size.x) / 3), ((GridView.size.x / 3) - GridView.UI.GetComponent<GridLayoutGroup>().spacing.x) / 1.5f);

        LayoutRebuilder.ForceRebuildLayoutImmediate(Background.UI);

    }

    void addChildren(Flex parent)
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject programLine = Instantiate(lineObj, parent.UI);

            Flex progLine = new Flex(programLine.GetComponent<RectTransform>(), 1, parent);

            Flex Num = new Flex(progLine.getChild(0), 1.25f, progLine);
            Flex Program = new Flex(progLine.getChild(1), 6, progLine);
            Flex Trash = new Flex(progLine.getChild(2), 1, progLine);
        }
    }

    Flex storeSection(Flex parent)
    {
        Flex Store = new Flex(parent.getChild(0), 4f);

        Flex StoreHeader = new Flex(Store.getChild(0), 2, Store);
        Flex VP = new Flex(StoreHeader.getChild(0), 1, StoreHeader);
        Flex Content = new Flex(VP.getChild(0), 1, VP);

        Flex SV = new Flex(Store.getChild(1), 8, Store);
        Flex GridVP = new Flex(SV.getChild(0), 1, SV);
        GridView = new Flex(GridVP.getChild(0), 1, GridVP);

        Content.setChildMultiW(500);

        foreach (ActionType tag in System.Enum.GetValues(typeof(ActionType)))
        {
            GameObject storeHeader = Instantiate(lineObj, Content.UI);
            Content.addChild(new Flex(storeHeader.GetComponent<RectTransform>(), 1));
        }

        for (int i = 0; i < 12; i++)
        {
            GameObject storeCard = Instantiate(lineObj, GridView.UI);
            GridView.addChild(new Flex(storeCard.GetComponent<RectTransform>(), 1));
        }

        return Store;
    }

    void hideImage(Flex flex)
    {
        flex.UI.GetComponent<Image>().enabled = false;
        flex.UI.GetComponent<Image>().raycastTarget = false;
    }

    void hideImage(Transform trans)
    {
        trans.GetComponent<Image>().enabled = false;

        trans.GetComponent<Image>().raycastTarget = false;
    }

    void showImage(Flex flex)
    {
        flex.UI.GetComponent<Image>().enabled = true;
        flex.UI.GetComponent<Image>().raycastTarget = true;
    }
    void showImage(Transform trans)
    {
        trans.GetComponent<Image>().enabled = true;
        trans.GetComponent<Image>().raycastTarget = true;
    }

    //Think about shifting all the reveal stuff by one function?

    public void StartTutorialDialogue()
    {
        tutAct1();
    }

    void tutAct1()
    {
        Scripts.dialogueManager.StartDialogue(2, tutAct2, TextAnchor.LowerCenter , new List<DialogueCondition>(), script);
    }

    void tutAct2()
    {
        Debug.Log("Act 2");
        //Unhide the Map view thing and do the script explaining that
        //Shows Map
        hideImage(MapArea);
        Scripts.dialogueManager.continueDialogue(2, tutAct3, TextAnchor.LowerLeft, new List<DialogueCondition>());
    }

    void tutAct3()
    {
        Debug.Log("Act 3");
        //Show zoom control and wait for it
        hideImage(Zoom);
        Scripts.dialogueManager.continueDialogue(2, tutAct4, TextAnchor.LowerLeft, new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.SliderChange, Scripts.mapDrag.zoomTrans, 0), new DialogueCondition(DialogueManager.DialogueConditionType.WaitCondition, null, 2) });
    }

    void tutAct4()
    {
        Debug.Log("Act 4");
        //See my feathers
        hideImage(Resize);
        showImage(MapArea);
        showImage(Zoom);
        Scripts.dialogueManager.continueDialogue(1, tutAct5, TextAnchor.LowerLeft, new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.ButtonClick, Scripts.mapDrag.resizeTrans, 0)});

    }

    void tutAct5()
    {
        //Show Programming section 
        Debug.Log("Act 5");
        showImage(Resize);


        hideImage(ExitButton);
        hideImage(InteracName);
        hideImage(InfoButton);

        foreach (Transform child in Content.UI)
        {
            hideImage(child.GetComponent<FlexInfo>().flex);
        }

        Scripts.dialogueManager.continueDialogue(1, showProgram1, TextAnchor.LowerRight ,new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.WaitCondition, null, 0.5f) });

    }

    //Alternative path

    void showProgram1 ()
    {
        //Show Line number
        showImage(ExitButton);
        showImage(InteracName);
        showImage(InfoButton);

        foreach (Transform child in Content.UI)
        {
            showImage(child.GetComponent<FlexInfo>().flex);
        }

        hideImage(Content.getChild(0));

        showImage(Content.getChild(0).GetChild(1));
        showImage(Content.getChild(0).GetChild(2));

        Scripts.dialogueManager.continueDialogue(1, showProgram2, TextAnchor.LowerRight, new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.WaitCondition, null, 0.5f) });

    }

    void showProgram2()
    {
        showImage(Content.getChild(0).GetChild(0));
        hideImage(Content.getChild(0).GetChild(1));

        Scripts.dialogueManager.continueDialogue(1, showProgram3, TextAnchor.LowerRight, new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.WaitCondition, null, 0.5f) });
    }

    void showProgram3()
    {
        showImage(Content.getChild(0).GetChild(1));
        hideImage(Content.getChild(0).GetChild(2));

        Scripts.dialogueManager.continueDialogue(1, tutAct6, TextAnchor.LowerRight, new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.WaitCondition, null, 0.5f) });
    }

    void tutAct6()
    {
        //Unhide store
        Debug.Log("Act 6");

        hideImage(Content.getChild(0).GetChild(0));
        hideImage(Content.getChild(0).GetChild(1));
        hideImage(Content.getChild(0).GetChild(2));

        showImage(Content.getChild(0));

        //Hide the programming Section


        //Reveal store section
        foreach (Transform child in StoreSection.getChild(0).GetChild(0).GetChild(0))
        {
            hideImage(child.GetComponent<FlexInfo>().flex);
        }

        foreach (Transform child in StoreSection.getChild(1).GetChild(0).GetChild(0))
        {
            hideImage(child.GetComponent<FlexInfo>().flex);
        }

        Scripts.dialogueManager.continueDialogue(1, showStore1, TextAnchor.UpperLeft, new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.WaitCondition, null, 0.5f) });

    }

    void showStore1 ()
    {
        foreach (Transform child in StoreSection.getChild(1).GetChild(0).GetChild(0))
        {
            showImage(child.GetComponent<FlexInfo>().flex);
        }

        Scripts.dialogueManager.continueDialogue(1, showStore2, TextAnchor.UpperLeft, new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.WaitCondition, null, 0.5f) });
    }

    void showStore2()
    {
        foreach (Transform child in StoreSection.getChild(0).GetChild(0).GetChild(0))
        {
            showImage(child.GetComponent<FlexInfo>().flex);
        }

        foreach (Transform child in StoreSection.getChild(1).GetChild(0).GetChild(0))
        {
            hideImage(child.GetComponent<FlexInfo>().flex);
        }

        Scripts.dialogueManager.continueDialogue(1, tutAct7, TextAnchor.UpperLeft, new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.WaitCondition, null, 0.5f) });
    }

    void tutAct7()
    {
        //Show the constraint section entirely
        Debug.Log("Act 7");

        //Hide store section
        foreach (Transform child in StoreSection.getChild(0).GetChild(0).GetChild(0))
        {
            showImage(child.GetComponent<FlexInfo>().flex);
        }

        foreach (Transform child in StoreSection.getChild(1).GetChild(0).GetChild(0))
        {
            showImage(child.GetComponent<FlexInfo>().flex);
        }

        //Reveal constraint section

        hideImage(CompleteLevel);
        hideImage(UsedBackground);
        hideImage(UsedProgressBar);
        hideImage(CollectedBackground);
        hideImage(CollectedProgressBar);

        Scripts.dialogueManager.continueDialogue(1, tutAct8, TextAnchor.UpperLeft, new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.WaitCondition, null, 0.5f) });

    }

    void tutAct8()
    {
        //Reveal collected only
        Debug.Log("Act 8");

        showImage(CompleteLevel);
        showImage(UsedBackground);
        showImage(UsedProgressBar);

        Scripts.dialogueManager.continueDialogue(1, tutAct9, TextAnchor.UpperLeft, new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.WaitCondition, null, 0.5f) });
    }

    void tutAct9()
    {
        //Reveal Used Lines only
        Debug.Log("Act 9");

        //hideImage(CompleteLevel);
        hideImage(UsedBackground);
        hideImage(UsedProgressBar);
        showImage(CollectedBackground);
        showImage(CollectedProgressBar);

        Scripts.dialogueManager.continueDialogue(1, tutAct10, TextAnchor.UpperLeft, new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.WaitCondition, null, 0.5f) });

    }

    void tutAct10()
    {
        //Reveal Complete button
        Debug.Log("Act 10");

        hideImage(CompleteLevel);
        showImage(UsedBackground);
        showImage(UsedProgressBar);

        Scripts.dialogueManager.continueDialogue(1, tutAct11, TextAnchor.UpperLeft, new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.WaitCondition, null, 0.5f) });
    }

    void tutAct11()
    {
        //Dialogue to start making the first program
        Debug.Log("Act 11");
        showImage(CompleteLevel);

        Scripts.dialogueManager.continueDialogue(1, tutAct12, TextAnchor.UpperRight, new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.WaitCondition, null, 0.5f) });
    }

    void tutAct12()
    {
        //Reveal thing needed for the first program
        Debug.Log("Act 12");

        hideImage(StoreSection.getChild(0).GetChild(0).GetChild(0).GetChild(0));
        hideImage(StoreSection.getChild(1).GetChild(0).GetChild(0).GetChild(0));

        hideImage(Content.getChild(0));

        //Add the wait for new program line condition
        Scripts.dialogueManager.continueDialogue(1, tutAct13, TextAnchor.UpperRight ,new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.CustomCondition, null, 0, checkProgramFirstLine) });

    }

    void tutAct13()
    {
        //Congratulate, and ask for the direction to change
        Debug.Log("Act 13");

        showImage(StoreSection.getChild(0).GetChild(0).GetChild(0).GetChild(0));
        showImage(StoreSection.getChild(1).GetChild(0).GetChild(0).GetChild(0));

        Scripts.dialogueManager.continueDialogue(2, tutAct14, TextAnchor.LowerCenter , new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.CustomCondition, null, 0, checkDirectionChange) });

    }

    void tutAct14()
    {
        //Ask for the value to change
        Debug.Log("Act 14");

        Scripts.dialogueManager.continueDialogue(1, tutAct15, TextAnchor.LowerCenter , new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.CustomCondition, null, 0, checkValueChange) });

    }

    void tutAct15()
    {
        //Hide the line access
        Debug.Log("Act 15");

        showImage(Content.getChild(0));

        Scripts.dialogueManager.continueDialogue(1, tutAct16, TextAnchor.LowerCenter,  new List<DialogueCondition>());
    }

    void tutAct16()
    {
        Debug.Log("Act 16");

        //Show Debug Button
        hideImage(DebugBTN);

        Scripts.dialogueManager.continueDialogue(1, tutAct17, TextAnchor.LowerLeft ,new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.ButtonClick, Scripts.programSection.debug.transform, 0), new DialogueCondition(DialogueManager.DialogueConditionType.CustomCondition, null, 0, revealMap), new DialogueCondition(DialogueManager.DialogueConditionType.WaitCondition, null, 5) });
    }

    void tutAct17 ()
    {
        //Ask to reset everything through debug
        Debug.Log("Act 17");
        showImage(MapArea);

        hideImage(DebugBTN);

        Scripts.dialogueManager.continueDialogue(2, tutAct18, TextAnchor.LowerLeft, new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.ButtonClick, Scripts.programSection.debug.transform, 0) });

    }

    void tutAct18()
    {
        //Hide everything
        Debug.Log("Act 18");

        showImage(DebugBTN);

        Scripts.dialogueManager.continueDialogue(1, tutAct19,TextAnchor.LowerCenter , new List<DialogueCondition>());

    }

    void tutAct19()
    {
        //Reveal second line 
        Debug.Log("Act 19");

        hideImage(StoreSection.getChild(0).GetChild(0).GetChild(0).GetChild(0));
        hideImage(StoreSection.getChild(1).GetChild(0).GetChild(0).GetChild(0));

        hideImage(Content.getChild(1));

        Scripts.dialogueManager.continueDialogue(1, tutAct20, TextAnchor.UpperRight, new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.CustomCondition, null, 0, checkSecondLine)});
    }

    void tutAct20()
    {
        //Reveal speed button
        Debug.Log("Act 20");

        showImage(StoreSection.getChild(0).GetChild(0).GetChild(0).GetChild(0));
        showImage(StoreSection.getChild(1).GetChild(0).GetChild(0).GetChild(0));

        showImage(Content.getChild(1));

        hideImage(ProgSpeed);

        Scripts.dialogueManager.continueDialogue(1, tutAct21,TextAnchor.LowerLeft , new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.CustomCondition, null, 0, checkProgramSpeed) });

    }

    void tutAct21()
    {
        //Get another debug click
        Debug.Log("Act 21");

        showImage(ProgSpeed);

        hideImage(DebugBTN);

        Scripts.dialogueManager.continueDialogue(1, tutAct22, TextAnchor.LowerLeft, new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.ButtonClick, Scripts.programSection.debug.transform, 0), new DialogueCondition(DialogueManager.DialogueConditionType.CustomCondition, null, 0, revealMap), new DialogueCondition(DialogueManager.DialogueConditionType.WaitCondition, null, 3) });


    }

    void tutAct22 ()
    {
        //Finish second debug and click it again
        Debug.Log("Act 22");

        showImage(MapArea);
        hideImage(DebugBTN);

        Scripts.dialogueManager.continueDialogue(2, tutAct23, TextAnchor.LowerLeft, new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.ButtonClick, Scripts.programSection.debug.transform, 0) });
    }

    void tutAct23 ()
    {
        //Set Up for line switching 
        Debug.Log("Act 23");

        showImage(DebugBTN);

        hideImage(Content.getChild(0));
        hideImage(Content.getChild(1));
        //Show the first and second line

        Scripts.dialogueManager.continueDialogue(2, tutAct24,TextAnchor.LowerRight ,new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.CustomCondition, null, 0, checkNewOrder) });
    }

    void tutAct24 ()
    {
        //Congratulate and wait for switch back
        Debug.Log("Act 24");

        Scripts.dialogueManager.continueDialogue(2, tutAct25, TextAnchor.LowerRight, new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.CustomCondition, null, 0, checkOldOrder) });

    }

    void tutAct25 ()
    {
        //Hide the 2 lines
        Debug.Log("Act 25");

        showImage(Content.getChild(0));
        showImage(Content.getChild(1));


        Scripts.dialogueManager.continueDialogue(1, tutAct26, TextAnchor.LowerCenter ,new List<DialogueCondition>());

    }

    void tutAct26 ()
    {
        //Unhide to place new block
        Debug.Log("Act 26");

        hideImage(Content.getChild(2));
        hideImage(StoreSection.getChild(0).GetChild(0).GetChild(0).GetChild(0));
        hideImage(StoreSection.getChild(1).GetChild(0).GetChild(0).GetChild(0));


        Scripts.dialogueManager.continueDialogue(1, tutAct27, TextAnchor.UpperRight ,new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.CustomCondition, null, 0, checkDelPlace) });
    }

    void tutAct27 ()
    {
        //Wait for delete of the line
        Debug.Log("Act 27");

        showImage(StoreSection.getChild(0).GetChild(0).GetChild(0).GetChild(0));
        showImage(StoreSection.getChild(1).GetChild(0).GetChild(0).GetChild(0));

        Scripts.dialogueManager.continueDialogue(1, tutAct28, TextAnchor.UpperRight, new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.CustomCondition, null, 0, checkDel) });
    }

    void tutAct28 ()
    {
        //Congratulate on end
        Debug.Log("Act 28");
        showImage(Content.getChild(2));

        Scripts.dialogueManager.continueDialogue(1, tutAct29, TextAnchor.LowerCenter, new List<DialogueCondition> ());
    }

    void tutAct29 ()
    {
        //Show Info Icon
        Debug.Log("Act 29");

        hideImage(InfoButton);

        Scripts.dialogueManager.continueDialogue(1, tutAct30, TextAnchor.LowerCenter,new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.WaitCondition, null, 1) });

    }

    void tutAct30 ()
    {
        //Show Exit button
        Debug.Log("Act 30");

        showImage(InfoButton);

        hideImage(ExitButton);

        Scripts.dialogueManager.continueDialogue(1, tutAct31, TextAnchor.LowerCenter ,new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.WaitCondition, null, 1) });

    }

    void tutAct31 ()
    {
        //show complete button and let it complete
        Debug.Log("Act 31");
        showImage(ExitButton);

        hideImage(CompleteLevel);

        Scripts.dialogueManager.continueDialogue(2, tutAct32, TextAnchor.UpperLeft,new List<DialogueCondition> { new DialogueCondition(DialogueManager.DialogueConditionType.ButtonClick, Scripts.levelScript.CompleteLevel.UI, 0) });

    }
    
    void tutAct32 ()
    {
        Debug.Log("Act 32 and complete");

        hideImage(MapArea);
    }


    //Custom Condition functions
    bool checkProgramFirstLine()
    {
        //Get reference to the program holder of the first line

        if (Scripts.programSection.selectedCharData.program.getProgramActionType(0) == ActionType.Movement && Scripts.programSection.selectedCharData.program.getMovementActionName(0) == MovementActionNames.Move)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    bool checkDirectionChange()
    {
        //Check if the direction is changed
        if (Scripts.programSection.selectedCharData.program.list[0].moveData.dir == Direction.Right)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool checkValueChange()
    {
        //Check if the value changed
        if (Scripts.programSection.selectedCharData.program.list[0].moveData.value == "5")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool revealMap()
    {
        hideImage(MapArea);

        showImage(DebugBTN);

        return true;
    }

    bool checkSecondLine()
    {
        if (Scripts.programSection.selectedCharData.program.getProgramActionType(1) == ActionType.Movement && Scripts.programSection.selectedCharData.program.getMovementActionName(1) == MovementActionNames.Move)
        {
            //Check if the direction is changed
            if (Scripts.programSection.selectedCharData.program.list[1].moveData.dir == Direction.Up)
            {
                if (Scripts.programSection.selectedCharData.program.list[1].moveData.value == "5")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    bool checkProgramSpeed()
    {
        if (Scripts.programSection.speed == ProgramSpeed.Op3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool checkNewOrder ()
    {
        if (Scripts.programSection.selectedCharData.program.getProgramActionType(0) == ActionType.Movement && Scripts.programSection.selectedCharData.program.getMovementActionName(0) == MovementActionNames.Move)
        {
            //Check if the direction is changed
            if (Scripts.programSection.selectedCharData.program.list[0].moveData.dir == Direction.Up)
            {
                if (Scripts.programSection.selectedCharData.program.list[0].moveData.value == "5")
                {
                    if (Scripts.programSection.selectedCharData.program.getProgramActionType(1) == ActionType.Movement && Scripts.programSection.selectedCharData.program.getMovementActionName(1) == MovementActionNames.Move)
                    {
                        //Check if the direction is changed
                        if (Scripts.programSection.selectedCharData.program.list[1].moveData.dir == Direction.Right)
                        {
                            if (Scripts.programSection.selectedCharData.program.list[1].moveData.value == "5")
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    bool checkOldOrder()
    {
        if (Scripts.programSection.selectedCharData.program.getProgramActionType(0) == ActionType.Movement && Scripts.programSection.selectedCharData.program.getMovementActionName(0) == MovementActionNames.Move)
        {
            //Check if the direction is changed
            if (Scripts.programSection.selectedCharData.program.list[0].moveData.dir == Direction.Right)
            {
                if (Scripts.programSection.selectedCharData.program.list[0].moveData.value == "5")
                {
                    if (Scripts.programSection.selectedCharData.program.getProgramActionType(1) == ActionType.Movement && Scripts.programSection.selectedCharData.program.getMovementActionName(1) == MovementActionNames.Move)
                    {
                        //Check if the direction is changed
                        if (Scripts.programSection.selectedCharData.program.list[1].moveData.dir == Direction.Up)
                        {
                            if (Scripts.programSection.selectedCharData.program.list[1].moveData.value == "5")
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    bool checkDelPlace ()
    {
        if (Scripts.programSection.selectedCharData.program.getProgramActionType(2) == ActionType.Movement && Scripts.programSection.selectedCharData.program.getMovementActionName(2) == MovementActionNames.Move)
        {
            //Check if the direction is changed
            if (Scripts.programSection.selectedCharData.program.list[2].moveData.dir == Direction.Up)
            {
                if (Scripts.programSection.selectedCharData.program.list[2].moveData.value == "0")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    bool checkDel ()
    {
        if (Scripts.programSection.selectedCharData.program.getProgramActionType(2) == ActionType.Movement && Scripts.programSection.selectedCharData.program.getMovementActionName(2) == MovementActionNames.None)
        {
            return true;
        } else
        {
            return false;
        }
    }

}
