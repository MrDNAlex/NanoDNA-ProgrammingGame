using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;
using FlexUI;
using UnityEngine.UI;
using DNASaveSystem;
using DNAMathAnimation;

public class EditValController : MonoBehaviour
{
    //Going to eventually have to check for key words as name 

    public Flex Parent;
    public Flex Holder;

    public Transform ParentTrans;

    public ProgramCard.PanelInfo panelInfo;

    public ProgramCard.ActionInfo actionInfo;

    public ProgramCard progCard;

    public MoveData moveData;
    public VariableData varData;
    public ActionData actData;

    public Language lang;

    public VariableType varType;
    public string value = "";
    public bool isPub;

    public int globalIndex = 0;

    public Vector3 OriginalPos;

   

   public List<UIWord> VariableTypes = new List<UIWord>();

    public static void genPanel (ProgramCard card)
    {
        GameObject panelParent = Camera.main.transform.GetChild(0).GetChild(2).gameObject;

        panelParent.SetActive(true);

        destroyChildren(panelParent);

        GameObject panel;

        switch (card.panelInfo.editDataType)
        {
            case EditDataType.Value:
                panel = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/Value") as GameObject, panelParent.transform);

                panel.GetComponent<ValueValController>().setPanel(panelParent.transform, card);
                break;

            case EditDataType.NewValue:
                panel = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/NewValue") as GameObject, panelParent.transform);

                panel.GetComponent<NewValueValController>().setPanel(panelParent.transform, card);
                break;

            case EditDataType.Multichoice:
                panel = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/MultiChoice") as GameObject, panelParent.transform);

                panel.GetComponent<MultiChoiceValController>().setPanel(panelParent.transform, card);
                break;

            case EditDataType.Variable:
                panel = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/VariableList") as GameObject, panelParent.transform);

                panel.GetComponent<VariableValController>().setPanel(panelParent.transform, card);
                break;
        }
    }

    public static void genPanel (MathOperations card)
    {
        GameObject panelParent = Camera.main.transform.GetChild(0).GetChild(2).gameObject;

        panelParent.SetActive(true);

        destroyChildren(panelParent);

        GameObject panel;

        panel = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/MathValue") as GameObject, panelParent.transform);

        panel.GetComponent<MathValueValController>().setPanel(panelParent.transform, card);
    }

    public static void destroyChildren(GameObject Obj)
    {
        //Only deletes children under the one referenced
        foreach (Transform child in Obj.transform)
        {
            //Safe to delete
            GameObject.Destroy(child.gameObject);
        }
    }

