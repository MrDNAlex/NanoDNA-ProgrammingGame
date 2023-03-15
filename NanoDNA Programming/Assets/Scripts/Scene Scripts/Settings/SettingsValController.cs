using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;
using FlexUI;
using UnityEngine.UI;
using DNASaveSystem;
using DNAMathAnimation;

public class SettingsValController : MonoBehaviour
{
    Transform parent;

    Flex Parent;

    SettingEditType editType;
    SettingValueType editValue;

    Language lang;

    UIWord English = new UIWord("English", "Anglais");
    UIWord French = new UIWord("French", "Français");

    UIWord Advanced = new UIWord("Advanced", "Avancée");
    UIWord Simple = new UIWord("Simple", "Simple");

    UIWord Col1 = new UIWord("Colour 1", "Couleur 1");
    UIWord Col2 = new UIWord("Colour 2", "Couleur 2");
    UIWord Col3 = new UIWord("Colour 3", "Couleur 3");
    UIWord Col4 = new UIWord("Colour 4", "Couleur 4");
    UIWord Col5 = new UIWord("Colour 5", "Couleur 5");
    UIWord Col6 = new UIWord("Colour 6", "Couleur 6");
    UIWord Col7 = new UIWord("Colour 7", "Couleur 7");
    UIWord Col8 = new UIWord("Colour 8", "Couleur 8");

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setPanel(SettingEditType editType, SettingValueType editValue, Transform parent)
    {
        this.parent = parent;
        this.editType = editType;
        this.editValue = editValue;

       
        //Load Language
        lang = PlayerSettings.language;


        setUI();
        setControls();
        setColour();

        Vector3 currentPos = parent.localPosition;
        Vector3 startPos = currentPos + new Vector3(0, -Screen.height, 0);

        parent.localPosition = startPos;

        StartCoroutine(DNAMathAnim.animateReboundRelocationLocal(parent, currentPos, 150, 1, true));
    }

    void setColour()
    {
        switch (editType)
        {
            case SettingEditType.MultiChoice:


                //Accent / Background
                UIHelper.setImage(Parent.getChild(0), PlayerSettings.colourScheme.getAccent());


                //Main
                UIHelper.setImage(Parent.getChild(0).GetChild(1), PlayerSettings.colourScheme.getMain());
                break;
        }
    }




    public void setUI()
    {
        Parent = new Flex(parent.GetComponent<RectTransform>(), 1);


        switch (editType)
        {
            case SettingEditType.MultiChoice:

                Flex Panel = new Flex(this.transform.GetComponent<RectTransform>(), 1, Parent);

                Flex ExitRow = new Flex(Panel.getChild(0), 1, Panel);
                Flex ExitBTN = new Flex(ExitRow.getChild(0), 1, ExitRow);

                Flex Grid = new Flex(Panel.getChild(1), 9, Panel);

                Flex Row1 = null;
                Flex Row2 = null;
                Flex Row3 = null;


                if (editValue == SettingValueType.Language)
                {
                    //Do one row only
                    Row1 = new Flex(Grid.getChild(0), 1, Grid);

                    Row1.setSpacingFlex(0.2f, 1);
                }
                else
                {
                    Row1 = new Flex(Grid.getChild(0), 1, Grid);
                    Row2 = new Flex(Grid.getChild(1), 1, Grid);
                    Row3 = new Flex(Grid.getChild(2), 1, Grid);

                }

                Panel.setSpacingFlex(0.1f, 1);

                Panel.setVerticalPadding(0.1f, 1, 0.1f, 1);
                Panel.setHorizontalPadding(0.01f, 1, 0.01f, 1);

                ExitBTN.setSquare();

                ExitRow.setSelfHorizontalPadding(0, 1, 0.02f, 1);

                Grid.setSpacingFlex(0.2f, 1);

                Grid.setAllPadSame(0.1f, 1);

                //Set gridview
                setGridView(Grid);

                Parent.setSize(new Vector2(1000, 700));

                Row1.UI.GetComponent<HorizontalLayoutGroup>().spacing = 20;

                if (Row2 != null)
                {
                    Row2.UI.GetComponent<HorizontalLayoutGroup>().spacing = 20;
                    Row3.UI.GetComponent<HorizontalLayoutGroup>().spacing = 20;
                }

                break;
        }
    }

