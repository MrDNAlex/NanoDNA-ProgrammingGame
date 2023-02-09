using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace DNASaveSystem
{
    public class SaveManager
    {

        public static void saveLevel (string name, LevelInfo info)
        {
           

            var dir = "Assets/Resources/Levels/Testing/" + name + ".dna";


            string jsonData = JsonUtility.ToJson(info, true);

            Debug.Log(jsonData);

            File.WriteAllText(dir, jsonData);

        }

      

        public static LevelInfo loadSaveFromPath(string path)
        {
            //This function loads the save named into the currently used save file

            //Debug.Log(path);
            string jsonData = "";
            if (File.Exists(path))
            {
                //Extract JSON Data
                jsonData = File.ReadAllText(path);
                Debug.Log(jsonData);
                return JsonUtility.FromJson<LevelInfo>(jsonData);
            }
            else
            {
                Debug.Log("Doesn't exist");
                return null;
            }

        }





    }
}


