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

    public GameObject selectedCharacter;
    public CharData selectedCharData;

    public int maxLineNum = 20;

    [SerializeField] GameObject progLine;
    [SerializeField] Button testBtn;
    [SerializeField] public GameObject charHolder;
    [SerializeField] Text nameHeader;
    [SerializeField] Button saveBtn;
    [SerializeField] Button undoBtn;
    [SerializeField] Tilemap obstacles;
    [SerializeField] Button progSpeed;

    ProgramSpeed speed = ProgramSpeed.Op1;
    int speedDivider = 1;

    public bool undo;
    bool testRunning;

    int charNum = 0;
    int completeThread = 0;

    //public Scripts allScripts;

    PlayLevelWords UIwords = new PlayLevelWords();

    public ProgramVirtualBox virtualBox;

    Language lang;

    private void Awake()
    {

        flex = setUI();

        Scripts.programSection = this;

        progSpeed.onClick.AddListener(editSpeed);

    }

    // Start is called before the first frame update
    void Start()
    {
        lang = PlayerSettings.language;

        levelType = Scripts.levelManager.info.levelType;

        testBtn.onClick.AddListener(testProgram);
        // saveBtn.onClick.AddListener(delegate
        //  {
        //     selectedCharData.displayProgram(true);
        // });
        //undoBtn.onClick.AddListener(undoProgram);

        testRunning = false;


        UIHelper.setText(progSpeed.transform.GetChild(0), "x1", PlayerSettings.colourScheme.getMainTextColor());

    }

    public void setAction(ProgramAction action, int index)
    {
        selectedCharData.program.setAction(action, index);
    }

    public Flex setUI()
    {
        Flex Content = new Flex(GetComponent<RectTransform>(), 1);

        Content.setChildMultiH((Screen.height * 0.9f) / 5.5f);

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

            testBtn.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/UIDesigns/Debug");
            //testBtn.transform.GetChild(0).GetComponent<Text>().text = UIwords.debug.getWord(lang);

            Scripts.levelManager.updateConstraints();
        }
        else
        {

            virtualBox = new ProgramVirtualBox(Scripts.programManager.getVars());

            foreach (Transform child in charHolder.transform)
            {
                if (child.GetComponent<CharData>() != null)
                {
                    StartCoroutine(runCharProgram(child.GetComponent<CharData>()));
                }
            }

            testRunning = true;

            testBtn.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/UIDesigns/DebugActive");
        }
    }

    public IEnumerator runCharProgram(CharData data)
    {
        for (int i = 0; i < data.program.list.Count; i++)
        {
            Program program = new Program(false);

            decompose(data.program.list[i], program, virtualBox);

            for (int j = 0; j < program.list.Count; j++)
            {
                readAction(data.gameObject, program.list[j], virtualBox);
                //This works
                if (program.list[j].actionType != ActionType.Variable)
                {
                    yield return new WaitForSeconds(1f / speedDivider);
                }
            }
        }
        virtualBox.displayAllVariables();
        completeThread++;
    }

    public string getUpdatedMathValue(ProgramAction action, ProgramVirtualBox virtualBox)
    {
        string val1 = "";
        string val2 = "";
        string calcVal = "";

        //Get Values
        if (action.varActData.mathData.refID1 != 0)
        {
            val1 = virtualBox.getVariableValue(action.varActData.mathData.refID1);
        }
        else
        {
            val1 = action.varActData.mathData.value1;
        }

        if (action.varActData.mathData.refID2 != 0)
        {
            val2 = virtualBox.getVariableValue(action.varActData.mathData.refID2);
        }
        else
        {
            val2 = action.varActData.mathData.value2;
        }

        return applyMath(val1, val2, action);
    }

    public string applyMath(string val1, string val2, ProgramAction action)
    {
        string value = "";

        switch (action.variableName)
        {
            case VariableActionNames.MathAddition:
                switch (action.varActData.mathData.varType)
                {
                    case VariableType.Number:
                        value = (int.Parse(val1) + int.Parse(val2)).ToString();
                        break;
                    case VariableType.Decimal:
                        value = (float.Parse(val1) + float.Parse(val2)).ToString();
                        break;
                    case VariableType.Text:
                        value = (val1 + val2);
                        break;
                }
                break;
            case VariableActionNames.MathSubtraction:
                switch (action.varActData.mathData.varType)
                {
                    case VariableType.Number:
                        value = (int.Parse(val1) - int.Parse(val2)).ToString();
                        break;
                    case VariableType.Decimal:
                        value = (float.Parse(val1) - float.Parse(val2)).ToString();
                        break;
                }
                break;
            case VariableActionNames.MathMultiplication:
                switch (action.varActData.mathData.varType)
                {
                    case VariableType.Number:
                        value = (int.Parse(val1) * int.Parse(val2)).ToString();
                        break;
                    case VariableType.Decimal:
                        value = (float.Parse(val1) * float.Parse(val2)).ToString();
                        break;
                }
                break;
            case VariableActionNames.MathDivision:
                switch (action.varActData.mathData.varType)
                {
                    case VariableType.Number:
                        value = (int.Parse(val1) / int.Parse(val2)).ToString();
                        break;
                    case VariableType.Decimal:
                        value = (float.Parse(val1) / float.Parse(val2)).ToString();
                        break;
                }
                break;
        }

        return value;
    }

    public void readAction(GameObject character, ProgramAction action, ProgramVirtualBox virtualBox)
    {
        switch (action.actionType)
        {
            case ActionType.Movement:

                switch (action.movementName)
                {
                    case MovementActionNames.Move:

                        //Calculate next position
                        Vector3 nextPos = character.transform.position + actionToMovement(action, virtualBox);

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
            case ActionType.Logic:

                break;
            case ActionType.Variable:

                VariableActionData varUpdate = new VariableActionData();
                string val = "";

                switch (action.variableName)
                {
                    case VariableActionNames.Variable:

                        //Update Variable value in program manager
                        virtualBox.updateVariable(action.varActData);

                        break;
                    case VariableActionNames.MathAddition:

                        varUpdate.setData.id = action.varActData.refID;
                        varUpdate.setData.value = getUpdatedMathValue(action, virtualBox);

                        Debug.Log("Val: " + val);

                        virtualBox.updateVariable(varUpdate);

                        break;

                    case VariableActionNames.MathSubtraction:

                        varUpdate.setData.id = action.varActData.refID;
                        varUpdate.setData.value = getUpdatedMathValue(action, virtualBox);

                        Debug.Log("Val: " + val);

                        virtualBox.updateVariable(varUpdate);

                        break;

                    case VariableActionNames.MathMultiplication:

                        varUpdate.setData.id = action.varActData.refID;
                        varUpdate.setData.value = getUpdatedMathValue(action, virtualBox);

                        Debug.Log("Val: " + val);

                        virtualBox.updateVariable(varUpdate);

                        break;

                    case VariableActionNames.MathDivision:

                        varUpdate.setData.id = action.varActData.refID;
                        varUpdate.setData.value = getUpdatedMathValue(action, virtualBox);

                        Debug.Log("Val: " + val);

                        virtualBox.updateVariable(varUpdate);

                        break;
                }
                break;
            case ActionType.Action:

                switch (action.actionName)
                {
                    case ActionActionNames.Speak:

                        //Delete all speak children first

                        destroyChildren(action.actData.character.gameObject);

                        string path = "Prefabs/Actions/Talk";
                        switch (action.actData.descriptor)
                        {
                            case ActionDescriptor.Whisper:
                                path = "Prefabs/Actions/Whisper";
                                break;
                            case ActionDescriptor.Talk:
                                path = "Prefabs/Actions/Talk";
                                break;
                            case ActionDescriptor.Yell:
                                path = "Prefabs/Actions/Yell";
                                break;
                        }

                        //Instantiate
                        GameObject bubbleText = Instantiate(Resources.Load(path) as GameObject, action.actData.character);

                        //Get Sprite renderer
                        SpriteRenderer charRender = action.actData.character.GetComponent<SpriteRenderer>();

                        //Set Message
                        if (action.actData.refID == 0)
                        {
                            bubbleText.GetComponent<ChatBubble>().setMessage(action.actData.data, charRender, action.actData.descriptor);
                        }
                        else
                        {
                            bubbleText.GetComponent<ChatBubble>().setMessage(Scripts.programManager.getVariableValue(action.actData.refID), charRender, action.actData.descriptor);
                        }
                        break;
                }
                break;
        }
    }

    public Vector3 actionToMovement(ProgramAction action, ProgramVirtualBox virtualBox)
    {
        switch (action.moveData.dir)
        {
            case Direction.Up:
                return new Vector3(0, getMovementVal(action, virtualBox), 0);
            case Direction.Down:
                return new Vector3(0, -1 * getMovementVal(action, virtualBox), 0);
            case Direction.Left:
                return new Vector3(-1 * getMovementVal(action, virtualBox), 0, 0);
            case Direction.Right:
                return new Vector3(getMovementVal(action, virtualBox), 0, 0);
            default:
                return new Vector3(0, getMovementVal(action, virtualBox), 0);
        }
    }

    public int getMovementVal(ProgramAction action, ProgramVirtualBox virtualBox)
    {
        if (action.moveData.refID != 0)
        {
            // Debug.Log("Hi");
            //Convert 
            return int.Parse(virtualBox.getVariableValue(action.moveData.refID));
        }
        else
        {
            //  Debug.Log("Hello");
            return int.Parse(action.moveData.value);
        }
    }

    public void renderProgram()
    {
        UIHelper.setText(nameHeader.transform, selectedCharData.name, PlayerSettings.colourScheme.getAccentTextColor());

        if (selectedCharData != null)
        {
            CharData data = selectedCharData;

            // data.displayProgram(true);

            //Delete program Child
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject programHolder = getProgramHolderRef(i);

                //Debug.Log(programHolder);

                Flex flex2 = Flex.findChild(programHolder, Scripts.levelScript.Background);

                //Delete Game Objects
                destroyChildren(programHolder);

                //Delete Flex References
                flex2.deleteAllChildren();

                //Instantiate new Object
                getProgramLineComp(i).reAddProgram(data.getAction(i));

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
            readAction(character, program.list[i], virtualBox);
            yield return new WaitForSeconds(1f / speedDivider);
        }

        Camera.main.GetComponent<ProgramManager>().displayAllVariables();
    }

    public void decompose(ProgramAction action, Program program, ProgramVirtualBox virtualBox)
    {
        //Make this more Complex probably

        //Maybe we make a custom Structure that takes in the action 

        switch (action.actionType)
        {
            case ActionType.Movement:

                switch (action.movementName)
                {
                    case MovementActionNames.Move:

                        for (int i = 0; i < getMovementVal(action, virtualBox); i++)
                        {
                            ProgramAction newAction = new ProgramAction();

                            newAction.movementName = MovementActionNames.Move;

                            newAction.moveData.value = "1";
                            newAction.moveData.dir = action.moveData.dir;
                            //newAction.moveData.refID = action.moveData.refID;

                            program.list.Add(newAction);
                        }
                        break;
                }
                break;

            case ActionType.Logic:

                break;
            case ActionType.Variable:
                program.list.Add(action);
                break;
            case ActionType.Action:

                switch (action.actionName)
                {
                    case ActionActionNames.Speak:
                        program.list.Add(action);
                        break;
                }
                break;
        }
    }

    /*
    public void undoProgram()
    {
        undo = true;

        character.GetComponent<CharData>().program = character.GetComponent<CharData>().undoState();

        //Re render program
        renderProgram(character);

        undo = false;

    }
    */

    public void completeLevel()
    {
        StartCoroutine(compLevel());
    }

    public IEnumerator compLevel()
    {
        completeThread = 0;
        yield return StartCoroutine(runFinalProgram());

        //Function that awaits all threads to finish
        StartCoroutine(awaitProgramFinish());
    }

    public IEnumerator awaitProgramFinish()
    {
        while (completeThread != charNum)
        {
            yield return null;
        }

        //Finish the level here 
       // Debug.Log("Completed!");
        Scripts.levelManager.finishLevel();
    }

    public IEnumerator runFinalProgram()
    {
        int chars = 0;
        foreach (Transform child in charHolder.transform)
        {
            if (child.GetComponent<CharData>() != null)
            {
                chars++;
                StartCoroutine(runCharProgram(child.GetComponent<CharData>()));
            }
            yield return null;
        }

        charNum = chars;
    }

    public void reload()
    {
        lang = Camera.main.GetComponent<LevelScript>().lang;
    }

    void editSpeed()
    {
        switch (speed)
        {
            case ProgramSpeed.Op1:
                speed = ProgramSpeed.Op2;
                UIHelper.setText(progSpeed.transform.GetChild(0), "x2", PlayerSettings.colourScheme.getMainTextColor());
                speedDivider = 2;
                break;
            case ProgramSpeed.Op2:
                speed = ProgramSpeed.Op3;
                UIHelper.setText(progSpeed.transform.GetChild(0), "x4", PlayerSettings.colourScheme.getMainTextColor());
                speedDivider = 4;
                break;
            case ProgramSpeed.Op3:
                speed = ProgramSpeed.Op4;
                UIHelper.setText(progSpeed.transform.GetChild(0), "x8", PlayerSettings.colourScheme.getMainTextColor());
                speedDivider = 8;
                break;
            case ProgramSpeed.Op4:
                speed = ProgramSpeed.Op1;
                UIHelper.setText(progSpeed.transform.GetChild(0), "x1", PlayerSettings.colourScheme.getMainTextColor());
                speedDivider = 1;
                break;
        }
    }

}

public enum ProgramSpeed
{
    Op1,
    Op2,
    Op3,
    Op4,
}
