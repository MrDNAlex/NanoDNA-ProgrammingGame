using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;
using DNASaveSystem;


[System.Serializable]
public class PlayerSettings
{

    public static Language language;
    public static int volume;
    public static ColourPaletteStorage colourScheme;
    public static bool advancedVariables;
    

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

}
