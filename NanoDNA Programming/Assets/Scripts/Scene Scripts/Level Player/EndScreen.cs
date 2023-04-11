using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DNAStruct;
using DNAMathAnimation;

public class EndScreen : MonoBehaviour
{

    Flex endScreen;

    public int maxLines;
    public int linesUsed;
    public int itemsColl;
    public int totalItems;

    UIWord ItemCollected = new UIWord("Items Collected", "Item Collecté");
    UIWord LinesUsed = new UIWord("Lines Used", "Ligne Utilisé");
    UIWord Result = new UIWord("Results", "Résultats");
    UIWord Finish = new UIWord("Finish", "Finir");

    Vector3 OriginalPos;

    Language lang = PlayerSettings.language;

   

    //Scripts allScripts;

    // Start is called before the first frame update
    void Start()
    {
        //allScripts = Camera.main.GetComponent<LevelScript>().allScripts;

        maxLines = Scripts.levelManager.maxLines;
        totalItems = Scripts.levelManager.maxItems;
        linesUsed = Scripts.levelManager.usedLines;
        itemsColl = Scripts.levelManager.itemsCollect;

        setUI();

        setText();

        OriginalPos = endScreen.UI.localPosition;

        Vector3 startPos = OriginalPos + new Vector3(0, -Screen.height, 0);

        endScreen.UI.localPosition = startPos;

        StartCoroutine(DNAMathAnim.animateReboundRelocationLocal(endScreen.UI, OriginalPos, DNAMathAnim.getFrameNumber(1.5f), 1, true));

        
        transform.GetChild(2).GetComponent<Button>().onClick.AddListener(FinishLevel);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setUI()
    {

        endScreen = new Flex(transform.GetComponent<RectTransform>(), 1);


        Flex Title = new Flex(endScreen.getChild(0), 2, endScreen);

        Flex Holder = new Flex(endScreen.getChild(1), 6, endScreen);

        Flex items = new Flex(Holder.getChild(0), 1, Holder);
        Flex lines = new Flex(Holder.getChild(1), 1, Holder);

        Flex Finish = new Flex(endScreen.getChild(2), 2, endScreen);

        endScreen.setSpacingFlex(1f, 1);

        Title.setSelfHorizontalPadding(1, 1, 1, 1);

        Holder.setAllPadSame(0.1f, 1);

        Holder.setSpacingFlex(0.5f, 1);

        endScreen.setHorizontalPadding(0.1f, 1, 0.1f, 1);

        Finish.setSelfHorizontalPadding(1, 1, 1, 1);

        endScreen.setVerticalPadding(1f, 1, 1f, 1);

        endScreen.setSize(new Vector2(Screen.width * 0.7f, Screen.height * 0.8f));


        UIHelper.setImage(endScreen.UI, PlayerSettings.colourScheme.getMain());
        UIHelper.setImage(Finish.UI, PlayerSettings.colourScheme.getAccent());
        UIHelper.setImage(Holder.UI, PlayerSettings.colourScheme.getSecondary());


    }

    public void FinishLevel()
    {
        SceneManager.LoadScene(0);

    }

    public void setText()
    {

        string itemCollected = ItemCollected.getWord(lang) + ": " + itemsColl + "/" + totalItems;

        string lineUsed = LinesUsed.getWord(lang) + ": " + +linesUsed + " / " + maxLines;

        UIHelper.setText(transform.GetChild(0), Result.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor());

        UIHelper.setText(transform.GetChild(1).GetChild(0), itemCollected, PlayerSettings.colourScheme.getBlackTextColor());

        UIHelper.setText(transform.GetChild(1).GetChild(1), lineUsed, PlayerSettings.colourScheme.getBlackTextColor());

        UIHelper.setText(transform.GetChild(2).GetChild(0), Finish.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor());


    }




}
