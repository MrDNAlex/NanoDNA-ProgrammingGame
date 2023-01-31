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


}
