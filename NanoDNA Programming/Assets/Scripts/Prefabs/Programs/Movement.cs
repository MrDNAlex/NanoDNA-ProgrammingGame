using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{

    public Flex Program;

    public string cardType = "mov";

    private void Awake()
    {
        Debug.Log("Awake 1");
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

        Program = new Flex(transform.GetComponent<RectTransform>(), 2);

        Flex Move = new Flex(Program.getChild(0), 1f);
        Flex Direction = new Flex(Program.getChild(1), 1);
        Flex Value = new Flex(Program.getChild(2), 1);

        Program.addChild(Move);
        Program.addChild(Direction);
        Program.addChild(Value);

        Program.setSpacingFlex(0.4f, 1);

        Program.setAllPadSame(0.1f, 1);

    }











}
