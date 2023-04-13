using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;

[System.Serializable]
public class InfoPanelData 
{

    [System.Serializable]
    public struct InfoPage
    {
        public string imgPath;
        public UIWord desc;
    }

    public List<InfoPage> pages;

    public string getImagePage (int pageNum)
    {
        if (pageNum < pages.Count)
        {
            return pages[pageNum].imgPath;
        } else
        {
            return null;
        }
    }

    public string getDescription (int pageNum)
    {
        if (pageNum < pages.Count)
        {
            return pages[pageNum].desc.getWord(PlayerSettings.language);
        }
        else
        {
            return "";
        }
    }

}
