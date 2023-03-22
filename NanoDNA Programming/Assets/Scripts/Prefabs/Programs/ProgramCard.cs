using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;
using DNAStruct;
using UnityEngine.Rendering;

public class ProgramCard : MonoBehaviour
{
    public struct PanelInfo
    {
        public EditDataType editDataType;

        public VariableType varType;

        public ValueEditType valEditType;
    }

    [System.Serializable]
    public struct ActionInfo
    {
        public ActionType actionType;

        public MovementActionNames movementName;
        public LogicActionNames logicName;
        public VariableActionNames variableName;
        public ActionActionNames actionName;
    }


    //Remove all of this and use ActionInfo Struct 

    public PanelInfo panelInfo;
    public ActionInfo actionInfo;

    //public ActionType actionType;
    public UIWord cardName;

   // public MovementActionNames movementName;
   // public MathActionNames mathName;
   // public LogicActionNames logicName;
   // public VariableActionNames variableName;
   // public ActionActionNames actionName;

    public Flex program;
    public Transform progLine;
    public RectTransform rectTrans;
    public int indent = 0;
    public int lineIndex;

    //Action Stuff
    public ProgramAction action;
    public IProgramCard Iprogram;

    UIWord Text = new UIWord("Text", "Texte");
    UIWord Number = new UIWord("Number", "Nombre");
    UIWord Decimal = new UIWord("Decimal", "Decimale");
    UIWord Bool = new UIWord("Boolean", "Booléen");

    public void getReferences ()
    {
        progLine = this.GetComponent<Transform>();
        rectTrans = this.GetComponent<RectTransform>();
    }

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        OnDemandRendering.renderFrameInterval = 12;
        
    }

    public void setEditable ()
    {
        //Need to seperate this to something editable
        Iprogram.setAction();
        Scripts.programSection.setAction(action, lineIndex);
        Scripts.levelManager.updateConstraints();
    }

    public void getAction (ProgramAction action)
    {
        this.action = action;

    }

    public bool noPanelOpen()
    {
        if (Camera.main.transform.GetChild(0).GetChild(2).childCount == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void destroyChildren(GameObject Obj)
    {
        //Only deletes children under the one referenced
        foreach (Transform child in Obj.transform)
        {
            //Safe to delete
            GameObject.Destroy(child.gameObject);
        }
    }

    public string getVarType(VariableType varType)
    {
        switch (varType)
        {
            case VariableType.Text:
                return Text.getWord(PlayerSettings.language);
            case VariableType.Number:
                return Number.getWord(PlayerSettings.language);
            case VariableType.Decimal:
                return Decimal.getWord(PlayerSettings.language);
            case VariableType.Bool:
                return Bool.getWord(PlayerSettings.language);
            default:
                return Text.getWord(PlayerSettings.language);

        }

    }

    public string getVarTypeImage(VariableType varType)
    {
        switch (varType)
        {
            case VariableType.Text:
                return "Images/EditControllerAssets/Text";
            case VariableType.Number:
                return "Images/EditControllerAssets/Number";
            case VariableType.Decimal:
                return "Images/EditControllerAssets/Decimal";
            case VariableType.Bool:
                return "Images/EditControllerAssets/Bool";
            default:
                return "Images/EditControllerAssets/Text";
        }
    }

    public void getLineNumber ()
    {
        lineIndex = transform.parent.parent.GetSiblingIndex();
    }

}
