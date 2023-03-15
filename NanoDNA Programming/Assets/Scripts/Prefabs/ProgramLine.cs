using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;
using DNAStruct;
using UnityEngine.Rendering;
using DNASaveSystem;

public class ProgramLine : MonoBehaviour
{

    [SerializeField] RectTransform background;

    [SerializeField] GameObject prefab1;
    [SerializeField] GameObject prefab2;
    [SerializeField] GameObject prefab3;
    [SerializeField] GameObject simpVar;

    public Flex Line;

    public GameObject ProgramObj;
    public Flex ProgramUI;

    // Flex Program;

    Scripts allScripts;


    public void Awake()
    {
        setUI();
        setNumber();
        setButton(transform.GetSiblingIndex());
    }

    // Start is called before the first frame update
    void Start()
    {
        allScripts = Camera.main.GetComponent<LevelScript>().allScripts;

        OnDemandRendering.renderFrameInterval = 12;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setUI()
    {
        //Define all the Flex components
        Line = new Flex(background, 1);
        Flex LineNumberHolder = new Flex(Line.getChild(0), 1);
        Flex LineNumber = new Flex(LineNumberHolder.getChild(0), 1);

        ProgramUI = new Flex(Line.getChild(1), 6);
        Flex Drag = new Flex(Line.getChild(2), 1);

        //Add children
        Line.addChild(LineNumberHolder);
        Line.addChild(ProgramUI);
        Line.addChild(Drag);

        LineNumberHolder.addChild(LineNumber);

        //Get extra References
        ProgramObj = Line.getChild(1).gameObject;
        //ProgramUI = ProgramUI;

        //Set Images
        UIHelper.setImage(Line.UI, PlayerSettings.colourScheme.getMain(true));
        UIHelper.setImage(LineNumberHolder.UI, PlayerSettings.colourScheme.getSecondary(true));

    }

    public void setNumber()
    {
        //Set the text to the correct number

        UIHelper.setText(Line.getChild(0).GetChild(0), (Line.UI.GetSiblingIndex() + 1).ToString(), PlayerSettings.colourScheme.getAccentTextColor());

    }

    void setButton(int index)
    {
        //Set the garbage can button
        Line.getChild(2).GetComponent<Button>().onClick.AddListener(delegate
        {
            deleteProgramLine(index);

            allScripts.programSection.selectedCharData.displayProgram(true);

        });
    }

    public void deleteLine()
    {
        //Loop through all children to delete
        for (int i = 0; i < ProgramUI.UI.childCount; i++)
        {
            Destroy(ProgramUI.UI.GetChild(0).gameObject);
        }

        //Remove all Flex Children
        ProgramUI.deleteAllChildren();

        //Reset color
        background.GetComponent<Image>().color = Color.white;

    }

    public void deleteProgramLine(int index)
    {
        deleteLine();

        Program prog = allScripts.programSection.selectedCharData.program;

        prog.RemoveLine(index);

        allScripts.levelManager.updateConstraints();

    }

    //Switch this to take in a CardInfo
    public void addProgram(CardInfo info, Transform trans)
    {
        //Exapnd this later
        deleteLine();
        GameObject ProgramCard = null;
        switch (info.actionType)
        {
            case ActionType.Movement:
                ProgramCard = Instantiate(prefab1, ProgramObj.transform);
                break;
            case ActionType.Variable:

                if (PlayerSettings.advancedVariables)
                {
                    ProgramCard = Instantiate(prefab2, ProgramObj.transform);
                } else
                {
                    ProgramCard = Instantiate(simpVar, ProgramObj.transform);
                }
               
                break;
            case ActionType.Action:
                ProgramCard = Instantiate(prefab3, ProgramObj.transform);
                break;
        }

        if (ProgramCard != null)
        {
            ProgramCard card = ProgramCard.GetComponent<ProgramCard>();

            ProgramCard.name += transform.GetSiblingIndex();

            ProgramUI.addChild(card.program);

            //Destroy(ProgramCard.GetComponent<DragController2>());

            Line.setSize(Line.size);

            card.progLine = transform;

            ProgramCard.AddComponent<DeleteIndentDrag>();

            card.setEditable();

           // Camera.main.GetComponent<LevelScript>().allScripts.programSection.selectedCharData.program.setAction(card.action, transform.parent.parent.GetSiblingIndex());

            allScripts.levelManager.updateConstraints();
        }
    }

    public void reAddProgram(ProgramAction action)
    {
        //Delete line
        deleteLine();

        GameObject program = null;

        //Edit this later
        switch (action.actionType)
        {
            case ActionType.Movement:

                switch (action.movementName)
                {
                    case MovementActionNames.Move:
                        program = Instantiate(prefab1, ProgramObj.transform);
                        break;
                }
                break;
            case ActionType.Variable:
                if (PlayerSettings.advancedVariables)
                {
                    program = Instantiate(prefab2, ProgramObj.transform);
                }
                else
                {
                   program = Instantiate(simpVar, ProgramObj.transform);
                }
                break;
            default:
                Debug.Log(action);
                break;
        }

        if (program != null)
        {
            ProgramCard card = program.GetComponent<ProgramCard>();

            program.name += transform.GetSiblingIndex();

            //Add as a Flex child
            ProgramUI.addChild(card.program);

            //Set the transform
            card.progLine = transform;

            //Set size of the component
            Line.setSize(Line.size);

            //Add the indent and delete Drag script
            program.AddComponent<DeleteIndentDrag>();

            //Set Info
            card.setInfo(action);

            //Make it editable
            card.setEditable();

            allScripts.levelManager.updateConstraints();
        }
    }

    public void destroySubChildren(GameObject Obj)
    {
        //Only deletes children under the one referenced
        foreach (Transform child in Obj.transform)
        {
            if (child.childCount == 0)
            {
                //Safe to delete
                Destroy(child.gameObject);
            }
            else
            {
                //Delete chidlren first
                destroySubChildren(child.gameObject);

                Destroy(child.gameObject);
            }
        }
    }

    






}
