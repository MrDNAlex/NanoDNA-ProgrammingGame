using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.UI;
using System.IO;
using DNAStruct;


public class StoreScript : MonoBehaviour
{
    public Flex Store;
    public int storeNum = 4;

    [SerializeField] GameObject storeSection;

    [SerializeField] GameObject storeCard;


    Flex GridView; 

    private void Awake()
    {
        setUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(renderStore(StoreTag.Movement));
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

        GridView = new Flex(Store.getChild(1), 5);

        Store.addChild(StoreHeader);
        Store.addChild(GridView);

        StoreHeader.addChild(VP);

        VP.addChild(Content);

        Content.setChildMultiW(230);

      //  StoreHeader.addChild(BTN1);
      //  StoreHeader.addChild(BTN2);
      //  StoreHeader.addChild(BTN3);
      //  StoreHeader.addChild(BTN4);

        /*
        for (int i = 0; i < storeNum; i ++)
        {
          
            Content.addChild(storeSecBtn(i));
        }
        */

        foreach (StoreTag tag in System.Enum.GetValues(typeof(StoreTag)))
        {

            Content.addChild(storeSecBtn(tag));

        }

        


    }

   public Flex storeSecBtn (StoreTag tag)
    {

        GameObject btn = Instantiate(storeSection, Store.getChild(0).GetChild(0).GetChild(0).transform);

        Flex section = new Flex(btn.GetComponent<RectTransform>(), 1);

        switch (tag)
        {

            case StoreTag.Movement:
                //Set button info and set it's store tag
                btn.transform.GetChild(0).GetComponent<Text>().text = "Movement";
                btn.transform.GetComponent<StoreBtn>().storeTag = StoreTag.Movement;
               
                break;
            case StoreTag.Math:
                btn.transform.GetChild(0).GetComponent<Text>().text = "Math";
                btn.transform.GetComponent<StoreBtn>().storeTag = StoreTag.Math;
              
                break;
            case StoreTag.Logic:
                btn.transform.GetChild(0).GetComponent<Text>().text = "Logic";
                btn.transform.GetComponent<StoreBtn>().storeTag = StoreTag.Logic;
              
                break;
            case StoreTag.Variable:
                btn.transform.GetChild(0).GetComponent<Text>().text = "Variable";
                btn.transform.GetComponent<StoreBtn>().storeTag = StoreTag.Variable;
               
                break;


        }

        btn.transform.GetComponent<StoreBtn>().onclick.AddListener(delegate
        {
            StartCoroutine(renderStore(btn.transform.GetComponent<StoreBtn>().storeTag));
            //Use a function that passes the tag and renders all the children of that tag
        });

        return section;
    }

    public IEnumerator renderStore (StoreTag tag)
    {
        //Delete all current children (Flex and real)
        destroyChildren(GridView.UI.gameObject);

        GridView.deleteAllChildren();

        //Add the children and size them
        //Instantiate storeCard
        Object[] storeItems = Resources.LoadAll(folderPaths(tag));

        foreach (Object obj in storeItems)
        {
            GameObject card = Instantiate(storeCard, GridView.UI.transform);

            card.GetComponent<StoreCard>().setStoreCard(obj as GameObject);

            card.GetComponent<StoreCard>().cardFlex.setSize(GridView.UI.GetComponent<GridLayoutGroup>().cellSize);
            yield return null;
        }
    }

    public void destroyChildren(GameObject Obj)
    {
        //Only deletes children under the one referenced
        foreach (Transform child in Obj.transform)
        {
            //Safe to delete
            Destroy(child.gameObject);
        }
    }

    public string folderPaths (StoreTag tag)
    {
        switch (tag)
        {
            case StoreTag.Movement:
                return "Prefabs/Programs/Movement";
            case StoreTag.Math:
                return "Prefabs/Programs/Math";
               
            case StoreTag.Logic:
                return "Prefabs/Programs/Logic";
                
            case StoreTag.Variable:
                return "Prefabs/Programs/Variable";
               
            default:
                return "";
            
        }

    }

    //Instantiate multiple button design with titles associated to the tag




}
