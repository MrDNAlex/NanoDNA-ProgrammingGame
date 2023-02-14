using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EndInfo 
{
    public string id;

    public EndDataInfo data;


    public EndInfo (EndDataInfo data, string id)
    {
        this.data = data;
        this.id = id;
    }
}
