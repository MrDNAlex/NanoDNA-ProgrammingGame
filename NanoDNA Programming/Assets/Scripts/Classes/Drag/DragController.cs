using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DNAMathAnimation;
using DNAStruct;

public class DragController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    //Object that will end up being dragged. 
    [SerializeField] Transform UIObject;

    int lastChildIndex;
    Vector3 lastPos;
    Transform lastParent;
    Vector3 newPos;

   public bool animating;

    bool updateRender;


    private void Update()
    {
       
    }

    public void OnPointerDown(PointerEventData eventData)
    {

       // lastChildIndex = UIObject.GetSiblingIndex();
        lastPos = UIObject.localPosition;
        lastParent = UIObject.parent;
        newPos = UIObject.localPosition;
      
    }

    public void OnDrag(PointerEventData eventData)
    {

        //Maybe add a timer for an animation? That will fix the glitchyness
        //Get the new Position
        newPos = new Vector3(newPos.x, newPos.y + eventData.delta.y, 0);
        UIObject.localPosition = newPos;

        //Loop through all other children.
        if (!animating)
        {
            for (int i = 0; i < lastParent.childCount; i++)
            {
                //Make sure it's not the same child
                if (i != UIObject.GetSiblingIndex())
                {
                    //Get the other objects information 
                    Transform iChild = lastParent.GetChild(i);
                    float distance = Vector3.Distance(UIObject.localPosition, iChild.localPosition);

                    //Check if it's within distance threshold
                    if (distance <= iChild.GetComponent<RectTransform>().sizeDelta.y/2)
                    {
                        //Change the order of the program actions too
                        Scripts.programSection.selectedCharData.program.SwitchActions(UIObject.GetSiblingIndex(), i);
                        //Switch positions
                        Vector3 iPos = iChild.localPosition;

                        animating = true;
                        updateRender = true;
                        StartCoroutine(animateCosinusoidalRelocationLocal(iChild, lastPos, DNAMathAnim.getFrameNumber(1), 1, true, true, UIObject.GetSiblingIndex()));
                        lastPos = iPos;

                    }
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Set Position
        UIObject.localPosition = lastPos;

        Scripts.programSection.selectedCharData.displayProgram(true);


        //Rerender program
    }


    public IEnumerator animateCosinusoidalRelocationLocal(Transform trans, Vector3 OGPos, int frameCount, int index, bool singleAxis, bool changeIndex = false, int newIndex = 0)
    {
        animating = true;

        //Index determines axis
        //x = 0
        //y = 1
        //z = 0

        Vector3 startPos = trans.localPosition;
        Vector3 fullAdd = Vector3.zero;

        //Length coefficient
        float B = (Mathf.PI) / (frameCount * 2);
        //Amplitude
        float A = 0;
        Vector3 As = Vector3.zero;

        if (singleAxis)
        {
            switch (index)
            {
                case 0:
                    A = DNAMathAnim.calcCosAmplitude(OGPos.x - trans.localPosition.x, B, frameCount);
                    break;
                case 1:
                    A = DNAMathAnim.calcCosAmplitude(OGPos.y - trans.localPosition.y, B, frameCount);
                    break;
                case 2:
                    A = DNAMathAnim.calcCosAmplitude(OGPos.z - trans.localPosition.z, B, frameCount);
                    break;
            }
           // Debug.Log(A);
        }
        else
        {
            As = new Vector3(DNAMathAnim.calcCosAmplitude(OGPos.x - trans.localPosition.x, B, frameCount), DNAMathAnim.calcCosAmplitude(OGPos.y - trans.localPosition.y, B, frameCount), DNAMathAnim.calcCosAmplitude(OGPos.z - trans.localPosition.z, B, frameCount));
        }

        for (int i = 0; i < frameCount; i++)
        {
            if (singleAxis)
            {
                float add = DNAMathAnim.sinEQ(A, B, 0, 0, i);
                switch (index)
                {
                    case 0:
                        fullAdd = new Vector3(fullAdd.x + add, fullAdd.y, fullAdd.z);
                        break;
                    case 1:
                        fullAdd = new Vector3(fullAdd.x, fullAdd.y + add, fullAdd.z);
                        break;
                    case 2:
                        fullAdd = new Vector3(fullAdd.x, fullAdd.y, fullAdd.z + add);
                        break;
                }
            }
            else
            {
                Vector3 add = new Vector3(DNAMathAnim.cosEQ(As[0], B, 0, 0, i), DNAMathAnim.cosEQ(As[1], B, 0, 0, i), DNAMathAnim.cosEQ(As[2], B, 0, 0, i));
                fullAdd = fullAdd + add;
            }
            trans.localPosition = startPos + fullAdd;
            yield return null;
        }

        if (changeIndex)
        {
            trans.SetSiblingIndex(newIndex);
            trans.GetComponent<ProgramLine>().setNumber();
            UIObject.GetComponent<ProgramLine>().setNumber();
        }

        animating = false;
        trans.localPosition = OGPos;
        Scripts.programSection.renderProgram();
    }
}
