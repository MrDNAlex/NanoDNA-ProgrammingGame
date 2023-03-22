using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.UI;
using DNAStruct;

public class MathVariable : ProgramCard, IProgramCard
{
    private void Awake()
    {
        Iprogram = this;
        getLineNumber();
        getReferences();
        setUI();
    }

    // Start is called before the first frame update
    void Start()
    {

        setInfo();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void setUI()
    {
        //Flex Variable Init
        program = new Flex(rectTrans, 2);

        Flex VarName = new Flex(program.getChild(0), 1f, program);
        Flex VarSign = new Flex(program.getChild(1), 0.5f, program);

        //Section that will hold it's own Math Program
        Flex MathHolder = new Flex(program.getChild(2), 2, program);

        program.setSpacingFlex(0.3f, 1);

        program.setAllPadSame(0.3f, 1);
    }

    public void setInfo()
    {
        string path = "";
        //Make sure it can only get a reference

        //Set the box collider
        transform.GetChild(2).gameObject.AddComponent<BoxCollider2D>();

        transform.GetChild(2).GetComponent<BoxCollider2D>().size = transform.GetChild(2).GetComponent<FlexInfo>().flex.size;


        if (action.varData.refID != 0)
        {
            UIHelper.setText(transform.GetChild(0).GetChild(0), Scripts.programManager.getVariableName(action.varData), PlayerSettings.colourScheme.getBlackTextColor());
        }
        else
        {
            //Get the first variable that can possible be grabbed and set the 
            List<VariableData> varData = Scripts.programManager.getVariables(VariableType.Number);

            action.varData = varData[0];

            action.varData.refID = varData[0].id;

            UIHelper.setText(transform.GetChild(0).GetChild(0), Scripts.programManager.getVariableName(action.varData), PlayerSettings.colourScheme.getBlackTextColor());
        }

        //Spawn the math block

        switch (action.varData.mathType)
        {
            case MathTypes.None:

                //Delete if there are any present

                break;
            case MathTypes.Addition:
                GameObject math = GameObject.Instantiate(Resources.Load("Prefabs/MathOperations/Addition") as GameObject, program.getChild(2));

                Flex flex = math.GetComponent<MathOperations>().flex;

                program.getChild(2).GetComponent<FlexInfo>().flex.addChild(flex);

                program.setSize(program.size);
                break;
        }
    }

    //Spawn Panel
    public void setAction()
    {
        action = createAction();

       transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
        {
            if (noPanelOpen())
            {
                //Set Panel Type
                panelInfo.editDataType = EditDataType.Variable;

                //Set Editable Variable Type
                panelInfo.varType = VariableType.Text;

                //Set the Data type it will change
                panelInfo.valEditType = ValueEditType.Name;

                EditValController.genPanel(this);
            }

        });

        transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate
        {
            if (noPanelOpen())
            {
                //Set Panel Type
                panelInfo.editDataType = EditDataType.Multichoice;

                //Set Editable Variable Type
                panelInfo.varType = VariableType.Number;

                //Set the Data type it will change
                panelInfo.valEditType = ValueEditType.MathOperation;

                EditValController.genPanel(this);
            }
        });
    }

    public ProgramAction createAction()
    {
        return new ProgramAction(actionInfo, action.varData);
    }

}
