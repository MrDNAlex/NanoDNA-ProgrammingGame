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
    PlayerSettings playSettings;
    
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
       
        setUI();
        addSettings();
        setLang(playSettings.language);
        setFunctionality();
        
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
    }

    public void addSettings ()
    {
        destroyChildren(content.gameObject);

        content.GetComponent<FlexInfo>().flex.deleteAllChildren();

        for (int i = 0; i < 3; i++)
        {
            

            SettingFunctionality(i);

            //800, 
            

        }

        settings.GetComponent<FlexInfo>().flex.setSize(new Vector2(Screen.width, Screen.height));
    }

    public void SettingFunctionality (int index)
    {
        GameObject gameObj = null;
        switch (index)
        {
            case 0:

                gameObj = Instantiate(Resources.Load("Prefabs/EditPanels/SettingsPanel/SettingCardButton") as GameObject, content);

                gameObj.GetComponent<SettingCard>().setInfoButton(Lang.getWord(playSettings.language) + ":", getLang(playSettings.language));

                gameObj.GetComponent<SettingCard>().onClick.AddListener(delegate
                {
                    infoEditPanel.SetActive(true);

                    destroyChildren(infoEditPanel);

                    GameObject panel = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/SettingsPanel/MultiChoiceSettings") as GameObject, infoEditPanel.transform);

                    panel.GetComponent<SettingsValController>().setPanel(SettingEditType.MultiChoice, SettingValueType.Language, infoEditPanel.transform, playSettings);

                });

                break;
            case 1:

                gameObj = Instantiate(Resources.Load("Prefabs/EditPanels/SettingsPanel/SettingCardButton") as GameObject, content);

                gameObj.GetComponent<SettingCard>().setInfoButton(Colour.getWord(playSettings.language) + ":", getColour(playSettings.colourScheme));

                gameObj.GetComponent<SettingCard>().onClick.AddListener(delegate
                {
                    infoEditPanel.SetActive(true);

                    destroyChildren(infoEditPanel);

                    GameObject panel = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/SettingsPanel/MultiChoiceSettings") as GameObject, infoEditPanel.transform);

                    panel.GetComponent<SettingsValController>().setPanel(SettingEditType.MultiChoice, SettingValueType.ColourScheme, infoEditPanel.transform, playSettings);

                });
                break;
            case 2:
                gameObj = Instantiate(Resources.Load("Prefabs/EditPanels/SettingsPanel/SettingCardSlider") as GameObject, content);

                gameObj.GetComponent<SettingCard>().setInfoSlider(Volume.getWord(playSettings.language) + ":", playSettings.volume);


                gameObj.GetComponent<SettingCard>().onChange.AddListener(delegate
                {
                    gameObj.GetComponent<SettingCard>().flex.getChild(1).GetChild(1).GetComponent<Text>().text = Mathf.FloorToInt(gameObj.GetComponent<SettingCard>().flex.getChild(1).GetChild(0).GetComponent<Slider>().value * 100) + "%";
                });
              
                break;

            default:

                gameObj = Instantiate(Resources.Load("Prefabs/EditPanels/SettingsPanel/SettingCardButton") as GameObject, content);

                gameObj.GetComponent<SettingCard>().setInfoButton(Lang.getWord(playSettings.language) + ":", getLang(playSettings.language));

                gameObj.GetComponent<SettingCard>().onClick.AddListener(delegate
                {
                    infoEditPanel.SetActive(true);

                    destroyChildren(infoEditPanel);

                    GameObject panel = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/SettingsPanel/MultiChoiceSettings") as GameObject, infoEditPanel.transform);

                    panel.GetComponent<SettingsValController>().setPanel(SettingEditType.MultiChoice, SettingValueType.Language, infoEditPanel.transform, playSettings);

                });

                break;

        }

        gameObj.GetComponent<SettingCard>().flex.setCustomSize(new Vector2(0, 80));

        content.GetComponent<FlexInfo>().flex.addChild(gameObj.GetComponent<SettingCard>().flex);
    }

    public void setFunctionality ()
    {
        saveBTN.onClick.AddListener(delegate
        {
            SaveManager.savePlaySettings(playSettings);

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

  

    public void setLang (Language lang)
    {
        settings.GetChild(0).GetComponent<Text>().text = Settings.getWord(lang);
    }

    private string getColour (SettingColourScheme colour)
    {
        string word = "";
        switch (colour)
        {
            case SettingColourScheme.Col1:
                word = Col1.getWord(playSettings.language);
                break;
            case SettingColourScheme.Col2:
                word = Col2.getWord(playSettings.language);
                break;
            case SettingColourScheme.Col3:
                word = Col3.getWord(playSettings.language);
                break;
            case SettingColourScheme.Col4:
                word = Col4.getWord(playSettings.language);
                break;
            case SettingColourScheme.Col5:
                word = Col5.getWord(playSettings.language);
                break;
            case SettingColourScheme.Col6:
                word = Col6.getWord(playSettings.language);
                break;
            case SettingColourScheme.Col7:
                word = Col7.getWord(playSettings.language);
                break;
            case SettingColourScheme.Col8:
                word = Col8.getWord(playSettings.language);
                break;
        }

        return word;

    }

    private string getLang (Language lang)
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

    public void updateSettings(PlayerSettings settings)
    {
        this.playSettings = settings;

        addSettings();
        setLang(playSettings.language);

    }

}