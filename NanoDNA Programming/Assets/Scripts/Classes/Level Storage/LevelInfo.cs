using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DNAStruct;


[System.Serializable]
//For something to be saved it must be public 
public class LevelInfo 
{
   [Header("Edit")]
    public UIWord levelName;
    public UIWord levelDescription;
    
    public int maxLine;
    public int maxItems;

    public LevelType levelType;

    public List<VariableData> levelVariables = new List<VariableData>();

    [Header("Automatic")]
    public string levelPath;

    public LevelIconInfo levelIcon;

    public int xMax;
    public int xMin;
    public int yMax;
    public int yMin;

    //Convert this to a list for multiple end conditions?
    public EndInfo endGoal;

    public List<SensorInfo> sensorInfo = new List<SensorInfo>();

    public List<InteractableInfo> interacInfo = new List<InteractableInfo>();

    public List<CharacterInfo> charInfo = new List<CharacterInfo>();

    //public Dictionary<string, TileInstance> ledger = new Dictionary<string, TileInstance>();

    public List<TileInfo> voidTiles = new List<TileInfo>();
    public List<TileInfo> backgroundTiles = new List<TileInfo>();
    public List<TileInfo> decorationTiles = new List<TileInfo>();
    public List<TileInfo> obstacleTiles = new List<TileInfo>();

    public LevelInfo ()
    {
        //Do Nothing
    }

    //Add an array or list for characters and interactables
    public void createLevelInfo (LevelMakerInfo info)
    {
        //Save Void Tiles
        createArrayInfo(info.voidMap, voidTiles);

        //Save Background Tiles
        createArrayInfo(info.backgroundMap, backgroundTiles);

        //Save Obstacle Tiles
        createArrayInfo(info.obstacleMap, obstacleTiles);

        //Save Decoration Tiles
        createArrayInfo(info.decorationMap, decorationTiles);

        //Save Character Info
        createCharArrayInfo(info.charHolder);

        //Save Interactables
        createInteractableArrayInfo(info.charHolder);

        //Create end goal
        createEndGoal(info.charHolder);

        //Create list of sensors
        createSensorArrayInfo(info.charHolder);

        //Get true boundaries
        getTrueCellBoundaries(backgroundTiles);

        //Save Level Icon
        this.levelIcon = new LevelIconInfo(info.levelIcon.id);

        //Save Level path
        this.levelPath = info.levelPath;

    }

    public void createArrayInfo (Tilemap map, List<TileInfo> info)
    {
        //Loop through all the tiles 

        for (int xIndex = map.cellBounds.xMin; xIndex < map.cellBounds.xMax; xIndex++)
        {
            for (int yIndex = map.cellBounds.yMin; yIndex < map.cellBounds.yMax; yIndex++)
            {
                TileBase tile = map.GetTile(new Vector3Int(xIndex, yIndex, 0));

                if (tile != null)
                {
                    info.Add(new TileInfo(tile.name, new Vector2Int(xIndex, yIndex)));
                }
                else
                {
                    //Debug.Log("Position " + new Vector2Int(xIndex, yIndex) + " is null");
                }
            }
        }
        //Debug.Log("Complete");
    }

    public void createCharArrayInfo (GameObject charHolder)
    {
        foreach (Transform child in charHolder.transform)
        {
            if (child.GetComponent<CharData>() != null)
            {
                //Loop through all children and save their sprite id for their ID and their charData

                charInfo.Add(new CharacterInfo(new CharDataInfo(child.GetComponent<CharData>()), child.GetComponent<SpriteRenderer>().sprite.name));
            }
        }
    }

    public void createInteractableArrayInfo (GameObject charHolder)
    {
        foreach (Transform child in charHolder.transform)
        {
            if (child.GetComponent<InteractableData>() != null)
            {
                //Loop through all children and save their sprite id for their ID and their charData

                interacInfo.Add(new InteractableInfo(new InteractableDataInfo(child.GetComponent<InteractableData>()), child.GetComponent<SpriteRenderer>().sprite.name));

                if (child.GetComponent<InteractableData>().collectible)
                {
                    //Add one to the max items
                    maxItems++;
                }

            }
        }
    }

    public void createEndGoal (GameObject charHolder)
    {
        foreach (Transform child in charHolder.transform)
        {
            if (child.GetComponent<EndData>() != null)
            {
                endGoal = new EndInfo(new EndDataInfo(child.GetComponent<EndData>()), child.GetComponent<SpriteRenderer>().sprite.name);
            }
        }
    }

    public void createSensorArrayInfo (GameObject charHolder)
    {
        foreach (Transform child in charHolder.transform)
        {
            if (child.GetComponent<LevelSensor>() != null)
            {
                sensorInfo.Add(new SensorInfo(new SensorDataInfo(child.GetComponent<LevelSensor>()), child.GetComponent<SpriteRenderer>().sprite.name));

            }
        }
    }


    public void getTrueCellBoundaries (List<TileInfo> info)
    {
        //Get Maximums
        xMax = getMax(true, info);
        yMax = getMax(false, info);

        //Get Minimums
        xMin = getMin(true, info);
        yMin = getMin(false, info);

    }

    public int getMax (bool X, List<TileInfo> info)
    {
        int max;

        if (X)
        {
            //Initialize the first max to the position of the first index
            max = info[0].position.x;

            //Get X Max
            foreach (TileInfo tile in info)
            {
                if (tile.position.x > max)
                {
                    max = tile.position.x;
                }
            }
        } else
        {
            //Initialize the first max to the position of the first index
            max = info[0].position.y;

            //Get Y Max
            foreach (TileInfo tile in info)
            {
                if (tile.position.y > max)
                {
                    max = tile.position.y;
                }
            }
        }
        return max;
    }

    public int getMin(bool X, List<TileInfo> info)
    {
        int min;

        if (X)
        {
            //Initialize the first max to the position of the first index
            min = info[0].position.x;

            //Get X Max
            foreach (TileInfo tile in info)
            {
                if (tile.position.x < min)
                {
                    min = tile.position.x;
                }
            }
        }
        else
        {
            //Initialize the first max to the position of the first index
            min = info[0].position.y;

            //Get Y Max
            foreach (TileInfo tile in info)
            {
                if (tile.position.y < min)
                {
                    min = tile.position.y;
                }
            }
        }
        return min;
    }

}