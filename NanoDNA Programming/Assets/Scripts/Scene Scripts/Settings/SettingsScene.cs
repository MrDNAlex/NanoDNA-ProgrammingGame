using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;
using DNAStruct;
using DNASaveSystem;

public class SettingsScene : MonoBehaviour
{
    [SerializeField] RectTransform settings;

    [SerializeField] GameObject infoEditPanel;

    //Settings
    [Header("Settings")]
    [SerializeField] Button languageBTN;
    [SerializeField] Button colourBTN;

    [SerializeField] Slider volumeSlider;
    [SerializeField] Text volumePercent;

    [SerializeField] Button saveBTN;


    //Create a new Class for storing settings details
    PlayerSettings playSettings;
    Language lang;
    SettingColourScheme colourScheme;


    //Maybe add all these words to a class


    //Words
    UIWord English = new UIWord("English", "Anglais");
    UIWord French = new UIWord("French", "Français");
    UIWord Lang = new UIWord("Language", "Langue");
    UIWord Colour = new UIWord("Colour", "Couleur");
    UIWord Volume = new UIWord("Volume", "Volume");
    UIWord Settings = new UIWord("Settings", "Paramètre");

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
        if (SaveManager.loadPlaySettings() == null)
        {
            Debug.Log("Error");
            playSettings = new PlayerSettings();
        } else
        {
            playSettings = SaveManager.loadPlaySettings();
        }
       

        lang = playSettings.language;

        setUI();
        setFunctionality();
        setLang(lang);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setUI ()
    {

        //Size of each setting is 150 tall?

        Flex Settings = new Flex(settings, 1);

        Flex Title = new Flex(Settings.getChild(0), 1, Settings);
        Flex SettingsPanel = new Flex(Settings.getChild(1), 5, Settings);

        Flex ScrollView = new Flex(SettingsPanel.getChild(0), 1, SettingsPanel);

        Flex Viewport = new Flex(ScrollView.getChild(0), 1, ScrollView);

        Flex Content = new Flex(Viewport.getChild(0), 1, Viewport);

        //ChildHeight?
        Flex ActualSettings = new Flex(Content.getChild(0), 5, Content);

        Flex SettingName = new Flex(ActualSettings.getChild(0), 1, ActualSettings);

        Flex LangName = new Flex(SettingName.getChild(0), 1, SettingName);
        Flex ColourName = new Flex(SettingName.getChild(1), 1, SettingName);
        Flex VolumeName = new Flex(SettingName.getChild(2), 1, SettingName);


        Flex SettingVal = new Flex(ActualSettings.getChild(1), 2, ActualSettings);

        Flex LangBTN = new Flex(SettingVal.getChild(0), 1, SettingVal);
        Flex ColourBTN = new Flex(SettingVal.getChild(1), 1, SettingVal);
        Flex VolumeBTN = new Flex(SettingVal.getChild(2), 1, SettingVal);

        Flex VolumeSlide = new Flex(VolumeBTN.getChild(0), 4, VolumeBTN);
        Flex VolumePercent = new Flex(VolumeBTN.getChild(1), 1, VolumeBTN);

        Flex SaveBTN = new Flex(SettingsPanel.getChild(1), 1, SettingsPanel);


        Settings.setHorizontalPadding(0.1f, 1, 0.1f, 1);
        Settings.setVerticalPadding(0.2f, 1, 0.2f, 1);
        Settings.setSpacingFlex(0.1f, 1);

        ActualSettings.setHorizontalPadding(0.2f, 1, 0.2f, 1);
        ActualSettings.setVerticalPadding(0.1f, 1, 0.1f, 1);

        SettingName.setSpacingFlex(1, 1);
        SettingVal.setSpacingFlex(1, 1);

        VolumeBTN.setSpacingFlex(0.2f, 1);

        Settings.setSize(new Vector2(Screen.width, Screen.height));


    }

