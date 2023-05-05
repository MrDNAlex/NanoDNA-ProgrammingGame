using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueScript 
{
    public int currentDialogue;

    public List<Dialogue> dialogue = new List<Dialogue>();

   public Dialogue getNextDialogue ()
    {
        currentDialogue++;

        return dialogue[currentDialogue];
    }

    public void Reset ()
    {
        currentDialogue = 0;
    }

    public Dialogue getStart ()
    {
        return dialogue[0];
    }

    public Dialogue getDialogue (int index)
    {
        return dialogue[index];
    }
    
    public void Start ()
    {
        currentDialogue = 0;
    }

    public void setIndex (int index)
    {
        currentDialogue = index;
    }

    public void addDialogue (Dialogue dia)
    {
        dialogue.Add(dia);
    }
}
