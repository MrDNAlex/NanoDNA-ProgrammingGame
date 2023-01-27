using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;

public class Variable : MonoBehaviour
{
    public Flex Program;

    public string cardType = "var";

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

        Program = new Flex(transform.GetComponent<RectTransform>(), 2);

        Flex Var = new Flex(Program.getChild(0), 2);
        Flex VarName = new Flex(Program.getChild(1), 1);
        Flex Sign = new Flex(Program.getChild(2), 1);
        Flex Val = new Flex(Program.getChild(3), 1);

        Program.addChild(Var);
        Program.addChild(VarName);
        Program.addChild(Sign);
        Program.addChild(Val);

        Program.setSpacingFlex(1, 1);

        Program.setAllPadSame(0.2f, 1);

        

    }
}
