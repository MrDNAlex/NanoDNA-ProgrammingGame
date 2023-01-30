using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;



public class MapDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IScrollHandler
{

    [SerializeField] Camera Cam;

    Vector3 lastPos;
    Vector3 newPos;
    string type;

    Vector3 OGPos;

    private void OnGUI()
    {
        
    }
    private void OnMouseOver()
    {
        //Scroll wheel zoom
    }

  
    

    // Start is called before the first frame update
    void Start()
    {
        //OGPos = transform.position;


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Get last and current position
        //lastPos = transform.position;
        //newPos = transform.localPosition;

        newPos = Cam.transform.position;


    }

    public void OnDrag(PointerEventData eventData)
    {

        newPos = new Vector3(newPos.x + (eventData.delta.x *-1)/Cam.orthographicSize, newPos.y + (eventData.delta.y*-1) / Cam.orthographicSize, 0);

        Cam.transform.position = newPos;

        //Handle pinch zoom here


        //Detect Scroll Wheel or 2 thumb 


        /*
        //Get the new Position
        newPos = new Vector3(newPos.x + eventData.delta.x, newPos.y, 0);
        transform.localPosition = newPos;

        //Get the distance from the original position
        Vector3 distance = OGPos - transform.position;

        //Display colour
        if (distance.x >= 2)
        {
            transform.parent.parent.GetComponent<Image>().color = Color.red;
        }
        else
        {
            transform.parent.parent.GetComponent<Image>().color = Color.cyan;
        }
        */

    }

    public void OnPointerUp(PointerEventData eventData)
    {

        /*
        Vector3 distance = OGPos - transform.position;

        float dist = Vector3.Distance(transform.position, OGPos);

        if (dist <= 0.9f)
        {

            transform.position = OGPos;
            transform.GetComponent<Program>().indent = 0;


        }
        else if (distance.x >= 2.2f)
        {
            //Delete the line
            transform.GetComponent<Program>().progLine.GetComponent<ProgramLine>().deleteLine();
            Debug.Log("Delete");
        }
        else if (distance.x < -1 && distance.x > -2)
        {
            //Set indent pos
            transform.position = OGPos + new Vector3(1, 0, 0);
            //Set indentation
            transform.GetComponent<Program>().indent = 1;
        }
        else if (distance.x < -2 && distance.x > -3)
        {
            //Set indent pos
            transform.position = OGPos + new Vector3(2, 0, 0);
            //Set indentation
            transform.GetComponent<Program>().indent = 2;

        }
        else if (distance.x < -3 && distance.x > -4)
        {
            //Set indent pos
            transform.position = OGPos + new Vector3(3, 0, 0);
            //Set indentation
            transform.GetComponent<Program>().indent = 3;

        }
        else
        {
            transform.position = lastPos;

            transform.parent.parent.GetComponent<ProgramLine>().ProgramUI.setSize(transform.parent.parent.GetComponent<ProgramLine>().ProgramUI.size);
        }
        */

    }


    /*
    public void BackCalc ()
    {

        Vector2 mouse = Input.mousePosition;

        float x = mouse.x - Mathf.Abs(screenPos.x);
        float y = mouse.y - Screen.height;

        float normalX = x / viewSize.x;
        float normalY = y / viewSize.y;

       

        return new Vector2(normalX * 1920, 1080 * (1 + normalY));
    }
    */


}
