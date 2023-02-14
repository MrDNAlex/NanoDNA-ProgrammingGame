using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableData : MonoBehaviour
{

    public string name;

    public Vector3 initPos;


    //Add a on collision detection that makes it disappear when touched by something with 


    private void OnTriggerEnter(Collider other)
    {
       

        if (other.gameObject.GetComponent<CharData>() != null)
        {
            this.gameObject.SetActive(false);

            //Add one point to the levels score

            Camera.main.GetComponent<LevelScript>().updateConstraints();


        }
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
