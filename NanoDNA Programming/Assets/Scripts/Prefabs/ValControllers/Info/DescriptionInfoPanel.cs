using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;
using DNAMathAnimation;
using FlexUI;
using UnityEngine.UI;
using DNAScenes;
using UnityEngine.SceneManagement;
using DNASaveSystem;

public class DescriptionInfoPanel : InfoPanelController
{

    public struct CollectibleDesc
    {
        public string id;
        public string name;
        public int count;
        public Sprite icon;

        public void setInfo(InteractableInfo info, List<InteractableInfo> list, InteractableLedger interacLedger)
        {
            this.id = info.id;
            this.name = info.data.name;

            //Loop through to get count
            this.count = 0;
            foreach (InteractableInfo collects in list)
            {
                if (this.id == collects.id)
                {
                    this.count++;
                }
            }

            this.icon = interacLedger.sprites.Find(c => c.id == info.id).sprite;


        }
    }


    [SerializeField] public InteractableLedger interacLedger;

    int pageNum;
    int totalPages;

    UIWord linesUsed = new UIWord("Lines Used", "Lignes Utiliser");
    

    List<CollectibleDesc> collectibles;

    public void setPanel(Transform parent, InfoPanelType type)
    {
        try
        {
            this.panelType = type;

            lang = PlayerSettings.language;

            ParentTrans = parent;

            if (type == InfoPanelType.CollectibleDescription)
            {
                //Load the icons needed
                loadItems();

                //Set Page number
                totalPages = collectibles.Count;
                pageNum = 0;
            }
            else
            {
                totalPages = 1;
                pageNum = 0;
            }

            //SetUI and panel design
            setUI();

            setColours();

            setControls();

            //Handle Animation
            OriginalPos = ParentTrans.localPosition;

            Vector3 startPos = OriginalPos + new Vector3(0, -Screen.height, 0);

            ParentTrans.localPosition = startPos;

            StartCoroutine(DNAMathAnim.animateReboundRelocationLocal(ParentTrans, OriginalPos, DNAMathAnim.getFrameNumber(0.75f), 1, true));

            //Set the panel
            setPage();
        }
        catch
        {
            closePanel();
        }
    }

    void setColours()
    {
        //Background
        UIHelper.setImage(Holder.UI, PlayerSettings.colourScheme.getAccent());

        UIHelper.setImage(Holder.getChild(1), PlayerSettings.colourScheme.getMain());

        UIHelper.setImage(Holder.getChild(1).GetChild(3).GetChild(0).GetChild(1), "Images/UIDesigns/Last");

        UIHelper.setImage(Holder.getChild(1).GetChild(3).GetChild(2).GetChild(1), "Images/UIDesigns/Next");

        //UIHelper.setImage(Holder.getChild(1).GetChild(1), PlayerSettings.colourScheme.getSecondary());

        UIHelper.setImage(Holder.getChild(1).GetChild(3).GetChild(0), PlayerSettings.colourScheme.getSecondary());

        UIHelper.setImage(Holder.getChild(1).GetChild(3).GetChild(2), PlayerSettings.colourScheme.getSecondary());

    }

    void setControls()
    {
        setText();

        //Exit Button
        Holder.getChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
        {
            closePanel();
        });

