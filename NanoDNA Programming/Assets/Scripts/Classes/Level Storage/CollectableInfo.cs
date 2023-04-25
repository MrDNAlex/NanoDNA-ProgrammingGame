using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollectableInfo
{

    //Interactable can have variables associated with it

    public CollectableDataInfo data;

    public string id;


    public CollectableInfo(CollectableDataInfo data, string id)
    {
        this.data = data;
        this.id = id;
    }


}
