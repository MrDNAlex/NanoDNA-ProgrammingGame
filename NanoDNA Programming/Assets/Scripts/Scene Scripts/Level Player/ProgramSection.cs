using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.Tilemaps;

public class ProgramSection : MonoBehaviour
{

    public LevelScript level;

    public GameObject character;
    public int maxLineNum = 20;
   
    [SerializeField] GameObject progLine;

    // Start is called before the first frame update
    void Start()
    {
        level = Camera.main.GetComponent<LevelScript>();
        character = level.character;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /*
    public void runProgram()
    {

        Program program = new Program();

        //Compile program

        for (int i = 0; i < transform.childCount; i++)
        {

            if (transform.GetChild(i).GetChild(1).childCount != 0)
            {
                program.list.Add(transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<ProgramCard>().action);
            }

        }

        character.GetComponent<CharData>().program = program;

        //Run Program
        for (int i = 0; i < character.GetComponent<CharData>().program.list.Count; i++)
        {
            readAction(character.GetComponent<CharData>().program.list[i]);
        }

    }
    */




    public void compileProgram()
    {
       
        Program program = new Program(false);

        //Compile program

        for (int i = 0; i < transform.childCount; i++)
        {

            if (transform.GetChild(i).GetChild(1).childCount != 0)
            {
               
                program.list.Add(getProgramRef(i).GetComponent<ProgramCard>().action);

              
            }
            else
            {
                program.list.Add(new ProgramAction("none", "up", 0));
            }

        }

        character.GetComponent<CharData>().program = program;

        //Debug.Log("Compile Done");

    }

  

  public void renderProgram(GameObject selected)
    {
        compileProgram();

        character = selected;

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
                    if (getProgramLineComp(i) != null)
                    {
                        
                        getProgramLineComp(i).reAddProgram(data.getAction(i));
                    }
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

}
