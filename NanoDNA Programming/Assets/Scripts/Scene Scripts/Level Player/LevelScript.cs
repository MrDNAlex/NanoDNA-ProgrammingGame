
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using DNASaveSystem;
using UnityEngine.Rendering;
using DNAStruct;

public class LevelScript : MonoBehaviour
{
    public Language lang;

    PlayLevelWords UIwords = new PlayLevelWords();

    public string levelPath;

    [SerializeField] RectTransform background;

    [Header("Rect Transforms")]

    //New Serialize

    [SerializeField] RectTransform mapView;

    [SerializeField] RectTransform content;

    [SerializeField] RectTransform gridView;

    [SerializeField] RectTransform store;

    [Header("Game Objects")]

    [SerializeField] GameObject prefab;

    [SerializeField] GameObject prefab2;


    [SerializeField] GameObject Variable;

    public RectTransform contentTrans;

    [SerializeField] Texture camText;

    [SerializeField] Camera Cam2;

    [Header("Tilemaps")]

    [SerializeField] Tilemap voidMap;
    [SerializeField] Tilemap backgroundMap;
  

    [SerializeField] TileBase tile;

    [SerializeField] Text resize;
    [SerializeField] Text debug;
    [SerializeField] Text complete;
    [SerializeField] Text save;

    [SerializeField] Button changeLangBtn;


    public Flex Background;
    public Flex MapView;

    //public GameObject selected;

    Flex Content;

    //public Scripts allScripts = new Scripts();

    private void Awake()
    {
        PlayerSettings.LoadSettings(SaveManager.loadPlaySettings());
        Scripts.levelScript = this;
        lang = PlayerSettings.language;

        setUI();
    }

    //Going to start needing a loading screen I think


    // Start is called before the first frame update
    void Start()
    {
        setUIText();

        contentTrans = content;

        OnDemandRendering.renderFrameInterval = 12;

    }

    // Update is called once per frame
    void setUI()
    {
        //Create Flex Components
        //Create a new function in the FlexUI library that gets the reference to the rect transform of the Flex Parent and gets the child index from there. 
        //That way I can call Background.getChild(1);

        //Later, split into multiple functions for better categorization.

        Background = new Flex(background, 1);

        Flex Reg1 = new Flex(Background.getChild(0), 4f, Background);
        Flex Header = new Flex(Reg1.getChild(0), 1, Reg1);
        Flex List = new Flex(Reg1.getChild(1), 8, Reg1);
        Flex SV = new Flex(List.getChild(0), 1, List);
        Flex VP = new Flex(SV.getChild(0), 10, SV);

        Flex Controls = new Flex(Header.getChild(0), 1.5f, Header);

        Flex Undo = new Flex(Controls.getChild(0), 1, Controls);
        Flex InteracName = new Flex(Controls.getChild(1), 4, Controls);
        Flex Save = new Flex(Controls.getChild(2), 1, Controls);

        Flex ScriptsTabs = new Flex(Header.getChild(1), 1);

        Flex Reg2 = new Flex(Background.getChild(1), 6f, Background);
        MapView = new Flex(Reg2.getChild(0), 2f, Reg2);


        Flex UIHolder = new Flex(MapView.getChild(0), 1, MapView);

        Flex Zoom = new Flex(UIHolder.getChild(0), 6, UIHolder);
        Flex Buttons = new Flex(UIHolder.getChild(1), 3, UIHolder);
        Flex ProgSpeed = new Flex(Buttons.getChild(0), 1, Buttons);
        Flex Resize = new Flex(Buttons.getChild(1), 1, Buttons);
        Flex DebugBTN = new Flex(Buttons.getChild(2), 1, Buttons);

        Flex Reg3 = new Flex(Reg2.getChild(1), 1f, Reg2);

        Flex Constraints = new Flex(Reg3.getChild(1), 1f, Reg3);

        Flex CollectedItems = new Flex(Constraints.getChild(0), 1, Constraints);
        Flex LinesUsed = new Flex(Constraints.getChild(1), 1, Constraints);
        Flex CompleteLevel = new Flex(Constraints.getChild(2), 1, Constraints);

        //Add Children
        VP.addChild(Scripts.programSection.flex);

        Reg3.addChild(store.GetComponent<StoreScript>().Store);

        MapView.setHorizontalPadding(12, 1, 0, 1);
        MapView.setVerticalPadding(0.02f, 1, 0.02f, 1);

        UIHolder.setSpacingFlex(0.2f, 1);
        
        ProgSpeed.setSquare();
        Resize.setSquare();
        DebugBTN.setSquare();

        Buttons.setSpacingFlex(0.3f, 1);

        Controls.setSpacingFlex(0.5f, 1);
        Controls.setAllPadSame(0.1f, 1);

        //Constraints and Abilities 
        Constraints.setAllPadSame(0.1f, 1);

        Background.setSize(new Vector2(Screen.width, Screen.height));

        //Calculate leftover height, and fix the size of the Zoom slider

        Buttons.UI.GetComponent<VerticalLayoutGroup>().spacing = 5;
        Zoom.setSize(new Vector2(Zoom.size.x, UIHolder.size.y - ProgSpeed.size.y * 3 - UIHolder.UI.GetComponent<VerticalLayoutGroup>().spacing - 10));

        //Set Images
        UIHelper.setImage(Header.UI, PlayerSettings.colourScheme.getSecondary(true));
        UIHelper.setImage(Constraints.UI, PlayerSettings.colourScheme.getSecondary(true));
        UIHelper.setImage(List.UI, PlayerSettings.colourScheme.getMain(true));

        UIHelper.setImage(ProgSpeed.UI, PlayerSettings.colourScheme.getMain());
        UIHelper.setImage(Resize.UI, PlayerSettings.colourScheme.getMain());
        UIHelper.setImage(DebugBTN.UI, PlayerSettings.colourScheme.getMain());

        UIHelper.setImage(Zoom.UI.GetChild(0), PlayerSettings.colourScheme.getMain());
        UIHelper.setImage(Zoom.UI.GetChild(1).GetChild(0), PlayerSettings.colourScheme.getSecondary());
        UIHelper.setImage(Zoom.UI.GetChild(2).GetChild(0), PlayerSettings.colourScheme.getAccent());

        UIHelper.setImage(Reg3.getChild(0).GetChild(1), PlayerSettings.colourScheme.getMain(true));

        UIHelper.setImage(CompleteLevel.UI, PlayerSettings.colourScheme.getAccent());

        UIHelper.setImage(Save.UI, PlayerSettings.colourScheme.getAccent());
        UIHelper.setImage(Undo.UI, PlayerSettings.colourScheme.getAccent());

        LayoutRebuilder.ForceRebuildLayoutImmediate(Background.UI);

    }

    public float orthoSizeCalc(LevelInfo info)
    {
        //Fit vertically
        float vertOrthoSize = ((float)((info.yMax - info.yMin) + 1) / 2 * backgroundMap.cellSize.y);

        //Fit Horizontally

        Debug.Log(MapView.size);

        float horOrthoSize = ((float)((info.xMax - info.yMin) + 1) / 2 * (backgroundMap.cellSize.x * ((float)MapView.size.y / (float)MapView.size.x)));

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

    public void setCamera(LevelInfo info)
    {
        //Set the Camera Texture size
        camText.width = (int)MapView.size.x;
        camText.height = (int)MapView.size.y;

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
        UIHelper.setText(complete.transform, UIwords.complete, PlayerSettings.colourScheme.getAccentTextColor());

        UIHelper.setText(save.transform, UIwords.save, PlayerSettings.colourScheme.getAccentTextColor());

    }

}
