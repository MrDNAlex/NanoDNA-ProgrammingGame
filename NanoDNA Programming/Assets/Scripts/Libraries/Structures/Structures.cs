using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;

namespace DNAStruct
{


    public struct CardInfo
    {
        public StoreTag storeTag;
        public string cardType; //eventually replace this with the storeTag I think
        public string cardName;

        public MovementCardNames movementName;
        public MathCardNames mathName;
        public LogicCardNames logicName;
        public VariableCardNames variableName;

        //Program Related Stuff
        public Flex flex;
        public RectTransform rectTrans;
        public Transform transform;
        public ProgramAction action;
        public ProgramCard programCard;

    }

    public enum StoreTag
    {
        
        Movement,
        Math,
        Logic, 
        Variable
    }

    public enum MovementCardNames
    {
        None,
        Move, 

    }

    public enum MathCardNames
    {
        None,
    }

    public enum LogicCardNames
    {
        None,
    }

    public enum VariableCardNames
    {
        None,
        Variable, 

    }






}


