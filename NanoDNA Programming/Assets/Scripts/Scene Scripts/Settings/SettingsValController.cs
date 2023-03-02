using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;
using FlexUI;
using UnityEngine.UI;
using DNASaveSystem;

public class SettingsValController : MonoBehaviour
{
    Transform parent;

    Flex Parent;

    SettingEditType editType;
    SettingValueType editValue;

    Language lang;

    PlayerSettings playSettings;

    UIWord English = new UIWord("English", "Anglais");
    UIWord French = new UIWord("French", "Français");

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

    public void setPanel(SettingEditType editType, SettingValueType editValue, Transform parent, PlayerSettings playSettings)
    {
        this.parent = parent;
        this.editType = editType;
        this.editValue = editValue;

        //Load the playerSettings from the right folder
        this.playSettings = playSettings;

        //Load Language
        lang = playSettings.language;


        setUI();
        setControls();
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

                Flex Row1;
                Flex Row2;
                Flex Row3;


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

                    Row1.setSpacingFlex(0.2f, 1);
                    Row2.setSpacingFlex(0.2f, 1);
                    Row3.setSpacingFlex(0.2f, 1);
                }



                Panel.setSpacingFlex(0.3f, 1);

                Panel.setHorizontalPadding(0.05f, 1, 0.05f, 1);

                Panel.setVerticalPadding(0.5f, 1, 0.5f, 1);

                ExitBTN.setSquare();

                Grid.setAllPadSame(0.05f, 1);

                Grid.setSpacingFlex(0.2f, 1);

                //Set gridview
                setGridView(Grid);

                Parent.setSize(new Vector2(1000, 700));

                break;
        }
    }

    public void setControls ()
    {
       
        //Exit Button
        Parent.getChild(0).GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
        {
            parent.gameObject.SetActive(false);

            Destroy(this.gameObject);
        });

        
    }

    public void setGridView (Flex GridView)
    {
       

        switch (editValue)
        {
            case SettingValueType.Language:

                instantiateDisplayCard("Images/SettingsControllerAssets/English", GridView, English, 0, 0);
                instantiateDisplayCard("Images/SettingsControllerAssets/French", GridView, French, 1, 0);

                break;
            case SettingValueType.ColourScheme:
                instantiateDisplayCard("Images/SettingsControllerAssets/Col1", GridView, Col1, 0, 0);
                instantiateDisplayCard("Images/SettingsControllerAssets/Col2", GridView, Col2, 1, 0);
                instantiateDisplayCard("Images/SettingsControllerAssets/Col3", GridView, Col3, 2, 0);
                instantiateDisplayCard("Images/SettingsControllerAssets/Col4", GridView, Col4, 3, 0);

                instantiateDisplayCard("Images/SettingsControllerAssets/Col5", GridView, Col5, 4, 1);
                instantiateDisplayCard("Images/SettingsControllerAssets/Col6", GridView, Col6, 5, 1);
                instantiateDisplayCard("Images/SettingsControllerAssets/Col7", GridView, Col7, 6, 1);
                instantiateDisplayCard("Images/SettingsControllerAssets/Col8", GridView, Col8, 7, 1);
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

        valDisp.transform.GetChild(1).GetComponent<Text>().text = word.getWord(lang);

        valDisp.transform.GetChild(0).GetComponent<Image>().sprite = Sprite.Create(image, new Rect(new Vector2(0, 0), new Vector2(image.width, image.height)), new Vector2(0, 0));


        valDisp.GetComponent<Button>().onClick.AddListener(delegate
        {
            //Edit Values of the settings class



            /*
             
            //Set Var Data
            info.action.varData = varData;

            //Set data
            setData(index);

            //Create and set the Action
            info.programCard.action = func.createAction(info);

            //Set Info
            func.setInfo(info);

            //Compile Program
            allScripts.programSection.compileProgram();

           

            
            */

            //Edit the settings
            setData(index);

            Camera.main.GetComponent<SettingsScene>().updateSettings(playSettings);

            this.parent.gameObject.SetActive(false);

            Destroy(this.gameObject);

            

        });

        //Create Flex, set Square and Add as Child
        Flex ValDisp = valDisp.GetComponent<ValueDisp>().flex;

        ValDisp.setSquare();

        parent.getChild(rowIndex).GetComponent<FlexInfo>().flex.addChild(ValDisp);

    }

    public void setData (int index)
    {
        switch (editValue)
        {
            case SettingValueType.Language:
                Debug.Log("set Language ");
                switch (index)
                {
                    case 0:
                        playSettings.setLanguage(Language.English);
                        break;
                    case 1:
                        playSettings.setLanguage(Language.French);
                        break;
                }

                break;
            case SettingValueType.ColourScheme:

                switch (index)
                {
                    case 0:
                        playSettings.setColourScheme(SettingColourScheme.Col1);
                        break;
                    case 1:
                        playSettings.setColourScheme(SettingColourScheme.Col2);
                        break;
                    case 2:
                        playSettings.setColourScheme(SettingColourScheme.Col3);
                        break;
                    case 3:
                        playSettings.setColourScheme(SettingColourScheme.Col4);
                        break;
                    case 4:
                        playSettings.setColourScheme(SettingColourScheme.Col5);
                        break;
                    case 5:
                        playSettings.setColourScheme(SettingColourScheme.Col6);
                        break;
                    case 6:
                        playSettings.setColourScheme(SettingColourScheme.Col7);
                        break;
                    case 7:
                        playSettings.setColourScheme(SettingColourScheme.Col8);
                        break;
                }
                break;
        }
    }

   




   
}
