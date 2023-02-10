using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DNAStruct;
using UnityEngine.Rendering;

public class StoreBtn : MonoBehaviour
{

    public ActionType actionType;

    public Button.ButtonClickedEvent onclick;


    private void Awake()
    {
        onclick = transform.GetComponent<Button>().onClick;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        OnDemandRendering.renderFrameInterval = 12;
    }

   
}