    public void setFunctionality ()
    {
        //Need to add a load of past settings
        volumeSlider.value = (float)playSettings.volume / 100;
        volumePercent.text = Mathf.FloorToInt(volumeSlider.value * 100) + "%";


        volumeSlider.onValueChanged.AddListener(delegate
        {
            volumePercent.text = Mathf.FloorToInt(volumeSlider.value * 100) + "%";
            playSettings.volume = Mathf.FloorToInt(volumeSlider.value * 100);
        });

        languageBTN.onClick.AddListener(delegate
        {
            //Spawn the info panel

            //Multi Choice

            infoEditPanel.SetActive(true);

            destroyChildren(infoEditPanel);

            GameObject panel = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/SettingsPanel/MultiChoiceSettings") as GameObject, infoEditPanel.transform);

            panel.GetComponent<SettingsValController>().setPanel(SettingEditType.MultiChoice, SettingValueType.Language, infoEditPanel.transform, playSettings);

        });

        colourBTN.onClick.AddListener(delegate
        {
            infoEditPanel.SetActive(true);

            destroyChildren(infoEditPanel);

            GameObject panel = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/SettingsPanel/MultiChoiceSettings") as GameObject, infoEditPanel.transform);

            panel.GetComponent<SettingsValController>().setPanel(SettingEditType.MultiChoice, SettingValueType.ColourScheme, infoEditPanel.transform, playSettings);

        });

        saveBTN.onClick.AddListener(delegate
        {
            //Save all settings

            playSettings.setVolume(playSettings.volume = Mathf.FloorToInt(volumeSlider.value * 100));
            playSettings.setLanguage(lang);
            playSettings.setColourScheme(colourScheme);

            SaveManager.savePlaySettings(playSettings);

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

  

    public void setLang (Language lang)
    {
        settings.GetChild(0).GetComponent<Text>().text = Settings.getWord(lang);

        //Names
        settings.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = Lang.getWord(lang) + ":";
        settings.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = Colour.getWord(lang) + ":";
        settings.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().text = Volume.getWord(lang) + ":";

        if (playSettings.language == Language.English)
        {
           //settings.GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = English.getWord(lang);
            languageBTN.transform.GetChild(0).GetComponent<Text>().text = English.getWord(lang);

        } else
        {
           // settings.GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = French.getWord(lang);
            languageBTN.transform.GetChild(0).GetComponent<Text>().text = French.getWord(lang);
        }

        //Colour Button
       // settings.GetChild(1).GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = getColour(playSettings.colourScheme);
        colourBTN.transform.GetChild(0).GetComponent<Text>().text = getColour(playSettings.colourScheme);

    }

    private string getColour (SettingColourScheme colour)
    {
        string word = "";
        switch (colour)
        {
            case SettingColourScheme.Col1:
                word = Col1.getWord(lang);
                break;
            case SettingColourScheme.Col2:
                word = Col2.getWord(lang);
                break;
            case SettingColourScheme.Col3:
                word = Col3.getWord(lang);
                break;
            case SettingColourScheme.Col4:
                word = Col4.getWord(lang);
                break;
            case SettingColourScheme.Col5:
                word = Col5.getWord(lang);
                break;
            case SettingColourScheme.Col6:
                word = Col6.getWord(lang);
                break;
            case SettingColourScheme.Col7:
                word = Col7.getWord(lang);
                break;
            case SettingColourScheme.Col8:
                word = Col8.getWord(lang);
                break;

        }

        return word;

    }

    public void updateSettings(PlayerSettings settings)
    {
        this.playSettings = settings;

        //Update Shit like language and colour
        lang = playSettings.language;
        colourScheme = playSettings.colourScheme;
        volumeSlider.value = (float)playSettings.volume / 100;
        volumePercent.text = Mathf.FloorToInt(volumeSlider.value * 100) + "%";


        setLang(lang);

    }






}


