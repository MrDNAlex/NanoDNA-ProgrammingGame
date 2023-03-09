using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;

[System.Serializable]
public class ColourPaletteStorage
{
    public SettingColourScheme colourScheme;

    public string MainS;
    public string SecondaryS;
    public string AccentS;
    public string MainC;
    public string SecondaryC;
    public string AccentC;

    //Color to stand out from the Main colour
    public Color textColorMain;

    //Colour to stand out from the Accent Colour
    public Color textColorAccent;
    public Color textColorBlack = Color.black;
    public string path;

    public ColourPaletteStorage(string path)
    {
        //Path relative from Resource Folder
        this.path = path;

        MainS = path + "/" + "MainS";
        SecondaryS = path + "/" + "SecondaryS";
        AccentS = path + "/" + "AccentS";

        MainC = path + "/" + "MainC";
        SecondaryC = path + "/" + "SecondaryC";
        AccentC = path + "/" + "AccentC";

        colourScheme = SettingColourScheme.Col1;

        textColorMain = Color.white;
        textColorAccent = Color.black;
    }

    public string getMain(bool solid = false)
    {
        if (solid)
        {
            return MainS;
        }
        else
        {
            return MainC;
        }
    }

    public string getSecondary(bool solid = false)
    {
        if (solid)
        {
            return SecondaryS;
        }
        else
        {
            return SecondaryC;
        }
    }

    public string getAccent(bool solid = false)
    {
        if (solid)
        {
            return AccentS;
        }
        else
        {
            return AccentC;
        }
    }

    public Color getAccentTextColor()
    {
        return textColorAccent;
    }

    public Color getMainTextColor()
    {
        return textColorMain;
    }

    public Color getBlackTextColor()
    {
        return textColorBlack;
    }


}
