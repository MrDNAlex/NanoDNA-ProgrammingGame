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
    }

    public void setUI ()
    {
        flex = new Flex(GetComponent<RectTransform>(), 1);

        Flex Button = new Flex(flex.getChild(0), 1, flex);

        Flex LogoHolder = new Flex(Button.getChild(0), 1, Button);

        Flex Logo = new Flex(LogoHolder.getChild(0), 1, LogoHolder);

        Flex Text = new Flex(Button.getChild(1), 3, Button);

        Button.setHorizontalPadding(0.2f, 1, 0.2f, 1);

        Button.setSpacingFlex(0.1f, 1);

        LogoHolder.setAllPadSame(0.1f, 1);

        Logo.setSquare();

    }

    public void setText (string text)
    {
        UIHelper.setText(flex.getChild(0).GetChild(1), text, PlayerSettings.colourScheme.getBlackTextColor(), PlayerSettings.getBigText());
    }

    public void setImage (ActionType tag)
    {
        string path = "";
        string logoPath = "";

        switch (tag)
        {
            case  ActionType.Movement:
                path = "Images/UIDesigns/StoreSections/MoveTab";
                logoPath = "Images/UIDesigns/StoreSections/MovementLogo";
                break;
            // case 1:
            //   path = "Images/UIDesigns/StoreSections/Math";
            //  break;
            case ActionType.Logic:
                path = "Images/UIDesigns/StoreSections/LogicTab";
                logoPath = "Images/UIDesigns/StoreSections/LogicLogo";
                break;
            case ActionType.Variable:
                path = "Images/UIDesigns/StoreSections/VariableTab";
                logoPath = "Images/UIDesigns/StoreSections/VariableLogo";
                break;
            case ActionType.Action:
                path = "Images/UIDesigns/StoreSections/ActionTab";
                logoPath = "Images/UIDesigns/StoreSections/ActionLogo";
                break;
        }

        UIHelper.setImage(flex.getChild(0), path);
        UIHelper.setImage(flex.getChild(0).GetChild(0).GetChild(0), logoPath);
    }

   
}
