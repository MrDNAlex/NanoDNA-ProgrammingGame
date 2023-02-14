using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;
using UnityEngine.Tilemaps;
using DNAStruct;
using UnityEngine.Rendering;

public class ProgramSection : MonoBehaviour
{

    public Flex flex;

    public LevelType levelType;

    public GameObject character;
    public int maxLineNum = 20;

    [SerializeField] GameObject progLine;
    [SerializeField] Button testBtn;
    [SerializeField] GameObject charHolder;
    [SerializeField] Text nameHeader;
    [SerializeField] Button saveBtn;
    [SerializeField] Button undoBtn;
    [SerializeField] Tilemap obstacles;

    public bool undo;
    bool testRunning;

    public Scripts allScripts;

    PlayLevelWords UIwords = new PlayLevelWords();

    Language lang;

    private void Awake()
    {

        flex = setUI();

        Camera.main.GetComponent<LevelScript>().allScripts.programSection = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        allScripts = Camera.main.GetComponent<LevelScript>().allScripts;

        lang = allScripts.levelScript.lang;

        levelType = allScripts.levelManager.info.levelType;

        character = allScripts.levelScript.character;

        testBtn.onClick.AddListener(testProgram);
        saveBtn.onClick.AddListener(compileProgram);
        undoBtn.onClick.AddListener(undoProgram);

        testRunning = false;

        OnDemandRendering.renderFrameInterval = 12;

    }

    public Flex setUI()
    {
        Flex Content = new Flex(GetComponent<RectTransform>(), 1);

        Content.setChildMultiH(150);

        //Add all the programLine Children

        addChildren(Content);

        return Content;
    }

    void addChildren(Flex parent)
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject programLine = Instantiate(progLine, parent.UI);

            parent.addChild(programLine.GetComponent<ProgramLine>().Line);

