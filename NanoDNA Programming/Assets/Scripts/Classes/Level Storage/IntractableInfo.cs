using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractableInfo 
{

    public InteractableDataInfo data;

    public string id;

    public InteractableInfo(InteractableDataInfo data, string id)
    {
        this.data = data;
        this.id = id;
    }
}
