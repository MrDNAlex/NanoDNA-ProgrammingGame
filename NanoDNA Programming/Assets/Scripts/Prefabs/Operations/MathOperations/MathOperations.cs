using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;
using DNAStruct;

public class MathOperations : MonoBehaviour
{

    public ProgramCard.PanelInfo panelInfo;
    public MathOperationData operationInfo;

    public IMathOperation IOperation;

    public UIWord cardName;

    public Flex flex;
    public Transform trans;
    public RectTransform rectTrans;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool noPanelOpen()
    {
        if (Camera.main.transform.GetChild(0).GetChild(2).childCount == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void getReferences()
    {
        trans = this.GetComponent<Transform>();
        rectTrans = this.GetComponent<RectTransform>();
    }
}
