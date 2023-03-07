using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DNAMathAnimation;

public class DeleteIndentDrag : MonoBehaviour,  IDragHandler, IBeginDragHandler, IEndDragHandler
{
    
    Vector3 lastPos;
    Vector3 newPos;
    string type;

    Vector3 OGPos;

    bool firstRun = true;

    Vector3 mouseStart;

    
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (firstRun)
        {
            firstRun = false;
            OGPos = transform.position;
           // Debug.Log("Init Pos: " + transform.position);
        }

        
    }

    public void OnBeginDrag (PointerEventData eventData)
    {
        //Get last and current position
        newPos = transform.localPosition;
        lastPos = transform.localPosition;

        mouseStart = Input.mousePosition;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 dist = mouseStart - Input.mousePosition;

        float NewPosx = lastPos.x - Mathf.Max(0, dist.x);

        transform.localPosition = new Vector3(NewPosx, lastPos.y, lastPos.z);

        //Display colour
        if (dist.x >= GetComponent<RectTransform>().sizeDelta.x * (0.75f))
        {
            transform.parent.parent.GetComponent<Image>().color = Color.red;
        } else
        {
            transform.parent.parent.GetComponent<Image>().color = Color.cyan;
        }
    }

    public void OnEndDrag (PointerEventData eventData)
    {
        Vector3 dist = mouseStart - Input.mousePosition;

        if (dist.x >= GetComponent<RectTransform>().sizeDelta.x * (0.75f))
        {
            //Delete the line
            StartCoroutine(transform.GetComponent<ProgramCard>().progLine.GetComponent<ProgramLine>().delLines());
            //transform.GetComponent<ProgramCard>().progLine.GetComponent<ProgramLine>().deleteProgramLine(transform.GetSiblingIndex());
        } else
        {
            //Start Coroutine to slide it back to original position

            transform.parent.parent.GetComponent<ProgramLine>().ProgramUI.setSize(transform.parent.parent.GetComponent<ProgramLine>().ProgramUI.size);

            StartCoroutine(DNAMathAnim.animateCosinusoidalRelocationLocal(transform, lastPos, 100, 0, false));
        }
    }

    public void updateOGPos ()
    {
       // Debug.Log("hi");
        OGPos = transform.position;

    }

   
    
}
