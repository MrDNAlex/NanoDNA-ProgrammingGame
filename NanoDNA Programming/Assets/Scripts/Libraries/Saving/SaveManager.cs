using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace DNASaveSystem
{
    public class SaveManager
    {

        static string commonPath = Application.persistentDataPath;

        public static void saveLevel (string name, LevelInfo info)
        {

            var dir = "Assets/Resources/Levels/Testing/" + name + ".json";
            

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

        public static void deepSave (string name, LevelInfo info)
        {
            var dir = commonPath + "/" + name + ".json";


            string jsonData = JsonUtility.ToJson(info, true);

            Debug.Log(jsonData);

            File.WriteAllText(dir, jsonData);

        }

        public static LevelInfo deepLoad (string name)
        {
            //This function loads the save named into the currently used save file

            string path = commonPath + "/" + name + ".json";

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

        public static void saveRandom (object data, string path, string name, string type)
        {

            var dir = path + name + "." + type;


            string jsonData = JsonUtility.ToJson(data, true);

            Debug.Log(jsonData);

            File.WriteAllText(dir, jsonData);

        }




    }
}

