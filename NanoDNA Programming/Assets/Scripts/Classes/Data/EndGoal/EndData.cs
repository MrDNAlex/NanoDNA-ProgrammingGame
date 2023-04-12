using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;

[System.Serializable]
public class EndData : MonoBehaviour
{

    public string name;

    public Vector3 pos;

    public Vector3 size;


    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.GetComponent<CharData>() != null)
        {
            //Run a function
            //Camera.main.GetComponent<LevelManager>().finishLevel();
            Scripts.levelManager.inCompleteArea = true;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        Scripts.levelManager.inCompleteArea = true;
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
