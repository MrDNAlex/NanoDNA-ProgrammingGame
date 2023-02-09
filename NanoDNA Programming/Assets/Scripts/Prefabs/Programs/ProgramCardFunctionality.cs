using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;
using DNAStruct;

public class ProgramCardFunctionality
{

    Flex Program;

    //
    //Set UI
    //
    public Flex setUI (CardInfo info)
    {

        //Go to the function storing the flex for 
        switch (info.storeTag)
        {
            case StoreTag.Movement:
                return setUIMovement(info);
            case StoreTag.Math:
                return setUIMovement(info);
              
            case StoreTag.Logic:
                return setUIMovement(info);
              
            case StoreTag.Variable:
                return setUIVariables(info);
               
            default:
                return setUIMovement(info);
        }
    }

    public Flex setUIMovement (CardInfo info)
    {
        switch (info.movementName)
        {
            case MovementCardNames.Move:

                //Flex Variable Init
                Program = new Flex(info.rectTrans, 2);

                Flex Move = new Flex(Program.getChild(0), 1f);
                Flex Direction = new Flex(Program.getChild(1), 1);
                Flex Value = new Flex(Program.getChild(2), 1);

                //Set Flex Parameters
                Program.addChild(Move);
                Program.addChild(Direction);
                Program.addChild(Value);

                Program.setSpacingFlex(0.4f, 1);

                Program.setAllPadSame(0.3f, 1);
                break;
        }
        return Program;
    }

    public Flex setUIVariables (CardInfo info)
    {
        switch (info.variableName)
        {
            case VariableCardNames.Variable:

                //Flex Variable Init
                Program = new Flex(info.rectTrans, 2);

                Flex Var = new Flex(Program.getChild(0), 2);
                Flex VarName = new Flex(Program.getChild(1), 1);
                Flex Sign = new Flex(Program.getChild(2), 1);
                Flex Val = new Flex(Program.getChild(3), 1);

                //Set Flex Parameters
                Program.addChild(Var);
                Program.addChild(VarName);
                Program.addChild(Sign);
                Program.addChild(Val);

                Program.setSpacingFlex(1, 1);

                Program.setAllPadSame(0.2f, 1);

                break;
        }
        return Program;
    }


    //
    //Set Info
    //

    public void setInfo (CardInfo info)
    {
       
        switch (info.storeTag)
        {
            case StoreTag.Movement:
               
                setInfoMovement(info);
                break;
            case StoreTag.Math:
                break;
            case StoreTag.Logic:
                break;
            case StoreTag.Variable:
                break;
            default:
                break;
        }
        
    }
    
    public void setInfoMovement (CardInfo info)
    {
        switch (info.movementName)
        {
            case MovementCardNames.Move:

                switch (info.action.dir)
                {
                    case "up":
                        info.rectTrans.GetChild(1).GetComponent<Dropdown>().value = 0;
                        break;
                    case "down":
                        info.rectTrans.GetChild(1).GetComponent<Dropdown>().value = 1;
                        break;
                    case "left":
                        info.rectTrans.GetChild(1).GetComponent<Dropdown>().value = 2;
                        break;
                    case "right":
                        info.rectTrans.GetChild(1).GetComponent<Dropdown>().value = 3;
                        break;
                }

                info.rectTrans.GetChild(2).GetComponent<InputField>().text = "" + info.action.value;
                break; 
        }
    }


    //
    //Set Action
    //

    public void setAction (CardInfo info)
    {
      
        switch (info.storeTag)
        {
            case StoreTag.Movement:
              
                setActionMovement(info);
                break;
            case StoreTag.Math:
                break;
            case StoreTag.Logic:
                break;
            case StoreTag.Variable:
                break;
            default:
                break;
        }

    }

    public void setActionMovement (CardInfo info)
    {
      
        switch (info.movementName)
        {
            case MovementCardNames.Move:

                //action = createAction(dir, value);

                info.transform.GetChild(1).GetComponent<Dropdown>().onValueChanged.AddListener(delegate
                {

                    //Get Direction
                    info.programCard.dir = info.transform.GetChild(1).GetComponent<Dropdown>().options[info.transform.GetChild(1).GetComponent<Dropdown>().value].text.ToLower();

                    //Create Action
                    info.programCard.action = info.programCard.createAction(info.programCard.dir, info.programCard.value);


                    if (Camera.main.GetComponent<LevelScript>().progSec.undo == false && info.programCard.setInf == false)
                    {
                        Camera.main.GetComponent<LevelScript>().progSec.compileProgram();
                    }

                });

                info.transform.GetChild(2).GetComponent<InputField>().onEndEdit.AddListener(delegate
                {

                    if (info.transform.GetChild(2).GetComponent<InputField>().textComponent.text != null)
                    {
                        //Get Value
                        info.programCard.value = int.Parse(info.transform.GetChild(2).GetComponent<InputField>().textComponent.text);
                    }
                    else
                    {
                        info.programCard.value = 0;
                    }

                    //Create Action
                    info.programCard.action = info.programCard.createAction(info.programCard.dir, info.programCard.value);


                    if (Camera.main.GetComponent<LevelScript>().progSec.undo == false && info.programCard.setInf == false)
                    {
                        Camera.main.GetComponent<LevelScript>().progSec.compileProgram();
                    }


                });



                break;

        }
    }


    //
    //Create Action
    //


    public ProgramAction createAction (CardInfo info)
    {
        
        switch (info.storeTag)
        {
            case StoreTag.Movement:
                return createMovementAction(info);


        }

    }

    public ProgramAction createMovementAction (CardInfo info)
    {
        switch (info.movementName)
        {
            case MovementCardNames.Move:
                return;

        }

    }



}
