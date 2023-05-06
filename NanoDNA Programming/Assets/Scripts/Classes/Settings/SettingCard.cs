using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;
using DNAStruct;
using DNASaveSystem;

public class SettingCard : MonoBehaviour
{
    //Change this later
    [SerializeField] SettingCardType type;

    public Flex flex;

    public Button.ButtonClickedEvent onClick;
    public Slider.SliderEvent onChange;

    public Color mainColor;
    public Color accentColor;


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

    void setUI()
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

    public void setColourScheme()
    {
        this.mainColor = PlayerSettings.colourScheme.getMainTextColor();

        this.accentColor = PlayerSettings.colourScheme.getAccentTextColor();

        UIHelper.setImage(flex.getChild(1), PlayerSettings.colourScheme.getAccent());
    }

    public void setInfoButton(string name, string value)
    {
        UIHelper.setText(flex.getChild(0), name, mainColor, PlayerSettings.getBigText());

        UIHelper.setText(flex.getChild(1).GetChild(0), value, mainColor, PlayerSettings.getBigText());

    }

    public void setInfoSlider(string name, int value)
    {
        //Value is int from 0-100
        UIHelper.setText(flex.getChild(0), name, mainColor, PlayerSettings.getBigText());

        UIHelper.setText(flex.getChild(1).GetChild(1), value + "%", mainColor, PlayerSettings.getBigText());

        flex.getChild(1).GetChild(0).GetComponent<Slider>().value = (float)value / 100;
    }






}


