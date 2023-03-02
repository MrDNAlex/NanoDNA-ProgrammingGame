using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DNAStruct;

public class ChatBubble : MonoBehaviour
{
    [SerializeField] SpriteRenderer bubble;
    [SerializeField] TextMeshPro textMesh;

    private int fontSize = 8;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMessage (string text, SpriteRenderer parent, ActionDescriptor desc)
    {
        //Set Text Mesh
        textMesh.SetText(text);
        textMesh.ForceMeshUpdate();
        textMesh.fontSize = fontSize;

        //Get size
        Vector2 size = textMesh.GetRenderedValues(false);

        //Set Padding
        Vector2 padding = new Vector2(0.3f, 0.3f);

        //Set size
        bubble.size = size + padding;

        //Update Position
        bubble.transform.localPosition = new Vector3((bubble.size.x - padding.x)/ 2, 0, 0);

        //Calculate text position
        Vector3 textPos = new Vector3(-1 * (bubble.size.x - padding.x)/ 2f, 1.3f, 0);

        transform.localPosition = textPos;

        //Destroy after 5 seconds
        Destroy(this.gameObject, 5f);
    }

    private void setColor (ActionDescriptor desc)
    {

        Color col = new Color();
        switch (desc)
        {
            case ActionDescriptor.Whisper:
                col = normalizeColor(70, 199, 54);
                break;
            case ActionDescriptor.Talk:
                col = normalizeColor(92, 75, 235);
                break;
            case ActionDescriptor.Yell:
                col = normalizeColor(186, 36, 36);
                break;
        }

        for (int x = 0; x < bubble.sprite.texture.width; x ++)
        {
            for (int y = 0; y < bubble.sprite.texture.height; y++)
            {
                if (bubble.sprite.texture.GetPixel(x,y) == Color.white)
                {
                    bubble.sprite.texture.SetPixel(x, y, col);
                }
            }
        }
    }

    public Color normalizeColor (int r, int g, int b)
    {
        return new Color((float)r / 255f, (float)g / 255f, (float)b / 255f);
    }

}
