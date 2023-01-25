using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.UI;
public class ProgramCard : MonoBehaviour
{
    [SerializeField] RectTransform card;

    [SerializeField] RectTransform script;

    [SerializeField] RectTransform var;
    [SerializeField] RectTransform varName;
    [SerializeField] RectTransform sign;
    [SerializeField] RectTransform val;

    [SerializeField] RectTransform cardName;


    public Flex Card;

    public string cardType;

    public Button.ButtonClickedEvent onClick;


    private void Awake()
    {
        getUI();
        onClick = card.GetComponent<Button>().onClick;
       
    }


    // Start is called before the first frame update
    void Start()
    {
        //setUI();


      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUI ()
    {
      

        
    }

    public Flex getUI ()
    {

        Card = new Flex(card, 1);

        Flex Script = new Flex(script, 2);

        Flex Var = new Flex(var, 2);
        Flex VarName = new Flex(varName, 1);
        Flex Sign = new Flex(sign, 1);
        Flex Val = new Flex(val, 1);

        Flex CardName = new Flex(cardName, 1);

        Card.addChild(Script);
        Card.addChild(CardName);

        Script.addChild(Var);
        Script.addChild(VarName);
        Script.addChild(Sign);
        Script.addChild(Val);

        Script.setSpacingFlex(1, 1);

        Script.setAllPadSame(0.2f, 1);

        Card.setSpacingFlex(0.3f, 1);

        //Card.setSize(new Vector2(500, 200));

        return Card;

        //Card.setSize(new Vector2(500, 200));

    }

  


}
