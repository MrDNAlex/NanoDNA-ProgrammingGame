using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.UI;
using System.IO;
using DNAStruct;
using UnityEngine.Rendering;
using DNASaveSystem;


public class StoreScript : MonoBehaviour
{
    public Flex Store;
    //public int storeNum = 3;

    [SerializeField] GameObject storeSection;

    [SerializeField] GameObject storeCard;

    [SerializeField] GameObject storeCardInfo;


    Flex GridView;
    Flex StoreHeader;
    Flex Content;

    Language lang;

    

    //Scripts allScripts;

    PlayLevelWords UIwords = new PlayLevelWords();


    private void Awake()
    {
        Scripts.storeScript = this;

        lang = PlayerSettings.language;

        setUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        //allScripts = Camera.main.GetComponent<LevelScript>().allScripts;

        StartCoroutine(renderStore(ActionType.Movement));

       
    }

    //Buttons get a 200 pixel multi
    public void setUI()
    {
        Store = new Flex(transform.GetComponent<RectTransform>(), 4f);

        StoreHeader = new Flex(Store.getChild(0), 2, Store);

        Flex VP = new Flex(StoreHeader.getChild(0), 1, StoreHeader);

        Content = new Flex(VP.getChild(0), 1, VP);

        Flex SV = new Flex(Store.getChild(1), 8, Store);

        Flex GridVP = new Flex(SV.getChild(0), 1, SV);

        GridView = new Flex(GridVP.getChild(0), 1, GridVP);

      //  GridVP.setAllPadSame(0.02f, 1);
        GridVP.setHorizontalPadding(0.02f, 1, 0.02f, 1);
        GridVP.setVerticalPadding(0.04f, 1, 0.04f, 1);

        //GridView.setSelfHorizontalPadding(0.02f, 1, 0.02f, 1);
       // GridView.setSelfVerticalPadding(0.02f, 1, 0.02f, 1);

        Content.setChildMultiW(400);

        foreach (ActionType tag in System.Enum.GetValues(typeof(ActionType)))
        {
            Content.addChild(storeSecBtn(tag));
        }

       // Content.setSpacingFlex(0.1f, 1);

        setImage(StoreHeader.UI, PlayerSettings.colourScheme.getSecondary(true));
    }

    public Flex storeSecBtn(ActionType tag)
    {
        GameObject btn = Instantiate(storeSection, Store.getChild(0).GetChild(0).GetChild(0).transform);

        StoreBtn storeBTN = btn.GetComponent<StoreBtn>();

        //Set Info
        storeBTN.setText(UIwords.getStoreTitle(tag, lang));
        storeBTN.setImage(tag);

        storeBTN.actionType = tag;

        storeBTN.onclick.AddListener(delegate
        {
            StartCoroutine(renderStore(storeBTN.actionType));
            //Use a function that passes the tag and renders all the children of that tag
        });

        Flex section = storeBTN.flex;

        return section;
    }

    public IEnumerator renderStore(ActionType tag)
    {
        //Delete all current children (Flex and real)
        destroyChildren(GridView.UI.gameObject);

        GridView.deleteAllChildren();

        GridView.UI.GetComponent<GridLayoutGroup>().cellSize = new Vector2(((GridView.size.x - (GridView.UI.GetComponent<GridLayoutGroup>().spacing.x * 2)) / 3) , ((GridView.size.x / 3) - GridView.UI.GetComponent<GridLayoutGroup>().spacing.x) /1.5f);

        //Instantiate storeCard
        Object[] storeItems = ProgramPrefabs.LoadAllPrefabs(tag);

        foreach (Object obj in storeItems)
        {
            GameObject card = Instantiate(storeCard, GridView.UI.transform);

            card.GetComponent<StoreCard>().setStoreCard(obj as GameObject);

            card.GetComponent<StoreCard>().cardFlex.setSize(GridView.UI.GetComponent<GridLayoutGroup>().cellSize);
            yield return null;
        }

        //GridView.setSize(GridView.size);
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

    void setImage(Transform trans, string path)
    {
        Sprite sprite = Resources.Load<Sprite>(path);

        trans.GetComponent<Image>().type = Image.Type.Sliced;

        trans.GetComponent<Image>().sprite = sprite;
    }

    /*
    public void reload ()
    {
        lang = Camera.main.GetComponent<LevelScript>().lang;

        destroyChildren(Content.UI.gameObject);

        Content.deleteAllChildren();

        setUI();

        StartCoroutine(renderStore(ActionType.Movement));

       
    }
    */

    //Instantiate multiple button design with titles associated to the tag

}
