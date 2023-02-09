using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DNASaveSystem;

public class LevelMaker : MonoBehaviour
{

    [Header("Level Data")]
    public LevelInfo level = new LevelInfo();


    [Header("Saving Utility")]
    [SerializeField] Tilemap voidMap;
    [SerializeField] Tilemap backgroundMap;
    [SerializeField] Tilemap decorationMap;
    [SerializeField] Tilemap obstaclesMap;

   

  

    // Start is called before the first frame update
    void Start()
    {
       

        level.createLevelInfo(voidMap, backgroundMap, decorationMap, obstaclesMap);

        SaveManager.saveLevel(level.levelName, level);


    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.Space))
        {

           



            
        }
        */



    }
}
