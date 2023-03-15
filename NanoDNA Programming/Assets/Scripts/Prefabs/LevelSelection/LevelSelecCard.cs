using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.UI;

public class LevelSelecCard  : MonoBehaviour 
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

    void setUI ()
    {
        flex = new Flex(card, 1);

        Flex ImgHolder = new Flex(flex.getChild(0), 4, flex);
        Flex LevelImage = new Flex(ImgHolder.getChild(0), 1, ImgHolder);

        Flex TextHolder = new Flex(flex.getChild(1), 3, flex);

        Flex LevelTitle = new Flex(TextHolder.getChild(0), 1, TextHolder);
        Flex LevelDesc = new Flex(TextHolder.getChild(1), 3, TextHolder);

        ImgHolder.setAllPadSame(0.1f, 1);

        LevelImage.setSquare();
        
        //Maybe one day make a dictionary in the parent flex that searches for the gameObjects name, that way we can easily access the right game object every time, 
    }

    public void setIconImage (Sprite sprite)
    {

        flex.getChild(0).GetChild(0).GetComponent<Image>().sprite = sprite;

    }


}
