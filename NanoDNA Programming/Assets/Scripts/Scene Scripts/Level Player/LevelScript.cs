
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{

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

    //New Serialize
    [SerializeField] RectTransform btn1;
    [SerializeField] RectTransform btn2;
    [SerializeField] RectTransform btn3;
    [SerializeField] RectTransform btn4;

    [SerializeField] Button resize;
    [SerializeField] Button play;

    [SerializeField] RectTransform mapView;

    [SerializeField] RectTransform content;

    [SerializeField] RectTransform gridView;



    [SerializeField] GameObject prefab;

    [SerializeField] GameObject prefab2;

    [SerializeField] GameObject Movement;

    [SerializeField] GameObject Program;

    [SerializeField] GameObject Variable;
    public string type;

    public RectTransform contentTrans;

    [SerializeField] Texture camText;

    [SerializeField] Camera Cam2;

   

    [SerializeField] Tilemap BackAndMap;
    [SerializeField] Tilemap Void;
    [SerializeField] Tilemap CharAndInt;
    [SerializeField] TileBase tile;



    [SerializeField] GameObject character;

   


    Vector2 screenPos;
    Vector2 viewSize;

    Dictionary<TileBase, CharData> dic = new Dictionary<TileBase, CharData>();


    private void Awake()
    {

     
        for (int i = 0; i < 10; i ++)
        {
            for (int j = 0; j < 10; j++)
            {
                BackAndMap.SetTile(new Vector3Int(i, j, 0), tile);
            }
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        setUI();

        btn1.GetComponent<Button>().onClick.AddListener(btn1Ac);
        btn2.GetComponent<Button>().onClick.AddListener(btn2Ac);
        btn3.GetComponent<Button>().onClick.AddListener(btn3Ac);
        btn4.GetComponent<Button>().onClick.AddListener(btn4Ac);

        contentTrans = content;

        resize.onClick.AddListener(ResizeCam);
        play.onClick.AddListener(runProgram);
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

        Flex Background = new Flex(background, 1);

        Flex Reg1 = new Flex(Background.getChild(0), 1);
        Flex Header = new Flex(Reg1.getChild(0), 1);
        Flex List = new Flex(Reg1.getChild(1), 10);
        Flex SV = new Flex(List.getChild(0), 1);
        Flex VP = new Flex(SV.getChild(0), 10);
       // Flex SB = new Flex(sB, 1);
        Flex Content = new Flex(VP.getChild(0), 1);

        Flex Reg2 = new Flex(Background.getChild(1), 2f);
        Flex MapView = new Flex(Reg2.getChild(0), 2f);

        Flex Resize = new Flex(MapView.getChild(0), 1);
        Flex Play = new Flex(MapView.getChild(1), 1);

        Resize.setCustomSize(new Vector2(100, 100));
        Play.setCustomSize(new Vector2(100, 100));

        Flex Reg3 = new Flex(Reg2.getChild(1), 1f);
        Flex Store = new Flex(Reg3.getChild(0), 4f);

        Flex StoreHeader = new Flex(Store.getChild(0), 1);
        Flex BTN1 = new Flex(StoreHeader.getChild(0), 1);
        Flex BTN2 = new Flex(StoreHeader.getChild(1), 1);
        Flex BTN3 = new Flex(StoreHeader.getChild(2), 1);
        Flex BTN4 = new Flex(StoreHeader.getChild(3), 1);

        Flex GridView = new Flex(Store.getChild(1), 5);

        Flex Constraints = new Flex(Reg3.getChild(1), 1f);

        //Add children
        List.addChild(SV);
        SV.addChild(VP);
       // SV.addChild(SB);
        VP.addChild(Content);

        Reg1.addChild(Header);
        Reg1.addChild(List);

        Reg2.addChild(MapView);
        Reg2.addChild(Reg3);

        MapView.addChild(Resize);
        MapView.addChild(Play);

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

        setCamera(new Vector2(Screen.width, Screen.height));

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

    void addStore (Flex parent)
    {

       for (int i = 0; i < 2; i++)
        {
          

            GameObject idk = Instantiate(prefab2, parent.UI);

            idk.GetComponent<StoreCard>().cardFlex.setSize(parent.UI.GetComponent<GridLayoutGroup>().cellSize);
            //idk.GetComponent<StoreCard>().cardType = "mov";
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

  
    /*
    public void sendRay ()
    {

        
     

        Vector2 worldPoint;
       

        worldPoint = Cam2.ScreenToWorldPoint(BackCalcPos());

        Image img;

        



        TileBase tile = CharAndInt.GetTile(CharAndInt.WorldToCell(worldPoint));

        if (dic[tile] != null)
        {
            for (int i = 0; i < dic[tile].program.list.Count; i++)
            {
                Debug.Log(dic[tile].program.list[i].dispAction());

                readAction(dic[tile].program.list[i]);
            }
        }

    }
    */

    public void readAction (ProgramAction action)
    {
        switch (action.type)
        {
            case "move":

                switch (action.dir)
                {
                    case "up":
                      
                        character.transform.position = character.transform.position + new Vector3(0, action.value, 0);

                        break;
                    case "left":
                        character.transform.position = character.transform.position + new Vector3(-action.value, 0, 0);
                        break;
                    case "right":
                      
                        character.transform.position = character.transform.position + new Vector3(action.value, 0, 0);
                        break;
                    case "down":
                        character.transform.position = character.transform.position + new Vector3(0, -action.value, 0);
                        break;



                }



                break;


        }



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

        return new Vector2(normalX * 1920, 1080 * (1 + normalY));

    }

    public float orthoSizeCalc ()
    {

      
        //Fit vertically
        float vertOrthoSize = ((BackAndMap.cellBounds.yMax - BackAndMap.cellBounds.yMin) / 2 * BackAndMap.cellSize.y);

        //Fit Horizontally
        float horOrthoSize = ((BackAndMap.cellBounds.xMax - BackAndMap.cellBounds.xMin)/2 * (BackAndMap.cellSize.x * ((float)Screen.height/(float)Screen.width)));

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


    public void setCamera (Vector2 size)
    {
        //Calculate max boundaries for both hor and vert
        //place the tile maps so that centers align
        //Have the camera fit to the largest of both dimensions

        //First calculate the absolute middle 

        //Get the middle of the void and align it so that it's center block is at 0,0
        //Get the middle of the background and make sure it's center 


        cleanUpMap(BackAndMap);

        camText.width = (int)size.x;
        camText.height = (int)size.y;

        Cam2.orthographicSize = orthoSizeCalc();

        //Set Backgroud position
        //Get center position
        Vector3 pos = BackAndMap.CellToWorld(getCenter(BackAndMap));
        //Invert center position 
        pos = pos * -1;
        //Leave z untouched
        pos.z = BackAndMap.transform.position.z;
        
        //Set position.
        BackAndMap.transform.SetPositionAndRotation(pos, new Quaternion(0, 0, 0, 0));
      
        //BackAndMap.WorldToCell()

        //Later
        //Add zoom
        //Add scroll

    }

    Vector3Int getCenter (Tilemap map)
    {
        int maxX;
        int maxY;
        int minX;
        int minY;


        maxX = map.cellBounds.xMax;
        maxY = map.cellBounds.yMax;
        minX = map.cellBounds.xMin;
        minY = map.cellBounds.yMin;


        int centerX = (int)(minX + maxX) / 2;
        int centerY = (int)(minY + maxY) / 2;

        return new Vector3Int(centerX, centerY, 0);

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

        /*
        for (int i = minX; i <= maxX; i ++)
        {
            for (int j = minY; j <= maxY; j++)
            {

               TileBase tile = map.GetTile(new Vector3Int(i, j, 0));

                if (tile == null)
                {
                    Debug.Log("Hello");
                    Destroy(tile);
                }
            }
        }
        */

    }

    public void ResizeCam ()
    {
        Cam2.orthographicSize = orthoSizeCalc();
        Cam2.transform.position = Vector3.zero;
    }


    public void runProgram ()
    {

        Program program = new Program();

        //Compile program

        for (int i = 0; i < content.childCount; i ++)
        {
            
            if (content.GetChild(i).GetChild(1).childCount != 0)
            {
                program.list.Add(content.GetChild(i).GetChild(1).GetChild(0).GetComponent<ProgramCard>().action);
            }

        }

        character.GetComponent<CharData>().program = program;

        //Run Program
        for (int i = 0; i < character.GetComponent<CharData>().program.list.Count; i ++)
        {
            readAction(character.GetComponent<CharData>().program.list[i]);
        }

    }

}
