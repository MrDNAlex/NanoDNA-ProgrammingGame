using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DragController2 : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    int lastChildIndex;
    Vector3 lastPos;
    Transform lastParent;
    Vector3 newPos;

   // GameObject hover = null;
    string type;


    // [SerializeField] RectTransform code;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        // lastChildIndex = UIObject.GetSiblingIndex();
        lastPos = transform.position;
        lastParent = transform.parent;
        newPos = transform.localPosition;

        //Camera.main.GetComponent<LevelScript>().type = transform.parent.GetComponent<ProgramCard>().cardType;
        //Debug.Log(Camera.main.GetComponent<LevelScript>().type);

        type = transform.GetComponent<ProgramCard>().cardType;
       
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
            Camera.main.GetComponent<LevelScript>().type = type;

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

                    child.GetComponent<ProgramLine>().addProgram(type);

                    //Camera.main.GetComponent<LevelScript>().addProgram(child.GetComponent<ProgramLine>(), type);
                }

            }

        }

        //Set Position
        transform.position = lastPos;

    }
}
