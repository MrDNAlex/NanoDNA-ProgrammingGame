using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using DNAStruct;
using UnityEngine.UI;


public class StoreCardDragInfo : MonoBehaviour
{

    //Maybe buyndle these into a structure?
    public Flex flex;
    public StoreCardInfo info;
    string path;
    public UIWord cardName;

    private void Awake()
    {
        flex = new Flex(transform.GetComponent<RectTransform>(), 2);

        setImage(getTypePath());
    }

    void setImage (string path)
    {
        Texture2D image = Resources.Load(path) as Texture2D;

        transform.GetComponent<Image>().sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

    }

    string getTypePath ()
    {
        switch (info.actionType)
        {
            case ActionType.Movement:
                return getMovementPath();
            case ActionType.Logic:
                return null;
            case ActionType.Math:
                return null;
            case ActionType.Variable:
                return getVariablePath();
            case ActionType.Action:
                return getActionPath();
            default:
                return getMovementPath();
        }
    }

    string getMovementPath ()
    {
        switch (info.movementName)
        {
            case MovementActionNames.Move:
                return "Images/StoreCard/Movement/MoveCard";
            default:
                return null;
        }
    }

    string getVariablePath()
    {
        switch (info.variableName)
        {
            case VariableActionNames.Variable:
                return "Images/StoreCard/Variables/VariableCard";
            default:
                return null;
        }
    }

    string getActionPath()
    {
        switch (info.actionName)
        {
            case ActionActionNames.Speak:
                return "Images/StoreCard/Action/SpeakCard";
            default:
                return null;
        }
    }

   
}