        //Previous
        Holder.getChild(1).GetChild(3).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
        {
            //Sub to page number
            pageNum--;

            //Clamp page number
            pageNum = Mathf.Clamp(pageNum, 0, totalPages - 1);

            //Set the page
            setPage();

            //Update the page number
            setText();


        });

        //Next
        Holder.getChild(1).GetChild(3).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate
        {
            //Add to page number
            pageNum++;

            //Clamp page number
            pageNum = Mathf.Clamp(pageNum, 0, totalPages - 1);

            //Set the page
            setPage();

            //Update the page number
            setText();


        });

    }

    void setText()
    {
        string pages = (pageNum + 1) + "/" + totalPages;
        UIHelper.setText(Holder.getChild(1).GetChild(3).GetChild(1), pages, PlayerSettings.colourScheme.getBlackTextColor(), PlayerSettings.getBigText());
    }

    void setPage()
    {

        switch (panelType)
        {
            case InfoPanelType.CollectibleDescription:

                UIHelper.setImage(Holder.getChild(1).GetChild(1).GetChild(0), collectibles[pageNum].icon);

                UIHelper.setText(Holder.getChild(1).GetChild(0), collectibles[pageNum].name, PlayerSettings.colourScheme.getBlackTextColor(), PlayerSettings.getBigText());

                //Display count
                string count = "Total Count: " + collectibles[pageNum].count;

                UIHelper.setText(Holder.getChild(1).GetChild(2), count, PlayerSettings.colourScheme.getBlackTextColor(), PlayerSettings.getMediumText());
                break;
            case InfoPanelType.LinesUsed:

                UIHelper.setImage(Holder.getChild(1).GetChild(1).GetChild(0), "Images/UIDesigns/Lines");

                UIHelper.setText(Holder.getChild(1).GetChild(0), linesUsed.getWord(lang), PlayerSettings.colourScheme.getBlackTextColor(), PlayerSettings.getBigText());

                //Display count
                string message = "Lines used : " + Scripts.levelManager.usedLines + " / " + Scripts.levelManager.maxLines;

                UIHelper.setText(Holder.getChild(1).GetChild(2), message, PlayerSettings.colourScheme.getBlackTextColor(), PlayerSettings.getMediumText());

                break;
        }
      
    }

    void setUI()
    {
        Parent = new Flex(ParentTrans.GetComponent<RectTransform>(), 1);

        Holder = new Flex(this.GetComponent<RectTransform>(), 1, Parent);

        Flex ExitRow = new Flex(Holder.getChild(0), 1, Holder);
        Flex Exit = new Flex(ExitRow.getChild(0), 1, ExitRow);

        Flex PanelInfo = new Flex(Holder.getChild(1), 9, Holder);

        Flex ItemTitle = new Flex(PanelInfo.getChild(0), 2, PanelInfo);
        Flex ImgHolder = new Flex(PanelInfo.getChild(1), 6f, PanelInfo);

        Flex Img = new Flex(ImgHolder.getChild(0), 1, ImgHolder);

        Flex Description = new Flex(PanelInfo.getChild(2), 2, PanelInfo);
        Flex PageController = new Flex(PanelInfo.getChild(3), 1.5f, PanelInfo);

        Flex PrevBTN = new Flex(PageController.getChild(0), 1, PageController);
        Flex PrevBTNImg = new Flex(PrevBTN.getChild(1), 1, PrevBTN);
        Flex PageNum = new Flex(PageController.getChild(1), 1, PageController);
        Flex NextBTN = new Flex(PageController.getChild(2), 1, PageController);
        Flex NextBTNImg = new Flex(NextBTN.getChild(1), 1, NextBTN);

        Exit.setSquare();
        PrevBTNImg.setSquare();
        NextBTNImg.setSquare();
        Img.setSquare();

        PanelInfo.setSpacingFlex(0.5f, 1);

        ImgHolder.setSelfHorizontalPadding(0.1f, 1, 0.1f, 1);
        Description.setSelfHorizontalPadding(0.1f, 1, 0.1f, 1);

        ExitRow.setSelfHorizontalPadding(0, 1, 0.02f, 1);

        Holder.setVerticalPadding(0.1f, 1, 0.1f, 1);
        Holder.setHorizontalPadding(0.01f, 1, 0.01f, 1);
        Holder.setSpacingFlex(0.1f, 1);

        Parent.setSize(new Vector2(Screen.height * 0.7f * 1.3f, Screen.height * 0.7f));

        if (totalPages <= 1)
        {
            PrevBTN.UI.gameObject.SetActive(false);
            PageNum.UI.gameObject.SetActive(false);
            NextBTN.UI.gameObject.SetActive(false);
        }

    }

    //Make this more efficient one day
    public void loadItems()
    {
        //Go through all intercatble items, for every unique ID same the name, Icon and 

        LevelInfo level = SaveManager.loadJSON<LevelInfo>(CurrentLevelLoader.path, CurrentLevelLoader.name);


        List<InteractableInfo> interactables = new List<InteractableInfo>();

        //Filter for all unique
        foreach (InteractableInfo data in level.interacInfo)
        {
            bool contained = false;
            foreach (InteractableInfo saved in interactables)
            {
                if (data.id == saved.id)
                {
                    contained = true;
                }
            }

            if (contained == false)
            {
                interactables.Add(data);
            }
        }

        collectibles = new List<CollectibleDesc>();

        //Create new list of collectibles
        foreach (InteractableInfo interacs in interactables)
        {
            CollectibleDesc collect = new CollectibleDesc();

            collect.setInfo(interacs, level.interacInfo, interacLedger);

            collectibles.Add(collect);
        }

    }




}
