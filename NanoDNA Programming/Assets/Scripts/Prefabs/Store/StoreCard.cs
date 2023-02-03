using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;

public class StoreCard : MonoBehaviour
{

    [SerializeField] RectTransform card;


    [SerializeField] GameObject Movement;
    [SerializeField] GameObject Variable;

    public Flex cardFlex;
    public string storeType;
    public string cardType;
    public int number;
    public GameObject Program; 


    public void Awake()
    {
        setUI();
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

        AddChild();
       


        Flex Name = new Flex(cardFlex.getChild(1), 1);

        cardFlex.addChild(Name);

        cardFlex.setSpacingFlex(0.3f, 1);

    }

    void AddChild ()
    {
        if (card.GetSiblingIndex() == 0)
        {
            //Instantiate Object and make it the child 
            Program = Instantiate(Movement, card.transform);

            
        } else
        {
            //Instantiate Object and make it the child 
            Program = Instantiate(Variable, card.transform);

           
        }

        Program.transform.SetSiblingIndex(0);
        cardFlex.addChild(Program.GetComponent<ProgramCard>().program);


    }




}
