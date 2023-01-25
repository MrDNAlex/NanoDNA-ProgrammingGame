using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;
using UnityEngine.EventSystems;

public class ProgramBlock : MonoBehaviour
{

    //Add a system that takes in a Script type and it reloads itself into a different block shaped like the script. 





    [SerializeField] RectTransform background;
    [SerializeField] RectTransform lineNumber;
    [SerializeField] RectTransform program;
    [SerializeField] RectTransform drag;

    public Flex Block;
    public int num;

    public Button.ButtonClickedEvent onClick;

    private void Awake()
    {
        //Create the Flex for the component
        //Debug.Log("Start");
        programBlock();
        setNumber();
        onClick = background.GetComponent<Button>().onClick;
    }

    // Start is called before the first frame update
    void Start()
    {
        //setUI();
       
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    void setUI ()
    {
        Flex Background = new Flex(background, 1);
        Flex LineNumber = new Flex(lineNumber, 1);
        Flex Program = new Flex(program, 8);
        Flex Drag = new Flex(drag, 1);

        Background.addChild(LineNumber);
        Background.addChild(Program);
        Background.addChild(Drag);

        //Background.setSize(new Vector2(500, 100));

    }

    void programBlock ()
    {
        //Define all the Flex components
        Block = new Flex(background, 1);
        Flex LineNumber = new Flex(lineNumber, 1);
        Flex Program = new Flex(program, 6);
        Flex Drag = new Flex(drag, 1);

        //Add children
        Block.addChild(LineNumber);
        Block.addChild(Program);
        Block.addChild(Drag);

        //Drag.setSquare();

       // return Background;
    }


    void setNumber ()
    {
        lineNumber.GetComponent<Text>().text = num.ToString();
    }

  


}
