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
        text.GetComponent<Text>().text = word.getWord(SaveManager.loadPlaySettings().language);

        text.GetComponent<Text>().color = col;
    }

    public static void setText(Transform text, string words, Color col)
    {
        text.GetComponent<Text>().text = words;

        text.GetComponent<Text>().color = col;
    }

    public static void setImage(Transform trans, string path)
    {
        Sprite sprite = Resources.Load<Sprite>(path);

        trans.GetComponent<Image>().type = Image.Type.Sliced;

        trans.GetComponent<Image>().sprite = sprite;
    }
}
