
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using DNASaveSystem;

public class LevelScript : MonoBehaviour
{

    public string levelPath;

    [SerializeField] RectTransform background;


    /*
    [SerializeField] RectTransform reg1;
    [SerializeField] RectTransform header;
    [SerializeField] RectTransform list;
    [SerializeField] RectTransform sV;
   // [SerializeField] RectTransform sB;
    [SerializeField] RectTransform vP;


    [SerializeField] RectTransform reg2;
    [SerializeField] RectTransform mapView;

    [SerializeField] RectTransform reg3;

    [Header("Store")]
    [SerializeField] RectTransform store;

    [SerializeField] RectTransform storeHeader;

    [SerializeField] RectTransform constraints;
    */

    [Header("Rect Transforms")]

    //New Serialize
    [SerializeField] RectTransform btn1;
    [SerializeField] RectTransform btn2;
    [SerializeField] RectTransform btn3;
    [SerializeField] RectTransform btn4;

    [SerializeField] RectTransform mapView;

    [SerializeField] RectTransform content;

    [SerializeField] RectTransform gridView;

    [SerializeField] RectTransform store;

    [SerializeField] Button resize;
    [SerializeField] Button play;


    [Header("Game Objects")]

    [SerializeField] GameObject prefab;

    [SerializeField] GameObject prefab2;

    [SerializeField] GameObject Movement;

    [SerializeField] GameObject Program;

    [SerializeField] GameObject Variable;
    public string type;

    public RectTransform contentTrans;

    [SerializeField] Texture camText;

    [SerializeField] Camera Cam2;

    [Header("Tilemaps")]

    [SerializeField] Tilemap voidMap;
    [SerializeField] Tilemap backgroundMap;
    [SerializeField] Tilemap obstacleMap;
    [SerializeField] Tilemap decorationMap;


    [SerializeField] TileBase tile;

    [SerializeField] public GameObject character;

    [Header("Requirement Section")]

    [SerializeField] Text collectedItems;

    [SerializeField] Text linesUsed;

    [SerializeField] Button complete;


    Vector2 screenPos;
    Vector2 viewSize;

    Dictionary<TileBase, CharData> dic = new Dictionary<TileBase, CharData>();

    public Flex Background;


    public GameObject selected;

    Flex Content;

    public ProgramSection progSec;

    int maxLines = 5;
    int usedLines = 0;


    private void Awake()
    {

        loadLevel();
        /*
        for (int i = 0; i < 10; i ++)
        {
            for (int j = 0; j < 10; j++)
            {
                backgroundMap.SetTile(new Vector3Int(i, j, 0), tile);
            }
        }
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        

       

        setUI();

        contentTrans = content;

        //resize.onClick.AddListener(ResizeCam);
        //play.onClick.AddListener(runProgram);

        progSec = content.GetComponent<ProgramSection>();


        initText();

        complete.onClick.AddListener(completeLevel);

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

        //Later, split into multiple functions for better categorization.

        Background = new Flex(background, 1);

        Flex Reg1 = new Flex(Background.getChild(0), 1);
        Flex Header = new Flex(Reg1.getChild(0), 1);
        Flex List = new Flex(Reg1.getChild(1), 8);
        Flex SV = new Flex(List.getChild(0), 1);
        Flex VP = new Flex(SV.getChild(0), 10);
       // Flex SB = new Flex(sB, 1);
        Content = new Flex(VP.getChild(0), 1);

        Flex Controls = new Flex(Header.getChild(0), 1.5f);

        Flex Undo = new Flex(Controls.getChild(0), 1);
        Flex InteracName = new Flex(Controls.getChild(1), 4);
        Flex Save = new Flex(Controls.getChild(2), 1);

       // Undo.setSquare();
      //  Save.setSquare();

        Flex Scripts = new Flex(Header.getChild(1), 1);

        Flex Reg2 = new Flex(Background.getChild(1), 2f);
        Flex MapView = new Flex(Reg2.getChild(0), 2f);

        Flex Zoom = new Flex(MapView.getChild(0), 6);
        Flex Resize = new Flex(MapView.getChild(1), 1);
        Flex Play = new Flex(MapView.getChild(2), 1);

        Zoom.setCustomSize(new Vector2(80, 0));
        Resize.setCustomSize(new Vector2(80, 0));
        Play.setCustomSize(new Vector2(80, 0));

        Flex Reg3 = new Flex(Reg2.getChild(1), 1f);
        /*
        Flex Store = new Flex(Reg3.getChild(0), 4f);

        Flex StoreHeader = new Flex(Store.getChild(0), 1);
        Flex BTN1 = new Flex(StoreHeader.getChild(0), 1);
        Flex BTN2 = new Flex(StoreHeader.getChild(1), 1);
        Flex BTN3 = new Flex(StoreHeader.getChild(2), 1);
        Flex BTN4 = new Flex(StoreHeader.getChild(3), 1);

        Flex GridView = new Flex(Store.getChild(1), 5);
        */

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
       // SV.addChild(SB);
        VP.addChild(Content);

        Reg1.addChild(Header);
        Reg1.addChild(List);

        Reg2.addChild(MapView);
        Reg2.addChild(Reg3);

        MapView.addChild(Zoom);
        MapView.addChild(Resize);
        MapView.addChild(Play);

        Reg3.addChild(store.GetComponent<StoreScript>().Store);
        Reg3.addChild(Constraints);

        Background.addChild(Reg1);
        Background.addChild(Reg2);

        //Background.addChild(Reg3);

        MapView.setSpacingFlex(0.2f, 1);

        Controls.setSpacingFlex(0.5f, 1);
        Controls.setAllPadSame(0.1f, 1);

        Constraints.addChild(CollectedItems);
        Constraints.addChild(LinesUsed);
        Constraints.addChild(CompleteLevel);

        //Add the programming blocks as children
        addChildren(Content);




        //addStore(GridView);

        //Constraints and Abilities 

        Content.setChildMultiH(150);

        Constraints.setAllPadSame(0.1f, 1);

        Background.setSize(new Vector2(Screen.width, Screen.height));

        screenPos = new Vector2(mapView.rect.x, Screen.height);
        viewSize = MapView.size;

    }

