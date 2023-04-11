using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DNAStruct;
using UnityEngine.Rendering;
using DNAMathAnimation;


public class StoreDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Vector3 OGpos;

    Vector3 lastPos;
    Vector3 newPos;

    CardInfo info;

    Transform parentObj;
    int siblingIndex;

    GameObject copy;

    // Start is called before the first frame update
    void Start()
    {
        OGpos = transform.position;
    }

    //Instantiate a new copy of the game object and make the current one invisible

    public void OnBeginDrag(PointerEventData eventData)
    {
        // lastChildIndex = UIObject.GetSiblingIndex();
        // Debug.Log("Begin: " +transform.localPosition);
       
        //Spawn Copy
        copy = Instantiate(this.gameObject, transform.parent.parent.parent.parent.parent);

        //PLace it in the same position
        copy.transform.position = transform.position;

        //Save local positions
        lastPos = copy.transform.localPosition;
        newPos = copy.transform.localPosition;

        transform.GetComponent<CanvasGroup>().alpha = 0;

        info = new CardInfo();

        //Get the info from the StoreCardDragInfo
        info.actionType = transform.GetComponent<ProgramCard>().actionInfo.actionType;

        info.movementName = transform.GetComponent<ProgramCard>().actionInfo.movementName;
        info.logicName = transform.GetComponent<ProgramCard>().actionInfo.logicName;
        info.variableName = transform.GetComponent<ProgramCard>().actionInfo.variableName;
        info.actionName = transform.GetComponent<ProgramCard>().actionInfo.actionName;

    }

    public void OnDrag(PointerEventData eventData)
    {
        //Get the new Position
        newPos = new Vector3(newPos.x + eventData.delta.x, newPos.y + eventData.delta.y, 0);
        // transform.localPosition = newPos;
        copy.transform.localPosition = newPos;

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

        StartCoroutine(animateReturn());
    }

    IEnumerator animateReturn ()
    {
        yield return StartCoroutine(DNAMathAnim.animateReboundRelocationLocal(copy.transform, lastPos, DNAMathAnim.getFrameNumber(1f), 0, false));
        Destroy(copy);
        transform.GetComponent<CanvasGroup>().alpha = 1;
    }
}
