using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.UI;

public class StoreScript : MonoBehaviour
{
    public Flex Store;
    public int storeNum = 4;

    [SerializeField] GameObject storeSection;


    private void Awake()
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


    //Buttons get a 200 pixel multi

    public void setUI ()
    {
        Store = new Flex(transform.GetComponent<RectTransform>(), 4f);

        Flex StoreHeader = new Flex(Store.getChild(0), 1);

        Flex VP = new Flex(StoreHeader.getChild(0), 1);

        Flex Content = new Flex(VP.getChild(0), 1);

        Flex GridView = new Flex(Store.getChild(1), 5);

        Store.addChild(StoreHeader);
        Store.addChild(GridView);

        StoreHeader.addChild(VP);

        VP.addChild(Content);

        Content.setChildMultiW(230);

      //  StoreHeader.addChild(BTN1);
      //  StoreHeader.addChild(BTN2);
      //  StoreHeader.addChild(BTN3);
      //  StoreHeader.addChild(BTN4);

        for (int i = 0; i < storeNum; i ++)
        {
          
            Content.addChild(storeSecBtn(i));
        }

    }

    void addStore(Flex parent)
    {

        for (int i = 0; i < 2; i++)
        {
           // GameObject idk = Instantiate(prefab2, parent.UI);

           // idk.GetComponent<StoreCard>().cardFlex.setSize(parent.UI.GetComponent<GridLayoutGroup>().cellSize);
            //idk.GetComponent<StoreCard>().cardType = "mov";
        }

    }


   public Flex storeSecBtn (int index)
    {

        GameObject btn = Instantiate(storeSection, Store.getChild(0).GetChild(0).GetChild(0).transform);

        Flex section = new Flex(btn.GetComponent<RectTransform>(), 1);

        switch (index)
        {

            case 1:
                //Set button info and set it's store tag
                btn.transform.GetChild(0).GetComponent<Text>().text = "Movement";
                break;
            case 2:
                btn.transform.GetChild(0).GetComponent<Text>().text = "Math";
                break;
            case 3:
                btn.transform.GetChild(0).GetComponent<Text>().text = "Logic";
                break;
            case 4:
                btn.transform.GetChild(0).GetComponent<Text>().text = "Variables";
                break;


        }

        return section;
    }




    //Instantiate multiple button design with titles associated to the tag




}
