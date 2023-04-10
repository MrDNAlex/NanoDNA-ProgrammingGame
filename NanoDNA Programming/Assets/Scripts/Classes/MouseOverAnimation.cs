using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DNAStruct;

public class MouseOverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool mouseOver;

    [SerializeField] Sprite hoverImage;
    [SerializeField] Sprite notHoverImage;

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        mouseOver = true;
        GetComponent<Image>().sprite = hoverImage;
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        mouseOver = false;
        GetComponent<Image>().sprite = notHoverImage;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = notHoverImage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
