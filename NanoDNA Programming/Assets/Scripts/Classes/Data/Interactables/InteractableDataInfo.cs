using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractableDataInfo
{

    public string name;

    public Interactable.InteractableType type;

    public Vector3 initPos;

    public Vector3 rotation;

    public Vector3 size1;

    public Vector3 size2;

    public bool solid;

    public List<string> spriteIDs;

    public List<Sprite> sprites;

    public List<int> sensorSignalIDs;
    //Maybe eventuall add List of variables/accessible data

    //Maybe add a program that can be edited or not edited

    public InteractableDataInfo(Interactable data)
    {

        this.name = data.name;
        this.type = data.type;
        this.initPos = data.initPos;
        this.rotation = data.rotation;
        this.size1 = data.size1;
        this.size2 = data.size2;
        this.solid = data.solid;
        this.spriteIDs = data.spriteIDs;
        this.sprites = data.sprites;
       // this.sprite1ID = data.sprite1ID;
       // this.sprite2ID = data.sprite2ID;
        this.sensorSignalIDs = data.sensorSignalIDs;

    }
    
}