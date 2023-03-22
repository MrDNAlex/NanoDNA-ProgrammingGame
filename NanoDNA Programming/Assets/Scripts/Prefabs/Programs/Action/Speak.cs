using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.UI;
using DNAStruct;

public class Speak : ProgramCard, IProgramCard
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

        Flex SpeakType = new Flex(program.getChild(0), 1);
        Flex SpeakMessage = new Flex(program.getChild(1), 1);

        program.addChild(SpeakType);
        program.addChild(SpeakMessage);

        program.setSpacingFlex(0.2f, 1);
        program.setAllPadSame(0.2f, 1);
    }

    public void setInfo()
    {
        string path = "";
        switch (action.actData.descriptor)
        {
            case ActionDescriptor.Talk:
                path = "Images/EditControllerAssets/Talk";
                break;
            case ActionDescriptor.Whisper:
                path = "Images/EditControllerAssets/Whisper";
                break;
            case ActionDescriptor.Yell:
                path = "Images/EditControllerAssets/Yell";
                break;
        }
        Texture2D image = Resources.Load(path) as Texture2D;

      transform.GetChild(0).GetComponent<Button>().image.sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

        if (action.actData.refID != 0)
        {
            //Make it equal to reference
            UIHelper.setText(transform.GetChild(1).GetChild(0), Camera.main.GetComponent<ProgramManager>().getVariableName(action.actData.refID), PlayerSettings.colourScheme.getBlackTextColor());
        }
        else
        {
            UIHelper.setText(transform.GetChild(1).GetChild(0), action.actData.data, PlayerSettings.colourScheme.getBlackTextColor());
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
                //Yell, Talk, Whipser
                //Set Panel Type
                panelInfo.editDataType = EditDataType.Multichoice;

                //Set Editable Variable Type
                panelInfo.varType = VariableType.Number;

                //Set the Data type it will change
                panelInfo.valEditType = ValueEditType.Speak;

                EditValController.genPanel(this);
            }
        });

        transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
        {
            if (noPanelOpen())
            {
                //Message

                //Set Editable Variable Type
                panelInfo.varType = VariableType.Text;

                //Set Panel Type
                panelInfo.editDataType = EditDataType.Value;

                //Set the Data type it will change
                panelInfo.valEditType = ValueEditType.Value;

                EditValController.genPanel(this);
            }
        });
    }

    public ProgramAction createAction()
    {
        return new ProgramAction(actionInfo, action.actData);
    }

    
}
