using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DNAMathAnimation;
using DNASaveSystem;

public class DeleteIndentDrag : MonoBehaviour,  IDragHandler, IBeginDragHandler, IEndDragHandler
{
    
    Vector3 lastPos;
    Vector3 newPos;
    string type;

    Vector3 OGPos;

    bool firstRun = true;

    Vector3 mouseStart;
    Vector3 contentStart;

    Transform contentTrans;

    float contentVelocity;
    float NewContentPos;

    // Start is called before the first frame update
    void Start()
    {
        contentTrans = transform.parent.parent.parent;
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
        contentStart = contentTrans.localPosition;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 dist = mouseStart - Input.mousePosition;

        float NewPosx = lastPos.x - Mathf.Max(0, dist.x);

        contentVelocity = ((contentStart.y - dist.y) - NewContentPos)/Time.deltaTime;

        NewContentPos = contentStart.y - dist.y;

        contentTrans.localPosition  = new Vector3(contentStart.x, NewContentPos, contentStart.y); 

        transform.localPosition = new Vector3(NewPosx, lastPos.y, lastPos.z);

        //Display colour
        if (dist.x >= GetComponent<RectTransform>().sizeDelta.x * (0.75f))
        {
            UIHelper.setImage(transform.parent.parent, "Images/UIDesigns/DelColor");
        } else
        {
            UIHelper.setImage(transform.parent.parent, PlayerSettings.colourScheme.getMain(true));
        }
    }

    public void OnEndDrag (PointerEventData eventData)
    {
        Vector3 dist = mouseStart - Input.mousePosition;

        if (dist.x >= GetComponent<RectTransform>().sizeDelta.x * (0.75f))
        {
            //Delete the line
            //StartCoroutine(transform.GetComponent<ProgramCard>().progLine.GetComponent<ProgramLine>().delLines());
            transform.GetComponent<ProgramCard>().progLine.GetComponent<ProgramLine>().deleteProgramLine(transform.parent.parent.GetSiblingIndex());

            UIHelper.setImage(transform.parent.parent, PlayerSettings.colourScheme.getMain(true));
        } else
        {
            //Start Coroutine to slide it back to original position

            if (!transform.parent.parent.GetChild(0).GetComponent<DragController>().animating)
            {
                transform.parent.parent.GetComponent<ProgramLine>().ProgramUI.setSize(transform.parent.parent.GetComponent<ProgramLine>().ProgramUI.size);

                StartCoroutine(DNAMathAnim.animateReboundRelocationLocal(transform, lastPos, DNAMathAnim.getFrameNumber(0.75f), 0, false));
            }
        }

        contentTrans.parent.parent.GetComponent<ScrollRect>().velocity = new Vector2(0, contentVelocity);
    }

    public void updateOGPos ()
    {
       // Debug.Log("hi");
        OGPos = transform.position;

    }

   



}
