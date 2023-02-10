using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.Tilemaps;

namespace DNAStruct
{
    [System.Serializable]
    public struct LevelMakerInfo
    {
        public Tilemap voidMap;
        public Tilemap backgroundMap;
        public Tilemap obstacleMap;
        public Tilemap decorationMap;
        public GameObject charHolder;

        public int maxLines;

    }


    public struct CardInfo
    {
        public ActionType actionType;
        public string cardType; //eventually replace this with the storeTag I think
        public string cardName;

        public MovementActionNames movementName;
        public MathActionNames mathName;
        public LogicActionNames logicName;
        public VariableActionNames variableName;

        //Program Related Stuff
        public Flex flex;
        public RectTransform rectTrans;
        public Transform transform;
        public ProgramAction action;
        public ProgramCard programCard;

    }

    //
    //Program Action stuff
    //
    public enum ActionType
    {
        Movement,
        Math,
        Logic,
        Variable
    }

    public enum MovementActionNames
    {
        None,
        Move,

    }

    public enum MathActionNames
    {
        None,
    }

    public enum LogicActionNames
    {
        None,
    }

    public enum VariableActionNames
    {
        None,
        Variable,

    }

    [System.Serializable]
    public enum Direction
    {
        Up, 
        Left,
        Right, 
        Down
    }

    //
    //Action Data structs
    //
    [System.Serializable]
    public struct MoveData
    {
        public Direction dir;
        public int value;
    }


}