using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragController2 : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

   // [SerializeField] RectTransform code;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int lastChildIndex;
    Vector3 lastPos;
    Transform lastParent;
    Vector3 newPos;

    GameObject hover = null;
    string type;


    public void OnPointerDown(PointerEventData eventData)
    {

        // lastChildIndex = UIObject.GetSiblingIndex();
        lastPos = transform.position;
        lastParent = transform.parent;
        newPos = transform.localPosition;

        //Camera.main.GetComponent<LevelScript>().type = transform.parent.GetComponent<ProgramCard>().cardType;
        //Debug.Log(Camera.main.GetComponent<LevelScript>().type);

        if (transform.parent.GetSiblingIndex() == 0)
        {
            type = transform.parent.GetComponent<ProgramCard>().cardType;
        } else
        {
            //Debug.Log("Hello");
            type = transform.parent.GetComponent<MovCard>().cardType;
        }
       
        Debug.Log(type);

    }

    public void OnDrag(PointerEventData eventData)
    {
        //Get the new Position
        newPos = new Vector3(newPos.x + eventData.delta.x, newPos.y + eventData.delta.y, 0);
        transform.localPosition = newPos;

        RectTransform content = Camera.main.GetComponent<LevelScript>().contentTrans;

        //Debug.Log(content);

        for (int i = 0; i < content.childCount; i ++)
        {
            Transform child = content.GetChild(i);
            float distance = Vector3.Distance(transform.position, child.position);
            //Set the type
            Camera.main.GetComponent<LevelScript>().type = type;




            //Display Hover colour
            Camera.main.GetComponent<LevelScript>().checkBackground(child.gameObject, distance, hover);
            // Camera.main.GetComponent<LevelScript>().checkBackground(child.gameObject, distance);
            
            /*
            if (distance < 1)
            {
                Debug.Log("Distance");
                hover = child.gameObject;
                //Camera.main.GetComponent<LevelScript>().type = type;
                
            } else
            {
                hover = null;
                //make an erase command
            }
            */
            
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
                    
                    Camera.main.GetComponent<LevelScript>().addProgram(child.GetComponent<ProgramLine>());
                }

                //child.GetComponent<ProgramLine>().ProgramObj = Instantiate(Camera.main.GetComponent<LevelScript>().)
               

            }

        }

        //Set Position
        transform.position = lastPos;




        if (hover != null)
        {
            Debug.Log("NotEmpty");
            Camera.main.GetComponent<LevelScript>().setBackground(hover);
        }



    }
}
