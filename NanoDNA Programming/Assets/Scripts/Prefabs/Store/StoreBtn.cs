using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DNAStruct;

public class StoreBtn : MonoBehaviour
{

    public StoreTag storeTag;

    public Button.ButtonClickedEvent onclick;


    private void Awake()
    {
        onclick = transform.GetComponent<Button>().onClick;
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
