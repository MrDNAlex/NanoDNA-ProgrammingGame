using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;

public class Program : MonoBehaviour
{

    public Flex program;
    public string cardType;
    public Transform progLine;
    public int indent = 0;


    public ProgramAction action;
    public string dir;
    public int value;



    private void Awake()
    {
        //Debug.Log("Awake 1");
        setUI();
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

        //Check the type of card it will be
        if (cardType == "move")
        {
            //Flex Variable Init
            program = new Flex(transform.GetComponent<RectTransform>(), 2);

            Flex Move = new Flex(program.getChild(0), 1f);
            Flex Direction = new Flex(program.getChild(1), 1);
            Flex Value = new Flex(program.getChild(2), 1);


            //Set Flex Parameters
            program.addChild(Move);
            program.addChild(Direction);
            program.addChild(Value);

            program.setSpacingFlex(0.4f, 1);

            program.setAllPadSame(0.3f, 1);

            setActionMovement();


        } else if (cardType == "var")
        {
            //Flex Variable Init
            program = new Flex(transform.GetComponent<RectTransform>(), 2);

            Flex Var = new Flex(program.getChild(0), 2);
            Flex VarName = new Flex(program.getChild(1), 1);
            Flex Sign = new Flex(program.getChild(2), 1);
            Flex Val = new Flex(program.getChild(3), 1);


            //Set Flex Parameters
            program.addChild(Var);
            program.addChild(VarName);
            program.addChild(Sign);
            program.addChild(Val);

            program.setSpacingFlex(1, 1);

            program.setAllPadSame(0.2f, 1);
        }
       
    }


    public void setActionMovement ()
    {
        //createAction("up", 1);

        transform.GetChild(1).GetComponent<Dropdown>().onValueChanged.AddListener(delegate
        {
           //Get Direction
            dir = transform.GetChild(1).GetComponent<Dropdown>().options[transform.GetChild(1).GetComponent<Dropdown>().value].text.ToLower();

            //Create Action
            createAction(dir, value);
        });

        transform.GetChild(2).GetComponent<InputField>().onEndEdit.AddListener(delegate
        {
           //Get Value
           value = int.Parse(transform.GetChild(2).GetComponent<InputField>().textComponent.text);

            //Create Action
            createAction(dir, value);
        });


    }

    public void createAction(string dir, int val)
    {
       
        action = new ProgramAction("move", dir, val);

    }




}
