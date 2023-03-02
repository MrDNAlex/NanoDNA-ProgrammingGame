using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DNAScenes
{

    public enum Scenes
    {
        Menu = 0, 
        Start = 1,
        Settings = 3,
        PlayLevel = 4,

        
    }

    public class SceneConversion
    {

       public static string GetScene (Scenes scene)
        {
            string sceneName = "";
            switch (scene)
            {
                case Scenes.Menu:
                    sceneName = "Menu";
                    break;
                case Scenes.Start:
                    break;
                case Scenes.Settings:
                    sceneName = "Settings";
                    break;
                case Scenes.PlayLevel:
                    sceneName = "Play Level";
                    break;
                default:
                    break;
            }

            return sceneName;
        }

    }

    




}


