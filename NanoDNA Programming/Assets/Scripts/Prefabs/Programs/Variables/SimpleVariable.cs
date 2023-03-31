using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.UI;
using DNAStruct;

public class SimpleVariable : ProgramCard, IProgramCard
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
        program = new Flex(this.GetComponent<RectTransform>(), 2);

        Flex VarName = new Flex(program.getChild(0), 1, program);
        Flex VarSign = new Flex(program.getChild(1), 0.5f, program);
        Flex VarValue = new Flex(program.getChild(2), 1, program);

        program.setSpacingFlex(0.5f, 1);

        program.setAllPadSame(0.3f, 1);
    }

    public void setInfo()
    {
        string path = "";
        //Simple Variable
        Texture2D image;

        UIHelper.setText(transform.GetChild(0).GetChild(0), action.varActData.name, PlayerSettings.colourScheme.getBlackTextColor());

        //Check if type is bool, set image in that cases
        if (action.varActData.setData.refID != 0)
        {

            //Set the value to the name of the reference variable
            UIHelper.setText(transform.GetChild(2).GetChild(0), Scripts.programManager.getVariableName(action.varActData.setData.refID), PlayerSettings.colourScheme.getBlackTextColor());

            path = "unity_builtin_extra/UISprite";

            image = Resources.Load(path) as Texture2D;

            transform.GetChild(2).GetComponent<Button>().image.sprite = null;

            action.varActData.setData.varType = Scripts.programManager.getVariableType(action.varActData.setData.refID);

        }
        else
        {

            if (action.varActData.setData.varType == VariableType.Bool)
            {
                if (action.varActData.setData.value == "true")
                {
                    path = "Images/EditControllerAssets/True";
                }
                else
                {
                    path = "Images/EditControllerAssets/False";
                }

                image = Resources.Load(path) as Texture2D;

                transform.GetChild(2).GetComponent<Button>().image.sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

                UIHelper.setText(transform.GetChild(2).GetChild(0), "", PlayerSettings.colourScheme.getBlackTextColor());

            }
            else
            {
                path = "unity_builtin_extra/UISprite";

                image = Resources.Load(path) as Texture2D;

                transform.GetChild(2).GetComponent<Button>().image.sprite = null;

                UIHelper.setText(transform.GetChild(2).GetChild(0), action.varActData.setData.value, PlayerSettings.colourScheme.getBlackTextColor());
            }
        }
    }

    //Spawn Panel
   public void setAction()
    {
        action = createAction();

        int offset = 0;

        //Name
        transform.GetChild(0 + offset).GetComponent<Button>().onClick.AddListener(delegate
        {
            if (noPanelOpen())
            {
                //Set Panel Type
                panelInfo.editDataType = EditDataType.NewValue;

                //Set Editable Variable Type
                panelInfo.varType = VariableType.Text;

                //Set the Data type it will change
                panelInfo.valEditType = ValueEditType.Name;

                EditValController.genPanel(this);
            }
        });

        //Value
        transform.GetChild(2 + offset).GetComponent<Button>().onClick.AddListener(delegate
        {
            if (noPanelOpen())
            {
                //Set Editable Variable Type
                panelInfo.varType = action.varActData.setData.varType;

                //Set Panel Type
                panelInfo.editDataType = EditDataType.Value;

                if (PlayerSettings.advancedVariables)
                {
                    if (panelInfo.varType == VariableType.Bool)
                    {
                        //Set the Data type it will change
                        panelInfo.valEditType = ValueEditType.Bool;
                    }
                    else
                    {
                        //Set the Data type it will change
                        panelInfo.valEditType = ValueEditType.Value;
                    }
                }
                else
                {
                    panelInfo.valEditType = ValueEditType.VariableSmartAssign;
                }

                EditValController.genPanel(this);
            }
        });
    }

    public ProgramAction createAction()
    {
        return new ProgramAction(actionInfo, action.varActData);
    }
}
