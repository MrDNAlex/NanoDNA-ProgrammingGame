using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DNAStruct;

public class MouseOverDetect : MonoBehaviour
{
    // Start is called before the first frame update

    public bool mouseOver;
    
    
    private void OnMouseOver()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }
    
    private void Awake()
    {
        OnDemandRendering.renderFrameInterval = 12;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
