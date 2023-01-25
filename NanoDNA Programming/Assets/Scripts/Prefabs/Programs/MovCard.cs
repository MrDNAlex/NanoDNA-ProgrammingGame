using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;

public class MovCard : MonoBehaviour
{
    public Flex Card;

    public string cardType = "mov";

    public void Awake()
    {
       
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

    public void setUI ()
    {
        Card = new Flex(transform.GetComponent<RectTransform>(), 1);


        Flex Program = new Flex(Card.getChild(0), 2);

        Flex Move = new Flex(Program.getChild(0), 1f);
        Flex Direction = new Flex(Program.getChild(1), 1);
        Flex Value = new Flex(Program.getChild(2), 1);

       


        //Flex Mov = new Flex(Card.getChild(0), 2);
        Flex Name = new Flex(Card.getChild(1), 1);

        Card.addChild(Program);
        Card.addChild(Name);

        Card.setSpacingFlex(0.3f, 1);


        Program.addChild(Move);
        Program.addChild(Direction);
        Program.addChild(Value);

        Program.setSpacingFlex(0.4f, 1);

        Program.setAllPadSame(0.1f, 1);




    }



}
