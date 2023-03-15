using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using DNAStruct;
using UnityEngine.UI;
using DNASaveSystem;

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
       // playSettings = SaveManager.loadPlaySettings();
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
            
            flex.setHorizontalPadding(0.1f, 1, 0.1f, 1);
            flex.setVerticalPadding(0.3f, 1, 0.3f, 1);
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

            UIHelper.setImage(flex.UI, PlayerSettings.colourScheme.getSecondary());

        }

    }

    public void setInfo (VariableData varData)
    {
        if (singleVal)
        {
            UIHelper.setText(transform.GetChild(0), varData.name, PlayerSettings.colourScheme.getBlackTextColor());

            this.varData = varData;
        } else
        {
            UIHelper.setText(transform.GetChild(0), varData.name, PlayerSettings.colourScheme.getBlackTextColor());

            UIHelper.setText(transform.GetChild(1), varData.value.ToString(), PlayerSettings.colourScheme.getBlackTextColor());

            this.varData = varData;
        }
       
    }


}
