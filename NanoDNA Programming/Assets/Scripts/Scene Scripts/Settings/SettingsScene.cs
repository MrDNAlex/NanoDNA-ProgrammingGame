using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;
using DNAStruct;
using DNASaveSystem;
using DNAScenes;
using UnityEngine.SceneManagement;

public class SettingsScene : MonoBehaviour
{
    [SerializeField] RectTransform settings;

    [SerializeField] GameObject infoEditPanel;

    //Settings
    [Header("Settings")]
    [SerializeField] RectTransform content;

    [SerializeField] Button saveBTN;

    //Create a new Class for storing settings details


    //Maybe add all these words to a class

    //Words
    UIWord English = new UIWord("English", "Anglais");
    UIWord French = new UIWord("French", "Français");
    UIWord Lang = new UIWord("Language", "Langue");
    UIWord Colour = new UIWord("Colour", "Couleur");
    UIWord Volume = new UIWord("Volume", "Volume");
    UIWord Settings = new UIWord("Settings", "Paramètre");
    UIWord Save = new UIWord("Save", "Sauve");
    UIWord AdvVaribales = new UIWord("Advanced Variables", "Variables Avancée");
    UIWord SimVariable = new UIWord("Simple Variables", "Variables Simple");

    UIWord Col1 = new UIWord("Colour 1", "Couleur 1");
    UIWord Col2 = new UIWord("Colour 2", "Couleur 2");
    UIWord Col3 = new UIWord("Colour 3", "Couleur 3");
    UIWord Col4 = new UIWord("Colour 4", "Couleur 4");
    UIWord Col5 = new UIWord("Colour 5", "Couleur 5");
    UIWord Col6 = new UIWord("Colour 6", "Couleur 6");
    UIWord Col7 = new UIWord("Colour 7", "Couleur 7");
    UIWord Col8 = new UIWord("Colour 8", "Couleur 8");


    //Get a list of the old variables, then check if they changed

    // Start is called before the first frame update
    void Start()
    {

        //Create a default
        PlayerSettings.LoadSettings(SaveManager.loadPlaySettings());

        setUI();
        addSettings();
        setLang(PlayerSettings.language);
        setFunctionality();

    }

    // Update is called once per frame
    void Update()
    {

    }


    void setUI()
    {
        //Size of each setting is 150 tall?

        Flex Settings = new Flex(settings, 1);

        Settings.deleteAllChildren();

        Flex Title = new Flex(Settings.getChild(0), 1, Settings);
        Flex SettingsPanel = new Flex(Settings.getChild(1), 5, Settings);

        Flex ScrollView = new Flex(SettingsPanel.getChild(0), 5, SettingsPanel);
        Flex Viewport = new Flex(ScrollView.getChild(0), 1, ScrollView);
        Flex Content = new Flex(Viewport.getChild(0), 1, Viewport);

        Flex SaveBTN = new Flex(SettingsPanel.getChild(1), 1, SettingsPanel);

        Settings.setHorizontalPadding(0.1f, 1, 0.1f, 1);
        Settings.setVerticalPadding(0.2f, 1, 0.2f, 1);
        Settings.setSpacingFlex(0.1f, 1);

        Content.setHorizontalPadding(0.1f, 1, 0.1f, 1);
        Content.setVerticalPadding(0.1f, 1, 0.1f, 1);

        Content.setSpacingFlex(1, 1);

        Content.setChildMultiH(80);

        //SetImage

        UIHelper.setImage(Settings.UI.transform, PlayerSettings.colourScheme.getMain(true));

        UIHelper.setImage(SettingsPanel.UI.transform, PlayerSettings.colourScheme.getSecondary());

        UIHelper.setImage(SaveBTN.UI.transform, PlayerSettings.colourScheme.getAccent());

    }

    public void addSettings()
    {
        destroyChildren(content.gameObject);

        content.GetComponent<FlexInfo>().flex.deleteAllChildren();

        for (int i = 0; i < 4; i++)
        {
            SettingFunctionality(i);

            //800, 
        }

        settings.GetComponent<FlexInfo>().flex.setSize(new Vector2(Screen.width, Screen.height));
    }