    private void OnDestroy()
    {
        ParentTrans.localPosition = OriginalPos;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
   
    public void checkValueType()
    {
        int index = 0;
        switch (panelInfo.editDataType)
        {
            case EditDataType.Value:
                index = 2;
                break;
            case EditDataType.NewValue:
                index = 1;
                break;
        }

        switch (panelInfo.valEditType)
        {
            case ValueEditType.VariableSmartAssign:
                try
                {
                    bool val = bool.Parse((string)value);
                    varData.setData(true, VariableType.Bool, varData.name, varData.value);
                    resetRefID(actionInfo.actionType);
                    Debug.Log("Var type is Bool");
                }
                catch
                {
                    //Try Int
                    try
                    {
                        int val = int.Parse((string)value);
                        Debug.Log(varData.value);
                        varData.setData(true, VariableType.Number, varData.name, varData.value);

                        resetRefID(actionInfo.actionType);
                        Debug.Log("Var type is Integer");
                    }
                    catch
                    {
                        //Try Float
                        try
                        {
                            float val = float.Parse((string)value);
                            varData.setData(true, VariableType.Decimal, varData.name, varData.value);
                            resetRefID(actionInfo.actionType);
                            Debug.Log("Var type is Decimal");
                        }
                        catch
                        {
                            //Default to Text
                            varData.setData(true, VariableType.Text, varData.name, varData.value);
                            resetRefID(actionInfo.actionType);
                            Debug.Log("Var type is Text");
                        }
                    }
                }
                break;
            default:

                switch (panelInfo.varType)
                {
                    case VariableType.Text:
                        //Do Nothing
                        Holder.getChild(1).GetChild(index).GetChild(0).GetComponent<Button>().enabled = true;
                        Holder.getChild(1).GetChild(index).GetChild(1).GetComponent<Text>().text = "";
                        resetRefID(actionInfo.actionType);
                        break;
                    case VariableType.Number:
                        try
                        {
                            int val = int.Parse((string)value);
                            Holder.getChild(1).GetChild(index).GetChild(0).GetComponent<Button>().enabled = true;
                            Holder.getChild(1).GetChild(index).GetChild(1).GetComponent<Text>().text = "";
                            varData.setData(varData.isPublic, varData.varType, varData.name, varData.value);
                            resetRefID(actionInfo.actionType);
                        }
                        catch
                        {
                            Holder.getChild(1).GetChild(index).GetChild(0).GetComponent<Button>().enabled = false;
                            Holder.getChild(1).GetChild(index).GetChild(1).GetComponent<Text>().text = LangDictionary.error.getWord(lang) + getVarType(varType);

                        }
                        break;
                    case VariableType.Decimal:
                        try
                        {
                            float val = float.Parse((string)value);
                            Holder.getChild(1).GetChild(index).GetChild(0).GetComponent<Button>().enabled = true;
                            Holder.getChild(1).GetChild(index).GetChild(1).GetComponent<Text>().text = "";
                            varData.setData(varData.isPublic, varData.varType, varData.name, varData.value);
                            resetRefID(actionInfo.actionType);
                        }
                        catch
                        {
                            Holder.getChild(1).GetChild(index).GetChild(0).GetComponent<Button>().enabled = false;
                            Holder.getChild(1).GetChild(index).GetChild(1).GetComponent<Text>().text = LangDictionary.error.getWord(lang) + getVarType(varType);
                        }
                        break;
                    case VariableType.Bool:

                        try
                        {
                            bool val = bool.Parse((string)value);
                            Holder.getChild(1).GetChild(index).GetChild(0).GetComponent<Button>().enabled = true;
                            Holder.getChild(1).GetChild(index).GetChild(1).GetComponent<Text>().text = "";
                            varData.setData(varData.isPublic, varData.varType, varData.name, varData.value);
                            resetRefID(actionInfo.actionType);
                        }
                        catch
                        {
                            Holder.getChild(1).GetChild(index).GetChild(0).GetComponent<Button>().enabled = false;
                            Holder.getChild(1).GetChild(index).GetChild(1).GetComponent<Text>().text = LangDictionary.error.getWord(lang) + getVarType(varType);
                        }
                        break;
                }
                break;
        }
    }

    public void setData(int index)
    {
      
        switch (actionInfo.actionType)
        {
            case ActionType.Movement:
                switch (actionInfo.movementName)
                {
                    case MovementActionNames.Move:
                        switch (panelInfo.valEditType)
                        {
                            case ValueEditType.Value:
                                //Get Direction
                                progCard.action.moveData.value = value;
                                break;
                            case ValueEditType.Direction:
                                progCard.action.moveData.dir = indexToDir(index);
                                break;
                        }
                        break;
                }
                break;

            case ActionType.Logic:

                break;
            case ActionType.Variable:

                switch (actionInfo.variableName)
                {
                    case VariableActionNames.Variable:
                        switch (panelInfo.valEditType)
                        {
                            case ValueEditType.Name:

                                //Set Value
                                progCard.action.varData.name = value.ToString();

                                break;
                            case ValueEditType.Value:

                                //Set Value
                                progCard.action.varData.value = value.ToString();

                                break;
                            case ValueEditType.VariableType:

                                progCard.action.varData.varType = GridViewVarType(index);

                                if (progCard.action.varData.varType == VariableType.Bool)
                                {
                                    progCard.action.varData.value = "false";
                                }
                                else
                                {
                                    progCard.action.varData.value = "";
                                }

                                break;
                            case ValueEditType.Public:

                                if (index == 0)
                                {
                                    progCard.action.varData.isPublic = true;
                                }
                                else
                                {
                                    progCard.action.varData.isPublic = false;
                                }
                                break;
                            case ValueEditType.Bool:
                                if (index == 0)
                                {
                                    progCard.action.varData.value = "true";
                                }
                                else
                                {
                                    progCard.action.varData.value = "false";
                                }
                                break;

                            case ValueEditType.VariableSmartAssign:
                                //Set Value
                                progCard.action.varData.value = value.ToString();
                                break;
                        }
                        break;
                    case VariableActionNames.MathVariable:

                        switch (panelInfo.valEditType)
                        {
                            case ValueEditType.Name:
                                progCard.action.varData.name = value.ToString();
                                break;
                            case ValueEditType.MathOperation:

                                switch (index)
                                {
                                    case 0:
                                        progCard.action.varData.mathType = MathTypes.Addition;
                                        break;
                                    case 1:
                                        progCard.action.varData.mathType = MathTypes.Subtraction;
                                        break;
                                    case 2:
                                        progCard.action.varData.mathType = MathTypes.Multiplication;
                                        break;
                                    case 3:
                                        progCard.action.varData.mathType = MathTypes.Division;
                                        break;
                                }
                                break;
                        }
                        break;
                }
                break;
            case ActionType.Action:
                switch (actionInfo.actionName)
                {
                    case ActionActionNames.Speak:

                        switch (panelInfo.valEditType)
                        {
                            case ValueEditType.Speak:

                                switch (index)
                                {
                                    case 0:
                                        progCard.action.actData.descriptor = ActionDescriptor.Whisper;
                                        break;
                                    case 1:
                                        progCard.action.actData.descriptor = ActionDescriptor.Talk;
                                        break;
                                    case 2:
                                        progCard.action.actData.descriptor = ActionDescriptor.Yell;
                                        break;
                                }
                                break;
                            case ValueEditType.Value:
                                progCard.action.actData.data = value.ToString();
                                break;
                                //Message
                        }
                        break;
                }
                break;
        }
    }

    public void instantiateEmpty(Flex parent, int rowIndex)
    {
        GameObject valDisp = Instantiate(Resources.Load("Prefabs/EditPanels/Empty") as GameObject, parent.UI.GetChild(rowIndex).transform);

        Flex ValDisp = new Flex(valDisp.GetComponent<RectTransform>(), 1);

        ValDisp.setSquare();

        parent.getChild(rowIndex).GetComponent<FlexInfo>().flex.addChild(ValDisp);

    }
   
    public VariableType GridViewVarType(int index)
    {
        switch (index)
        {
            case 0:
                return VariableType.Text;
            case 1:
                return VariableType.Number;
            case 2:
                return VariableType.Decimal;
            case 3:
                return VariableType.Bool;
            default:
                Debug.Log("here");
                return VariableType.Text;
        }
    }

    public Direction indexToDir(int index)
    {
        Direction dir = Direction.Up;
        switch (index)
        {
            case 0:
                dir = Direction.Up;
                break;
            case 1:
                dir = Direction.Left;
                break;
            case 2:
                dir = Direction.Right;
                break;
            case 3:
                dir = Direction.Down;
                break;
        }
        return dir;
    }

    //Maybe make a language manager
    public string getVarType(VariableType varType)
    {
        switch (varType)
        {
            case VariableType.Text:
                return LangDictionary.Text.getWord(lang);
            case VariableType.Number:
                return LangDictionary.Number.getWord(lang);
            case VariableType.Decimal:
                return LangDictionary.Decimal.getWord(lang);
            case VariableType.Bool:
                return LangDictionary.Bool.getWord(lang);
            default:
                return LangDictionary.Text.getWord(lang);
        }
    }

    public void resetRefID(ActionType type)
    {
        switch (type)
        {
            case ActionType.Movement:
                moveData.refID = 0;
                break;
            case ActionType.Variable:
                varData.refID = 0;
                break;
            case ActionType.Action:
                actData.refID = 0;
                break;
        }
    }

}