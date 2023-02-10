using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DNAStruct;
using UnityEngine.Rendering;


public class DragController2 : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    int lastChildIndex;
    Vector3 lastPos;
    Transform lastParent;
    Vector3 newPos;

    CardInfo info;


    // [SerializeField] RectTransform code;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        // lastChildIndex = UIObject.GetSiblingIndex();
        lastPos = transform.position;
        lastParent = transform.parent;
        newPos = transform.localPosition;

        info = new CardInfo();

        info.actionType = transform.GetComponent<ProgramCard>().actionType;

        info.movementName = transform.GetComponent<ProgramCard>().movementName;
        info.mathName = transform.GetComponent<ProgramCard>().mathName;
        info.logicName = transform.GetComponent<ProgramCard>().logicName;
        info.variableName = transform.GetComponent<ProgramCard>().variableName;

    }

    public void OnDrag(PointerEventData eventData)
    {
        //Get the new Position
        newPos = new Vector3(newPos.x + eventData.delta.x, newPos.y + eventData.delta.y, 0);
        transform.localPosition = newPos;

        RectTransform content = Camera.main.GetComponent<LevelScript>().contentTrans;

        //Debug.Log(content);

        //IS this needed?
        for (int i = 0; i < content.childCount; i ++)
        {
            Transform child = content.GetChild(i);
            float distance = Vector3.Distance(transform.position, child.position);
            //Set the type
           // Camera.main.GetComponent<LevelScript>().type = type;

            if (distance <= 0.9f)
            {
                child.GetComponent<Image>().color = Color.red;
            } else
            {
                child.GetComponent<Image>().color = Color.cyan;
            }

        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        RectTransform content = Camera.main.GetComponent<LevelScript>().contentTrans;

        for (int i = 0; i < content.childCount; i++)
        {
            Transform child = content.GetChild(i); //Program Line
            float distance = Vector3.Distance(transform.position, child.position);

            if (EventSystem.current.IsPointerOverGameObject())
            {
                //Find a way to raycast 
                if (distance < 0.9f)
                {

                    child.GetComponent<ProgramLine>().addProgram(info);

                    //Camera.main.GetComponent<LevelScript>().addProgram(child.GetComponent<ProgramLine>(), type);
                }
            }
        }

        //Set Position
        transform.position = lastPos;
    }
}
