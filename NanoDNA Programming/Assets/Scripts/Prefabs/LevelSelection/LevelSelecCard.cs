using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.UI;

public class LevelSelecCard : MonoBehaviour
{

    public Flex flex;
    public Button.ButtonClickedEvent onClick;


    [Header("UI")]
    [SerializeField] RectTransform card;


    private void Awake()
    {
        setUI();

        onClick = GetComponent<Button>().onClick;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void setUI()
    {
        flex = new Flex(card, 1);

        Flex ImgHolder = new Flex(flex.getChild(0), 4, flex);
        Flex LevelImage = new Flex(ImgHolder.getChild(0), 1, ImgHolder);

        Flex TextHolder = new Flex(flex.getChild(1), 3, flex);

        Flex LevelTitle = new Flex(TextHolder.getChild(0), 1, TextHolder);
        Flex LevelDesc = new Flex(TextHolder.getChild(1), 2, TextHolder);

        ImgHolder.setAllPadSame(0.05f, 1);

        TextHolder.setSpacingFlex(0.5f, 1);

        LevelImage.setSquare();

        TextHolder.setSelfHorizontalPadding(0.05f, 1, 0.05f, 1);

        TextHolder.setSelfVerticalPadding(0.07f, 1, 0.07f, 1);

        TextHolder.setHorizontalPadding(0.02f, 1, 0.02f, 1);

        UIHelper.setImage(flex.UI, PlayerSettings.colourScheme.getSecondary());

        UIHelper.setImage(TextHolder.UI, PlayerSettings.colourScheme.getAccent());

        //Maybe one day make a dictionary in the parent flex that searches for the gameObjects name, that way we can easily access the right game object every time, 
    }

    public void setIconImage(Sprite sprite)
    {
        flex.getChild(0).GetChild(0).GetComponent<Image>().sprite = sprite;
    }

    public void setText(string title, string desc)
    {
        UIHelper.setText(flex.getChild(1).GetChild(0), title, PlayerSettings.colourScheme.getBlackTextColor());

        UIHelper.setText(flex.getChild(1).GetChild(1), desc, PlayerSettings.colourScheme.getBlackTextColor());
    }
}
