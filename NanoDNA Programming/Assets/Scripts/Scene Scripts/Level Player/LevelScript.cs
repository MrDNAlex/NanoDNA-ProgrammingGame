using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{

    [SerializeField] RectTransform background;

    [SerializeField] RectTransform reg1;
    [SerializeField] RectTransform header;
    [SerializeField] RectTransform list;


    [SerializeField] RectTransform reg2;
    [SerializeField] RectTransform mapView;

    [SerializeField] RectTransform reg3;
    [SerializeField] RectTransform store;
    [SerializeField] RectTransform constraints;








    // Start is called before the first frame update
    void Start()
    {
        setUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void setUI ()
    {
        //Create a new function in the FlexUI library that gets the reference to the rect transform of the Flex Parent and gets the child index from there. 
        //That way I can call Background.getChild(1);
        Flex Background = new Flex(background, 1);

        Flex Reg1 = new Flex(reg1, 1);
        Flex Header = new Flex(header, 1);
        Flex List = new Flex(list, 10);

        Flex Reg2 = new Flex(reg2, 2f);
        Flex MapView = new Flex(mapView, 2f);

        Flex Reg3 = new Flex(reg3, 1f);
        Flex Store = new Flex(store, 4f);
        Flex Constraints = new Flex(constraints, 1f);


        Reg1.addChild(Header);
        Reg1.addChild(List);

        Reg2.addChild(MapView);
        Reg2.addChild(Reg3);

        Reg3.addChild(Store);
        Reg3.addChild(Constraints);

        Background.addChild(Reg1);
        Background.addChild(Reg2);
        //Background.addChild(Reg3);


        Background.setSize(new Vector2(Screen.width, Screen.height));



    }



}
