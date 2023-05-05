using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;

[System.Serializable]
public class Dialogue 
{
    public string imagePath;
    public UIWord dialogue;

    public Dialogue (string imgPath, UIWord text)
    {
        this.imagePath = imgPath;
        this.dialogue = text;
    }
}
