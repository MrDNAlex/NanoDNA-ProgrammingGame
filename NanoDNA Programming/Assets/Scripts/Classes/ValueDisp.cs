using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using DNAStruct;
using UnityEngine.UI;

public class ValueDisp : MonoBehaviour
{
    // public VariableType varType;
    // public string name;
    //  public object value;

    public bool singleVal;

    public Flex flex;

    public VariableData varData;

    private void Awake()
    {
        setUI();
       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUI ()
    {
        if (singleVal)
        {
           // Debug.Log("Hi");
            flex = new Flex(this.GetComponent<RectTransform>(), 1);

            Flex Image = new Flex(flex.getChild(0), 5);

            Flex Name = new Flex(flex.getChild(1), 1f);

            flex.addChild(Image);
            flex.addChild(Name);
            

            //flex.setSpacingFlex(0.3f, 1);

            flex.setAllPadSame(0.1f, 1);
        } else
        {
            flex = new Flex(this.GetComponent<RectTransform>(), 1);

            Flex Name = new Flex(flex.getChild(0), 1);

            Flex Value = new Flex(flex.getChild(1), 1);

            flex.addChild(Name);
            flex.addChild(Value);

            flex.setSpacingFlex(0.3f, 1);

            flex.setHorizontalPadding(0.2f, 1, 0.2f, 1);

            flex.setVerticalPadding(0.07f, 1, 0.07f, 1);
        }

        

       
    }

    public void setInfo (VariableData varData)
    {
        if (singleVal)
        {
            this.transform.GetChild(0).GetComponent<Text>().text = varData.name;

            this.varData = varData;
        } else
        {
            this.transform.GetChild(0).GetComponent<Text>().text = varData.name;

            this.transform.GetChild(1).GetComponent<Text>().text = varData.value.ToString();

            this.varData = varData;
        }
       
    }




}
