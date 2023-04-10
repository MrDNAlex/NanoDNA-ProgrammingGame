using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.UI;
using DNAStruct;

public class MathAddition : ProgramCard, IProgramCard
{
    private void Awake()
    {
        Iprogram = this;
        getLineNumber();
        getReferences();
        setUI();

        action.varActData.actionType = VariableActionType.ChangeVal;
        action.varActData.mathData.operationType = MathOperationTypes.Addition;
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
        Flex Equal = new Flex(program.getChild(1), 0.5f, program);

        Flex FirstVal = new Flex(program.getChild(2), 1, program);
        Flex VarSign = new Flex(program.getChild(3), 0.5f, program);
        Flex SecondVal = new Flex(program.getChild(4), 1, program);

        program.setSpacingFlex(0.3f, 1);

        program.setAllPadSame(0.3f, 1);
    }

    public void setInfo()
    {
      
        //Make sure it can only get a reference

        if (action.varActData.refID != 0)
        {
            UIHelper.setText(transform.GetChild(0).GetChild(0), Scripts.programManager.getVariableName(action.varActData.refID), PlayerSettings.colourScheme.getBlackTextColor());

            action.varActData.mathData.varType = Scripts.programManager.getVariableType(action.varActData.refID);
        }
        else
        {
            //Get the first variable that can possible be grabbed and set the 
            List<VariableData> varData = Scripts.programManager.getVariables(VariableType.Number);

            if (varData.Count > 0)
            {
                action.varActData.refID = varData[0].id;
            }

            UIHelper.setText(transform.GetChild(0).GetChild(0), Scripts.programManager.getVariableName(action.varActData.refID), PlayerSettings.colourScheme.getBlackTextColor());

            action.varActData.mathData.varType = Scripts.programManager.getVariableType(action.varActData.refID);
        }

        //First Value

        if (action.varActData.mathData.refID1 != 0)
        {
            UIHelper.setText(transform.GetChild(2).GetChild(0), Scripts.programManager.getVariableName(action.varActData.mathData.refID1), PlayerSettings.colourScheme.getBlackTextColor());
        } else
        {
            UIHelper.setText(transform.GetChild(2).GetChild(0), action.varActData.mathData.value1, PlayerSettings.colourScheme.getBlackTextColor());
        }

        if (action.varActData.mathData.refID2 != 0)
        {
            UIHelper.setText(transform.GetChild(4).GetChild(0), Scripts.programManager.getVariableName(action.varActData.mathData.refID2), PlayerSettings.colourScheme.getBlackTextColor());
        }
        else
        {
            UIHelper.setText(transform.GetChild(4).GetChild(0), action.varActData.mathData.value2, PlayerSettings.colourScheme.getBlackTextColor());
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
                panelInfo.varType = action.varActData.mathData.varType;

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
                panelInfo.editDataType = EditDataType.Value;

                //Set Editable Variable Type
                panelInfo.varType = action.varActData.mathData.varType;

                //Set the Data type it will change
                panelInfo.valEditType = ValueEditType.Value1;

                EditValController.genPanel(this);
            }
        });

        transform.GetChild(4).GetComponent<Button>().onClick.AddListener(delegate
        {
            if (noPanelOpen())
            {
                //Set Panel Type
                panelInfo.editDataType = EditDataType.Value;

                //Set Editable Variable Type
                panelInfo.varType = action.varActData.mathData.varType;

                //Set the Data type it will change
                panelInfo.valEditType = ValueEditType.Value2;

                EditValController.genPanel(this);
            }
        });
    }

    public ProgramAction createAction()
    {
        return new ProgramAction(actionInfo, action.varActData);
    }
}
