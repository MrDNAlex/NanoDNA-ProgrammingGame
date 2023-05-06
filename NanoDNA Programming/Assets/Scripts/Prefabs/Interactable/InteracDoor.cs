using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteracDoor : Interactable, IInteractable
{

    private void Awake()
    {
        this.iInterac = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Activate ()
    {
        //Activation stuff

        Debug.Log("Activate!");

        //Debug.Log(sprites.Count);

        this.GetComponent<SpriteRenderer>().sprite = sprites[1];

        this.solid = false;

    }

    public void Deactivate()
    {
        //Deactivation stuff
        Debug.Log("Deactivate!");

        this.GetComponent<SpriteRenderer>().sprite = sprites[0];

        this.solid = true;
    }

    public void initState ()
    {
        //Reset everything to the initial state
    }

}
