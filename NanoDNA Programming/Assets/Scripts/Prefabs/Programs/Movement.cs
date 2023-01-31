using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{

    public Flex Program;

    public string cardType = "move";

    public ProgramAction action;

    public string dir = "up";
    public int value = 0;

    private void Awake()
    {
        //Debug.Log("Awake 1");
        setUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        createAction("up", 1);

        transform.GetChild(1).GetComponent<Dropdown>().onValueChanged.AddListener(delegate
        {
            Debug.Log("Drop");
            dir = transform.GetChild(1).GetComponent<Dropdown>().options[transform.GetChild(1).GetComponent<Dropdown>().value].ToString().ToLower();

            createAction( dir, value);
        });

        transform.GetChild(2).GetComponent<InputField>().onValueChanged.AddListener(delegate
        {
            Debug.Log("Text");
            //Surround with try?
            value = int.Parse(transform.GetChild(1).GetComponent<InputField>().textComponent.text);

            createAction(dir, value);
        });
        */


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUI ()
    {

        Program = new Flex(transform.GetComponent<RectTransform>(), 2);

        Flex Move = new Flex(Program.getChild(0), 1f);
        Flex Direction = new Flex(Program.getChild(1), 1);
        Flex Value = new Flex(Program.getChild(2), 1);

        Program.addChild(Move);
        Program.addChild(Direction);
        Program.addChild(Value);

        Program.setSpacingFlex(0.4f, 1);

        Program.setAllPadSame(0.3f, 1);

    }

    public void createAction (string dir, int val)
    {
        Debug.Log("action");
       action = new ProgramAction("move", dir, val);

    }














}
