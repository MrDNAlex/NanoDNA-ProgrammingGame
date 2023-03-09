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
        UIHelper.setImage(Line.UI, SaveManager.loadPlaySettings().colourScheme.getMain(true));
        UIHelper.setImage(LineNumberHolder.UI, SaveManager.loadPlaySettings().colourScheme.getSecondary(true));

    }

    public void setNumber()
    {
        //Set the text to the correct number

        UIHelper.setText(Line.getChild(0).GetChild(0), Line.UI.GetSiblingIndex().ToString(), SaveManager.loadPlaySettings().colourScheme.getAccentTextColor());

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
                ProgramCard = Instantiate(prefab2, ProgramObj.transform);
                break;
            case ActionType.Action:
                ProgramCard = Instantiate(prefab3, ProgramObj.transform);
                break;
        }

        if (ProgramCard != null)
        {
            ProgramCard.name += transform.GetSiblingIndex();

            ProgramUI.addChild(ProgramCard.GetComponent<ProgramCard>().program);

            //Destroy(ProgramCard.GetComponent<DragController2>());

            Line.setSize(Line.size);

            ProgramCard.GetComponent<ProgramCard>().setEditable();

            ProgramCard.GetComponent<ProgramCard>().progLine = transform;

            ProgramCard.AddComponent<DeleteIndentDrag>();

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
                program = Instantiate(prefab2, ProgramObj.transform);
                break;
            default:
                Debug.Log(action);
                break;
        }

        if (program != null)
        {
            program.name += transform.GetSiblingIndex();

            //Add as a Flex child
            ProgramUI.addChild(program.GetComponent<ProgramCard>().program);

            //Set the transform
            program.GetComponent<ProgramCard>().progLine = transform;

            //Set size of the component
            Line.setSize(Line.size);

            //Add the indent and delete Drag script
            program.AddComponent<DeleteIndentDrag>();

            //Set Info
            program.GetComponent<ProgramCard>().setInfo(action);

            //Make it editable
            program.GetComponent<ProgramCard>().setEditable();

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
