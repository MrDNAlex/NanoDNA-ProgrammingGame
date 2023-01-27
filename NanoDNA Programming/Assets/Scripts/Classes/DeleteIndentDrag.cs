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

        Debug.Log(lastPos);

        //type = transform.GetComponent<Program>().cardType;

    }

    public void OnDrag(PointerEventData eventData)
    {
        //Get the new Position
        newPos = new Vector3(newPos.x + eventData.delta.x, newPos.y, 0);
        transform.localPosition = newPos;

        Vector3 distance = OGPos - transform.position;

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


        } else if (distance.x >= 2.2f)
        {
            transform.GetComponent<Program>().progLine.GetComponent<ProgramLine>().deleteLine();
            Debug.Log("Delete");
        }
        else if (distance.x < -1 && distance.x > -2)
        {
            transform.position = lastPos + new Vector3(1, 0, 0);
            Debug.Log("Option 1");
        }
        else if (distance.x < -2 && distance.x > -3)
        {
            transform.position = lastPos + new Vector3(2, 0, 0);
            Debug.Log("Option 2");
        }
        else if (distance.x < -3 && distance.x > -4)
        {
            transform.position = lastPos + new Vector3(3, 0, 0);
            Debug.Log("Option 3");
        } else
        {
            transform.position = lastPos;
            Debug.Log("Nothing");

            transform.parent.parent.GetComponent<ProgramLine>().ProgramUI.setSize(transform.parent.parent.GetComponent<ProgramLine>().ProgramUI.size);

        }

    }
}
