using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;
using DNAStruct;

public class DialogueManager : MonoBehaviour
{

    public enum DialogueConditionType
    {
        NextDialogue,
        ButtonClick,
        SliderChange,
        PlacedProgramLine,
        CustomCondition,
        WaitCondition,
    }




    [SerializeField] public RectTransform dialogueHolder;
    [SerializeField] public DialogueBox dialogueBox;
    public DialogueScript script;

    public List<DialogueCondition> conditions = new List<DialogueCondition>();

    public bool dialogueCompleted;

    public VerticalLayoutGroup layoutGroup;

    public delegate void MyFunc();


    private void Awake()
    {
        Scripts.dialogueManager = this;
        layoutGroup = dialogueHolder.GetComponent<VerticalLayoutGroup>();
        setUI();


    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void setUI()
    {
        Flex DialogueBox = dialogueBox.setUI();

        DialogueBox.setSize(new Vector2(Screen.height * 0.55f * ((float)19f / 9f), Screen.height * 0.55f));

        dialogueBox.Camera.setSize(new Vector2(dialogueBox.Camera.size.x, dialogueBox.Camera.size.x));

        dialogueBox.IconHolder.setSize(new Vector2(dialogueBox.IpadHolder.size.y * 0.8f, dialogueBox.IpadHolder.size.y * 0.8f));

        dialogueBox.TextHolder.setSize(new Vector2(dialogueBox.IpadHolder.size.x - dialogueBox.IconHolder.size.y - 20, dialogueBox.IpadHolder.size.y - 20));

        dialogueBox.ContinueButton.setSize(new Vector2(dialogueBox.IconHolder.size.x, dialogueBox.IpadHolder.size.y - dialogueBox.IconHolder.size.y - 20));

        dialogueBox.IpadHolder.UI.GetComponent<HorizontalLayoutGroup>().padding.left = 10;
        dialogueBox.IpadHolder.UI.GetComponent<HorizontalLayoutGroup>().padding.top = 10;
        dialogueBox.IpadHolder.UI.GetComponent<HorizontalLayoutGroup>().padding.bottom = 10;
        dialogueBox.IpadHolder.UI.GetComponent<HorizontalLayoutGroup>().padding.right = 10;

    }

    public void loadScript(DialogueScript script)
    {
        this.script = script;
    }

    public void StartDialogue(int count, MyFunc func, TextAnchor alignment = TextAnchor.LowerCenter, List<DialogueCondition> conditions = null, DialogueScript script = null)
    {
        //Show dialogue box
        //Load current dialogue
        //Apply dialogue

        if (script != null)
        {
            this.script = script;
        }

        if (conditions != null)
        {
            this.conditions = conditions;
        }

        count--;

        layoutGroup.childAlignment = alignment;

        dialogueHolder.gameObject.SetActive(true);

        dialogueBox.setInfo(this.script.getStart());

        dialogueBox.onClick.AddListener(delegate
        {
            handleNextDialogue(count, func, alignment);
        });
    }

    public void handleNextDialogue(int count, MyFunc func, TextAnchor alignment = TextAnchor.LowerCenter, List<DialogueCondition> conditions = null)
    {
        if (conditions != null)
        {
            this.conditions = conditions;
        }


        //Start Coroutine to load new tasks ?
        hideDialogue();

        StartCoroutine(awaitCondition(count, func, alignment));
    }


    void hideDialogue()
    {
        dialogueBox.gameObject.SetActive(false);
    }

    void showDialogue()
    {
        dialogueBox.gameObject.SetActive(true);
    }

    IEnumerator awaitCondition(int count, MyFunc func, TextAnchor alignment = TextAnchor.LowerCenter)
    {
        //Only run conditions on the last one
        if (count == 0)
        {
            //Go through all conditions
            foreach (DialogueCondition cond in conditions)
            {
                if (cond.type == DialogueConditionType.WaitCondition)
                {
                    yield return new WaitForSeconds(cond.value);
                    cond.completed = true;
                }
                else
                {
                    cond.runCondition();
                }

                while (!cond.completed)
                {
                    cond.recheckCondition();
                    yield return null;
                }
            }
            conditions = new List<DialogueCondition>();
        }

        if (count > 0)
        {
            //  Debug.Log(count);
            startNextDialogue(count, func, alignment);
        }
        else
        {
            // Debug.Log("Run next function");
            if (func != null)
            {
                func();
            }
        }
    }

    void startNextDialogue(int count, MyFunc func, TextAnchor alignment = TextAnchor.LowerCenter)
    {
        //Set dialogue to next option
        showDialogue();

        count--;

        layoutGroup.childAlignment = alignment;

        dialogueBox.setInfo(script.getNextDialogue());

        dialogueBox.onClick.RemoveAllListeners();

        dialogueBox.onClick.AddListener(delegate
        {
            handleNextDialogue(count, func, alignment);
        });

        //Animate dialogue
    }

    public void continueDialogue(int count, MyFunc func, TextAnchor alignment = TextAnchor.LowerCenter, List<DialogueCondition> conditions = null)
    {
        if (conditions != null)
        {
            this.conditions = conditions;
        }

        count--;

        showDialogue();

        layoutGroup.childAlignment = alignment;

        dialogueBox.onClick.RemoveAllListeners();

        dialogueBox.setInfo(script.getNextDialogue());

        dialogueHolder.gameObject.SetActive(true);

        dialogueBox.onClick.AddListener(delegate
        {
            handleNextDialogue(count, func, alignment);
        });
    }

    //Make a continue to next dialogue based off the 
    //Make other continue on conditions, like on slider value change
    //Wait for time condition


    //Spawn the 






}
