using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;
using DNASaveSystem;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{

    //Level Loader
   // https://www.youtube.com/watch?v=OmobsXZSRKo

    //Try and Compress this section

    [SerializeField] CharLedger charLedger;
    [SerializeField] TileLedger Tileledger;
    [SerializeField] InteractableLedger interacLedger;
    [SerializeField] EndLedger endLedger;

    [SerializeField] GameObject charPrefab;
    [SerializeField] GameObject interacPrefab;
    [SerializeField] GameObject endGoalPrefab;

    [SerializeField] GameObject constraints;

    public LevelInfo info;

    Button complete;

    Text collectedItems;

    Text linesUsed;

    [SerializeField] Text name;

    [Header("Tilemaps")]

    [SerializeField] Tilemap voidMap;
    Tilemap backgroundMap;
    Tilemap obstacleMap;
    Tilemap decorationMap;
    GameObject charHolder;

    public Scripts allScripts;

    public int maxLines = 0;
    public int usedLines = 0;
    public int maxItems = 0;
    public int itemsCollect = 0;

    bool tryComplete;

    public GameObject selected;

    PlayLevelWords UIwords = new PlayLevelWords();

    List<CharData> charData = new List<CharData>();

    Language lang;

    private void Awake()
    {
        //Get the levelInfo
        
        //Load Info
        info = SaveManager.loadJSON<LevelInfo>(CurrentLevelLoader.path, CurrentLevelLoader.name);

        Camera.main.GetComponent<LevelScript>().allScripts.levelManager = this;

        allScripts.levelManager = this;

        getTileMaps();

        getConstraints();
    }

    // Start is called before the first frame update
    void Start()
    {
        allScripts = Camera.main.GetComponent<LevelScript>().allScripts;

        lang = allScripts.levelScript.lang;

        complete.onClick.AddListener(completeLevel);

        loadLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateConstraints()
    {
        //Check all programs, count number of lines, write it down
        //Design something that doesn't use the holder, maybe get access 
        
        lang = allScripts.levelScript.lang;

        //Something in here
        int lines = 0;

        //Update Lines used
        usedLines = 0;

        foreach (CharData data in charData)
        {
            lines += data.program.getLength();
        }

        UIHelper.setText(linesUsed.transform, lines + "/" + maxLines + " " + UIwords.used.getWord(lang), PlayerSettings.colourScheme.getAccentTextColor());

        //Update Collectibles
        itemsCollect = 0;

        foreach (Transform child in charHolder.transform)
        {
            if (child.gameObject.activeSelf == false)
            {
                if (child.GetComponent<InteractableData>() != null)
                {
                    itemsCollect++;
                }
            }
        }
        UIHelper.setText(collectedItems.transform, itemsCollect + "/" + maxItems + " " + UIwords.collected.getWord(lang), PlayerSettings.colourScheme.getAccentTextColor());

    }

    public void completeLevel()
    {
        //Check if max line num is exceeded
        if (tryComplete == false)
        {
            if (usedLines > maxLines)
            {
                //send error message
                Debug.Log("Your program is too long!");
            }
            else
            {
                complete.transform.GetChild(0).GetComponent<Text>().text = UIwords.reset.getWord(lang);

                tryComplete = true;

                //Run final program
                allScripts.programSection.runFinalProgram();
            }
        }
        else
        {
            tryComplete = false;

            //Stop all coroutines
            StopAllCoroutines();

            allScripts.programSection.StopAllCoroutines();

            //Set all characters to initial position
            foreach (Transform child in charHolder.transform)
            {
                if (child.GetComponent<CharData>() != null)
                {
                    child.localPosition = child.GetComponent<CharData>().initPos;
                }
                else
                {
                    //Make interactive appear again
                    child.gameObject.SetActive(true);
                }
            }

            UIHelper.setText(complete.transform.GetChild(0), UIwords.complete, PlayerSettings.colourScheme.getAccentTextColor());

            updateConstraints();
        }
        //If not start running the program
    }

    public void loadLevel()
    {
        //Set Void Tiles
        setTileMap(voidMap, info.voidTiles);
        //yield return null;

        //Set Background Tiles
        setTileMap(backgroundMap, info.backgroundTiles);
        // yield return null;

        //Set Obstacles Tiles
        setTileMap(obstacleMap, info.obstacleTiles);
        //yield return null;

        //Set Decoration Tiles
        setTileMap(decorationMap, info.decorationTiles);
        // yield return null;

        //Set Characters
        setCharacters(info.charInfo);

        //Set Interactables
        setInteractables(info.interacInfo);

        //Set End Goal
        setEndGoal(info.endGoal);

        //Set Other Info
        allScripts.levelScript.setCamera(info);

        maxLines = info.maxLine;
        maxItems = info.maxItems;

        updateConstraints();
        // yield return null;

        //Get the first character instance
        bool findingChar = true;
        int index = 0;
        while (findingChar)
        {
            if (charHolder.transform.GetChild(index).GetComponent<CharData>() != null)
            {
                //Set Character
                allScripts.programSection.selectedCharacter = charHolder.transform.GetChild(index).gameObject;
                allScripts.programSection.selectedCharData = charHolder.transform.GetChild(index).GetComponent<CharData>();

                findingChar = false;
            }
            else
            {
                index++;
            }
        }

        foreach (Transform child in charHolder.transform)
        {
          
            if (child.GetComponent<CharData>() != null)
            {
                charData.Add(child.GetComponent<CharData>());
            }
           
        }
        //Reload Program
        allScripts.programSection.renderProgram();

    }

    public void setTileMap(Tilemap map, List<TileInfo> tileList)
    {
        foreach (TileInfo info in tileList)
        {
            map.SetTile(new Vector3Int(info.position.x, info.position.y, 0), Tileledger.tiles.Find(t => t.id == info.id).tile);
        }
    }

    public void setCharacters(List<CharacterInfo> chars)
    {

        name.text = chars[0].data.name.getWord(lang);
        foreach (CharacterInfo info in chars)
        {

            //Instantiate new Character prefab
            GameObject newChar = Instantiate(charPrefab, charHolder.transform);

            //Set Char data
            newChar.GetComponent<CharData>().name = info.data.name;
            newChar.GetComponent<CharData>().program = info.data.program;
            newChar.GetComponent<CharData>().programStates = info.data.programStates;
            newChar.GetComponent<CharData>().initPos = info.data.initPos;

            //Set Sprite
            newChar.GetComponent<SpriteRenderer>().sprite = charLedger.chars.Find(c => c.id == info.id).sprite;

            //Set initial position
            newChar.transform.localPosition = info.data.initPos;
        }
    }

    public void setInteractables(List<InteractableInfo> interac)
    {

        foreach (InteractableInfo info in interac)
        {
            //Instantiate new Interactable
            GameObject newInterac = Instantiate(interacPrefab, charHolder.transform);

            //Set Interac Data
            newInterac.GetComponent<InteractableData>().name = info.data.name;
            newInterac.GetComponent<InteractableData>().initPos = info.data.initPos;
            newInterac.GetComponent<InteractableData>().collectible = info.data.collectible;

            //Set Sprite
            newInterac.GetComponent<SpriteRenderer>().sprite = interacLedger.sprites.Find(c => c.id == info.id).sprite;

            //Set initial position
            newInterac.transform.localPosition = info.data.initPos;
        }
    }

    public void setEndGoal(EndInfo info)
    {
        //Instantiate Prefab
        GameObject endGoal = Instantiate(endGoalPrefab, charHolder.transform);

        //Set Data
        endGoal.GetComponent<EndData>().name = info.data.name;
        endGoal.GetComponent<EndData>().pos = info.data.pos;
        endGoal.GetComponent<EndData>().size = info.data.size;

        //Set Sprite
        endGoal.GetComponent<SpriteRenderer>().sprite = endLedger.sprites.Find(s => s.id == info.id).sprite;

        //Set Initial Position
        endGoal.transform.localPosition = info.data.pos;

        //Set Size
        endGoal.GetComponent<BoxCollider>().size = info.data.size;
    }

    public void finishLevel()
    {
        if (tryComplete)
        {
            Debug.Log("All finished Thanks for Playing");

            //transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

            transform.GetChild(0).GetChild(1).gameObject.SetActive(true);

        }
    }

    void getTileMaps()
    {
        backgroundMap = voidMap.transform.GetChild(0).GetComponent<Tilemap>();
        obstacleMap = backgroundMap.transform.GetChild(0).GetComponent<Tilemap>();
        decorationMap = obstacleMap.transform.GetChild(0).GetComponent<Tilemap>();
        charHolder = decorationMap.transform.GetChild(0).gameObject;
    }

    void getConstraints()
    {
        collectedItems = constraints.transform.GetChild(0).GetComponent<Text>();

        linesUsed = constraints.transform.GetChild(1).GetComponent<Text>();

        complete = constraints.transform.GetChild(2).GetComponent<Button>();
    }

}