    void addChildren (Flex parent)
    {
        
        for (int i = 0; i < 20; i ++)
        {
            GameObject idk = Instantiate(prefab, parent.UI);

            parent.addChild(idk.GetComponent<ProgramLine>().Line);

           // Debug.Log(idk.GetComponent<ProgramBlock>().Block);

            idk.GetComponent<RectTransform>().GetChild(0).GetComponent<Text>().text = i.ToString();
            //idk.GetComponent<Button>().onClick.AddListener(delegate { setBackground(idk); });
        }
    }

    public void addProgram (ProgramLine parent, string type)
    {

        parent.deleteLine();
        GameObject idk = null;

        if (type == "move")
        {
            idk = Instantiate(Program, parent.ProgramObj.transform);
            
        } else if (type == "var")
        {
            idk = Instantiate(Variable, parent.ProgramObj.transform);
           
        }

        parent.ProgramUI.addChild(idk.GetComponent<ProgramCard>().program);

        Destroy(idk.GetComponent<DragController2>());

        parent.Line.setSize(parent.Line.size);

        idk.GetComponent<ProgramCard>().progLine = parent.transform;

        idk.AddComponent<DeleteIndentDrag>();

    }

    public Vector2 BackCalcPos ()
    {
        //Debug.Log("Mouse:" + Input.mousePosition);

       // Debug.Log("ScreenPos:" + screenPos);
       // Debug.Log("ScreenSize:" + viewSize);

        Vector2 mouse = Input.mousePosition;

        float x = mouse.x - Mathf.Abs(screenPos.x);
        float y = mouse.y - Screen.height;

        float normalX = x / viewSize.x;
        float normalY = y / viewSize.y;

       // Debug.Log("x:" + x);
       // Debug.Log("y:" + y);
       // Debug.Log("Nx:" + normalX);
       // Debug.Log("Ny:" + normalY);

       // Debug.Log(new Vector2(normalX * 1920, 1080 * (1 + normalY)));

        return new Vector2(normalX * Screen.width, Screen.height * (1 + normalY));
    }

    public float orthoSizeCalc (LevelInfo info)
    {
       
        //Fit vertically
        float vertOrthoSize = ((float)((info.yMax - info.yMin)+1) / 2 * backgroundMap.cellSize.y);

        //Fit Horizontally
        float horOrthoSize = ((float)((info.xMax - info.yMin)+1) /2 * (backgroundMap.cellSize.x * ((float)Screen.height/(float)Screen.width)));

        if (vertOrthoSize >= horOrthoSize)
        {
            //Give Vert
           
            return vertOrthoSize;
        } else
        {
           
            //Give Hor
            return horOrthoSize;
        }

    }

