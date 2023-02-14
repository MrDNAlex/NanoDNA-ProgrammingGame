using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{

    Flex endScreen;

    public int maxLines;
    public int linesUsed;
    public int itemsColl;
    public int totalItems;

    // Start is called before the first frame update
    void Start()
    {


        maxLines = Camera.main.GetComponent<LevelScript>().maxLines;
        totalItems = Camera.main.GetComponent<LevelScript>().maxItems;
       linesUsed = Camera.main.GetComponent<LevelScript>().usedLines;
        itemsColl = Camera.main.GetComponent<LevelScript>().itemsCollect;

        setUI();

        setText();

        transform.GetChild(3).GetComponent<Button>().onClick.AddListener(FinishLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUI ()
    {

        endScreen = new Flex(transform.GetComponent<RectTransform>(), 1);


        Flex Title = new Flex(endScreen.getChild(0), 2);

        Flex items = new Flex(endScreen.getChild(1), 3);
        Flex lines = new Flex(endScreen.getChild(2), 3);

        Flex Finish = new Flex(endScreen.getChild(3), 2);

        endScreen.addChild(Title);
        endScreen.addChild(items);
        endScreen.addChild(lines);
        endScreen.addChild(Finish);


        endScreen.setSpacingFlex(2, 1);

        Title.setSelfHorizontalPadding(1, 1, 1, 1);

        endScreen.setAllPadSame(0.1f, 1);

        Finish.setSelfHorizontalPadding(1, 1, 1, 1);

        endScreen.setSize(new Vector2(Screen.width * 0.7f, Screen.height * 0.8f));

    }

    public void FinishLevel ()
    {
        SceneManager.LoadScene(0);

    }

    public void setText ()
    {

        transform.GetChild(1).GetComponent<Text>().text = "Items Collected: " + itemsColl + " / " + totalItems;

        transform.GetChild(2).GetComponent<Text>().text = "Lines Used: " + linesUsed + " / " + maxLines;


    }
    



}
