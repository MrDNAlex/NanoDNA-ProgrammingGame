using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.UI;
using System.IO;
using DNAStruct;
using UnityEngine.Rendering;


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
        StartCoroutine(renderStore(ActionType.Movement));

        OnDemandRendering.renderFrameInterval = 12;
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

        foreach (ActionType tag in System.Enum.GetValues(typeof(ActionType)))
        {
            Content.addChild(storeSecBtn(tag));
        }

    }

   public Flex storeSecBtn (ActionType tag)
    {

        GameObject btn = Instantiate(storeSection, Store.getChild(0).GetChild(0).GetChild(0).transform);

        Flex section = new Flex(btn.GetComponent<RectTransform>(), 1);

        switch (tag)
        {

            case ActionType.Movement:
                //Set button info and set it's store tag
                btn.transform.GetChild(0).GetComponent<Text>().text = "Movement";
                btn.transform.GetComponent<StoreBtn>().actionType = ActionType.Movement;
               
                break;
            case ActionType.Math:
                btn.transform.GetChild(0).GetComponent<Text>().text = "Math";
                btn.transform.GetComponent<StoreBtn>().actionType = ActionType.Math;
              
                break;
            case ActionType.Logic:
                btn.transform.GetChild(0).GetComponent<Text>().text = "Logic";
                btn.transform.GetComponent<StoreBtn>().actionType = ActionType.Logic;
              
                break;
            case ActionType.Variable:
                btn.transform.GetChild(0).GetComponent<Text>().text = "Variable";
                btn.transform.GetComponent<StoreBtn>().actionType = ActionType.Variable;
               
                break;
            default:
                section.flex = 0.01f;
                break;
        }

        btn.transform.GetComponent<StoreBtn>().onclick.AddListener(delegate
        {
            StartCoroutine(renderStore(btn.transform.GetComponent<StoreBtn>().actionType));
            //Use a function that passes the tag and renders all the children of that tag
        });

        return section;
    }

    public IEnumerator renderStore (ActionType tag)
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

    public string folderPaths (ActionType tag)
    {
        switch (tag)
        {
            case ActionType.Movement:
                return "Prefabs/Programs/Movement";
            case ActionType.Math:
                return "Prefabs/Programs/Math";
               
            case ActionType.Logic:
                return "Prefabs/Programs/Logic";
                
            case ActionType.Variable:
                return "Prefabs/Programs/Variable";
               
            default:
                return "";
            
        }

    }

    //Instantiate multiple button design with titles associated to the tag

}