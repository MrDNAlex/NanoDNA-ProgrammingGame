using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Tilemaps;



public class MapDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    [SerializeField] Camera Cam;
    [SerializeField] Tilemap tileMap;

    Vector3 lastPos;
    Vector3 newPos;
    string type;

    Vector3 OGPos;

    float OrthoSize;

    
    private void OnGUI()
    {
        
       float ortho = Cam.orthographicSize - Input.mouseScrollDelta.y/3;

        ortho = Mathf.Clamp(ortho, 1, 30);
        

        Cam.orthographicSize = ortho;
    }
    

   

  
    

    // Start is called before the first frame update
    void Start()
    {
        //OGPos = transform.position;

        OrthoSize = Cam.orthographicSize;
        
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

        Debug.Log(tileNumVert());
        Debug.Log(tileNumHor());


    }

    public void OnDrag(PointerEventData eventData)
    {

        float normalX = eventData.delta.x / GetComponent<RectTransform>().sizeDelta.x;

        float normalY = eventData.delta.y / GetComponent<RectTransform>().sizeDelta.y;


        newPos = new Vector3(newPos.x + (tileNumHor()*normalX*tileMap.cellSize.x * -1), newPos.y + ((tileNumVert() * normalY * tileMap.cellSize.y * -1)) , 0);

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


    
    public Vector2 BackCalc (Vector2 input)
    {

       

        float x = input.x;
        float y = input.y;

        float normalX = x / Screen.width;
        float normalY = y / Screen.height;

       

        return new Vector2(normalX * this.GetComponent<RectTransform>().sizeDelta.x, this.GetComponent<RectTransform>().sizeDelta.y * (1 + normalY));
    }
    

    public float tileNumVert ()
    {
        return (Cam.orthographicSize * 2) / (tileMap.cellSize.y);
    }

    public float tileNumHor()
    {
        return (Cam.orthographicSize * 2) / (tileMap.cellSize.x * ((float)Screen.height / (float)Screen.width));
    }


}
