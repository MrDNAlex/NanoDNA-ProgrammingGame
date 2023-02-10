using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DNASaveSystem;
using DNAStruct;

public class LevelMaker : MonoBehaviour
{

    [Header("Level Data")]
    public LevelInfo level = new LevelInfo();

    


    [Header("Saving Utility")]
    [SerializeField] Tilemap voidMap;
    [SerializeField] Tilemap backgroundMap;
    [SerializeField] Tilemap decorationMap;
    [SerializeField] Tilemap obstaclesMap;

    [Header("Ledger")]
    [SerializeField] TileLedger ledger;

    [SerializeField] GameObject charHolder;



   

   
    //Ok so we need to find a way to save a ledger of block types.

  

    // Start is called before the first frame update
    void Start()
    {

        LevelMakerInfo info = new LevelMakerInfo();

        info.voidMap = voidMap;
        info.backgroundMap = backgroundMap;
        info.decorationMap = decorationMap;
        info.obstacleMap = obstaclesMap;
        info.charHolder = charHolder;



        level.createLevelInfo(info);

       // SaveManager.saveLevel(level.levelName, level);

        SaveManager.deepSave(level.levelName, level);


    }

    // Update is called once per frame
    void Update()
    {
       



    }
}
