using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DNAStruct;
using UnityEngine.Rendering;
using DNAMathAnimation;

public class MathDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    Vector3 OGpos;

    Vector3 lastPos;
    Vector3 newPos;

    CardInfo info;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // lastChildIndex = UIObject.GetSiblingIndex();
        // Debug.Log("Begin: " +transform.localPosition);
        lastPos = transform.localPosition;
        newPos = transform.localPosition;


        //Adapt this to the math type program cards





        info = new CardInfo();

        //Get the info from the StoreCardDragInfo

        info.actionType = transform.GetComponent<ProgramCard>().actionType;

        info.movementName = transform.GetComponent<ProgramCard>().movementName;
        info.mathName = transform.GetComponent<ProgramCard>().mathName;
        info.logicName = transform.GetComponent<ProgramCard>().logicName;
        info.variableName = transform.GetComponent<ProgramCard>().variableName;
        info.actionName = transform.GetComponent<ProgramCard>().actionName;

    }

    public void OnDrag(PointerEventData eventData)
    {
        //Get the new Position
        newPos = new Vector3(newPos.x + eventData.delta.x, newPos.y + eventData.delta.y, 0);
        transform.localPosition = newPos;

        RectTransform content = Camera.main.GetComponent<LevelScript>().contentTrans;

        for (int i = 0; i < content.childCount; i++)
        {
            Transform child = content.GetChild(i);

            MouseOverDetect mouse = content.GetChild(i).GetChild(1).GetComponent<MouseOverDetect>();

            if (mouse.mouseOver)
            {
                child.GetComponent<Image>().color = Color.red;
            }
            else
            {
                child.GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RectTransform content = Camera.main.GetComponent<LevelScript>().contentTrans;

        for (int i = 0; i < content.childCount; i++)
        {
            Transform child = content.GetChild(i);
            MouseOverDetect mouse = content.GetChild(i).GetChild(1).GetComponent<MouseOverDetect>();

            if (mouse.mouseOver)
            {
                child.GetComponent<ProgramLine>().addProgram(info, transform);
            }
        }

        //So the glitch is being caused by the text for lines being used updating, it updates all the position of the UI, but it looks like we can hide this glitch by making this animation faster
        // StartCoroutine(DNAMathAnim.animateCosinusoidalRelocationLocal(transform, lastPos, 300, 0, false));

        StartCoroutine(DNAMathAnim.animateReboundRelocationLocal(transform, lastPos, 300, 0, false));
    }
}
