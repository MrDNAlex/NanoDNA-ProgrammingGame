using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;
using DNAStruct;

public class ProgramCard : MonoBehaviour
{

    public StoreTag storeTag;
    //public string storeTag;
    public string cardType; //eventually replace this with the storeTag I think
    public string cardName;

    public MovementCardNames movementName;
    public MathCardNames mathName;
    public LogicCardNames logicName;
    public VariableCardNames variableName;

    //public CardInfo info;

    public Flex program;
    public Transform progLine;
    public int indent = 0;

    //Action Stuff
    public ProgramAction action;
    public string dir = "up";
    public int value = 0;

    public bool setInf = false;

    ProgramCardFunctionality functionality;


    private void Awake()
    {

        functionality = new ProgramCardFunctionality();

        setFunctionality();

        if (dir == null)
        {
            dir = "up";
        }
        if (value == null)
        {
            value = 0;
        }

        

    }


    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setFunctionality()
    {
        //Set the UI
        program = functionality.setUI(setCardInfo());

        functionality.setAction(setCardInfo());

        //Set the Functionality
        
           // setActionMovement();
       
    }


    public void setActionMovement ()
    {
        action = createAction(dir, value);

        transform.GetChild(1).GetComponent<Dropdown>().onValueChanged.AddListener(delegate
        {
            
           //Get Direction
            dir = transform.GetChild(1).GetComponent<Dropdown>().options[transform.GetChild(1).GetComponent<Dropdown>().value].text.ToLower();

            //Create Action
            action = createAction(dir, value);

            
            if (Camera.main.GetComponent<LevelScript>().progSec.undo == false && setInf == false)
            {
                Camera.main.GetComponent<LevelScript>().progSec.compileProgram();
            } 
            
        });

        transform.GetChild(2).GetComponent<InputField>().onEndEdit.AddListener(delegate
        {

           if (transform.GetChild(2).GetComponent<InputField>().textComponent.text != null)
            {
                //Get Value
                value = int.Parse(transform.GetChild(2).GetComponent<InputField>().textComponent.text);
            } else
            {
                value = 0;
            }

            //Create Action
            action = createAction(dir, value);

            
            if (Camera.main.GetComponent<LevelScript>().progSec.undo == false && setInf == false)
            {
                Camera.main.GetComponent<LevelScript>().progSec.compileProgram();
            }
            
        });
    }

    public ProgramAction createAction(string dir, int val)
    {
        return new ProgramAction("move", dir, val);
    }

    public void setInfo (ProgramAction action)
    {
        //Maybe switch these with a CardInfo type structure, same with the action

        //The action type will have a reference to a function for it's handle so that when the program is being read it doens't need to go around and search for said function, it just instantly runs it, associated with it will be the types and values it needs!

        //Make sure it won't compile
        setInf = true;

        //Set info
        cardType = action.type;
        value = action.value;
        dir = action.dir;

        this.action = createAction(action.dir, action.value);

        //Actually paste info on the UI
        functionality.setInfo(setCardInfo());

        //Make sure it won't compile
        setInf = false;

    }

    public CardInfo setCardInfo ()
    {
        CardInfo info = new CardInfo();

        info.storeTag = storeTag;

        info.movementName = movementName;
        info.mathName = mathName;
        info.logicName = logicName;
        info.variableName = variableName;


       // info.storeTag = storeTag;
        //info.cardName = cardName;
        info.cardType = cardType;

        info.flex = program;
        info.rectTrans = transform.GetComponent<RectTransform>();
        info.transform = transform;
        info.action = action;

        info.programCard = this;

        return info;

    }

}
