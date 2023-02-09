using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using DNASaveSystem;



public class MapDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IScrollHandler
{
    [SerializeField] Camera Cam;
    [SerializeField] Tilemap tileMap;
    [SerializeField] GameObject content;
    [SerializeField] Slider zoomSlide;
    [SerializeField] Button resize;
    [SerializeField] Tilemap BackAndMap;

    Vector3 lastPos;
    Vector3 newPos;
    string type;

    Vector3 OGPos;

    float orthoSize;

    float maxOrthoSize;
    float minOrthoSize;

   
    public void OnScroll(PointerEventData eventData)
    {
       
        StartCoroutine(smoothZoomMouse());

    }

    // Start is called before the first frame update
    void Start()
    {
        //OGPos = transform.position;

        orthoSize = Cam.orthographicSize;

        maxOrthoSize = Camera.main.GetComponent<LevelScript>().getOrthoSize()*2;
        minOrthoSize = maxOrthoSize / 8;

        zoomSlide.value = 0.5f;

        resize.onClick.AddListener(ResizeCam);

        zoomSlide.onValueChanged.AddListener(delegate
        {
            Cam.orthographicSize = zoomCalc(zoomSlide.value);
        });

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

                // sec.compileProgram();
                ProgramSection sec = Camera.main.GetComponent<LevelScript>().progSec;

                sec.renderProgram(rayHit.collider.gameObject);

               // sec.updateOGPos();

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

        return new Vector2(normalX * Screen.width, Screen.height * (1 + normalY));

    }

    public float zoomCalc (float value)
    {
     
        float slope = (minOrthoSize - maxOrthoSize) / (1);

        float intercept = maxOrthoSize;

        //y = mx + b equation
        return slope * value + intercept;

    }

    public float sliderValCalc (float ortho)
    {
        float slope = (minOrthoSize - maxOrthoSize) / (1);

        float intercept = maxOrthoSize;

        return (ortho - intercept) / slope;
    }

    public IEnumerator smoothZoomMouse ()
    {
        //Sinusoidal movement could be cool
        
        float slope = (Cam.orthographicSize - (Cam.orthographicSize + Input.mouseScrollDelta.y)) / (50);

        float intercept = Cam.orthographicSize;

        for (int i = 0; i < 50; i ++)
        {
            orthoSize = slope * i + intercept;

            orthoSize = Mathf.Clamp(orthoSize, minOrthoSize, maxOrthoSize);

            Cam.orthographicSize = orthoSize;

            zoomSlide.value = sliderValCalc(orthoSize);

            yield return new WaitForSeconds(0.00005f);

        }

    }

    public void ResizeCam()
    {

        LevelInfo info = SaveManager.loadSaveFromPath(Camera.main.GetComponent<LevelScript>().levelPath);

        Cam.orthographicSize = orthoSizeCalc(info);
        Cam.transform.position = Vector3.zero;
    }

    public float orthoSizeCalc(LevelInfo info)
    {

        //Fit vertically
        float vertOrthoSize = ((float)((info.yMax - info.yMin) + 1) / 2 * BackAndMap.cellSize.y);

        //Fit Horizontally
        float horOrthoSize = ((float)((info.xMax - info.yMin) + 1) / 2 * (BackAndMap.cellSize.x * ((float)Screen.height / (float)Screen.width)));

        if (vertOrthoSize >= horOrthoSize)
        {
            zoomSlide.value = sliderValCalc(vertOrthoSize);
            //Give Vert
            return vertOrthoSize;
        }
        else
        {
            zoomSlide.value = sliderValCalc(horOrthoSize);
            //Give Hor
            return horOrthoSize;
        }

    }


}
