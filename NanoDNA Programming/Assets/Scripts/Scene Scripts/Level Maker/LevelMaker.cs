using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DNASaveSystem;
using DNAStruct;

public class LevelMaker : MonoBehaviour
{
    [Header("Location")]
    [SerializeField] string path;

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

        SaveManager.saveJSON(level, path, level.levelName);


    }

   
}
