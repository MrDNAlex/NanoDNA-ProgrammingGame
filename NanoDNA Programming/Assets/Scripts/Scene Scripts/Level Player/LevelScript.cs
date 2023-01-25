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
    [SerializeField] RectTransform sV;
   // [SerializeField] RectTransform sB;
    [SerializeField] RectTransform vP;
   [SerializeField] RectTransform content;

    [SerializeField] RectTransform reg2;
    [SerializeField] RectTransform mapView;

    [SerializeField] RectTransform reg3;

    [Header("Store")]
    [SerializeField] RectTransform store;

    [SerializeField] RectTransform storeHeader;

    [SerializeField] RectTransform btn1;
    [SerializeField] RectTransform btn2;
    [SerializeField] RectTransform btn3;
    [SerializeField] RectTransform btn4;



    [SerializeField] RectTransform gridView;





    [SerializeField] RectTransform constraints;

    [SerializeField] GameObject prefab;

    [SerializeField] GameObject prefab2;

    [SerializeField] GameObject Movement;

    [SerializeField] GameObject Program;

    public string type;

    public RectTransform contentTrans;

    // Start is called before the first frame update
    void Start()
    {
        setUI();

        btn1.GetComponent<Button>().onClick.AddListener(btn1Ac);
        btn2.GetComponent<Button>().onClick.AddListener(btn2Ac);
        btn3.GetComponent<Button>().onClick.AddListener(btn3Ac);
        btn4.GetComponent<Button>().onClick.AddListener(btn4Ac);

        contentTrans = content;
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(Input.mousePosition);
    }


    void setUI ()
    {
        //Create Flex Components
        //Create a new function in the FlexUI library that gets the reference to the rect transform of the Flex Parent and gets the child index from there. 
        //That way I can call Background.getChild(1);
        Flex Background = new Flex(background, 1);

        Flex Reg1 = new Flex(reg1, 1);
        Flex Header = new Flex(header, 1);
        Flex List = new Flex(list, 10);
        Flex SV = new Flex(sV, 1);
        Flex VP = new Flex(vP, 10);
       // Flex SB = new Flex(sB, 1);
        Flex Content = new Flex(content, 1);

        Flex Reg2 = new Flex(reg2, 2f);
        Flex MapView = new Flex(mapView, 2f);

        Flex Reg3 = new Flex(reg3, 1f);
        Flex Store = new Flex(store, 4f);

        Flex StoreHeader = new Flex(storeHeader, 1);
        Flex BTN1 = new Flex(btn1, 1);
        Flex BTN2 = new Flex(btn2, 1);
        Flex BTN3 = new Flex(btn3, 1);
        Flex BTN4 = new Flex(btn4, 1);

        Flex GridView = new Flex(gridView, 5);

        Flex Constraints = new Flex(constraints, 1f);

        //Add children
        List.addChild(SV);
        SV.addChild(VP);
       // SV.addChild(SB);
        VP.addChild(Content);

        Reg1.addChild(Header);
        Reg1.addChild(List);

        Reg2.addChild(MapView);
        Reg2.addChild(Reg3);

        Reg3.addChild(Store);
        Reg3.addChild(Constraints);

        Background.addChild(Reg1);
        Background.addChild(Reg2);

        Store.addChild(StoreHeader);
        Store.addChild(GridView);

        StoreHeader.addChild(BTN1);
        StoreHeader.addChild(BTN2);
        StoreHeader.addChild(BTN3);
        StoreHeader.addChild(BTN4);



        //Background.addChild(Reg3);

        //Add the programming blocks as children
        addChildren(Content);
        addStore(GridView);

        //Constraints and Abilities 

        Content.setChildMultiH(150);
        //Content.useChildMulti = true;

        //Content.setSelfHorizontalPadding(0, 0, 0.05f, 1);

        Background.setSize(new Vector2(Screen.width, Screen.height));

    }

    void addChildren (Flex parent)
    {
        
        for (int i = 0; i < 20; i ++)
        {
            GameObject idk = Instantiate(prefab, parent.UI);

            parent.addChild(idk.GetComponent<ProgramLine>().Line);

           // Debug.Log(idk.GetComponent<ProgramBlock>().Block);

            idk.GetComponent<RectTransform>().GetChild(0).GetComponent<Text>().text = i.ToString();
            idk.GetComponent<Button>().onClick.AddListener(delegate { setBackground(idk); });
        }

    }

    void addStore (Flex parent)
    {

        for (int i = 0; i < 2; i++)
        {

            if (i == 0)
            {
                //Debug.Log(parent.UI);
                GameObject idk = Instantiate(prefab2, parent.UI);

                idk.GetComponent<ProgramCard>().Card.setSize(parent.UI.GetComponent<GridLayoutGroup>().cellSize);

                if (i % 2 == 0)
                {
                    //setType1();
                    idk.GetComponent<ProgramCard>().onClick.AddListener(setType1);
                    idk.GetComponent<ProgramCard>().cardType = "if";
                }
                else
                {
                    idk.GetComponent<ProgramCard>().onClick.AddListener(setType2);
                    idk.GetComponent<ProgramCard>().cardType = "while";
                }
            } else
            {
                //Debug.Log(parent.UI);
                GameObject idk = Instantiate(Movement, parent.UI);

                idk.GetComponent<MovCard>().Card.setSize(parent.UI.GetComponent<GridLayoutGroup>().cellSize);


               


            }



            

        }
    }





    void btn1Ac ()
    {
        gridView.GetComponent<Image>().color = Color.red;
    }

    void btn2Ac()
    {
        gridView.GetComponent<Image>().color = Color.blue;
    }

    void btn3Ac()
    {
        gridView.GetComponent<Image>().color = Color.magenta;
    }

    void btn4Ac()
    {
        gridView.GetComponent<Image>().color = Color.cyan;
    }

    public void setType1 ()
    {
        Debug.Log("setting type 1");
        type = "if";
    }
    public void setType2()
    {
        Debug.Log("setting type 2");
        type = "while";
    }

    public void setBackground (GameObject obj)
    {
        //Debug.Log(obj);
        if (type == "if")
        {
            obj.GetComponent<Image>().color = Color.magenta;
            type = "";
        } else if (type == "while")
        {
            obj.GetComponent<Image>().color = Color.yellow;
            type = "";

        } else if (type == "mov")
        {

        }
    }

    public void checkBackground(GameObject obj, float distance, GameObject hover)
    {
        if (distance < 1)
        {

           

            if (type == "if")
            {
                obj.GetComponent<Image>().color = Color.magenta;
                //type = "";
               // hover = obj;
               
            }
            else if (type == "while")
            {
                obj.GetComponent<Image>().color = Color.yellow;
                //type = "";
               // hover = obj;

            } else if (type == "mov")
            {
                obj.GetComponent<Image>().color = Color.red;
                //type = "";
                //hover = obj;

               // GameObject idk = Instantiate( Program, obj.transform.GetChild(1));

                
               








            }
        } else
        {
            obj.GetComponent<Image>().color = Color.blue;
            type = "";
           
        }

      
    }

    public void setBackground2()
    {

    }

    public void addProgram (ProgramLine parent)
    {
        GameObject idk = Instantiate(Program, parent.ProgramObj.transform);

        parent.ProgramUI.addChild(idk.GetComponent<Movement>().Program);

        parent.Line.setSize(parent.Line.size);

    }








}
