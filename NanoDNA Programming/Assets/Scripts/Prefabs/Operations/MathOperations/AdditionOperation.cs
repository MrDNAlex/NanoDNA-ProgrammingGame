using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;
using DNAStruct;

public class AdditionOperation : MathOperations, IMathOperation
{

    private void Awake()
    {
        IOperation = this;
        getReferences();
        setUI();
        Debug.Log("Awake");
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

    void setUI ()
    {
        flex = new Flex(rectTrans, 1);

        Flex Num1 = new Flex(flex.getChild(0), 1, flex);
        Flex Plus = new Flex(flex.getChild(1), 0.5f, flex);
        Flex Num2 = new Flex(flex.getChild(2), 1, flex);

        flex.setSpacingFlex(0.2f, 1);
    }

    //Set UI

    //Set current info 

    //Set Controls

    public void setInfo()
    {
        //Set Value of Num 1
        if (operationInfo.refID1 != 0)
        {
            UIHelper.setText(rectTrans.GetChild(0).GetChild(0), Scripts.programManager.getVariableName(operationInfo.refID1), PlayerSettings.colourScheme.getBlackTextColor());
        }
        else
        {
            UIHelper.setText(rectTrans.GetChild(0).GetChild(0), operationInfo.value1, PlayerSettings.colourScheme.getBlackTextColor());
        }

        //Set Value of Num 2
        if (operationInfo.refID2 != 0)
        {
            UIHelper.setText(rectTrans.GetChild(2).GetChild(0), Scripts.programManager.getVariableName(operationInfo.refID2), PlayerSettings.colourScheme.getBlackTextColor());
        }
        else
        {
            UIHelper.setText(rectTrans.GetChild(2).GetChild(0), operationInfo.value2, PlayerSettings.colourScheme.getBlackTextColor());
        }
    }

    //Spawn Panel
    public void setAction()
    {
       // action = createAction();

        //Value 1
        transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
        {
            //Debug.Log("Click");
            if (noPanelOpen())
            {
                //Debug.Log("Panel not open");
                //Set Panel Type
                panelInfo.editDataType = EditDataType.Value;

                //Set Editable Variable Type
                panelInfo.varType = VariableType.Number;

                //Set the Data type it will change
                panelInfo.valEditType = ValueEditType.Value;

                EditValController.genPanel(this, 0);
            }
        });

        //Value 2
        transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate
        {
            if (noPanelOpen())
            {
                //Set Panel Type
                panelInfo.editDataType = EditDataType.Value;

                //Set Editable Variable Type
                panelInfo.varType = VariableType.Number;

                //Set the Data type it will change
                panelInfo.valEditType = ValueEditType.Value;

                EditValController.genPanel(this, 1);
            }
        });
    }

  //  public ProgramAction createAction()
   //{
        //Create a default math operation of like 0+0
        //return new ProgramAction(actionInfo, action.moveData);
   // }
   








}
