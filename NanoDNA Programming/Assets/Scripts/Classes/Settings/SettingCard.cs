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
            //Size will be 800x100
            case SettingCardType.Button:

                Flex Background = new Flex(this.transform.GetComponent<RectTransform>(), 1);

                Flex Name = new Flex(Background.getChild(0), 4.5f, Background);
                Flex Button = new Flex(Background.getChild(1), 4.5f, Background);

                Background.setSpacingFlex(1, 1);

                break;
        }

    }

    public void setInfo ()
    {

    }
}