    public float getOrthoSize ()
    {
        return Cam2.orthographicSize;
    }


    public void setCamera (LevelInfo info)
    {
        //Calculate max boundaries for both hor and vert
        //place the tile maps so that centers align
        //Have the camera fit to the largest of both dimensions

        //First calculate the absolute middle 

        //Get the middle of the void and align it so that it's center block is at 0,0
        //Get the middle of the background and make sure it's center 

        //Set the Camera Texture size
        camText.width = (int)Screen.width;
        camText.height = (int)Screen.height;

        //Clean up the tile
        cleanUpMap(backgroundMap);

        //Set the Orthographic size
        Cam2.orthographicSize = orthoSizeCalc(info);

        //Set Backgroud position
        //Get center position
        Vector3 pos = (voidMap.CellToWorld(getCenter(info, true)) + voidMap.CellToWorld(getCenter(info, false)))/2;

        //Invert center position 
        pos = pos * -1;

        //Leave z untouched
        pos.z = voidMap.transform.position.z;

        //Set position.
        voidMap.transform.SetPositionAndRotation(pos, new Quaternion(0, 0, 0, 0));
      
    }

    Vector3Int getCenter (LevelInfo info, bool floor)
    {
        float centerX = ((float)(info.xMin + info.xMax + 1) / 2);
        float centerY = ((float)(info.yMin + info.yMax + 1) / 2);

        if (floor)
        {
            return new Vector3Int(Mathf.FloorToInt(centerX), Mathf.FloorToInt(centerY), 0);
        } else
        {
            return new Vector3Int(Mathf.CeilToInt(centerX), Mathf.CeilToInt(centerY), 0);
        }

    }

    public void cleanUpMap (Tilemap map)
    {
        int maxX;
        int maxY;
        int minX;
        int minY;


        maxX = map.cellBounds.xMax;
        maxY = map.cellBounds.yMax;
        minX = map.cellBounds.xMin;
        minY = map.cellBounds.yMin;

    }

    public void destroySubChildren (GameObject Obj)
    {
        //Only deletes children under the one referenced
        foreach (Transform child in Obj.transform)
        {
            if (child.childCount == 0)
            {
                //Safe to delete
                Destroy(child.gameObject);
            } else
            {
                //Delete chidlren first
                destroySubChildren(child.gameObject);

                Destroy(child.gameObject);
            }
        }
    }

    public void initText ()
    {

        Dictionary<string, Vector2> idk = new Dictionary<string, Vector2>();


        collectedItems.text = "0/3 Collected";

        linesUsed.text = "0/3 Used";
    }

    public void updateLineUsed (GameObject holder)
    {
        //Check all programs, count number of lines, write it down


        //Design something that doesn't use the holder, maybe get access 

       usedLines = 0;
        foreach (Transform child in holder.transform)
        {
            Program prog = child.GetComponent<CharData>().program;

            usedLines += prog.progLength;

        }

        linesUsed.text = usedLines + "/" + maxLines + " Used";

    }

    public void completeLevel ()
    {
        //Check if max line num is exceeded

        if (usedLines > maxLines)
        {
            //send error message
            Debug.Log("Your program is too long!");

        } else
        {
            //Run final program

            Debug.Log("Running final Program");

            progSec.runFinalProgram();

            //Give out score if successful

        }

        //If not start running the program

    }

    public void loadLevel ()
    {
        //Load Info
        LevelInfo info = SaveManager.loadSaveFromPath(levelPath);

        //Set Void Tiles
        setTileMap(voidMap, info.voidTiles);

        //Set Background Tiles
        setTileMap(backgroundMap, info.backgroundTiles);

        //Set Obstacles Tiles
        setTileMap(obstacleMap, info.obstacleTiles);

        //Set Decoration Tiles
        setTileMap(decorationMap, info.decorationTiles);


        setCamera(info);
    }

    public void setTileMap (Tilemap map, List<TileInfo> tileList)
    {
        foreach (TileInfo info in tileList)
        {

           // Debug.Log(info.tile);

            map.SetTile(new Vector3Int(info.position.x, info.position.y, 0), info.tile);
           
        }
    }

   







}
