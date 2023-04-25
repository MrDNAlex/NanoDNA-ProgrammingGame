using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterInfo 
{

    public CharDataInfo data;

    public string id;


    public CharacterInfo (CharDataInfo data, string id)
    {
        this.data = data;
        this.id = id;
    }

}
