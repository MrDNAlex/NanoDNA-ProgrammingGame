using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;
using DNAStruct;
using UnityEngine.Rendering;

public class ProgramCard : MonoBehaviour
{

    public ActionType actionType;
    public UIWord cardName;

    public MovementActionNames movementName;
    public MathActionNames mathName;
    public LogicActionNames logicName;
    public VariableActionNames variableName;
    public ActionActionNames actionName;

    public Flex program;
    public Transform progLine;
    public int indent = 0;

    //Action Stuff
    public ProgramAction action;

    //public bool setInf = false;

    ProgramCardFunctionality functionality;

    private void Awake()
    {
        functionality = new ProgramCardFunctionality();
        setFunctionality();

        //Add action
        action = functionality.createAction(setCardInfo());

      //  Debug.Log(action.dispAction());
        Camera.main.GetComponent<LevelScript>().allScripts.programSection.selectedCharData.program.setAction(action, transform.parent.parent.GetSiblingIndex());
     
    }

    // Start is called before the first frame update
    void Start()
    {
        OnDemandRendering.renderFrameInterval = 12;
    }

    public void setFunctionality()
    {
        //Set the UI
        program = functionality.setUI(setCardInfo());

    }

    public void setInfo (ProgramAction action)
    {
        //The action type will have a reference to a function for it's handle so that when the program is being read it doens't need to go around and search for said function, it just instantly runs it, associated with it will be the types and values it needs!

        //Set Action
        this.actionType = action.actionType;
        this.action = action;
        Camera.main.GetComponent<LevelScript>().allScripts.programSection.selectedCharData.program.setAction(action, transform.parent.parent.GetSiblingIndex());

        //Actually paste info on the UI
        functionality.setInfo(setCardInfo());

    }

    public void setEditable ()
    {
        functionality.setAction(setCardInfo());
    }

    public CardInfo setCardInfo ()
    {
        CardInfo info = new CardInfo();

        info.actionType = actionType;

        info.movementName = movementName;
        info.mathName = mathName;
        info.logicName = logicName;
        info.variableName = variableName;
        info.actionName = actionName;

        info.cardName = cardName;
      
        info.flex = program;
        info.rectTrans = transform.GetComponent<RectTransform>();
        info.transform = transform;
        info.action = action;

        info.programCard = this;

        //Change this
        info.varType = VariableType.Number;

        info.progLineIndex = transform.parent.parent.GetSiblingIndex();

        return info;

    }

}
