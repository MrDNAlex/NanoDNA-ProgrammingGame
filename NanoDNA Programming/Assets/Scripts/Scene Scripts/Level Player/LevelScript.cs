
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using DNASaveSystem;
using UnityEngine.Rendering;
using DNAStruct;

public class LevelScript : MonoBehaviour
{
    public Language lang = Language.English;

    PlayLevelWords UIwords = new PlayLevelWords();

    public string levelPath;

    [SerializeField] RectTransform background;

    [Header("Rect Transforms")]

    //New Serialize

    [SerializeField] RectTransform mapView;

    [SerializeField] RectTransform content;

    [SerializeField] RectTransform gridView;

    [SerializeField] RectTransform store;

    //[SerializeField] Button resize;
    //[SerializeField] Button play;


    [Header("Game Objects")]

    [SerializeField] GameObject prefab;

    [SerializeField] GameObject prefab2;


    [SerializeField] GameObject Variable;
    // public string type;

    public RectTransform contentTrans;

    [SerializeField] Texture camText;

    [SerializeField] Camera Cam2;

    [Header("Tilemaps")]

    [SerializeField] Tilemap voidMap;
    [SerializeField] Tilemap backgroundMap;
  

    [SerializeField] TileBase tile;

    [SerializeField] public GameObject character;


    [SerializeField] Text resize;
    [SerializeField] Text debug;
    [SerializeField] Text complete;
    [SerializeField] Text save;
    [SerializeField] Text changeLang;

    [SerializeField] Button changeLangBtn;

    Vector2 screenPos;
    Vector2 viewSize;

    public Flex Background;

    public GameObject selected;

    Flex Content;

    public Scripts allScripts = new Scripts();

    private void Awake()
    {
        allScripts.levelScript = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        setUI();

        setUIText();

        contentTrans = content;

        OnDemandRendering.renderFrameInterval = 12;

        changeLangBtn.onClick.AddListener(langChange);
    }

    // Update is called once per frame
    void setUI()
    {
        //Create Flex Components
        //Create a new function in the FlexUI library that gets the reference to the rect transform of the Flex Parent and gets the child index from there. 
        //That way I can call Background.getChild(1);

        //Later, split into multiple functions for better categorization.

        Background = new Flex(background, 1);

        Flex Reg1 = new Flex(Background.getChild(0), 1);
        Flex Header = new Flex(Reg1.getChild(0), 1);
        Flex List = new Flex(Reg1.getChild(1), 8);
        Flex SV = new Flex(List.getChild(0), 1);
        Flex VP = new Flex(SV.getChild(0), 10);

        Flex Controls = new Flex(Header.getChild(0), 1.5f);

        Flex Undo = new Flex(Controls.getChild(0), 1);
        Flex InteracName = new Flex(Controls.getChild(1), 4);
        Flex Save = new Flex(Controls.getChild(2), 1);

        Flex Scripts = new Flex(Header.getChild(1), 1);

        Flex Reg2 = new Flex(Background.getChild(1), 2f);
        Flex MapView = new Flex(Reg2.getChild(0), 2f);

        Flex ChangeLang = new Flex(MapView.getChild(0), 1);
        Flex Zoom = new Flex(MapView.getChild(1), 6);
        Flex Resize = new Flex(MapView.getChild(2), 1);
        Flex Play = new Flex(MapView.getChild(3), 1);

        Flex Reg3 = new Flex(Reg2.getChild(1), 1f);

        Flex Constraints = new Flex(Reg3.getChild(1), 1f);

        Flex CollectedItems = new Flex(Constraints.getChild(0), 1);
        Flex LinesUsed = new Flex(Constraints.getChild(1), 1);
        Flex CompleteLevel = new Flex(Constraints.getChild(2), 1);

        //Add children
        Header.addChild(Controls);
        Header.addChild(Scripts);

        Controls.addChild(InteracName);
        Controls.addChild(Undo);
        Controls.addChild(Save);


        List.addChild(SV);
        SV.addChild(VP);
        VP.addChild(allScripts.programSection.flex);

        Reg1.addChild(Header);
        Reg1.addChild(List);

        Reg2.addChild(MapView);
        Reg2.addChild(Reg3);

        MapView.addChild(ChangeLang);
        MapView.addChild(Zoom);
        MapView.addChild(Resize);
        MapView.addChild(Play);

        Reg3.addChild(store.GetComponent<StoreScript>().Store);
        Reg3.addChild(Constraints);

        Background.addChild(Reg1);
        Background.addChild(Reg2);

        ChangeLang.setSelfHorizontalPadding(12, 1, 0, 1);
        Zoom.setSelfHorizontalPadding(12, 1, 0, 1);
        Resize.setSelfHorizontalPadding(12, 1, 0, 1);
        Play.setSelfHorizontalPadding(12, 1, 0, 1);

        MapView.setSpacingFlex(0.2f, 1);

        Controls.setSpacingFlex(0.5f, 1);
        Controls.setAllPadSame(0.1f, 1);

        Constraints.addChild(CollectedItems);
        Constraints.addChild(LinesUsed);
        Constraints.addChild(CompleteLevel);

        //Constraints and Abilities 
        Constraints.setAllPadSame(0.1f, 1);

        Background.setSize(new Vector2(Screen.width, Screen.height));

        screenPos = new Vector2(mapView.rect.x, Screen.height);
        viewSize = MapView.size;

    }

