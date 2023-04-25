using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;

public class CollectableData : MonoBehaviour
{

    public string name;

    public Vector3 initPos;

    public bool collectible;

    public List<int> sensorSignalIDs;

    //Add a on collision detection that makes it disappear when touched by something with 


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

    public void receiveSignal (SensorSignal signal)
    {

        if (sensorSignalIDs.Contains(signal.id))
        {
            Debug.Log("Signal Received");
        } else
        {
          //  Debug.Log("Signal Not received");
        }
       
    }

    public void setInfo (CollectableInfo info)
    {
        this.name = info.data.name;
        this.initPos = info.data.initPos;
        this.collectible = info.data.collectible;
        this.sensorSignalIDs = info.data.sensorSignalIDs;

        //Set initial position
        this.transform.localPosition = info.data.initPos;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