    public void SettingFunctionality(int index)
    {
        GameObject gameObj = null;
        switch (index)
        {
            case 0:

                gameObj = Instantiate(Resources.Load("Prefabs/EditPanels/SettingsPanel/SettingCardButton") as GameObject, content);

                gameObj.GetComponent<SettingCard>().setColourScheme();

                gameObj.GetComponent<SettingCard>().setInfoButton(Lang.getWord(PlayerSettings.language) + ":", getLang(PlayerSettings.language));

                gameObj.GetComponent<SettingCard>().onClick.AddListener(delegate
                {
                    infoEditPanel.SetActive(true);

                    destroyChildren(infoEditPanel);

                    GameObject panel = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/SettingsPanel/MultiChoiceSettings") as GameObject, infoEditPanel.transform);

                    panel.GetComponent<SettingsValController>().setPanel(SettingEditType.MultiChoice, SettingValueType.Language, infoEditPanel.transform);

                });

                break;
            case 1:

                gameObj = Instantiate(Resources.Load("Prefabs/EditPanels/SettingsPanel/SettingCardButton") as GameObject, content);

                gameObj.GetComponent<SettingCard>().setColourScheme();

                gameObj.GetComponent<SettingCard>().setInfoButton(Colour.getWord(PlayerSettings.language) + ":", getColour(PlayerSettings.colourScheme.colourScheme));

                gameObj.GetComponent<SettingCard>().onClick.AddListener(delegate
                {
                    infoEditPanel.SetActive(true);

                    destroyChildren(infoEditPanel);

                    GameObject panel = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/SettingsPanel/MultiChoiceSettings") as GameObject, infoEditPanel.transform);

                    panel.GetComponent<SettingsValController>().setPanel(SettingEditType.MultiChoice, SettingValueType.ColourScheme, infoEditPanel.transform);

                });
                break;
            case 2:
                gameObj = Instantiate(Resources.Load("Prefabs/EditPanels/SettingsPanel/SettingCardSlider") as GameObject, content);

                SettingCard setCard = gameObj.GetComponent<SettingCard>();

                setCard.mainColor = PlayerSettings.colourScheme.getMainTextColor();

                setCard.accentColor = PlayerSettings.colourScheme.getAccentTextColor();

                setCard.setInfoSlider(Volume.getWord(PlayerSettings.language) + ":", PlayerSettings.volume);

                setCard.flex.getChild(1).GetChild(1).GetComponent<Text>().color = PlayerSettings.colourScheme.getMainTextColor();

                setCard.onChange.AddListener(delegate
                {
                    setCard.flex.getChild(1).GetChild(1).GetComponent<Text>().text = Mathf.FloorToInt(setCard.flex.getChild(1).GetChild(0).GetComponent<Slider>().value * 100) + "%";
                });

                break;

            case 3:

                //Advanced Variables
                gameObj = Instantiate(Resources.Load("Prefabs/EditPanels/SettingsPanel/SettingCardButton") as GameObject, content);

                //
                //
                //

                gameObj.GetComponent<SettingCard>().setColourScheme();

                gameObj.GetComponent<SettingCard>().setInfoButton(AdvVaribales.getWord(PlayerSettings.language) + ":", getAdvVariable(PlayerSettings.advancedVariables));

                gameObj.GetComponent<SettingCard>().onClick.AddListener(delegate
                {
                    infoEditPanel.SetActive(true);

                    destroyChildren(infoEditPanel);

                    GameObject panel = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/SettingsPanel/MultiChoiceSettings") as GameObject, infoEditPanel.transform);

                    panel.GetComponent<SettingsValController>().setPanel(SettingEditType.MultiChoice, SettingValueType.AdvancedVariables, infoEditPanel.transform);

                });


                break;

            default:

                gameObj = Instantiate(Resources.Load("Prefabs/EditPanels/SettingsPanel/SettingCardButton") as GameObject, content);

                gameObj.GetComponent<SettingCard>().setColourScheme();

                gameObj.GetComponent<SettingCard>().setInfoButton(Lang.getWord(PlayerSettings.language) + ":", getLang(PlayerSettings.language));

                gameObj.GetComponent<SettingCard>().onClick.AddListener(delegate
                {
                    infoEditPanel.SetActive(true);

                    destroyChildren(infoEditPanel);

                    GameObject panel = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/SettingsPanel/MultiChoiceSettings") as GameObject, infoEditPanel.transform);

                    panel.GetComponent<SettingsValController>().setPanel(SettingEditType.MultiChoice, SettingValueType.Language, infoEditPanel.transform);

                });

                break;

        }

        gameObj.GetComponent<SettingCard>().flex.setCustomSize(new Vector2(0, 80));

        content.GetComponent<FlexInfo>().flex.addChild(gameObj.GetComponent<SettingCard>().flex);
    }

    public void setFunctionality()
    {
        saveBTN.onClick.AddListener(delegate
        {
            SaveManager.savePlaySettings(PlayerSettings.CreateSave());

            //Head to Menu Scene
            SceneManager.LoadScene(SceneConversion.GetScene(Scenes.Menu), LoadSceneMode.Single);
        });
    }


    public void destroyChildren(GameObject Obj)
    {
        //Only deletes children under the one referenced
        foreach (Transform child in Obj.transform)
        {
            //Safe to delete
            GameObject.Destroy(child.gameObject);
        }
    }



    public void setLang(Language lang)
    {
        UIHelper.setText(settings.GetChild(0), Settings, PlayerSettings.colourScheme.getMainTextColor());

        UIHelper.setText(saveBTN.transform.GetChild(0), Save, PlayerSettings.colourScheme.getAccentTextColor());
    }

    private string getColour(SettingColourScheme colour)
    {
        string word = "";
        switch (colour)
        {
            case SettingColourScheme.Col1:
                word = Col1.getWord(PlayerSettings.language);
                break;
            case SettingColourScheme.Col2:
                word = Col2.getWord(PlayerSettings.language);
                break;
            case SettingColourScheme.Col3:
                word = Col3.getWord(PlayerSettings.language);
                break;
            case SettingColourScheme.Col4:
                word = Col4.getWord(PlayerSettings.language);
                break;
            case SettingColourScheme.Col5:
                word = Col5.getWord(PlayerSettings.language);
                break;
            case SettingColourScheme.Col6:
                word = Col6.getWord(PlayerSettings.language);
                break;
            case SettingColourScheme.Col7:
                word = Col7.getWord(PlayerSettings.language);
                break;
            case SettingColourScheme.Col8:
                word = Col8.getWord(PlayerSettings.language);
                break;
        }

        return word;

    }

    private string getAdvVariable(bool advVariables)
    {
        if (advVariables)
        {
            return AdvVaribales.getWord(PlayerSettings.language);
        }
        else
        {
            return SimVariable.getWord(PlayerSettings.language);
        }
    }

    private string getLang(Language lang)
    {
        string word = "";
        switch (lang)
        {
            case Language.English:
                word = English.getWord(lang);
                break;
            case Language.French:
                word = French.getWord(lang);
                break;
        }
        return word;
    }

    public void updateSettings()
    {
        setUI();
        addSettings();

        setLang(PlayerSettings.language);
    }

    


}