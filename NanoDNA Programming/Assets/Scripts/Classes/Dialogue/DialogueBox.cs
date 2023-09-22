using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;
using UnityEngine.UI;
using DNAMathAnimation;
using DNAStruct;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] public RectTransform holder;


    

    public Flex Background;
    public Flex IconHolder;
    public Flex TextHolder;
    public Flex ContinueButton;
    public Flex IpadHolder;
    public Flex Camera;

    public Button.ButtonClickedEvent onClick;
    public Transform buttonRef;

    string dialogue;
    int index;
    char[] letters;

    UIWord cont = new UIWord("Continue", "Continue");

    private void Awake()
    {
        // setUI();
        setText();
       
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Flex setUI()
    {
        Background = new Flex(holder, 1);

        Camera = new Flex(Background.getChild(0), 0.05f, Background);

        Flex ScreenEffect = new Flex(Background.getChild(1), 1, Background);

        IpadHolder = new Flex(ScreenEffect.getChild(0), 1, ScreenEffect);

        TextHolder = new Flex(IpadHolder.getChild(0), 2, IpadHolder);
        Flex Text = new Flex(TextHolder.getChild(0), 1, TextHolder);

        Flex Holder = new Flex(IpadHolder.getChild(1), 1, IpadHolder);

        IconHolder = new Flex(Holder.getChild(0), 6, Holder);
        Flex Icon = new Flex(IconHolder.getChild(0), 1, IconHolder);

        ContinueButton = new Flex(Holder.getChild(1), 1, Holder);

        IconHolder.setAllPadSame(0.05f, 1);

        Background.setHorizontalPadding(0.01f, 1, 0.05f, 1);
        Background.setVerticalPadding(0.05f, 1, 0.05f, 1);

        TextHolder.setHorizontalPadding(0.015f, 1, 0.015f, 1);
        TextHolder.setVerticalPadding(0.010f, 1, 0.010f, 1);

        Background.setSpacingFlex(0.01f, 1);

       // ScreenEffect.setHorizontalPadding(0.025f, 1, 0.025f, 1);
        //ScreenEffect.setVerticalPadding(0.025f, 1, 0.025f, 1);

        //ContinueButton.setSelfHorizontalPadding(2, 1, 0, 1);

        //Consider having the dialogue box look like an ipad or a phone?

        Camera.setSelfHorizontalPadding(0.05f, 1, 0.05f, 1);

        IpadHolder.setSpacingFlex(0.01f, 1);

        UIHelper.setImage(Background.UI, "Images/Dialogue/IpadBackground");
        UIHelper.setImage(ScreenEffect.UI, "Images/Dialogue/IpadScreen");
        UIHelper.setImage(Camera.UI, "Images/Dialogue/Camera");
        UIHelper.setImage(IconHolder.UI, "Images/Dialogue/IpadBackground");

        
        buttonRef = ContinueButton.UI;
        onClick = buttonRef.GetComponent<Button>().onClick;

        return Background;
    }

    void setText ()
    {
        UIHelper.setText(ContinueButton.getChild(0), cont.getWord(PlayerSettings.language) + " >", Color.white, PlayerSettings.getBigText());
    }

    public void setInfo(Dialogue dialogue)
    {
        //Replace this with animate
        //Save text info
        this.dialogue = dialogue.dialogue.getWord(PlayerSettings.language);
        index = 0;
        letters = this.dialogue.ToCharArray();

        //Hide continue button
        ContinueButton.UI.gameObject.SetActive(false);

        //Reset text
        UIHelper.setText(TextHolder.getChild(0), "", Color.white, PlayerSettings.getBigText());

        try
        {
            UIHelper.setImage(IconHolder.getChild(0), dialogue.imagePath);
        }
        catch
        {

        }

        StartCoroutine(animateText());

        //UIHelper.setText(TextHolder.getChild(0), dialogue.dialogue.getWord(PlayerSettings.language), PlayerSettings.colourScheme.getBlackTextColor(), PlayerSettings.getBigText());
    }


    IEnumerator typeDialogue(string dialogue)
    {
        //Wait for object to spawn
        yield return null;

        //Set settings for it
        UIHelper.setText(TextHolder.getChild(0), "", PlayerSettings.colourScheme.getBlackTextColor(), PlayerSettings.getBigText());

        foreach (char let in dialogue)
        {
            TextHolder.getChild(0).GetComponent<Text>().text += let;
            yield return null;
        }

        ContinueButton.UI.gameObject.SetActive(true);
    }

    IEnumerator animateText()
    {
        yield return StartCoroutine(DNAMathAnim.animateLinearFloatCount(setAnimateText, this.dialogue.Length, DNAMathAnim.getFrameNumber(1f)));

        ContinueButton.UI.gameObject.SetActive(true);
    }


    public void setAnimateText(int difference)
    {
        for (int i = 0; i < difference; i++)
        {
            if (index < letters.Length)
            {
                TextHolder.getChild(0).GetComponent<Text>().text += letters[index];

                index++;
            }
        }

    }


    //Make an animate function

}
