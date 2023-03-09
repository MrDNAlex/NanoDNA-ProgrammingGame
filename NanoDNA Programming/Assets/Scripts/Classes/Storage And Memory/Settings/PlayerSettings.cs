using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;


[System.Serializable]
public class PlayerSettings
{

    public Language language;
    public int volume;
    //public SettingColourScheme colourScheme;
    public ColourPaletteStorage colourScheme;
    
    public PlayerSettings (Language lang = Language.English)
    {
        //Set Default settings

        this.language = lang;
        this.volume = 50;
        this.colourScheme = new ColourPaletteStorage("Images/UIDesigns/Palettes/Palette 1");

    }

    public void setLanguage (Language lang)
    {
        this.language = lang;
    }

    public void setVolume (int volume)
    {
        this.volume = volume;
    }

    public void setColourScheme (SettingColourScheme colour, string path)
    {
        colourScheme = new ColourPaletteStorage(path);
        colourScheme.colourScheme = colour;
    }

  


  
}
