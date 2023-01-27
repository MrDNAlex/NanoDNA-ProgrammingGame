using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    //Object that will end up being dragged. 
    [SerializeField] Transform UIObject;

    int lastChildIndex;
    Vector3 lastPos;
    Transform lastParent;
    Vector3 newPos;


    public void OnPointerDown(PointerEventData eventData)
    {

       // lastChildIndex = UIObject.GetSiblingIndex();
        lastPos = UIObject.position;
        lastParent = UIObject.parent;
        newPos = UIObject.localPosition;
      
    }

    public void OnDrag(PointerEventData eventData)
    {

        //Maybe add a timer for an animation? That will fix the glitchyness

        //Get the new Position
        newPos = new Vector3(newPos.x, newPos.y + eventData.delta.y, 0);
        UIObject.localPosition = newPos;


        //Debug.Log(transform.position);


        //Loop through all other children.

        for (int i = 0; i < lastParent.childCount; i++)
        {
            //Make sure it's not the same child
            if (i != UIObject.GetSiblingIndex())
            {
                //Get the other objects information 
                Transform iChild = lastParent.GetChild(i);
                float distance = Vector3.Distance(UIObject.localPosition, iChild.localPosition);

                //Check if it's within distance threshold
                if (distance <= 90)
                {
                    //Switch positions
                    Vector3 iPos = iChild.position;
                    iChild.localPosition = lastPos;
                    UIObject.SetSiblingIndex(iChild.GetSiblingIndex());
                    //lastChildIndex = UIObject.GetSiblingIndex();
                    lastPos = iPos;
                    //transform.position = iPos;

                }

            }
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Set Position
        UIObject.position = lastPos;
    }


}