    public void setControls()
    {

        //Exit Button
        Parent.getChild(0).GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
        {
            parent.gameObject.SetActive(false);

            Destroy(this.gameObject);
        });


    }

    public void setGridView(Flex GridView)
    {


        switch (editValue)
        {
            case SettingValueType.Language:

                instantiateDisplayCard("Images/SettingsControllerAssets/English", GridView, English, 0, 0);
                instantiateDisplayCard("Images/SettingsControllerAssets/French", GridView, French, 1, 0);

                break;
            case SettingValueType.ColourScheme:
                instantiateDisplayCard("Images/SettingsControllerAssets/PalCol1", GridView, Col1, 0, 0);
                instantiateDisplayCard("Images/SettingsControllerAssets/PalCol2", GridView, Col2, 1, 0);
                instantiateDisplayCard("Images/SettingsControllerAssets/PalCol3", GridView, Col3, 2, 0);
                instantiateDisplayCard("Images/SettingsControllerAssets/PalCol4", GridView, Col4, 3, 0);

                instantiateDisplayCard("Images/SettingsControllerAssets/Col5", GridView, Col5, 4, 1);
                instantiateDisplayCard("Images/SettingsControllerAssets/Col6", GridView, Col6, 5, 1);
                instantiateDisplayCard("Images/SettingsControllerAssets/Col7", GridView, Col7, 6, 1);
                instantiateDisplayCard("Images/SettingsControllerAssets/Col8", GridView, Col8, 7, 1);
                break;
            case SettingValueType.AdvancedVariables:
                instantiateDisplayCard("Images/SettingsControllerAssets/Advanced", GridView, Advanced, 0, 0);
                instantiateDisplayCard("Images/SettingsControllerAssets/Simple", GridView, Simple, 1, 0);
                break;
        }


    }

    public void instantiateEmpty(Flex parent, int rowIndex)
    {
        GameObject valDisp = Instantiate(Resources.Load("Prefabs/EditPanels/Empty") as GameObject, parent.UI.GetChild(rowIndex).transform);

        Flex ValDisp = new Flex(valDisp.GetComponent<RectTransform>(), 1);

        ValDisp.setSquare();

        parent.getChild(rowIndex).GetComponent<FlexInfo>().flex.addChild(ValDisp);

    }
    public void instantiateDisplayCard(string imagePath, Flex parent, UIWord word, int index, int rowIndex)
    {
        //Determine Image, Instantiate Object and Set Text
        Texture2D image = Resources.Load(imagePath) as Texture2D;

        GameObject valDisp = Instantiate(Resources.Load("Prefabs/EditPanels/ValueDispCard") as GameObject, parent.UI.GetChild(rowIndex).transform);

        UIHelper.setText(valDisp.transform.GetChild(1), word.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor());

        valDisp.transform.GetChild(0).GetComponent<Image>().sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));

        UIHelper.setImage(valDisp.transform, PlayerSettings.colourScheme.getSecondary());

        valDisp.GetComponent<Button>().onClick.AddListener(delegate
        {

            //Edit the settings
            setData(index);

            Camera.main.GetComponent<SettingsScene>().updateSettings();

            this.parent.gameObject.SetActive(false);

            Destroy(this.gameObject);

        });

        //Create Flex, set Square and Add as Child
        Flex ValDisp = valDisp.GetComponent<ValueDisp>().flex;

        ValDisp.setSquare();

        parent.getChild(rowIndex).GetComponent<FlexInfo>().flex.addChild(ValDisp);

    }

    public void setData(int index)
    {
        switch (editValue)
        {
            case SettingValueType.Language:
                Debug.Log("set Language ");
                switch (index)
                {
                    case 0:
                        PlayerSettings.language = Language.English;
                        break;
                    case 1:
                        PlayerSettings.language = Language.French;
                        break;
                }

                break;
            case SettingValueType.ColourScheme:
                Color white = new Color(1, 1, 1, 1);
                Color black = new Color(0, 0, 0, 1);
                switch (index)
                {
                    case 0:
                        PlayerSettings.setColourScheme(SettingColourScheme.Col1, "Images/UIDesigns/Palettes/Palette 1");

                        PlayerSettings.colourScheme.textColorMain = white;
                        PlayerSettings.colourScheme.textColorAccent = black; //Try black

                        break;
                    case 1:
                        PlayerSettings.setColourScheme(SettingColourScheme.Col2, "Images/UIDesigns/Palettes/Palette 2");



                        PlayerSettings.colourScheme.textColorMain = white;
                        PlayerSettings.colourScheme.textColorAccent = black;
                        break;
                    case 2:
                        PlayerSettings.setColourScheme(SettingColourScheme.Col3, "Images/UIDesigns/Palettes/Palette 3");

                        PlayerSettings.colourScheme.textColorMain = Color.black;
                        PlayerSettings.colourScheme.textColorAccent = Color.black;

                        break;
                    case 3:
                        PlayerSettings.setColourScheme(SettingColourScheme.Col4, "Images/UIDesigns/Palettes/Palette 4");

                        PlayerSettings.colourScheme.textColorMain = Color.white;
                        PlayerSettings.colourScheme.textColorAccent = Color.black;

                        break;
                    case 4:
                        PlayerSettings.setColourScheme(SettingColourScheme.Col5, "Images/UIDesigns/Palettes/Palette 5");
                        break;
                    case 5:
                        PlayerSettings.setColourScheme(SettingColourScheme.Col6, "Images/UIDesigns/Palettes/Palette 6");
                        break;
                    case 6:
                        PlayerSettings.setColourScheme(SettingColourScheme.Col7, "Images/UIDesigns/Palettes/Palette 7");
                        break;
                    case 7:
                        PlayerSettings.setColourScheme(SettingColourScheme.Col8, "Images/UIDesigns/Palettes/Palette 8");
                        break;
                }
                break;

            case SettingValueType.AdvancedVariables:

                switch (index)
                {
                    case 0:
                        PlayerSettings.advancedVariables = true;
                        break;
                    case 1:
                        PlayerSettings.advancedVariables = false;
                        break;
                }

                break;
        }
    }







}
