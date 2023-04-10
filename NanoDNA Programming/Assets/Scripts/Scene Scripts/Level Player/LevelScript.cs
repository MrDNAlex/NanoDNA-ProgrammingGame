
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

    [SerializeField] Material camMaterial;

    [SerializeField] Texture camText;

    [SerializeField] Camera Cam2;

    [Header("Tilemaps")]

    [SerializeField] Tilemap voidMap;
    [SerializeField] Tilemap backgroundMap;


    [SerializeField] TileBase tile;

    [SerializeField] Text resize;
    [SerializeField] public Text debug;
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

        Flex ControlHolder = new Flex(Controls.getChild(0), 1, Controls);

        Flex ExitButton = new Flex(ControlHolder.getChild(0), 1, ControlHolder);
        Flex ExitImage = new Flex(ExitButton.getChild(0), 1, ExitButton);

        Flex InteracName = new Flex(ControlHolder.getChild(1), 4, ControlHolder);

        Flex InfoButton = new Flex(ControlHolder.getChild(2), 1, ControlHolder);
        Flex InfoImage = new Flex(InfoButton.getChild(0), 1, InfoButton);

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

        Flex Constraints = new Flex(Reg3.getChild(0), 1f, Reg3);

        Flex ProgressHolder = new Flex(Constraints.getChild(0), 2, Constraints);

        Flex CollectedHolder = new Flex(ProgressHolder.getChild(0), 1, ProgressHolder);
        Flex CollectedBackground = new Flex(CollectedHolder.getChild(0), 1, CollectedHolder);
        Flex CollectedIcon = new Flex(CollectedBackground.getChild(0), 1, CollectedBackground);
        Flex CollectedProgressBar = new Flex(CollectedHolder.getChild(1), 2f, CollectedHolder);

        Flex UsedHolder = new Flex(ProgressHolder.getChild(1), 1, ProgressHolder);
        Flex UsedBackground = new Flex(UsedHolder.getChild(0), 1, UsedHolder);
        Flex UsedIcon = new Flex(UsedBackground.getChild(0), 1, UsedBackground);
        Flex UsedProgressBar = new Flex(UsedHolder.getChild(1), 2f, UsedHolder);

        Flex CompleteLevel = new Flex(Constraints.getChild(1), 1, Constraints);

        //Add Children
        VP.addChild(Scripts.programSection.flex);

        Reg3.addChild(store.GetComponent<StoreScript>().Store);

        MapView.setHorizontalPadding(11, 1, 0, 1);
        MapView.setVerticalPadding(0.02f, 1, 0.02f, 1);

        UIHolder.setSpacingFlex(0.2f, 1);

        //Squares
        ProgSpeed.setSquare();
        Resize.setSquare();
        DebugBTN.setSquare();

        CollectedBackground.setAllPadSame(0.1f, 1);
        UsedBackground.setAllPadSame(0.1f, 1);

        CollectedHolder.setSpacingFlex(0.2f, 1);
        UsedHolder.setSpacingFlex(0.2f, 1);

        CollectedIcon.setSquare();
        UsedIcon.setSquare();

        Buttons.setSpacingFlex(0.3f, 1);

        ProgressHolder.setSpacingFlex(0.1f, 1);

        //  Controls.setSpacingFlex(0.5f, 1);
       //Controls.setAllPadSame(0.1f, 1);
        Controls.setHorizontalPadding(0.02f, 1, 0.02f, 1);
        Controls.setVerticalPadding(0.1f, 1, 0.1f, 1);

        //Constraints and Abilities 
        Constraints.setAllPadSame(0.1f, 1);
        Constraints.setSpacingFlex(0.2f, 1);

        //Calculate leftover height, and fix the size of the Zoom slider

        Background.setSize(new Vector2(Screen.width, Screen.height));

        MapView.setSize(new Vector2(MapView.size.x, MapView.size.x * ((float)Screen.height / Screen.width)));

        Reg3.setSize(new Vector2(Reg3.size.x, Screen.height - MapView.size.y));

        Buttons.UI.GetComponent<VerticalLayoutGroup>().spacing = 5;
        Zoom.setSize(new Vector2(Zoom.size.x, UIHolder.size.y - ProgSpeed.size.y * 3 - UIHolder.UI.GetComponent<VerticalLayoutGroup>().spacing - 10));

        CollectedBackground.setSize(new Vector2(CollectedBackground.size.x, CollectedBackground.size.x));
        UsedBackground.setSize(new Vector2(UsedBackground.size.x, UsedBackground.size.x));

        CollectedProgressBar.setSize(new Vector2(CollectedProgressBar.size.x, CollectedHolder.size.y - 10 - CollectedHolder.size.x));
        UsedProgressBar.setSize(new Vector2(UsedProgressBar.size.x, UsedHolder.size.y - 10 - UsedHolder.size.x));

        ExitButton.setSize(new Vector2(ExitButton.size.y, ExitButton.size.y));
        InfoButton.setSize(new Vector2(InfoButton.size.y, InfoButton.size.y));

        InteracName.setSize(new Vector2(ControlHolder.size.x - (ControlHolder.size.y * 2) + (ControlHolder.size.y - ExitButton.size.y), InteracName.size.y));


        //Set Images

        UIHelper.setImage(ExitButton.UI, PlayerSettings.colourScheme.getAccent());
        UIHelper.setImage(InfoButton.UI, PlayerSettings.colourScheme.getAccent());

        UIHelper.setImage(Header.UI, PlayerSettings.colourScheme.getSecondary(true));
        UIHelper.setImage(Constraints.UI, PlayerSettings.colourScheme.getSecondary(true));
        UIHelper.setImage(List.UI, PlayerSettings.colourScheme.getMain(true));

        UIHelper.setImage(ProgSpeed.UI, PlayerSettings.colourScheme.getMain());
        UIHelper.setImage(Resize.UI, PlayerSettings.colourScheme.getMain());
        UIHelper.setImage(DebugBTN.UI, PlayerSettings.colourScheme.getMain());

        UIHelper.setImage(Zoom.UI.GetChild(0), PlayerSettings.colourScheme.getMain());
        UIHelper.setImage(Zoom.UI.GetChild(1).GetChild(0), PlayerSettings.colourScheme.getSecondary());
        UIHelper.setImage(Zoom.UI.GetChild(2).GetChild(0), PlayerSettings.colourScheme.getAccent());

        UIHelper.setImage(Reg3.getChild(1).GetChild(1).GetChild(0).GetChild(0), PlayerSettings.colourScheme.getMain(true));
        UIHelper.setImage(Reg3.getChild(1).GetChild(1).GetChild(0), PlayerSettings.colourScheme.getMain(true));
        UIHelper.setImage(Reg3.getChild(1).GetChild(1), PlayerSettings.colourScheme.getMain(true));

        UIHelper.setImage(CompleteLevel.UI, PlayerSettings.colourScheme.getAccent());

        //UIHelper.setImage(Save.UI, PlayerSettings.colourScheme.getAccent());
        // UIHelper.setImage(Undo.UI, PlayerSettings.colourScheme.getAccent());

        UIHelper.setImage(UsedBackground.UI, PlayerSettings.colourScheme.getAccent());
        UIHelper.setImage(CollectedBackground.UI, PlayerSettings.colourScheme.getAccent());

        UIHelper.setImage(UsedProgressBar.UI, PlayerSettings.colourScheme.getAccent());
        UIHelper.setImage(CollectedProgressBar.UI, PlayerSettings.colourScheme.getAccent());

        //Set background
        UIHelper.setImage(UsedProgressBar.getChild(0), PlayerSettings.colourScheme.getMain());
        UIHelper.setImage(UsedProgressBar.getChild(1), PlayerSettings.colourScheme.getMain());
        UIHelper.setImage(CollectedProgressBar.getChild(0), PlayerSettings.colourScheme.getMain());
        UIHelper.setImage(CollectedProgressBar.getChild(1), PlayerSettings.colourScheme.getMain());

        LayoutRebuilder.ForceRebuildLayoutImmediate(Background.UI);

    }

    public float orthoSizeCalc(LevelInfo info)
    {
        //Fit vertically
        float vertOrthoSize = ((float)((info.yMax - info.yMin) + 1) / 2 * backgroundMap.cellSize.y);

        //Fit Horizontally

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
        RenderTexture text = new RenderTexture(new RenderTextureDescriptor((int)MapView.size.x, (int)MapView.size.y));

        //Set the Camera Texture size

        Cam2.targetTexture = text;

        camMaterial.mainTexture = text;

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

    public void setUIText()
    {
        UIHelper.setText(complete.transform, UIwords.complete, PlayerSettings.colourScheme.getAccentTextColor());

      //  UIHelper.setText(save.transform, UIwords.save, PlayerSettings.colourScheme.getAccentTextColor());

    }

    public void LiveDebug(string str)
    {
        debug.text = str;
    }

}
