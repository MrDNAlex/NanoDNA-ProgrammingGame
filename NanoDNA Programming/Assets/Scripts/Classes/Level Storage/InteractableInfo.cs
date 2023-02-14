using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractableInfo 
{

    //Interactable can have variables associated with it

    public InteractableDataInfo data;

    public string id;


    public InteractableInfo(InteractableDataInfo data, string id)
    {
        this.data = data;
        this.id = id;
    }


}
