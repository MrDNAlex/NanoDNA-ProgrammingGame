using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;
using DNASaveSystem;


[System.Serializable]
public class PlayerSettings
{
    public enum TextSize
    {
        Small, 
        Medium, 
        Big
    }


    public static Language language;
    public static int volume;
    public static ColourPaletteStorage colourScheme;
    public static bool advancedVariables;

    public static int smallTextSize = 30;
    public static int mediumTextSize = 50;
    public static int bigTextSize = 70;
    public static int giganticTextSize;

    

    public PlayerSettings ()
    {
        //Create Default settings
    }

    public static void setColourScheme (SettingColourScheme colour, string path)
    {
        PlayerSettings.colourScheme = new ColourPaletteStorage(path);
        PlayerSettings.colourScheme.colourScheme = colour;

        return;
    }

    public static void LoadSettings (SavedPlayerSettings settings)
    {
        PlayerSettings.language = settings.language;
        PlayerSettings.volume = settings.volume;
        PlayerSettings.advancedVariables = settings.advancedVariables;
        PlayerSettings.colourScheme = settings.colourScheme;
    }

    public static SavedPlayerSettings CreateSave ()
    {
        SavedPlayerSettings save = new SavedPlayerSettings();

        save.language = PlayerSettings.language;
        save.colourScheme = PlayerSettings.colourScheme;
        save.advancedVariables = PlayerSettings.advancedVariables;
        save.volume = PlayerSettings.volume;

        return save;
    }

    public static int getBigText ()
    {
        return bigTextSize;
    }

    public static int getMediumText()
    {
        return mediumTextSize;
    }
    public static int getSmallText()
    {
        return smallTextSize;
    }
    
    public static int getGiganticText()
    {
        return giganticTextSize;
    }



    public static int getTextSize (TextSize size)
    {
        switch (size)
        {
            case TextSize.Small:
                return smallTextSize;
            case TextSize.Medium:
                return mediumTextSize;
            case TextSize.Big:
                return bigTextSize;
            default:
                return mediumTextSize;
        }
    }

}