    public float orthoSizeCalc(LevelInfo info)
    {

      

        //Fit vertically
        float vertOrthoSize = ((float)((info.yMax - info.yMin) + 1) / 2 * backgroundMap.cellSize.y);

        //Fit Horizontally
        float horOrthoSize = ((float)((info.xMax - info.yMin) + 1) / 2 * (backgroundMap.cellSize.x * ((float)Screen.height / (float)Screen.width)));

        if (vertOrthoSize >= horOrthoSize)
        {
            //Give Vert

            return vertOrthoSize;
        }
        else
        {

            //Give Hor
            return horOrthoSize;
        }
    }

    public float getOrthoSize()
    {
        return Cam2.orthographicSize;
    }


    public void setCamera(LevelInfo info)
    {


        //Set the Camera Texture size
        camText.width = (int)Screen.width;
        camText.height = (int)Screen.height;

        //Set the Orthographic size
        Cam2.orthographicSize = orthoSizeCalc(info);

        //Set Backgroud position

        //Get center position
        Vector3 pos = (voidMap.CellToWorld(getCenter(info, true)) + voidMap.CellToWorld(getCenter(info, false))) / 2;

        //Invert center position 
        pos = pos * -1;

        //Leave z untouched
        pos.z = voidMap.transform.position.z;

        //Set position.
        voidMap.transform.SetPositionAndRotation(pos, new Quaternion(0, 0, 0, 0));

    }

    Vector3Int getCenter(LevelInfo info, bool floor)
    {
        float centerX = ((float)(info.xMin + info.xMax + 1) / 2);
        float centerY = ((float)(info.yMin + info.yMax + 1) / 2);

        if (floor)
        {
            return new Vector3Int(Mathf.FloorToInt(centerX), Mathf.FloorToInt(centerY), 0);
        }
        else
        {
            return new Vector3Int(Mathf.CeilToInt(centerX), Mathf.CeilToInt(centerY), 0);
        }

    }

    public void destroySubChildren(GameObject Obj)
    {
        //Only deletes children under the one referenced
        foreach (Transform child in Obj.transform)
        {
            if (child.childCount == 0)
            {
                //Safe to delete
                Destroy(child.gameObject);
            }
            else
            {
                //Delete chidlren first
                destroySubChildren(child.gameObject);

                Destroy(child.gameObject);
            }
        }
    }

   public void setUIText ()
    {
        resize.text = UIwords.resize.getWord(lang);
        debug.text = UIwords.debug.getWord(lang);
        complete.text = UIwords.complete.getWord(lang);
        save.text = UIwords.save.getWord(lang);
        changeLang.text = UIwords.changeLang.getWord(lang);

    }

    public void langChange ()
    {
        
        if (lang == Language.English)
        {
            //Switch to French
            lang = Language.French;

            setUIText();

            allScripts.levelManager.updateConstraints();

            allScripts.storeScript.reload();

            allScripts.mapDrag.reload();

            allScripts.programSection.reload();

            setUI();

        } else
        {
            //Switch to English
            lang = Language.English;

            setUIText();

            allScripts.levelManager.updateConstraints();

            allScripts.storeScript.reload();

            allScripts.mapDrag.reload();

            allScripts.programSection.reload();

            setUI();
        }
        
    }

}
