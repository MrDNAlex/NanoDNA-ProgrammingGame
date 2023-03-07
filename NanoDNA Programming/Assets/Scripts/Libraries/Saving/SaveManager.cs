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

        public static void savePlaySettings (PlayerSettings playSettings)
        {
            var dir = commonPath + "/" + "PlaySettings" + ".json";


            string jsonData = JsonUtility.ToJson(playSettings, true);

            Debug.Log(jsonData);

            File.WriteAllText(dir, jsonData);
        }

        public static PlayerSettings loadPlaySettings ()
        {
            //This function loads the save named into the currently used save file

            string path = commonPath + "/" + "PlaySettings" + ".json";

            //Debug.Log(path);
            string jsonData = "";
            if (File.Exists(path))
            {
                //Extract JSON Data
                jsonData = File.ReadAllText(path);
                Debug.Log(jsonData);
                return JsonUtility.FromJson<PlayerSettings>(jsonData);
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

        public static Type loadJSON<Type> (string path, string name)
        {
            var dir = path + "/" + name;
            TextAsset json = null;
            try
            {
                json = Resources.Load<TextAsset>(dir);
               
            } catch
            {
                Debug.Log("Doesn't Exist or Wrong Type");
            }
            return JsonUtility.FromJson<Type>(json.text);
        }

        public static void saveJSON (object json,string path, string name)
        {
            Debug.Log(json);

            var dir = path + "/" + name + "." + "json";

            string jsonData = JsonUtility.ToJson(json, true);

            Debug.Log(jsonData);

            File.WriteAllText(dir, jsonData);
        }

        

    }
}


