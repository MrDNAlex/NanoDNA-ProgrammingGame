using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;
using DNAStruct;
using UnityEngine.Rendering;

public class ProgramLine : MonoBehaviour
{

    [SerializeField] RectTransform background;

    [SerializeField] GameObject prefab1;
    [SerializeField] GameObject prefab2;


    public Flex Line;

    public GameObject ProgramObj;
    public Flex ProgramUI;

   // Flex Program;


    public void Awake()
    {
        setUI();
        setNumber();
        setButton();
    }

    // Start is called before the first frame update
    void Start()
    {
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
        Flex LineNumber = new Flex(Line.getChild(0), 1);
        ProgramUI = new Flex(Line.getChild(1), 6);
        Flex Drag = new Flex(Line.getChild(2), 1);

        //Add children
        Line.addChild(LineNumber);
        Line.addChild(ProgramUI);
        Line.addChild(Drag);

        //Get extra References
        ProgramObj = Line.getChild(1).gameObject;
        //ProgramUI = ProgramUI;

    }


    public void setNumber()
    {
        //Set the text to the correct number
        Line.getChild(0).GetComponent<Text>().text = Line.UI.GetSiblingIndex().ToString();
        
    }

    public void setNumber(string num)
    {
        //Set the text to the correct number
        Line.getChild(0).GetComponent<Text>().text = num;

    }

    void setButton ()
    {
        //Set the garbage can button
        Line.getChild(2).GetComponent<Button>().onClick.AddListener(deleteLine);
    }

    public void deleteLine ()
    {
        //Loop through all children to delete
        for (int i = 0; i < ProgramUI.UI.childCount; i++)
        {
            Destroy(ProgramUI.UI.GetChild(0).gameObject);
        }

        //Remove all Flex Children
        ProgramUI.deleteAllChildren();

        //Reset color
        background.GetComponent<Image>().color = Color.cyan;

        
    }

    //Switch this to take in a CardInfo
    public void addProgram(CardInfo info)
    {

        //GameObject prefab = Resources.Load("Prefabs/ProgramLine") as GameObject;

        //Exapnd this later

       deleteLine();
        GameObject idk = null;

        switch (info.actionType)
        {
            case ActionType.Movement:
                idk = Instantiate(prefab1, ProgramObj.transform);
                break;
            case ActionType.Variable:
                idk = Instantiate(prefab2, ProgramObj.transform);
                break;
        }

        if (idk != null)
        {

           ProgramUI.addChild(idk.GetComponent<ProgramCard>().program);

            Destroy(idk.GetComponent<DragController2>());

            Line.setSize(Line.size);

            idk.GetComponent<ProgramCard>().progLine = transform;

            idk.AddComponent<DeleteIndentDrag>();

            Camera.main.GetComponent<LevelScript>().progSec.compileProgram();
        }

    }

    public void reAddProgram (ProgramAction action)
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
            //Get rid of Drag controller
            Destroy(program.GetComponent<DragController2>());

            //Add as a Flex child
            ProgramUI.addChild(program.GetComponent<ProgramCard>().program);  

            //Set the transform
            program.GetComponent<ProgramCard>().progLine = transform;

            //Add the indent and delete Drag script
            program.AddComponent<DeleteIndentDrag>();

            //Set size of the component
            Line.setSize(Line.size);

            //Set Info
            program.GetComponent<ProgramCard>().setInfo(action);

           // ProgramObj = program;

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
