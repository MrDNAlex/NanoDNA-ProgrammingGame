using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;
using DNAStruct;

public class ProgramCardFunctionality
{

    Flex Program;

    Scripts allScripts;

    public ProgramCardFunctionality ()
    {
        allScripts = Camera.main.GetComponent<LevelScript>().allScripts;
    }

    //
    //Set UI
    //
    public Flex setUI (CardInfo info)
    {

        //Go to the function storing the flex for 
        switch (info.actionType)
        {
            case ActionType.Movement:
                return setUIMovement(info);
            case ActionType.Math:
                return setUIMovement(info);
              
            case ActionType.Logic:
                return setUIMovement(info);
              
            case ActionType.Variable:
                return setUIVariables(info);
               
            default:
                Debug.Log("Here");
                return setUIMovement(info);
        }
    }

    public Flex setUIMovement (CardInfo info)
    {
        switch (info.movementName)
        {
            case MovementActionNames.Move:

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
            case VariableActionNames.Variable:

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
       
        switch (info.actionType)
        {
            case ActionType.Movement:
               
                setInfoMovement(info);
                break;
            case ActionType.Math:
                break;
            case ActionType.Logic:
                break;
            case ActionType.Variable:
                break;
            default:
                Debug.Log("Here");
                break;
        }
        
    }
    
    public void setInfoMovement (CardInfo info)
    {
        switch (info.movementName)
        {
            case MovementActionNames.Move:

                switch (info.action.moveData.dir)
                {
                    case Direction.Up:
                        info.rectTrans.GetChild(1).GetComponent<Dropdown>().value = 0;
                        break;
                    case Direction.Down:
                        info.rectTrans.GetChild(1).GetComponent<Dropdown>().value = 1;
                        break;
                    case Direction.Left:
                        info.rectTrans.GetChild(1).GetComponent<Dropdown>().value = 2;
                        break;
                    case Direction.Right:
                        info.rectTrans.GetChild(1).GetComponent<Dropdown>().value = 3;
                        break;
                }

                info.rectTrans.GetChild(2).GetComponent<InputField>().text = "" + info.action.moveData.value;
                break; 
        }
    }


    //
    //Set Action
    //

    public void setAction (CardInfo info)
    {
        switch (info.actionType)
        {
            case ActionType.Movement:
              
                setActionMovement(info);
                break;
            case ActionType.Math:
                break;
            case ActionType.Logic:
                break;
            case ActionType.Variable:
                break;
            default:
                Debug.Log("Here");
                break;
        }

    }

    public void setActionMovement (CardInfo info)
    {
      
        switch (info.movementName)
        {
            case MovementActionNames.Move:

                info.programCard.action = createAction(info);

                info.transform.GetChild(1).GetComponent<Dropdown>().onValueChanged.AddListener(delegate
                {
                   
                    //Get Direction
                    info.action.moveData.dir = getDirec(info.transform.GetChild(1).GetComponent<Dropdown>().options[info.transform.GetChild(1).GetComponent<Dropdown>().value].text.ToLower());

                    //Set Data
                    info.programCard.moveData = info.action.moveData;
                    
                    //Create and set the Action
                    info.programCard.action = createAction(info);


                    if (allScripts.programSection.undo == false && info.programCard.setInf == false)
                    {
                        allScripts.programSection.compileProgram();
                    }

                });

                info.transform.GetChild(2).GetComponent<InputField>().onEndEdit.AddListener(delegate
                {
                   

                    if (info.transform.GetChild(2).GetComponent<InputField>().textComponent.text != null)
                    {
                        //Get Value

                        info.action.moveData.value = int.Parse(info.transform.GetChild(2).GetComponent<InputField>().textComponent.text);
                    }
                    else
                    {
                        info.action.moveData.value = 0;
                    }

                    info.programCard.moveData = info.action.moveData;
                   

                    //Create Action
                    info.programCard.action = createAction(info);

                    //Create Action
                  
                    if (allScripts.programSection.undo == false && info.programCard.setInf == false)
                    {
                        allScripts.programSection.compileProgram();
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
        
        switch (info.actionType)
        {
            case ActionType.Movement:
                return createMovementAction(info);

            default:
                Debug.Log("Here");
                return null;

        }

    }

    public ProgramAction createMovementAction (CardInfo info)
    {
        switch (info.movementName)
        {
            case MovementActionNames.Move:
                //This may go wrong
                return new ProgramAction(info, info.programCard.moveData);
                //Return new Program Action
            default:
                Debug.Log("Here");
                return null;
        }

    }


    //
    //Converters
    //

    public Direction getDirec (string dir)
    {
        switch (dir)
        {
            case "up":
                return Direction.Up;
                
            case "left":
                return Direction.Left;
              
            case "right":
                return Direction.Right;
              
            case "down":
                return Direction.Down;
            default:
                return Direction.Up;
              
        }
    }


}
