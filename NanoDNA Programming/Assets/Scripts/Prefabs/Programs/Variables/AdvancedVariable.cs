using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.UI;
using DNAStruct;

public class AdvancedVariable : ProgramCard, IProgramCard
{

    // Start is called before the first frame update
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

        Flex PublicAndType = new Flex(program.getChild(0), 0.5f, program);

        Flex Public = new Flex(PublicAndType.getChild(0), 1, PublicAndType);
        Flex VarType = new Flex(PublicAndType.getChild(1), 1, PublicAndType);

        Flex VarName = new Flex(program.getChild(1), 1, program);
        Flex VarSign = new Flex(program.getChild(2), 0.5f, program);
        Flex VarValue = new Flex(program.getChild(3), 1, program);

        PublicAndType.setSpacingFlex(0.05f, 1);

        program.setSpacingFlex(0.2f, 1);

        program.setAllPadSame(0.2f, 1);
    }

    public void setInfo()
    {
        string path = "";
        //Check if public or local
        if (action.varData.isPublic)
        {
            //Public
            // Debug.Log("Public Path");
            path = "Images/EditControllerAssets/Public";

        }
        else
        {
            //Local
            // Debug.Log("Private Path");
            path = "Images/EditControllerAssets/Local";
        }

        Texture2D image = Resources.Load(path) as Texture2D;

        //Public / Private
        transform.GetChild(0).GetChild(0).GetComponent<Button>().image.sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

        //Variable Type
        path = getVarTypeImage(action.varData.varType);

        image = Resources.Load(path) as Texture2D;

        transform.GetChild(0).GetChild(1).GetComponent<Button>().image.sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

        UIHelper.setText(transform.GetChild(0).GetChild(1).GetChild(0), "", PlayerSettings.colourScheme.getBlackTextColor());

        UIHelper.setText(transform.GetChild(1).GetChild(0), action.varData.name, PlayerSettings.colourScheme.getBlackTextColor());

        //Check if type is bool, set image in that cases
        if (action.varData.refID != 0)
        {

            //Set the value to the name of the reference variable
            UIHelper.setText(transform.GetChild(3).GetChild(0), Camera.main.GetComponent<ProgramManager>().getVariableName(action.varData), PlayerSettings.colourScheme.getBlackTextColor());

            path = "unity_builtin_extra/UISprite";

            image = Resources.Load(path) as Texture2D;

            transform.GetChild(3).GetComponent<Button>().image.sprite = null;

        }
        else
        {

            if (action.varData.varType == VariableType.Bool)
            {
                if (action.varData.value == "true")
                {
                    path = "Images/EditControllerAssets/True";
                }
                else
                {
                    path = "Images/EditControllerAssets/False";
                }

                image = Resources.Load(path) as Texture2D;

                transform.GetChild(3).GetComponent<Button>().image.sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

                UIHelper.setText(transform.GetChild(3).GetChild(0), "", PlayerSettings.colourScheme.getBlackTextColor());

            }
            else
            {
                path = "unity_builtin_extra/UISprite";

                image = Resources.Load(path) as Texture2D;

                transform.GetChild(3).GetComponent<Button>().image.sprite = null;

                UIHelper.setText(transform.GetChild(3).GetChild(0), action.varData.value, PlayerSettings.colourScheme.getBlackTextColor());
            }
        }
    }

    //Spawn Panel
   public void setAction()
    {
        action = createAction();

        int offset = 0;

        offset = 1;

        //Public Local
        transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
        {
            if (noPanelOpen())
            {
                //Set Panel Type
                panelInfo.editDataType = EditDataType.Multichoice;

                //Set Editable Variable Type
                panelInfo.varType = action.varData.varType;

                //Set the Data type it will change
                panelInfo.valEditType = ValueEditType.Public;

                EditValController.genPanel(this);
            }
        });

        //Variable Type
        transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
        {
            if (noPanelOpen())
            {
                //Set Panel Type
                panelInfo.editDataType = EditDataType.Multichoice;

                //Set Editable Variable Type
                panelInfo.varType = action.varData.varType;

                //Set the Data type it will change
                panelInfo.valEditType = ValueEditType.VariableType;

                EditValController.genPanel(this);
            }
        });

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
                panelInfo.varType = action.varData.varType;

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
        return new ProgramAction(actionInfo, action.varData);
    }
}
