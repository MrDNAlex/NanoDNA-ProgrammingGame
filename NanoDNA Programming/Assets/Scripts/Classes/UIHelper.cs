using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;
using UnityEngine.UI;
using DNASaveSystem;

public class UIHelper 
{

    //public static PlayerSettings playSettings = SaveManager.loadPlaySettings();

    public static void setText(Transform text, UIWord word, Color col)
    {
        text.GetComponent<Text>().text = word.getWord(PlayerSettings.language);

        text.GetComponent<Text>().color = col;
    }

    public static void setText(Transform text, string words, Color col, int size)
    {
        text.GetComponent<Text>().text = words;

        text.GetComponent<Text>().color = col;

        text.GetComponent<Text>().fontSize = size;

        // text.GetComponent<Text>().
    }

    public static void setImage(Transform trans, string path)
    {
        Sprite sprite = Resources.Load<Sprite>(path);

        //Check if Image component exists
        if (trans.GetComponent<Image>() == null)
        {
            //Add image component 
            trans.gameObject.AddComponent<Image>();
        }

        trans.GetComponent<Image>().type = Image.Type.Sliced;

        trans.GetComponent<Image>().sprite = sprite;
    }  
    
    public static void setImage(Transform trans, Sprite sprite)
    {
        //Check if Image component exists
        if (trans.GetComponent<Image>() == null)
        {
            //Add image component 
            trans.gameObject.AddComponent<Image>();
        }

        trans.GetComponent<Image>().type = Image.Type.Sliced;

        trans.GetComponent<Image>().sprite = sprite;
    }
}
