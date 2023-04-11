using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using DNAStruct;
using DNASaveSystem;
using UnityEngine.UI;
using DNAScenes;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    [SerializeField] RectTransform menu;


    Language lang;

    PlayerSettings playSettings;

    UIWord TitleWord = new UIWord("Nano Program", "Program Nano");

    UIWord StartWord = new UIWord("Start", "Commence");
    UIWord SettingsWord = new UIWord("Settings", "Paramètre");
    UIWord ExitWord = new UIWord("Exit", "Fermer");
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        PlayerSettings.LoadSettings(SaveManager.loadPlaySettings());

        lang = PlayerSettings.language;

        setUI();
        setFunctionality();
        setLang();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUI ()
    {
        Flex Menu = new Flex(menu, 1);

        Flex Buttons = new Flex(Menu.getChild(0), 1, Menu);
        Flex Empty = new Flex(Menu.getChild(1), 2.5f, Menu);

        Flex Title = new Flex(Buttons.getChild(0), 1.5f, Buttons);
        Flex Start = new Flex(Buttons.getChild(1), 1, Buttons);
        Flex Settings = new Flex(Buttons.getChild(2), 1, Buttons);
        Flex Exit = new Flex(Buttons.getChild(3), 1, Buttons);

        //Edit
        Buttons.setHorizontalPadding(0.1f, 1, 0.1f, 1);
        Buttons.setVerticalPadding(3, 1, 3, 1);
        Buttons.setSpacingFlex(1.5f, 1);

        Start.setSelfHorizontalPadding(0.1f, 1, 0.1f, 1);
        Settings.setSelfHorizontalPadding(0.1f, 1, 0.1f, 1);
        Exit.setSelfHorizontalPadding(0.1f, 1, 0.1f, 1);

        //Set Size
        Menu.setSize(new Vector2(Screen.width, Screen.height));

        UIHelper.setImage(Start.UI, PlayerSettings.colourScheme.getAccent());
        UIHelper.setImage(Settings.UI, PlayerSettings.colourScheme.getAccent());
        UIHelper.setImage(Exit.UI, PlayerSettings.colourScheme.getAccent());
    }

    public void setFunctionality ()
    {
        //Start goes to next page
        menu.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(StartGame);

        //Settings goes to setting page
        menu.GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(GoToSettings);

        //Exit Closes the application
        menu.GetChild(0).GetChild(3).GetComponent<Button>().onClick.AddListener(ExitProgram);
    }

    public void ExitProgram ()
    {
        //This does work, don't change
        Application.Quit();
        Debug.Log("Exit");
    }

    public void StartGame ()
    {
        //Head to Next scene

        Debug.Log("Start Scene");

        SceneManager.LoadScene(SceneConversion.GetScene(Scenes.SelectLevel), LoadSceneMode.Single);

    }

    public void GoToSettings()
    {
        //Head to Next scene

        Debug.Log("Settings Scene");

        SceneManager.LoadScene(SceneConversion.GetScene(Scenes.Settings), LoadSceneMode.Single);
    }

    public void setLang ()
    {
        //Title
        UIHelper.setText(menu.GetChild(0).GetChild(0), TitleWord, PlayerSettings.colourScheme.getBlackTextColor());

        //Start
        UIHelper.setText(menu.GetChild(0).GetChild(1).GetChild(0), StartWord, PlayerSettings.colourScheme.getAccentTextColor());

        //Settings
        UIHelper.setText(menu.GetChild(0).GetChild(2).GetChild(0), SettingsWord, PlayerSettings.colourScheme.getAccentTextColor());

        //Exit
        UIHelper.setText(menu.GetChild(0).GetChild(3).GetChild(0), ExitWord, PlayerSettings.colourScheme.getAccentTextColor());
    }

}

