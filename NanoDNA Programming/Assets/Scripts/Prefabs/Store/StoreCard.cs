using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;
using DNAStruct;
using DNASaveSystem;

public class StoreCard : MonoBehaviour
{

    [SerializeField] RectTransform card;
    //[SerializeField] GameObject Movement;
    //[SerializeField] GameObject Variable;

    public Flex cardFlex;
    public string storeType;
    public string cardType;
    public int number;
    public GameObject Program;

    Language lang;


    public void Awake()
    {
        setUI();
        lang = Camera.main.GetComponent<LevelScript>().lang;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUI ()
    {
        //Set up default UI
        //Instantiate store item
        //Return everything

        //Set up UI 
        cardFlex = new Flex(card, 1);

        Flex Name = new Flex(cardFlex.getChild(0), 1);

        //Add children
        cardFlex.addChild(Name);

        cardFlex.setSpacingFlex(0.1f, 1);
    }

    public void setStoreCard (GameObject prog)
    {
        //Instantiate Object
        Program = Instantiate(prog, card.transform);

        //Make it the first Child
        Program.transform.SetSiblingIndex(0);

        //Program.AddComponent<DragController2>();

        //Add the child 
        cardFlex.addChild(Program.GetComponent<ProgramCard>().program);

        //Set name
        UIHelper.setText(cardFlex.getChild(1), cardFlex.getChild(0).GetComponent<ProgramCard>().cardName.getWord(lang), PlayerSettings.colourScheme.getMainTextColor(), PlayerSettings.getMediumText());

    }
}
