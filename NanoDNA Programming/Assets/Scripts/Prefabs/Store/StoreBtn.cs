using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DNAStruct;
using UnityEngine.Rendering;
using FlexUI;
using DNASaveSystem;

public class StoreBtn : MonoBehaviour
{

    public ActionType actionType;

    public Button.ButtonClickedEvent onclick;


    public Flex flex;

    private void Awake()
    {
        onclick = transform.GetChild(0).GetComponent<Button>().onClick;
        setUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        OnDemandRendering.renderFrameInterval = 12;
    }

    public void setUI ()
    {
        flex = new Flex(GetComponent<RectTransform>(), 1);

        Flex Button = new Flex(flex.getChild(0), 1, flex);

        Flex Text = new Flex(Button.getChild(0), 1, Button);

        Button.setHorizontalPadding(0.1f, 1, 0.1f, 1);

    }

    public void setText (string text)
    {
        UIHelper.setText(flex.getChild(0).GetChild(0), text, PlayerSettings.colourScheme.getBlackTextColor());
    }

    public void setImage ()
    {
        string path = "";
        switch (this.transform.GetSiblingIndex())
        {
            case 0:
                path = "Images/UIDesigns/StoreSections/Movement";
                break;
            case 1:
                path = "Images/UIDesigns/StoreSections/Math";
                break;
            case 2:
                path = "Images/UIDesigns/StoreSections/Logic";
                break;
            case 3:
                path = "Images/UIDesigns/StoreSections/Variable";
                break;
            case 4:
                path = "Images/UIDesigns/StoreSections/Action";
                break;
        }

        UIHelper.setImage(flex.getChild(0), path);
    }

   
}
