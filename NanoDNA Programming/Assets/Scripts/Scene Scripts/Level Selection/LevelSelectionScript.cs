using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using DNASaveSystem;
using UnityEngine.UI;
using DNAScenes;
using UnityEngine.SceneManagement;
using DNAStruct;

public class LevelSelectionScript : MonoBehaviour
{

    [Header("Levels")]
    [SerializeField] List<TextAsset> levelFiles;
    [SerializeField] LevelIconLedger iconLedger;
    [SerializeField] GameObject levelCardPrefab;

    [Header("UI")]
    [SerializeField] RectTransform background;
    // Start is called before the first frame update
    void Start()
    {
        PlayerSettings.LoadSettings(SaveManager.loadPlaySettings());
        setUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setUI()
    {
        Flex Background = new Flex(background, 1);

        for (int i = 0; i < levelFiles.Count; i++)
        {
            GameObject card = Instantiate(levelCardPrefab, Background.UI);

            //Get the script reference
            LevelSelecCard cardScript = card.GetComponent<LevelSelecCard>();

            //Load the level
            LevelInfo level = SaveManager.LoadLevelFromFile(levelFiles[i]);

            cardScript.setIconImage(iconLedger.icons.Find(c => c.id == level.levelIcon.id).sprite);

            cardScript.onClick.AddListener(delegate
            {
                CurrentLevelLoader.path = level.levelPath;
                CurrentLevelLoader.name = level.levelName.english;

                //Change the scene
                Debug.Log("Saved");
                SceneManager.LoadScene(SceneConversion.GetScene(Scenes.PlayLevel), LoadSceneMode.Single);
            });

            cardScript.setText(level.levelName.getWord(PlayerSettings.language), level.levelDescription.getWord(PlayerSettings.language));

            Background.addChild(cardScript.flex);

        }

        Background.setVerticalPadding(0.2f, 1, 0.2f, 1);
        Background.setHorizontalPadding(0.3f, 1, 0.3f, 1);

        Background.setSpacingFlex(0.3f, 1);

        Background.setSize(Flex.ScreenSize());

        LayoutRebuilder.ForceRebuildLayoutImmediate(Background.UI);

        UIHelper.setImage(Background.UI, PlayerSettings.colourScheme.getMain(true));
    }


}
