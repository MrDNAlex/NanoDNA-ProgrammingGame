using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;

public class Interactable : MonoBehaviour
{
    [System.Serializable]
    public enum InteractableType
    {
        Door
    }

    public InteractableType type;

    public string name;

    public Vector3 initPos;

    public Vector3 rotation;

    public Vector3 size1;

    public Vector3 size2;

    public bool solid;

    public List<string> spriteIDs;

    public List<Sprite> sprites;

    public List<int> sensorSignalIDs;

    public IInteractable iInterac;

   


    //Add a on collision detection that makes it disappear when touched by something with 

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (collectible)
        {
            if (other.gameObject.GetComponent<CharData>() != null)
            {
                this.gameObject.SetActive(false);

                //Add one point to the levels score

                Camera.main.GetComponent<LevelManager>().updateConstraints();

            }
        }
    }
    */
    public void receiveSignal (SensorSignal signal)
    {
        
        if (sensorSignalIDs.Contains(signal.id))
        {
            Debug.Log("Signal Received");

            //Activate interactable
            iInterac.Activate();
        } else
        {
          //  Debug.Log("Signal Not received");

        }
     
        
       
    }

    public void setInfo (InteractableInfo info, InteractableLedger ledger)
    {
        this.name = info.data.name;
        this.type = info.data.type;
        this.initPos = info.data.initPos;
        this.rotation = info.data.rotation;
        this.size1 = info.data.size1;
        this.size2 = info.data.size2;
        this.solid = info.data.solid;
        this.spriteIDs = info.data.spriteIDs;
        //this.sprite1ID = info.data.sprite1ID;
        //this.sprite2ID = info.data.sprite2ID;
        this.sensorSignalIDs = info.data.sensorSignalIDs;

        transform.localPosition = initPos;
        transform.rotation = Quaternion.Euler(rotation);
        transform.GetComponent<BoxCollider>().size = size1;
        transform.GetComponent<BoxCollider>().isTrigger = true;

        //Load and find the sprites, save to list
        sprites = new List<Sprite>();
        foreach (string id in spriteIDs)
        {
            sprites.Add(ledger.sprites.Find(c => c.id == id).sprite);
        }

        //Create a initial state variable, save it

    }


    /*
    public void setInfo (CollectableInfo info)
    {
        this.name = info.data.name;
        this.initPos = info.data.initPos;
        this.collectible = info.data.collectible;
        this.sensorSignalIDs = info.data.sensorSignalIDs;

        //Set initial position
        this.transform.localPosition = info.data.initPos;
    }
    */

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
