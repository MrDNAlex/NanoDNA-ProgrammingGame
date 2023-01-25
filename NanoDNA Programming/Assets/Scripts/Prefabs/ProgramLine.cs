using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;

public class ProgramLine : MonoBehaviour
{

    [SerializeField] RectTransform background;

    public Flex Line;

    public GameObject ProgramObj;
    public Flex ProgramUI;


    public void Awake()
    {
        setUI();
        setNumber();
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
        Flex Program = new Flex(Line.getChild(1), 6);
        Flex Drag = new Flex(Line.getChild(2), 1);

        //Add children
        Line.addChild(LineNumber);
        Line.addChild(Program);
        Line.addChild(Drag);

        //Get extra References
        ProgramObj = Line.getChild(1).gameObject;
        ProgramUI = Program;

    }


    void setNumber()
    {
        //Set the text to the correct number
        Line.getChild(0).GetComponent<Text>().text = Line.UI.GetSiblingIndex().ToString();
    }


    //Add a function to slide to one side to delete the line







}