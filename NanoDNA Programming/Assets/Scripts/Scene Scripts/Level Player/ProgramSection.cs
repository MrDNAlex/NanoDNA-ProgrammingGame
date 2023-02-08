using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlexUI;
using UnityEngine.Tilemaps;

public class ProgramSection : MonoBehaviour
{

    public LevelScript level;

    public GameObject character;
    public int maxLineNum = 20;
   
    [SerializeField] GameObject progLine;
    [SerializeField] Button testBtn;
    [SerializeField] GameObject charHolder;
    [SerializeField] Text nameHeader;
    [SerializeField] Button saveBtn;
    [SerializeField] Button undoBtn;

    public bool undo;
    bool testRunning;
    

    // Start is called before the first frame update
    void Start()
    {
        level = Camera.main.GetComponent<LevelScript>();
        character = level.character;

        testBtn.onClick.AddListener(testProgram);
        saveBtn.onClick.AddListener(compileProgram);
        undoBtn.onClick.AddListener(undoProgram);

        testRunning = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void testProgram ()
    {
        if (testRunning)
        {

            //Stop all coroutines
            StopAllCoroutines();

            //Set all characters to initial position
            foreach (Transform child in charHolder.transform)
            {
                child.position = child.GetComponent<CharData>().initPos;
            }

            testRunning = false;

            testBtn.transform.GetChild(0).GetComponent<Text>().text = "Test";

        } else
        {
            compileProgram();

            foreach (Transform child in charHolder.transform)
            {

                Program program = new Program(false);


                //Decompose the program into more basic parts
                for (int i = 0; i < child.GetComponent<CharData>().program.list.Count; i++)
                {
                    decompose(child.GetComponent<CharData>().program.list[i], program);
                }

                StartCoroutine(runProgram(child.gameObject, program));

            }

            testRunning = true;

            testBtn.transform.GetChild(0).GetComponent<Text>().text = "Reset";

        }

    }

    public void readAction(GameObject character, ProgramAction action)
    {
        switch (action.type)
        {
            case "move":
                switch (action.dir)
                {
                    case "up":
                        character.transform.position = character.transform.position + new Vector3(0, action.value, 0);
                        break;
                    case "left":
                        character.transform.position = character.transform.position + new Vector3(-action.value, 0, 0);
                        break;
                    case "right":
                        character.transform.position = character.transform.position + new Vector3(action.value, 0, 0);
                        break;
                    case "down":
                        character.transform.position = character.transform.position + new Vector3(0, -action.value, 0);
                        break;
                }
                break;
        }
    }

    public void compileProgram()
    {

       // Debug.Log("Compile");

       character.GetComponent<CharData>().addPastState(character.GetComponent<CharData>().program);

        Program program = new Program(false);

        //Compile program

        for (int i = 0; i < transform.childCount; i++)
        {

            if (transform.GetChild(i).GetChild(1).childCount != 0)
            {
                program.list.Add(getProgramRef(i).GetComponent<ProgramCard>().action);
            }
        }

        program.updateLength();

        character.GetComponent<CharData>().program = program;

        level.updateLineUsed(charHolder);

       //Debug.Log("Compile Done");

    }

  

  public void renderProgram(GameObject selected)
    {
        compileProgram();

        character = selected;

        nameHeader.text = character.GetComponent<CharData>().name;

        if (selected != null)
        {

            if (selected.GetComponent<CharData>() != null)
            {

                CharData data = selected.GetComponent<CharData>();

                //Delete program Child

                for (int i = 0; i < transform.childCount; i ++)
                {
                    GameObject programHolder = getProgramHolderRef(i);

                    //Debug.Log(programHolder);

                    Flex flex2 = Flex.findChild(programHolder, level.Background);

                    //Delete Game Objects
                    destroyChildren(programHolder);

                    //Delete Flex References
                    flex2.deleteAllChildren();

                    //Instantiate new Object
                    getProgramLineComp(i).reAddProgram(data.getAction(i));
                }

            }
            else
            {
               

            }
        }
        else
        {
            
          
        }

     
        //Check if selected object has a script

    }

    public void deleteProgram ()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            Destroy(child.gameObject);
        }
    }

    public void updateOGPos ()
    {
        //Debug.Log("Hello");
        for (int i = 0; i < transform.childCount; i ++)
        {
            GameObject program = getProgramRef(i);
            if (program != null)
            {
                program.GetComponent<DeleteIndentDrag>().updateOGPos();
            }
            
        }

    }

    public GameObject getProgramRef (int childIndex)
    {
        if (transform.GetChild(childIndex).GetChild(1).childCount != 0)
        {
            return transform.GetChild(childIndex).GetChild(1).GetChild(0).gameObject;
        } else
        {
            return null;
        }
    }

    public GameObject getProgramHolderRef (int childIndex)
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

    public GameObject getProgramLine (int index)
    {
        if (transform.childCount != 0 && (transform.childCount > index))
        {
            return transform.GetChild(index).gameObject;
        } else
        {
            return null;
        }
    }

    public ProgramLine getProgramLineComp(int index)
    {

        if (getProgramLine(index).GetComponent<ProgramLine>() != null)
        {
            return getProgramLine(index).GetComponent<ProgramLine>();
        } else
        {
            return null;
        }


      
    }

    public IEnumerator runProgram (GameObject character, Program program)
    {
        for (int i = 0; i < program.list.Count; i++)
        {
             readAction(character, program.list[i]);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void decompose (ProgramAction action, Program program)
    {
        switch (action.type)
        {
            case "move":

                for (int i = 0; i < action.value; i ++)
                {
                    program.list.Add(new ProgramAction(action.type, action.dir, 1));

                }

                break;

        }

    }

    public void undoProgram ()
    {
        undo = true;
        character.GetComponent<CharData>().program = character.GetComponent<CharData>().undoState();

        //Re render program
        renderProgram(character);

        undo = false;

    }

    public void runFinalProgram ()
    {
            compileProgram();

            foreach (Transform child in charHolder.transform)
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

}
