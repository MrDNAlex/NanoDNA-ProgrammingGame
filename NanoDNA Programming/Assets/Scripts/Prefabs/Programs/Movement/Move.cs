using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.UI;
using DNAStruct;

public class Move : ProgramCard, IProgramCard
{
    //ProgramAction action;



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

        Flex Move = new Flex(program.getChild(0), 1f);
        Flex Direction = new Flex(program.getChild(1), 1);
        Flex Value = new Flex(program.getChild(2), 1);

        //Set Flex Parameters

        program.addChild(Direction);
        program.addChild(Value);
        program.addChild(Move);

        program.setSpacingFlex(0.2f, 1);

        program.setAllPadSame(0.2f, 1);

        
    }

    public void setInfo()
    {
        string path = "";
        switch (action.moveData.dir)
        {
            case Direction.Up:
                path = "Images/EditControllerAssets/up";
                break;
            case Direction.Left:
                path = "Images/EditControllerAssets/left";
                break;
            case Direction.Right:
                path = "Images/EditControllerAssets/right";
                break;
            case Direction.Down:
                path = "Images/EditControllerAssets/down";
                break;
        }

        Texture2D image = Resources.Load(path) as Texture2D;

        rectTrans.GetChild(1).GetChild(0).GetComponent<Image>().sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

        if (action.moveData.refID != 0)
        {
            UIHelper.setText(rectTrans.GetChild(2).GetChild(0), Camera.main.GetComponent<ProgramManager>().getVariableName(action.moveData.refID), PlayerSettings.colourScheme.getBlackTextColor());
        }
        else
        {
            UIHelper.setText(rectTrans.GetChild(2).GetChild(0), action.moveData.value, PlayerSettings.colourScheme.getBlackTextColor());
        }

        UIHelper.setText(rectTrans.GetChild(0), cardName, PlayerSettings.colourScheme.getBlackTextColor());
    }

    //Spawn Panel
    public void setAction()
    {
        action = createAction();

        //Direction
        transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
        {
            if (noPanelOpen())
            {
                //Set Panel Type
                panelInfo.editDataType = EditDataType.Multichoice;

                //Set Editable Variable Type
                panelInfo.varType = VariableType.Number;

                //Set the Data type it will change
                panelInfo.valEditType = ValueEditType.Direction;

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
                panelInfo.varType = VariableType.Number;

                //Set the Data type it will change
                panelInfo.valEditType = ValueEditType.Value;

                EditValController.genPanel(this);
            }
        });
    }

    public ProgramAction createAction()
    {
        return new ProgramAction(actionInfo, action.moveData);
    }
}
