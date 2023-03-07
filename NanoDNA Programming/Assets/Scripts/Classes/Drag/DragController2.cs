using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DNAStruct;
using UnityEngine.Rendering;
using DNAMathAnimation;


public class DragController2 : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Vector3 OGpos;
    
    Vector3 lastPos;
    //Transform lastParent;
    Vector3 newPos;

    Vector3 curGlobPos;
    Vector3 curLocPos;

    CardInfo info;

    // Start is called before the first frame update
    void Start()
    {
        OGpos = transform.position;
        Debug.Log(OGpos);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // lastChildIndex = UIObject.GetSiblingIndex();
       // Debug.Log("Begin: " +transform.localPosition);
        lastPos = transform.localPosition;
        newPos = transform.localPosition;

        info = new CardInfo();

        //Get the info from the StoreCardDragInfo

        info.actionType = transform.GetComponent<StoreCardDragInfo>().info.actionType;

        info.movementName = transform.GetComponent<StoreCardDragInfo>().info.movementName;
        info.mathName = transform.GetComponent<StoreCardDragInfo>().info.mathName;
        info.logicName = transform.GetComponent<StoreCardDragInfo>().info.logicName;
        info.variableName = transform.GetComponent<StoreCardDragInfo>().info.variableName;
        info.actionName = transform.GetComponent<StoreCardDragInfo>().info.actionName;

    }

    public void OnDrag(PointerEventData eventData)
    {
       // Debug.Log("Drag: " + transform.localPosition);

        //Get the new Position
        newPos = new Vector3(newPos.x + eventData.delta.x, newPos.y + eventData.delta.y, 0);
        transform.localPosition = newPos;

        curGlobPos = transform.position;
        curLocPos = transform.localPosition;

        RectTransform content = Camera.main.GetComponent<LevelScript>().contentTrans;

       // Debug.Log(transform.position);
      //  Debug.Log(transform.localPosition);
        
       // Debug.Log(transform);

        for (int i = 0; i < content.childCount; i ++)
        {
            Transform child = content.GetChild(i);

            MouseOverDetect mouse = content.GetChild(i).GetChild(1).GetComponent<MouseOverDetect>();

            if (mouse.mouseOver)
            {
                child.GetComponent<Image>().color = Color.red;
            } else
            {
                child.GetComponent<Image>().color = Color.cyan;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       // Debug.Log("End: " +transform.localPosition);
        //Vector3 curPos = transform.localPosition;

        RectTransform content = Camera.main.GetComponent<LevelScript>().contentTrans;


        for (int i = 0; i < content.childCount; i++)
        {
           
            Transform child = content.GetChild(i);
            MouseOverDetect mouse = content.GetChild(i).GetChild(1).GetComponent<MouseOverDetect>();

            if (mouse.mouseOver)
            {
                //Debug.Log(info.variableName);
               // Debug.Log(transform.position);
               // Debug.Log(transform.localPosition);
               // Debug.Log(transform);

                child.GetComponent<ProgramLine>().addProgram(info, transform);

            }

        }

        // transform.localPosition = curPos;
       // transform.position = curGlobPos;
       // transform.localPosition = curLocPos;

        Debug.Log("End: " + transform.localPosition);

        //So the glitch is being caused by the text for lines being used updating, it updates all the position of the UI, but it looks like we can hide this glitch by making this animation faster
        StartCoroutine(DNAMathAnim.animateCosinusoidalRelocationLocal(transform, lastPos, 100, 0, false));



    }
}
