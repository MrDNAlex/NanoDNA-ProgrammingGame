using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueCondition 
{
    public DialogueManager.DialogueConditionType type;
    public bool completed;
    public Transform transRef;
    public float value;
    public MyFunc function;

    public delegate bool MyFunc();

    

    

    public DialogueCondition (DialogueManager.DialogueConditionType type, Transform transRef, float value, MyFunc func = null)
    {
        this.type = type;
        this.transRef = transRef;
        this.value = value;

        if (func != null)
        {
            this.function = func;
        }
    }

    public void runCondition ()
    {
        switch (type)
        {
            case DialogueManager.DialogueConditionType.NextDialogue:
                transRef.GetComponent<Button>().onClick.AddListener(nextDialogueClick);
                break;
            case DialogueManager.DialogueConditionType.WaitCondition:
                waitSeconds();
                break;
            case DialogueManager.DialogueConditionType.SliderChange:
                transRef.GetComponent<Slider>().onValueChanged.AddListener(delegate
                {
                    sliderChange();
                });
                break;
            case DialogueManager.DialogueConditionType.ButtonClick:
                transRef.GetComponent<Button>().onClick.AddListener(nextDialogueClick);
                break;
            
        }
    }

    public void recheckCondition ()
    {
        switch (type)
        {
            case DialogueManager.DialogueConditionType.PlacedProgramLine:

                break;
            case DialogueManager.DialogueConditionType.CustomCondition:
                this.completed = function();
                break;
        }
    }


    public void waitSeconds ()
    {
       
        Debug.Log("Wait condition met");
        this.completed = true;
    }

    public void nextDialogueClick ()
    {
       // transRef.GetComponent<Button>().onClick.RemoveAllListeners();
        Debug.Log("Next Button Condition met");
        this.completed = true;
    }

    public void sliderChange ()
    {
        //transRef.GetComponent<Slider>().onValueChanged.RemoveAllListeners();
        this.completed = true;
    }
}
