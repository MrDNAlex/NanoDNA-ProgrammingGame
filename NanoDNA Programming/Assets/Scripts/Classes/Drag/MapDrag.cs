using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Tilemaps;



public class MapDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IScrollHandler
{

    [SerializeField] Camera Cam;
    [SerializeField] Tilemap tileMap;
    [SerializeField] GameObject content;

    Vector3 lastPos;
    Vector3 newPos;
    string type;

    Vector3 OGPos;

    float OrthoSize;

    bool zoom;

  

    public void OnScroll(PointerEventData eventData)
    {
        float ortho = Cam.orthographicSize - Input.mouseScrollDelta.y / 2;

        ortho = Mathf.Clamp(ortho, 1, 30);

        //Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, ortho, Time.deltaTime);

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
       
        newPos = Cam.transform.position;

        //Send raycast to collide with a character

        RaycastHit rayHit;

        Vector2 worldPos = Cam.ScreenToWorldPoint(BackCalcPos());

        if (Physics.Raycast(new Vector3(worldPos.x, worldPos.y, 0), Vector3.forward, out rayHit, Mathf.Infinity))
        {
           

            //Check if there is character data associated
            if (rayHit.collider.GetComponent<CharData>() != null)
            {
                //content.GetComponent<ProgramSection>().character = rayHit.collider.gameObject;

                ProgramSection sec = Camera.main.GetComponent<LevelScript>().progSec;

               sec.renderProgram(rayHit.collider.gameObject);

                sec.updateOGPos();


            }
            
        } else
        {
            //Remove the script

        }
       
    }

    public void OnDrag(PointerEventData eventData)
    {

        float normalX = eventData.delta.x / GetComponent<RectTransform>().sizeDelta.x;

        float normalY = eventData.delta.y / GetComponent<RectTransform>().sizeDelta.y;


        newPos = new Vector3(newPos.x + (tileNumHor()*normalX*tileMap.cellSize.x * -1), newPos.y + ((tileNumVert() * normalY * tileMap.cellSize.y * -1)) , 0);

        Cam.transform.position = newPos;

        //Handle pinch zoom here


        //Detect Scroll Wheel or 2 thumb 

    }

    public void OnPointerUp(PointerEventData eventData)
    {

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

    public Vector2 BackCalcPos()
    {
        //Debug.Log("Mouse:" + Input.mousePosition);


        // Debug.Log("ScreenPos:" + screenPos);
        // Debug.Log("ScreenSize:" + viewSize);


        Vector2 mouse = Input.mousePosition;

        float x = mouse.x - Mathf.Abs(transform.GetComponent<RectTransform>().rect.x);
        float y = mouse.y - Screen.height;

        float normalX = x / transform.GetComponent<RectTransform>().sizeDelta.x;
        float normalY = y / transform.GetComponent<RectTransform>().sizeDelta.y;

      
        // Debug.Log("x:" + x);
        // Debug.Log("y:" + y);
        // Debug.Log("Nx:" + normalX);
        // Debug.Log("Ny:" + normalY);

        // Debug.Log(new Vector2(normalX * 1920, 1080 * (1 + normalY)));

        return new Vector2(normalX * 1920, 1080 * (1 + normalY));

    }


}
