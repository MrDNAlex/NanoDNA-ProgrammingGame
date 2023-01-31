using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeleteIndentDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    
    Vector3 lastPos;
    Vector3 newPos;
    string type;

    Vector3 OGPos;

    
    // Start is called before the first frame update
    void Start()
    {
        OGPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Get last and current position
        lastPos = transform.position;
        newPos = transform.localPosition;

    }

    public void OnDrag(PointerEventData eventData)
    {
        //Get the new Position
        newPos = new Vector3(newPos.x + eventData.delta.x, newPos.y, 0);
        transform.localPosition = newPos;

        //Get the distance from the original position
        Vector3 distance = OGPos - transform.position;

        //Display colour
        if (distance.x >= 2)
        {
            transform.parent.parent.GetComponent<Image>().color = Color.red;
        } else
        {
            transform.parent.parent.GetComponent<Image>().color = Color.cyan;
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {


        Vector3 distance = OGPos - transform.position;

        float dist = Vector3.Distance(transform.position, OGPos);

        if (dist <= 0.9f)
        {

            transform.position = OGPos;
            transform.GetComponent<ProgramCard>().indent = 0;


        } else if (distance.x >= 2.2f)
        {
            //Delete the line
            transform.GetComponent<ProgramCard>().progLine.GetComponent<ProgramLine>().deleteLine();
            Debug.Log("Delete");
        }
        else if (distance.x < -1 && distance.x > -2)
        {
            //Set indent pos
            transform.position = OGPos + new Vector3(1, 0, 0);
            //Set indentation
            transform.GetComponent<ProgramCard>().indent = 1;
        }
        else if (distance.x < -2 && distance.x > -3)
        {
            //Set indent pos
            transform.position = OGPos + new Vector3(2, 0, 0);
            //Set indentation
            transform.GetComponent<ProgramCard>().indent = 2;
           
        }
        else if (distance.x < -3 && distance.x > -4)
        {
            //Set indent pos
            transform.position = OGPos + new Vector3(3, 0, 0);
            //Set indentation
            transform.GetComponent<ProgramCard>().indent = 3;
            
        } else
        {
            transform.position = lastPos;
            
            transform.parent.parent.GetComponent<ProgramLine>().ProgramUI.setSize(transform.parent.parent.GetComponent<ProgramLine>().ProgramUI.size);
        }

    }
}