            programLine.GetComponent<RectTransform>().GetChild(0).GetComponent<Text>().text = i.ToString();

        }
    }


    public void testProgram()
    {
        if (testRunning)
        {
            //Stop all coroutines
            StopAllCoroutines();

            //Set all characters to initial position
            foreach (Transform child in charHolder.transform)
            {
                if (child.GetComponent<CharData>() != null)
                {
                    child.localPosition = child.GetComponent<CharData>().initPos;

                }
                else
                {
                    //Make interactive appear again
                    child.gameObject.SetActive(true);
                }

            }

            testRunning = false;

            testBtn.transform.GetChild(0).GetComponent<Text>().text = UIwords.debug.getWord(lang);

            allScripts.levelManager.updateConstraints();


        }
        else
        {
            compileProgram();

            foreach (Transform child in charHolder.transform)
            {
                if (child.GetComponent<CharData>() != null)
                {
                    Program program = new Program(false);

                    //Decompose the program into more basic parts
                    for (int i = 0; i < child.GetComponent<CharData>().program.list.Count; i++)
                    {
                        decompose(child.GetComponent<CharData>().program.list[i], program);
                    }

                    StartCoroutine(runProgram(child.gameObject, program));
                }

            }

            testRunning = true;

            testBtn.transform.GetChild(0).GetComponent<Text>().text = UIwords.reset.getWord(lang);
        }
    }

    public void readAction(GameObject character, ProgramAction action)
    {
        switch (action.actionType)
        {
            case ActionType.Movement:

                switch (action.movementName)
                {
                    case MovementActionNames.Move:

                        //Calculate next position
                        Vector3 nextPos = character.transform.position + actionToMovement(action);

                        //Check if the tile in that position exists
                        if (obstacles.GetTile(obstacles.WorldToCell(nextPos)) == null)
                        {
                            //Debug.Log("Clear");
                            character.transform.position = nextPos;
                        }
                        else
                        {
                            //Debug.Log("Not Clear");
                        }

                        break;
                }

                break;
            case ActionType.Math:

                break;
            case ActionType.Logic:

                break;
            case ActionType.Variable:

                break;
        }
    }

    public Vector3 actionToMovement(ProgramAction action)
    {
        switch (action.moveData.dir)
        {
            case Direction.Up:
                return new Vector3(0, action.moveData.value, 0);
            case Direction.Down:
                return new Vector3(0, -action.moveData.value, 0);
            case Direction.Left:
                return new Vector3(-action.moveData.value, 0, 0);
            case Direction.Right:
                return new Vector3(action.moveData.value, 0, 0);
            default:
                return new Vector3(0, action.moveData.value, 0);
        }
    }

    public void compileProgram()
    {
        if (character != null)
        {
            //Debug.Log("Compile");
            if (character.GetComponent<CharData>() != null)
            {
                character.GetComponent<CharData>().addPastState(character.GetComponent<CharData>().program);

                Program program = new Program(false);

                //Compile program

                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).GetChild(1).childCount != 0)
                    {
                        // Debug.Log("Index: " + i + " " + getProgramRef(i).GetComponent<ProgramCard>().action.dispAction());
                        program.list.Add(getProgramRef(i).GetComponent<ProgramCard>().action);
                    }
                }

                program.updateLength();

                character.GetComponent<CharData>().program = program;


                allScripts.levelManager.updateConstraints();

                //Debug.Log("Compile Done");

            }
        }
    }

    public void renderProgram(GameObject selected)
    {
        compileProgram();

        character = selected;

        nameHeader.text = character.GetComponent<CharData>().name.getWord(lang);
        if (selected != null)
        {
            if (selected.GetComponent<CharData>() != null)
            {
                CharData data = selected.GetComponent<CharData>();

                //Delete program Child
                for (int i = 0; i < transform.childCount; i++)
                {
                    GameObject programHolder = getProgramHolderRef(i);

                    //Debug.Log(programHolder);

                    Flex flex2 = Flex.findChild(programHolder, allScripts.levelScript.Background);

                    //Delete Game Objects
                    destroyChildren(programHolder);

                    //Delete Flex References
                    flex2.deleteAllChildren();

                    //Instantiate new Object
                    getProgramLineComp(i).reAddProgram(data.getAction(i));
                }
            }
        }
    }

    public void deleteProgram()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            Destroy(child.gameObject);
        }
    }

    public void updateOGPos()
    {
        //Debug.Log("Hello");
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject program = getProgramRef(i);
            if (program != null)
            {
                program.GetComponent<DeleteIndentDrag>().updateOGPos();
            }
        }
    }

    public GameObject getProgramRef(int childIndex)
    {
        if (transform.GetChild(childIndex).GetChild(1).childCount != 0)
        {
            return transform.GetChild(childIndex).GetChild(1).GetChild(0).gameObject;
        }
        else
        {
            return null;
        }
    }

    public GameObject getProgramHolderRef(int childIndex)
    {
        if (transform.GetChild(childIndex).GetChild(1) != null)
        {
            return transform.GetChild(childIndex).GetChild(1).gameObject;
        }
        else
        {
            return null;
        }
    }

    public void destroyChildren(GameObject Obj)
    {
        //Only deletes children under the one referenced
        foreach (Transform child in Obj.transform)
        {
            //Safe to delete
            Destroy(child.gameObject);
        }
    }

    public GameObject getProgramLine(int index)
    {
        if (transform.childCount != 0 && (transform.childCount > index))
        {
            return transform.GetChild(index).gameObject;
        }
        else
        {
            return null;
        }
    }

    public ProgramLine getProgramLineComp(int index)
    {
        if (getProgramLine(index).GetComponent<ProgramLine>() != null)
        {
            return getProgramLine(index).GetComponent<ProgramLine>();
        }
        else
        {
            return null;
        }
    }

    public IEnumerator runProgram(GameObject character, Program program)
    {
        for (int i = 0; i < program.list.Count; i++)
        {
            readAction(character, program.list[i]);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void decompose(ProgramAction action, Program program)
    {
        //Make this more Complex probably

        //Maybe we make a custom Structure that takes in the action 

        switch (action.actionType)
        {
            case ActionType.Movement:

                switch (action.movementName)
                {
                    case MovementActionNames.Move:

                        for (int i = 0; i < action.moveData.value; i++)
                        {
                            ProgramAction newAction = new ProgramAction();

                            newAction.movementName = MovementActionNames.Move;

                            newAction.moveData.value = 1;
                            newAction.moveData.dir = action.moveData.dir;

                            program.list.Add(newAction);
                        }
                        break;
                }
                break;
            case ActionType.Math:

                break;
            case ActionType.Logic:

                break;
            case ActionType.Variable:

                break;

        }
    }

    public void undoProgram()
    {
        undo = true;

        character.GetComponent<CharData>().program = character.GetComponent<CharData>().undoState();

        //Re render program
        renderProgram(character);

        undo = false;

    }

    public void runFinalProgram()
    {
        compileProgram();

        foreach (Transform child in charHolder.transform)
        {
            Program program = new Program(false);

            if (child.GetComponent<CharData>() != null)
            {
                //Decompose the program into more basic parts
                for (int i = 0; i < child.GetComponent<CharData>().program.list.Count; i++)
                {
                    decompose(child.GetComponent<CharData>().program.list[i], program);
                }

                StartCoroutine(runProgram(child.gameObject, program));
            }
        }
    }

    public void reload()
    {
        lang = Camera.main.GetComponent<LevelScript>().lang;

       // flex = setUI();
    }

}
