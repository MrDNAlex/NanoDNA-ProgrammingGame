using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;
using DNAStruct;

public class SettingCard : MonoBehaviour
{
    //Change this later
    [SerializeField] SettingCardType type;

    public Flex flex;

    public Button.ButtonClickedEvent onClick;
    public Slider.SliderEvent onChange;


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

    void setUI ()
    {
        //Check what type it is potentially?
        switch (type)
        {
            //Everything must sum up to 8
            //Size will be 800x100
            case SettingCardType.Button:

                flex = new Flex(this.transform.GetComponent<RectTransform>(), 1);

                //Both 4.5f
                Flex ButtonName = new Flex(flex.getChild(0), 4f, flex);
                Flex Button = new Flex(flex.getChild(1), 4f, flex);

                //Used to be 1
               // flex.setSpacingFlex(2, 1);

                onClick = flex.getChild(1).GetComponent<Button>().onClick;
                break;

            case SettingCardType.Slider:
                flex = new Flex(this.transform.GetComponent<RectTransform>(), 1);

                Flex SliderName = new Flex(flex.getChild(0), 4f, flex);

                Flex SlideHolder = new Flex(flex.getChild(1), 4, flex);

                Flex Slider = new Flex(SlideHolder.getChild(0), 3f, SlideHolder);
                Flex SliderPercent = new Flex(SlideHolder.getChild(1), 1f, SlideHolder);

                //flex.setSpacingFlex(2, 1);

               
                onChange = flex.getChild(1).GetChild(0).GetComponent<Slider>().onValueChanged;
                break;
        }

    }

    public void setInfoButton (string name, string value)
    {
        flex.getChild(0).GetComponent<Text>().text = name;

        flex.getChild(1).GetChild(0).GetComponent<Text>().text = value;
    }

    public void setInfoSlider (string name, int value)
    {
        //Value is int from 0-100
        flex.getChild(0).GetComponent<Text>().text = name;

        flex.getChild(1).GetChild(0).GetComponent<Slider>().value = (float)value / 100;

        flex.getChild(1).GetChild(1).GetComponent<Text>().text = value + "%";
    }
}


